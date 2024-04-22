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
        
        public static Task<SafetyData> Extract(string url, CancellationToken cancelToken)
        {
            Trace.WriteLine($"Downloading {url}");
            
            try
            {
                var safetyData = new SafetyData();
                if (cancelToken.IsCancellationRequested)
                {
                    return Task.FromResult(safetyData);
                }

                List<string> hazardCodes = new List<string>();
                List<string> precautionaryCodes = new List<string>();
                List<string> euHazardCodes = new List<string>();
                string odour = "N/A";
                PdfReader reader = new PdfReader(new Uri(url));
                Trace.WriteLine("Extracting");

                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    if (cancelToken.IsCancellationRequested)
                    {
                        return Task.FromResult(safetyData);
                    }
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, page);
                    

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
                        odour = pageText.Split("c) Odor").Last().Split("d)").First().Split(" ", 2).First().Trim();
                        Trace.WriteLine(odour);
                        
                        if (odour.Contains("No data available"))
                        {
                            odour = "N/A";
                        }
                        if (cancelToken.IsCancellationRequested)
                        {
                            return Task.FromResult(safetyData);
                        }
                        
                        break;
                    }
                    if (cancelToken.IsCancellationRequested)
                    {
                        return Task.FromResult(safetyData);
                    }
                }
                
                safetyData = new SafetyData(hazardCodes, precautionaryCodes);
                safetyData.Odour = odour;
                return Task.FromResult(safetyData);
                
            }
            catch (HttpRequestException e)
            {
                Trace.WriteLine($"Request error: {e.Message}");
            }
            
            return Task.FromResult(new SafetyData());
        }
    }
}
