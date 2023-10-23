using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        static public void extract()
        {
            return;
            Regex HazardStatementRegex = new Regex(@"H\d{3}[dfDFi]{0,2}(?:\s?\+\s?H\d{3}){0,2}(?: H\d{3}[dfDFi]{0,2}(?:\s?\+\s?H\d{3}){0,2})*", RegexOptions.Multiline);
            Regex PrecautionaryStatementRegex = new Regex(@"P\d{3}(?:\s?\+\s?P\d{3}){0,2}(?: P\d{3}(?:\s?\+\s?P\d{3}){0,2})*", RegexOptions.Multiline);
            Regex EUHazardStatementRegex = new Regex(@"EUH\d{3}A?", RegexOptions.Multiline);

            using (WordprocessingDocument doc = WordprocessingDocument.Open("C:\\Users\\Jeffx\\Downloads\\COSHH-Chemical-Pool .docx", true))
            {
                MainDocumentPart mainPart = doc.MainDocumentPart;

                // Get the table from the document
                Table table = mainPart.Document.Body.Elements<Table>().First();

                // Iterate through rows
                foreach (TableRow row in table.Elements<TableRow>())
                {
                    // Iterate through cells in this row
                    var cells = row.Elements<TableCell>();
                    var name = cells.ElementAt(0).Elements<Paragraph>().First().Elements<Run>().First().InnerText;
                    var hazards = cells.ElementAt(2).Elements<Paragraph>().First().Elements<Run>().First().InnerText;
                    var exposuresRun = cells.ElementAt(3).Elements<Paragraph>().First().Elements<Run>();
                    var exposures = exposuresRun.Count() == 0 ? new List<string>() : exposuresRun.First().InnerText.Trim().Split(',').ToList();
                    var exposureTxt = "\n";
                    var amount = cells.ElementAt(1).Elements<Paragraph>().First().Elements<Run>().First().InnerText;
                    foreach (var exposure in exposures)
                    {
                        var e = exposure.Trim();
                        if (e == "Eyes")
                        {
                            exposureTxt += "\t\tEyes = true,\n";
                        }
                        else if(e == "Mouth")
                        {
                            exposureTxt += "\t\tIngestion = true,\n";
                        }
                        else if (e == "Inhalation")
                        {
                            exposureTxt += "\t\tInhalation = true,\n";
                        }
                        else if (e == "Skin")
                        {
                            exposureTxt += "\t\tSkin = true,\n";
                        }
                    }
                    var precautions = cells.ElementAt(4).Elements<Paragraph>().First().Elements<Run>().First().InnerText;



                    var hazardCodes = HazardStatementRegex.Matches(hazards).Cast<Match>().Select(match => "\"" + match.Value.Trim().Replace(" ", "+") + "\"").ToList();
                    var precautionaryCodes = PrecautionaryStatementRegex.Matches(precautions).Cast<Match>().Select(match => "\"" + match.Value.Trim().Replace(" ", "+") + "\"").ToList();
                    
                    
                    
                    var substance =
                        String.Format("""
                        new SubstanceEntry()
                        {{
                            DisplayName = "{0}",
                            Amount = "{1}",
                            extractionTask = Task.FromResult(new SafetyData(
                                new List<string> {{ {2} }},
                                new List<string> {{ {3} }})
                            {{{4}
                            }})
                        }},
                        """, name, amount, String.Join(", ", hazardCodes.ToArray()), String.Join(", ", precautionaryCodes.ToArray()), exposureTxt);
                    Trace.WriteLine(substance);
                    //Trace.WriteLine(hazards);
                }
            };
        }



        private static Regex HazardStatementRegex = new Regex(@"^H\d{3}[dfDFi]{0,2}(?:\s?\+\s?H\d{3}){0,2}", RegexOptions.Multiline);
        private static Regex PrecautionaryStatementRegex = new Regex(@"P\d{3}(?:\s?\+\s?P\d{3}){0,2}", RegexOptions.Multiline);
        private static Regex EUHazardStatementRegex = new Regex(@"EUH\d{3}A?", RegexOptions.Multiline);
        //TODO check for malodour
        public static async Task<SafetyData> Extract(string url, CancellationToken cancelToken, Action<string>? callback = null)
        {
            //Trace.WriteLine("Downloading");
            string text = string.Empty;
            HttpResponseMessage response = await SigmaAldrich.client.GetAsync(url);
            Stream pdfStream = await response.Content.ReadAsStreamAsync();

            if(cancelToken.IsCancellationRequested)
            {
                return new SafetyData();
            }

            //Trace.WriteLine("Downloaded");
            List<string> hazardCodes = new List<string>();
            List<string> precautionaryCodes = new List<string>();
            List<string> euHazardCodes = new List<string>();

            PdfReader reader = new PdfReader(pdfStream);
            //Trace.WriteLine("Extracting");
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
                    var odour = pageText.Split("c) Odor").Last().Split("d)").First().Split("Millipore").First().Trim();
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
