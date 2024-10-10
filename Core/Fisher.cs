using System.Net;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System.Linq;
using System.DirectoryServices;

namespace COSHH_Generator.Core
{

    public class Fisher: SDSProvider
    {
        public static Regex HazardStatementRegex = new Regex(@"^H\d{3}[dfDFi]{0,2}(?:\s?\+\s?H\d{3}){0,2}", RegexOptions.Multiline);
        public static Regex PrecautionaryStatementRegex = new Regex(@"P\d{3}(?:\s?\+\s?P\d{3}){0,2}", RegexOptions.Multiline);
        public static Regex EUHazardStatementRegex = new Regex(@"EUH\d{3}A?", RegexOptions.Multiline);

        private readonly static HttpClient Client = new HttpClient(new HttpClientHandler
        {
            UseProxy = false,
        })
        {
            Timeout = TimeSpan.FromSeconds(5)
        };
        public static int Timeout {
            get { return (int)Client.Timeout.TotalMilliseconds; }
            set { Client.Timeout = TimeSpan.FromMilliseconds(value); }
        }
        public string Name => "Fisher";

        public async Task<List<Result>> SearchAsync(string query, CancellationToken cancelToken = default)
        {
            Trace.WriteLine("Searching " + query);
            string encodedQuery = WebUtility.UrlEncode(query.Trim().ToLower().Replace(" ", "-"));
            List<Result> results = new List<Result>();
            

            var parameters = new Dictionary<string, string>
            {
                {"selectLang", "EN"},
                {"store", ""},
                {"msdsKeyword", encodedQuery}
            };
            

            if (!Client.DefaultRequestHeaders.Any())
            {
                var headers = new Dictionary<string, string>
                {
                    {"accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7"},
                    {"accept-language", "en-GB,en;q=0.9,zh-CN;q=0.8,zh;q=0.7"},
                    {"cache-control", "max-age=0"},
                    {"priority", "u=0, i"},
                    {"referer", "https://www.fishersci.co.uk/gb/en/catalog/search/sdshome.html"},
                    {"sec-ch-ua", "\"Google Chrome\";v=\"129\", \"Not=A?Brand\";v=\"8\", \"Chromium\";v=\"129\""},
                    {"sec-ch-ua-mobile", "?0"},
                    {"sec-ch-ua-platform", "\"Windows\""},
                    {"sec-fetch-dest", "document"},
                    {"sec-fetch-mode", "navigate"},
                    {"sec-fetch-site", "same-origin"},
                    {"sec-fetch-user", "?1"},
                    {"upgrade-insecure-requests", "1"},
                    {"user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36"}
                };
                foreach (var header in headers)
                {
                    Client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
                

            var fullUrl = $"https://www.fishersci.co.uk/gb/en/catalog/search/sds?{new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result}";

            // Send the GET request
            if (cancelToken.IsCancellationRequested) { return new List<Result>(); }
            HttpResponseMessage response = await Client.GetAsync(fullUrl);
            if (cancelToken.IsCancellationRequested) { return new List<Result>(); }
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            Trace.WriteLine(responseBody);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(responseBody);

            var productNodes = doc.DocumentNode.SelectNodes("//div[@class='ptitleblack bold']");
            var catalogDataNodes = doc.DocumentNode.SelectNodes("//td[@class='catalog_data']//div[@class='catlog_items'][1]/a");
            Trace.WriteLine(catalogDataNodes);
            if (productNodes != null && catalogDataNodes != null)
            {
                for (int i = 0; i < productNodes.Count; i++)
                {
                    var product = productNodes[i].InnerText.Replace("&nbsp", "").Trim();
                    var sdsLink = "https://www.fishersci.co.uk/" + catalogDataNodes[i].Attributes["href"].Value;
                    results.Add(new Result { ProductName = product, Link = sdsLink });
                }
            }
            else
            {
                results.Add(new Result { ProductName = "No Results", Link = null });
                Trace.WriteLine("No products found.");
            }



            return results;
        }

        public async Task<SafetyData> ExtractAsync(string url, CancellationToken cancelToken, Action<string> errorCallback)
        {
            Trace.WriteLine($"Downloading {url}");
            SafetyData safetyData = new SafetyData();
            if (cancelToken.IsCancellationRequested) { return safetyData; }


            try
            {
                HttpResponseMessage response = await Client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    errorCallback($"Failed to download the SDS: {response.StatusCode}");
                    return safetyData;
                }

                var body = await response.Content.ReadAsStringAsync();
                PdfReader reader = new PdfReader(await response.Content.ReadAsStreamAsync());
                Trace.WriteLine("Extracting");

                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    if (cancelToken.IsCancellationRequested){ return safetyData; }
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, page);
                    if (page == 1)
                    {
                        var productName = pageText.Split("Product Description:", 2, StringSplitOptions.None).Last();
                        safetyData.SubstanceName = productName.Split("\n", 2, StringSplitOptions.None).First().Trim();
                        Trace.WriteLine($"Product Name: {safetyData.SubstanceName}");
                    } 

                    if (pageText.Contains("2.2. Label elements"))
                    {
                        pageText = (pageText + PdfTextExtractor.GetTextFromPage(reader, page + 1))
                            .Split("2.3. Other hazards", 2, StringSplitOptions.None).First()
                            .Split("Hazard Statement", 2, StringSplitOptions.None).Last();
                        var hazardCodes = Fisher.HazardStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", ""));
                        var euHazardCodes = Fisher.EUHazardStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", ""));
                        safetyData.PrecautionaryCodes = Fisher.PrecautionaryStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", "")).ToList();
                        safetyData.HazardCodes = hazardCodes.Concat(euHazardCodes).ToList();
                    }
                    else if (pageText.Contains("SECTION 9"))
                    {
                        pageText += PdfTextExtractor.GetTextFromPage(reader, page + 1)
                                                    .Split("SECTION 10", 2, StringSplitOptions.None).First();

                        var odour = pageText.Split("Odor", 2, StringSplitOptions.None).Last()
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
                errorCallback($"Request error: {e.Message}");
            }

            return safetyData;
        }
        
    }



}
