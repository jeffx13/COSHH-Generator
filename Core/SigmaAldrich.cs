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

namespace COSHH_Generator.Core
{

    public static class SigmaAldrich
    {
        private readonly static HttpClient Client = new HttpClient(new HttpClientHandler
        {
            UseProxy = false,
        })
        {
            Timeout = TimeSpan.FromSeconds(5)
        };

        static public async Task<List<Result>> SearchAsync(string query, CancellationToken cancelToken = default)
        {
            Trace.WriteLine("Searching " + query);
            string encodedQuery = WebUtility.UrlEncode(query.Trim().ToLower().Replace(" ", "-"));

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://www.sigmaaldrich.com/api?operation=ProductSearch");

            request.Headers.Add("accept", "*/*");
            request.Headers.Add("accept-language", "en-GB,en;q=0.9");
            request.Headers.Add("cookie", "language=en; country=GB; GUID=e975251a-02a8-45dd-a7e3-7c2395e6c7a5|NULL|1728491419043; accessToken=c5ce43f1-865b-11ef-963e-09d4d6bcc903; dtCookie=v_4_srv_20_sn_78BA9C6AAB034B53AD60F282339BEA22_perc_69892_ol_1_app-3A49e38e2e60c8cd4b_0; akaalb_origin-alb=~op=origin_epc:EMEA|~rv=47~m=EMEA:0|~os=a22342633dc1bd552d693ae0b80a3fbd~id=2038879bee095376a3c55ca5d187a765; optimizelyEndUserId=oeu1728491419363r0.2928617470115864; userErpType=ANONYMOUS; BVImplmain_site=15557; OptanonAlertBoxClosed=2024-10-09T16:30:21.989Z; bm_mi=BC87AE83F59E8940F2FD13D2D4151919~YAAQv25WaC+snGaSAQAAhTwfchmP59WZ5N/m/IOq2XyAaBsH8zAOTyHBuIS4fjDQpP9neY51IrdUgsqHu3v2a7xMStrZpCjEAhqltc08Mj9O6naljfi+X+UHa6kyZWkY597YAsUY/Bl+PgPzCKHDGOiuE0w+qLH2JoFUS97Q8dr5U0rSok5lY1eLsszzadE1WY7DMjNBtOiuRYBBTmD+CaC5T/LOumG/uH+EraY5Xfr9WIYHiROBK8i8dwVBEn+mR4l7PRDsTla4HTzzU+Xy+BL4e74s4zczCGbnm/TDcJqGY2AvAjJEBZdRCazSVXW4uXiM6LtAJ0cLAJQN6XTmZ2ut54A=~1; bm_sv=72F85A6E918DB6E8E4B312DFD1DEDD95~YAAQv25WaGWsnGaSAQAAXUYfchnjQ0KxV0QIdjnHRlbo1WGX5wMDtrl70+gXm29OyC43IVRvEz+n1LgEiIZ+QHm2sHMhzP9buUf+tmkFL094SeUQSHSPRL/05wS8qUsOyQs7lAyFI2BJ+CV3i8Ao67fn9/CjYO224yXtnp0NGnWvf8X6uzkGTpF9Mi2JLOAFgnpibd6QHrkKY/8ZiAVVayZVJysRlg/ja8Iwe/fX7I4l4BvwSxtgwJHuI6aNfz7e2k00rM8oFQ==~1; ak_bmsc=DD354ACB3FE2E88FBAB5DB2668DCCFF9~000000000000000000000000000000~YAAQv25WaDO3nGaSAQAAPLIgchmPyEsNPX13TnhtVb+Z5BJT1/gyYAOEmZG2LEZHqSqdX8yUtq6iApsQoZqFuxHhLT4SqwLH28woQx2WUOoEgNBpCE3t4H1hcDCDaNlaLLqN6BPUSfeXu3N78Rg2+LiPeHSm2OTIPnZc/34CGF8+//SCMQCgSeKEKxGxeWyVEPooA1bTBKE2zi8MyMe1i9Voc/BRjIMH/Q15sYdSYl6MNSDbowG9UwCMhhSiM9USBzzm0TGFqeW6FCbX9yZHSCeLk+Kw3kzvHrATEsKoUN7ik3NfrhGBYfg23Yt+Yk4EZ6XqnZEB1DrWawc6JIzOAnhwEjMGF2k855EaN/llgj582KWcRW1595Y3qnvDj7eQKKKkffrZ6iC293sT1vRgoehZqTDi+aTGDmuIGW3NfOgisMIt+803QM1JMbN9eDHtqEFk/eGQmj5Hm8uQGU0jgQcbhIzIKNjXpqAhtmIZL/M3ZbcoqM/KSxckAnDqn3RnRPD4bnuOaS4sjewE4n0t2QbhK3deorgbMntY+QQCYQgnlKD4dYNy3iqeO8wa5zhLjJAHPHBB5QMbBcMGZP0=; OptanonConsent=isGpcEnabled=0&datestamp=Wed+Oct+09+2024+17%3A38%3A44+GMT%2B0100+(British+Summer+Time)&version=202401.1.0&browserGpcFlag=0&isIABGlobal=false&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0003%3A1%2CC0004%3A1&geolocation=GB%3BENG&AwaitingReconsent=false");
            request.Headers.Add("origin", "https://www.sigmaaldrich.com");
            request.Headers.Add("priority", "u=1, i");
            request.Headers.Add("referer", "https://www.sigmaaldrich.com/GB/en/search/methanol?focus=products&page=1&perpage=30&sort=relevance&term=methanol&type=product");
            request.Headers.Add("sec-ch-ua", "\"Google Chrome\";v=\"129\", \"Not=A?Brand\";v=\"8\", \"Chromium\";v=\"129\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-fetch-dest", "empty");
            request.Headers.Add("sec-fetch-mode", "cors");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36");
            request.Headers.Add("variation", "undefined");
            request.Headers.Add("x-gql-country", "GB");
            request.Headers.Add("x-gql-language", "en");
            request.Headers.Add("x-gql-operation-name", "ProductSearch");
            request.Headers.Add("x-gql-user-erp-type", "ANONYMOUS");

            request.Content = new StringContent($"{{\"operationName\":\"ProductSearch\",\"variables\":{{\"searchTerm\":\"{encodedQuery}\",\"page\":1,\"group\":\"substance\",\"selectedFacets\":[],\"sort\":\"relevance\",\"type\":\"PRODUCT\"}},\"query\":\"query ProductSearch($searchTerm: String, $page: Int!, $sort: Sort, $group: ProductSearchGroup, $selectedFacets: [FacetInput!], $type: ProductSearchType, $catalogType: CatalogType, $orgId: String, $region: String, $facetSet: [String], $filter: String) {{\\n  getProductSearchResults(input: {{searchTerm: $searchTerm, pagination: {{page: $page}}, sort: $sort, group: $group, facets: $selectedFacets, type: $type, catalogType: $catalogType, orgId: $orgId, region: $region, facetSet: $facetSet, filter: $filter}}) {{\\n    ...ProductSearchFields\\n    __typename\\n  }}\\n}}\\n\\nfragment ProductSearchFields on ProductSearchResults {{\\n  metadata {{\\n    itemCount\\n    setsCount\\n    page\\n    perPage\\n    numPages\\n    redirect\\n    suggestedType\\n    __typename\\n  }}\\n  items {{\\n    ... on Substance {{\\n      ...SubstanceFields\\n      __typename\\n    }}\\n    ... on Product {{\\n      ...SubstanceProductFields\\n      __typename\\n    }}\\n    __typename\\n  }}\\n  facets {{\\n    key\\n    numToDisplay\\n    isHidden\\n    isCollapsed\\n    multiSelect\\n    prefix\\n    options {{\\n      value\\n      count\\n      __typename\\n    }}\\n    __typename\\n  }}\\n  didYouMeanTerms {{\\n    term\\n    count\\n    __typename\\n  }}\\n  __typename\\n}}\\n\\nfragment SubstanceFields on Substance {{\\n  _id\\n  id\\n  name\\n  synonyms\\n  empiricalFormula\\n  linearFormula\\n  molecularWeight\\n  legalName\\n  aliases {{\\n    key\\n    label\\n    value\\n    __typename\\n  }}\\n  images {{\\n    sequence\\n    altText\\n    smallUrl\\n    mediumUrl\\n    largeUrl\\n    brandKey\\n    productKey\\n    label\\n    videoUrl\\n    __typename\\n  }}\\n  casNumber\\n  products {{\\n    ...SubstanceProductFields\\n    __typename\\n  }}\\n  __typename\\n}}\\n\\nfragment SubstanceProductFields on Product {{\\n  name\\n  displaySellerName\\n  productNumber\\n  productKey\\n  isSial\\n  isMarketplace\\n  marketplaceSellerId\\n  marketplaceOfferId\\n  cardCategory\\n  cardAttribute {{\\n    citationCount\\n    application\\n    __typename\\n  }}\\n  attributes {{\\n    key\\n    label\\n    values\\n    __typename\\n  }}\\n  speciesReactivity\\n  brand {{\\n    key\\n    erpKey\\n    name\\n    color\\n    __typename\\n  }}\\n  images {{\\n    altText\\n    smallUrl\\n    mediumUrl\\n    largeUrl\\n    __typename\\n  }}\\n  description\\n  sdsLanguages\\n  sdsPnoKey\\n  similarity\\n  paMessage\\n  features\\n  catalogId\\n  materialIds\\n  erp_type\\n  legalNameSuffix\\n  __typename\\n}}\\n\"}}");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            

            
            if (cancelToken.IsCancellationRequested) { return new List<Result>(); }
            try
            {
                using (HttpResponseMessage response = await Client!.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    var jtoken = JsonSerializer.Deserialize<JsonElement>(HttpUtility.HtmlDecode(content));
                    var results = jtoken.GetProperty("data").GetProperty("getProductSearchResults").GetProperty("items").Deserialize<List<SigmaAldrich.Result>>();
                    Trace.WriteLine("Fetched results");
                    return results!;
                }
            }
            catch (HttpRequestException e)
            {
                Trace.WriteLine($"Request error: {e.Message}");
            }


            return new List<SigmaAldrich.Result>();
        }

        public static async Task<SafetyData> Extract(string url, CancellationToken cancelToken, Action errorCallback)
        {
            Trace.WriteLine($"Downloading {url}");
            SafetyData safetyData = new SafetyData();
            if (cancelToken.IsCancellationRequested)
            {
                return safetyData;
            }

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("accept", "application/pdf");
            request.Headers.Add("accept-language", "en-GB,en;q=0.9");
            request.Headers.Add("accept-encoding", "gzip, deflate, br");
            request.Headers.Add("origin", "https://www.sigmaaldrich.com");
            request.Headers.Add("sec-ch-ua", "\"Google Chrome\";v=\"129\", \"Not=A?Brand\";v=\"8\", \"Chromium\";v=\"129\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-fetch-dest", "document");
            request.Headers.Add("sec-fetch-mode", "navigate");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36");
            request.Headers.Add("service-worker-navigation-preload", "true");
            request.Headers.Add("upgrade-insecure-requests", "1");

            try
            {
                HttpResponseMessage response = await Client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    errorCallback();
                    Trace.WriteLine(response.StatusCode);
                    return safetyData;
                }

                var body = await response.Content.ReadAsStringAsync();
                Trace.WriteLine(body);
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
            catch (Exception e)
            {
                errorCallback();
                Trace.WriteLine($"Request error: {e.Message}");
            }

            return safetyData;
        }


        public class Result
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
