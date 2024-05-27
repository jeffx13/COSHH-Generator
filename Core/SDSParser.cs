using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Windows;

namespace COSHH_Generator.Core
{
    static class SDSParser
    {
        private static Regex HazardStatementRegex = new Regex(@"^H\d{3}[dfDFi]{0,2}(?:\s?\+\s?H\d{3}){0,2}", RegexOptions.Multiline);
        private static Regex PrecautionaryStatementRegex = new Regex(@"P\d{3}(?:\s?\+\s?P\d{3}){0,2}", RegexOptions.Multiline);
        private static Regex EUHazardStatementRegex = new Regex(@"EUH\d{3}A?", RegexOptions.Multiline);
        private static HttpClient client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(3)
        };

        public static async Task<SafetyData> Extract(string url, CancellationToken cancelToken, Action errorCallback)
        {
            Trace.WriteLine($"Downloading {url}");
            SafetyData safetyData = new SafetyData();
            if (cancelToken.IsCancellationRequested)
            {
                return safetyData;
            }

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    errorCallback();
                    return safetyData;
                }
                
                //PdfReader reader = await Task.Run(() => new PdfReader(new Uri(url)));
                PdfReader reader = new PdfReader(await response.Content.ReadAsStreamAsync());
                Trace.WriteLine("Extracting");

                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    if (cancelToken.IsCancellationRequested)
                    {
                        return safetyData;
                    }
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, page);
                    if (pageText.Contains("SECTION 2: Hazards identification"))
                    {
                        if (cancelToken.IsCancellationRequested)
                        {
                            return safetyData;
                        }
                        pageText = (pageText + PdfTextExtractor.GetTextFromPage(reader, page + 1))
                            .Split("SECTION 3", 2, StringSplitOptions.None).First()
                            .Split("Hazard statement", 2, StringSplitOptions.None).Last()
                            .Split("Reduced Labeling", 2, StringSplitOptions.None).First();
                        var hazardCodes = HazardStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", ""));
                        var euHazardCodes = EUHazardStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", ""));
                        safetyData.PrecautionaryCodes = PrecautionaryStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", "")).ToList();
                        safetyData.HazardCodes = hazardCodes.Concat(euHazardCodes).ToList();
                    }
                    else if (pageText.Contains("SECTION 9"))
                    {
                        if (cancelToken.IsCancellationRequested)
                        {
                            return safetyData;
                        }
                        pageText += PdfTextExtractor.GetTextFromPage(reader, page + 1)
                                                    .Split("SECTION 10", 2, StringSplitOptions.None).First();

                        var odour = pageText.Split("c) Odor", 2, StringSplitOptions.None).Last()
                                            .Split("\n", 2, StringSplitOptions.None).First().Trim();

                        if (!odour.Contains("No data available"))
                        {
                            safetyData.Odour = odour;
                        }
                        Trace.WriteLine($"Odour: {safetyData.Odour}");
                        break;
                    }
                }
                
                return safetyData;
            }
            catch (Exception e)
            {
                errorCallback();
                Trace.WriteLine($"Request error: {e.Message}");
            }
            
            return safetyData;
        }
    }
}
