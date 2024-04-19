using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Threading;


namespace COSHH_Generator.Scrapers
{
    static class SDSParser
    {
        private static Regex HazardStatementRegex = new Regex(@"^H\d{3}[dfDFi]{0,2}(?:\s?\+\s?H\d{3}){0,2}", RegexOptions.Multiline);
        private static Regex PrecautionaryStatementRegex = new Regex(@"P\d{3}(?:\s?\+\s?P\d{3}){0,2}", RegexOptions.Multiline);
        private static Regex EUHazardStatementRegex = new Regex(@"EUH\d{3}A?", RegexOptions.Multiline);
        
        public static async Task<SafetyData> Extract(string url, CancellationToken cancelToken, Action<string>? callback = null)
        {
            Trace.WriteLine($"Downloading {url}");
            try
            {
                return Analyse(new Uri(url), cancelToken, callback); 
            }
            catch (HttpRequestException e)
            {
                Trace.WriteLine($"Request error: {e.Message}");
            }
            return new SafetyData();
        }

        
        private static SafetyData Analyse(Uri url, CancellationToken cancelToken, Action<string>? callback = null)
        {
            if (cancelToken.IsCancellationRequested)
            {
                return new SafetyData();
            }

            Trace.WriteLine("Downloaded");
            List<string> hazardCodes = new List<string>();
            List<string> precautionaryCodes = new List<string>();
            List<string> euHazardCodes = new List<string>();

            PdfReader reader = new PdfReader(url);
            Trace.WriteLine("Extracting");
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                if (cancelToken.IsCancellationRequested)
                {
                    return new SafetyData();
                }
                string pageText = PdfTextExtractor.GetTextFromPage(reader, page);
                //Console.WriteLine(pageText.Contains("SECTION 2: Hazards identification"));
                if (pageText.Contains("SECTION 2: Hazards identification"))
                {
                    pageText = pageText + PdfTextExtractor.GetTextFromPage(reader, page + 1).Split("SECTION 3").First();
                    pageText = pageText.Split("Hazard statement", 1).Last().Split("Reduced Labeling").First();
                    hazardCodes = HazardStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", "")).ToList();
                    precautionaryCodes = PrecautionaryStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", "")).ToList();
                    euHazardCodes = EUHazardStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", "")).ToList();
                }
                else if (pageText.Contains("SECTION 9"))
                {
                    //TODO check for malodorous
                    pageText += PdfTextExtractor.GetTextFromPage(reader, page + 1).Split("SECTION 10").First();
                    var odour = pageText.Split("c) Odor").Last().Split("d)").First().Split("Millipore").First().Split("  ").First().Replace("\n", " ").Trim();
                    if (odour.Contains("No data available"))
                    {
                        odour = "N/A";
                    }
                    if (cancelToken.IsCancellationRequested)
                    {
                        return new SafetyData();
                    }
                    else if (callback != null) callback(odour);
                    break;
                }
            }
            if (cancelToken.IsCancellationRequested)
            {
                return new SafetyData();
            }

            return new SafetyData(hazardCodes, precautionaryCodes);
        }
    }
}
