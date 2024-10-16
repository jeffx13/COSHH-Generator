using System.Net;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading;
using System.Text.Json;

using System.Text.Json.Serialization;
using System;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Http.Json;
using System.Text;
using DocumentFormat.OpenXml.Drawing;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Windows;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using static COSHH_Generator.Core.Fisher;

namespace COSHH_Generator.Core
{

    public class SigmaAldrich : SDSProvider
    {
        private readonly HttpClient Client = new HttpClient(new HttpClientHandler
        {
            UseProxy = false,
            AllowAutoRedirect = true,
        })
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        private readonly static HttpClient ClientWithDecompression = new HttpClient(new HttpClientHandler
        {
            UseProxy = false,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            AllowAutoRedirect = true,
        })
        {
            Timeout = TimeSpan.FromSeconds(15),
            DefaultRequestHeaders = {
                { "accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7" },
                { "accept-language", "en-GB,en;q=0.9,zh-CN;q=0.8,zh;q=0.7" },
                { "cache-control", "max-age=0" },
                { "priority", "u=0, i" },
                { "sec-ch-ua", "\"Google Chrome\";v=\"129\", \"Not=A?Brand\";v=\"8\", \"Chromium\";v=\"129\"" },
                { "sec-ch-ua-mobile", "?0" },
                { "sec-ch-ua-platform", "\"Windows\"" },
                { "sec-fetch-dest", "document" },
                { "sec-fetch-mode", "navigate" },
                { "sec-fetch-site", "same-origin" },
                { "sec-fetch-user", "?1" },
                { "service-worker-navigation-preload", "true" },
                { "upgrade-insecure-requests", "1" },
                { "user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36" },
            }
        };

        public int Timeout
        {
            get { return (int)Client.Timeout.TotalMilliseconds; }
            set { Client.Timeout = TimeSpan.FromMilliseconds(value); }
        }


        public string Name => "Sigma Aldrich";

        public async Task<List<Result>> SearchAsync(string query, CancellationToken cancelToken = default)
        {
            string encodedQuery = WebUtility.UrlEncode(query.Trim().ToLower().Replace(" ", "-"));
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://www.sigmaaldrich.com/api?operation=ProductSearch");
            request.Headers.Add("accept", "*/*");
            request.Headers.Add("accept-language", "en-GB,en;q=0.9,zh-CN;q=0.8,zh;q=0.7");
            request.Headers.Add("origin", "https://www.sigmaaldrich.com");
            request.Headers.Add("priority", "u=1, i");
            request.Headers.Add("referer", "https://www.sigmaaldrich.com/GB/en/search/hcl?focus=products&page=1&perpage=30&sort=relevance&term=hcl&type=product");
            request.Headers.Add("sec-ch-ua", "\"Google Chrome\";v=\"129\", \"Not=A?Brand\";v=\"8\", \"Chromium\";v=\"129\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-fetch-dest", "empty");
            request.Headers.Add("sec-fetch-mode", "cors");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36");
            request.Headers.Add("variation", "undefined");
            request.Headers.Add("x-gql-access-token", "7cbacb30-7f5c-11ef-a8a9-d9dc84c702c7");
            request.Headers.Add("x-gql-country", "GB");
            request.Headers.Add("x-gql-language", "en");
            request.Headers.Add("x-gql-operation-name", "ProductSearch");
            request.Headers.Add("x-gql-user-erp-type", "ANONYMOUS");

            request.Content = new StringContent("{\"operationName\":\"ProductSearch\",\"variables\":{\"searchTerm\":\"" + encodedQuery + "\",\"page\":1,\"group\":\"substance\",\"selectedFacets\":[],\"sort\":\"relevance\",\"type\":\"PRODUCT\"},\"query\":\"query ProductSearch($searchTerm: String, $page: Int!, $sort: Sort, $group: ProductSearchGroup, $selectedFacets: [FacetInput!], $type: ProductSearchType, $catalogType: CatalogType, $orgId: String, $region: String, $facetSet: [String], $filter: String) {\\n  getProductSearchResults(input: {searchTerm: $searchTerm, pagination: {page: $page}, sort: $sort, group: $group, facets: $selectedFacets, type: $type, catalogType: $catalogType, orgId: $orgId, region: $region, facetSet: $facetSet, filter: $filter}) {\\n    ...ProductSearchFields\\n    __typename\\n  }\\n}\\n\\nfragment ProductSearchFields on ProductSearchResults {\\n  metadata {\\n    itemCount\\n    setsCount\\n    page\\n    perPage\\n    numPages\\n    redirect\\n    suggestedType\\n    __typename\\n  }\\n  items {\\n    ... on Substance {\\n      ...SubstanceFields\\n      __typename\\n    }\\n    ... on Product {\\n      ...SubstanceProductFields\\n      __typename\\n    }\\n    __typename\\n  }\\n  facets {\\n    key\\n    numToDisplay\\n    isHidden\\n    isCollapsed\\n    multiSelect\\n    prefix\\n    options {\\n      value\\n      count\\n      __typename\\n    }\\n    __typename\\n  }\\n  didYouMeanTerms {\\n    term\\n    count\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment SubstanceFields on Substance {\\n  _id\\n  id\\n  name\\n  synonyms\\n  empiricalFormula\\n  linearFormula\\n  molecularWeight\\n  legalName\\n  aliases {\\n    key\\n    label\\n    value\\n    __typename\\n  }\\n  images {\\n    sequence\\n    altText\\n    smallUrl\\n    mediumUrl\\n    largeUrl\\n    brandKey\\n    productKey\\n    label\\n    videoUrl\\n    __typename\\n  }\\n  casNumber\\n  products {\\n    ...SubstanceProductFields\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment SubstanceProductFields on Product {\\n  name\\n  displaySellerName\\n  productNumber\\n  productKey\\n  isSial\\n  isMarketplace\\n  marketplaceSellerId\\n  marketplaceOfferId\\n  cardCategory\\n  cardAttribute {\\n    citationCount\\n    application\\n    __typename\\n  }\\n  attributes {\\n    key\\n    label\\n    values\\n    __typename\\n  }\\n  speciesReactivity\\n  brand {\\n    key\\n    erpKey\\n    name\\n    color\\n    __typename\\n  }\\n  images {\\n    altText\\n    smallUrl\\n    mediumUrl\\n    largeUrl\\n    __typename\\n  }\\n  description\\n  sdsLanguages\\n  sdsPnoKey\\n  similarity\\n  paMessage\\n  features\\n  catalogId\\n  materialIds\\n  erp_type\\n  legalNameSuffix\\n  __typename\\n}\\n\"}");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            if (cancelToken.IsCancellationRequested) { return new List<Result>(); }
            HttpResponseMessage response = await ClientWithDecompression.SendAsync(request, cancelToken);
            if (!response.IsSuccessStatusCode) { 
                Trace.WriteLine($"Request failed: {response.StatusCode}");
                return new List<Result>(); 
            }
            string content = await response.Content.ReadAsStringAsync();
            var jtoken = JsonSerializer.Deserialize<JsonElement>(HttpUtility.HtmlDecode(content));
            var searchResults = jtoken.GetProperty("data").GetProperty("getProductSearchResults").GetProperty("items").Deserialize<List<SigmaAldrich.ApiResult>>();
            List<Result> results = new List<Result>();

            if (searchResults.Count == 0)
            {
                results.Add(new Result { ProductName = "No Results", Link = null });
              
            }

            foreach (var result in searchResults)
            {
                results.Add(new Result
                {
                    ProductName = result.Name,
                    Link = null
                });

                for (int j = 0; j < result.Products.Count; j++)
                {
                    ApiResult.Product product = result.Products[j];
                    results.Add(new Result
                    {
                        ProductName = $"{j + 1}. {product.Description}",
                        SubstanceName = result.Name,
                        Link = product.Link,
                    });
                }
            }
            return results!;
        }

        public async Task<SafetyData> ExtractAsync(string url, CancellationToken cancelToken, Action<string> errorCallback)
        {
            Trace.WriteLine($"Extracting {url}");
            SafetyData safetyData = new SafetyData();
            if (cancelToken.IsCancellationRequested) { return safetyData; }

            var challengeRequest = new HttpRequestMessage(HttpMethod.Get, $"{url}?userType=anonymous");
            var challengeResponse = await ClientWithDecompression.SendAsync(challengeRequest, cancelToken);
            if (!challengeResponse.IsSuccessStatusCode)
            {
                errorCallback($"Request for challenge failed: {challengeResponse.StatusCode}");
                return safetyData;
            }

            string pattern = @"var i = (\d+); var j = i \+ Number\(""(.*?)"" \+ ""(.*?)"".*?bm-verify"": ""([^""]+)";
            var match = Regex.Match(await challengeResponse.Content.ReadAsStringAsync(), pattern);
            if (!match.Success)
            {
                errorCallback("Failed to match challenges.");
                return safetyData;
            }
            string bmVerify = match.Groups[4].Value;
            int j = int.Parse(match.Groups[1].Value) + int.Parse(match.Groups[2].Value) + int.Parse(match.Groups[3].Value);


            // Step 5: Send a POST request with the payload
            HttpRequestMessage challengeEndpointRequest = new HttpRequestMessage(HttpMethod.Post, "https://www.sigmaaldrich.com/_sec/verify?provider=interstitial");
            challengeEndpointRequest.Content = new StringContent($"{{\"bm-verify\": \"{bmVerify}\", \"pow\": {j}}}");
            challengeEndpointRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            HttpResponseMessage challengeEndpointResponse = await Client.SendAsync(challengeEndpointRequest, cancelToken);

            var locationJson = JsonSerializer.Deserialize<Dictionary<string, object>>(await challengeEndpointResponse.Content.ReadAsStringAsync());
            if (!locationJson!.ContainsKey("location"))
            {
                errorCallback("The 'location' of PDF was not found.");
                return safetyData;
            }

            // Download the PDF
            string pdfUrl = "https://www.sigmaaldrich.com" + locationJson["location"].ToString();
            try
            {
                return await ParsePdf(pdfUrl, safetyData, cancelToken);
            } catch (Exception e)
            {
                //errorCallback($"Failed to parse PDF: {e.Message}");
                return safetyData;
            }

        }

        private static async Task<SafetyData> ParsePdf(string pdfUrl, SafetyData safetyData, CancellationToken cancelToken)
        {
            Trace.WriteLine($"Downloading PDF from {pdfUrl}");
            HttpRequestMessage pdfRequest = new HttpRequestMessage(HttpMethod.Get, pdfUrl);

            var pdfResponse = await ClientWithDecompression.SendAsync(pdfRequest, cancelToken);
            PdfReader reader = new PdfReader(await pdfResponse.Content.ReadAsStreamAsync());

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
                    var hazardCodes = Fisher.HazardStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", ""));
                    var euHazardCodes = Fisher.EUHazardStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", ""));
                    safetyData.PrecautionaryCodes = Fisher.PrecautionaryStatementRegex.Matches(pageText).Cast<Match>().Select(match => match.Value.Replace(" ", "")).ToList();
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


        public class ApiResult
        {
            [JsonPropertyName("name")]
            public required string Name { get; set; }

            [JsonPropertyName("empiricalFormula")]
            public string EmpiricalFormula { get; set; }

            [JsonPropertyName("molecularWeight")]
            public string MolecularWeight { get; set; }

            [JsonPropertyName("products")]
            public List<Product> Products { get; set; }

            public class Product
            {
                [JsonPropertyName("name")]
                public string Name { get; set; }

                [JsonPropertyName("description")]
                public string? Description { get; set; }

                public string? FullName
                {
                    get
                    {
                        return $"{Name} {Description}";
                    }
                }

                [JsonPropertyName("productNumber")]
                public string ProductNumber { get; set; }

                [JsonPropertyName("productKey")]
                public string ProductKey { get; set; }

                [JsonPropertyName("brand")]
                public Brand brand { get; set; }

                [JsonPropertyName("sdsLanguages")]
                public List<string>? SdsLanguages { get; set; }

                public string? Link
                {
                    get
                    {
                        if (SdsLanguages == null || SdsLanguages.Count == 0) return null;
                        return $"https://www.sigmaaldrich.com/GB/en/sds/{brand.Key.ToLower()}/{ProductNumber}";
                    }
                }

                public class Brand
                {
                    [JsonPropertyName("key")]
                    public string Key { get; set; }

                    [JsonPropertyName("erpKey")]
                    public string ErpKey { get; set; }

                    [JsonPropertyName("name")]
                    public string Name { get; set; }

                    [JsonPropertyName("color")]
                    public string Color { get; set; }

                    [JsonPropertyName("__typename")]
                    public string Typename { get; set; }
                }
            }
        }

    }



}
