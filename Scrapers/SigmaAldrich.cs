using System.Net;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace COSHH_Generator.Scrapers
{
    
    static class SigmaAldrich
    {
        public static HttpClient client = new HttpClient();
        private static string apiUrl = "https://www.sigmaaldrich.com/api";
        //Regex to find the statements
        
        static public async void SearchAsync(string query, Action<List<SigmaAldrich.Result>>? callback = null)
        {
            
            var results = await SearchAsync(query);
            if (callback is not null) callback(results);

        }

        static public async Task<List<Result>> SearchAsync(string query)
        {
            string encodedQuery = WebUtility.UrlEncode(query.Trim().ToLower().Replace(" ", "-"));
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://www.sigmaaldrich.com/api?operation=ProductSearch");
            request.Content = new StringContent("{\"operationName\":\"ProductSearch\",\"variables\":{\"searchTerm\":\"" + encodedQuery + "\",\"page\":1,\"group\":\"substance\",\"selectedFacets\":[],\"sort\":\"relevance\",\"type\":\"PRODUCT\"},\"query\":\"query ProductSearch($searchTerm: String, $page: Int!, $sort: Sort, $group: ProductSearchGroup, $selectedFacets: [FacetInput!], $type: ProductSearchType, $catalogType: CatalogType, $orgId: String, $region: String, $facetSet: [String], $filter: String) {\\n  getProductSearchResults(input: {searchTerm: $searchTerm, pagination: {page: $page}, sort: $sort, group: $group, facets: $selectedFacets, type: $type, catalogType: $catalogType, orgId: $orgId, region: $region, facetSet: $facetSet, filter: $filter}) {\\n    ...ProductSearchFields\\n    __typename\\n  }\\n}\\n\\nfragment ProductSearchFields on ProductSearchResults {\\n  metadata {\\n    itemCount\\n    setsCount\\n    page\\n    perPage\\n    numPages\\n    redirect\\n    suggestedType\\n    __typename\\n  }\\n  items {\\n    ... on Substance {\\n      ...SubstanceFields\\n      __typename\\n    }\\n    ... on Product {\\n      ...SubstanceProductFields\\n      __typename\\n    }\\n    __typename\\n  }\\n  facets {\\n    key\\n    numToDisplay\\n    isHidden\\n    isCollapsed\\n    multiSelect\\n    prefix\\n    options {\\n      value\\n      count\\n      __typename\\n    }\\n    __typename\\n  }\\n  didYouMeanTerms {\\n    term\\n    count\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment SubstanceFields on Substance {\\n  _id\\n  id\\n  name\\n  synonyms\\n  empiricalFormula\\n  linearFormula\\n  molecularWeight\\n  aliases {\\n    key\\n    label\\n    value\\n    __typename\\n  }\\n  images {\\n    sequence\\n    altText\\n    smallUrl\\n    mediumUrl\\n    largeUrl\\n    brandKey\\n    productKey\\n    label\\n    videoUrl\\n    __typename\\n  }\\n  casNumber\\n  products {\\n    ...SubstanceProductFields\\n    __typename\\n  }\\n  __typename\\n}\\n\\nfragment SubstanceProductFields on Product {\\n  name\\n  displaySellerName\\n  productNumber\\n  productKey\\n  isSial\\n  isMarketplace\\n  marketplaceSellerId\\n  marketplaceOfferId\\n  cardCategory\\n  cardAttribute {\\n    citationCount\\n    application\\n    __typename\\n  }\\n  attributes {\\n    key\\n    label\\n    values\\n    __typename\\n  }\\n  speciesReactivity\\n  brand {\\n    key\\n    erpKey\\n    name\\n    color\\n    __typename\\n  }\\n  images {\\n    altText\\n    smallUrl\\n    mediumUrl\\n    largeUrl\\n    __typename\\n  }\\n  description\\n  sdsLanguages\\n  sdsPnoKey\\n  similarity\\n  paMessage\\n  features\\n  catalogId\\n  materialIds\\n  erp_type\\n  __typename\\n}\\n\"}");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            request.Headers.Add("authority", "www.sigmaaldrich.com");
            request.Headers.Add("accept", "*/*");
            request.Headers.Add("accept-language", "en-GB,en;q=0.9,zh-CN;q=0.8,zh;q=0.7");
            request.Headers.Add("cache-control", "no-cache");
            request.Headers.Add("cookie", "akaalb_origin-alb=~op=origin_upc:NASA|origin_apc:APAC|origin_epc:EMEA|~rv=4~m=NASA:0|APAC:0|EMEA:0|~os=a22342633dc1bd552d693ae0b80a3fbd~id=8e15a2cad778f4c6f49baeb013e5d3e3; OptanonAlertBoxClosed=2024-01-22T19:21:57.700Z; BVImplmain_site=15557; language=en; country=GB; GUID=23f75d22-16cf-4b5d-9397-5d010c18d743|NULL|1709169373487; accessToken=3a2beba1-fe5b-11ee-ad83-9527b9ed3d42; dtCookie=v_4_srv_23_sn_BF5B80D8DDCFEF85A058D3271BB4BCE3_perc_100000_ol_0_mul_1_app-3A49e38e2e60c8cd4b_1; ak_bmsc=864D76CE93EAAFF816823CF291D87509~000000000000000000000000000000~YAAQBIR7XITq5eiOAQAAREHQ9hcM/JUzDaYMoC7mLdISNS6aDWpOhY+dZZIB6f5NbrgpbH/jlfZCqpD3AS4DsxmbZuctB3E5EcmDcMVhxo1DSdy0idsWFwLwgQFBgzx4lk5AV8FPtyC6Uw7CQaG42SOrfuUn12kK5hDkWRCZR91xGtHORxII7+/xhMyBjkNaLHsRVlJJGs0SCLeb9n7xF8qtY2IHUXV4MLkb+/pSLebZZpJS5RH2d0nUGr44umhpA2+s58brAnwqs3gMeKPdoKlwdgJ3RT7raeEMJm2UiOpC0Uv6LU50Dn0hwZHQyC3JgDYMvxe9gkljS4igrBTPHbNr+NFBc/HdD559nHGB/WoeD2+h2id3Hs15Vt0V/g19VN0b5oepY4wTAnlNXjVUUA==; OptanonConsent=isIABGlobal=false&datestamp=Fri+Apr+19+2024+15%3A43%3A48+GMT%2B0100+(British+Summer+Time)&version=202312.1.0&hosts=&landingPath=NotLandingPage&groups=C0001%3A1%2CC0003%3A1%2CC0004%3A1&geolocation=GB%3BENG&AwaitingReconsent=false&isGpcEnabled=0&browserGpcFlag=0; userErpType=ANONYMOUS; bm_mi=C223FD41CE790A28632C134DF37253E0~YAAQBIR7XLTs5eiOAQAA+kvQ9hd5mwg3HoqkRVFrDsrqVIyv9TZWQg4MOlM6IhTp6TYg+c89myxtrdUYkDesRZ/u08zwwXcHTZsx2yWdOExvIM28cXg/5W8lK30DgnvOVF8sHh3Nl2ZnRpoGxA4lZQUqMGuO/T5IATJ6J0Cg42iZwUpek40tvLs5gUGHFyV/7zaCr2D8ShLlkY3tyqc7URFo2aNIVyYFiIVTfMaIZfO/8rUNwlJ9pQSGY9n3xSvVMR6Tu51k1/ZVVvZrmACKFM1Gn1Ez2F3zeWBn2xCeL9Kgub3I0eBYGfCQAN1UMvbnWh3QZEKRoN/5u/q7apXwsgos3EiooAlqOYVR53ouhow=~1; bm_sv=C4E022C63BF02F5005CCB9C658E9CF4E~YAAQBIR7XMII5uiOAQAAiu3Q9hdakiYknIfOOTonOqPk3cVScFiC6g2RczJxttyHE9XqvFQRmms3Ler9MLjd+eZamxuwkhveYpqt2UElGvm7KQ9Nf1vcUODvv8mMBWcJCvKk4u87ifCHM5S8IifXE2n82pLOdIxuqF1TBTpFXa+jmlnQFrFqEkK7UKin+dfiJpFnfidMmF0QLz/qz1h9+I9zblqHBOcdRGDNP87y5ldQdAQuE1rEh8uN17Iu6+4KcrkR3hJ4eg==~1");
            request.Headers.Add("origin", "https://www.sigmaaldrich.com");
            request.Headers.Add("pragma", "no-cache");
            request.Headers.Add("referer", "https://www.sigmaaldrich.com/GB/en/search/naoh?focus=products&page=1&perpage=30&sort=relevance&term=naoh&type=product");
            request.Headers.Add("sec-ch-ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-fetch-dest", "empty");
            request.Headers.Add("sec-fetch-mode", "cors");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            request.Headers.Add("x-gql-access-token", "3a2beba1-fe5b-11ee-ad83-9527b9ed3d42");
            request.Headers.Add("x-gql-country", "GB");
            request.Headers.Add("x-gql-language", "en");
            request.Headers.Add("x-gql-operation-name", "ProductSearch");
            request.Headers.Add("x-gql-user-erp-type", "ANONYMOUS");

            try
            {
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {   
                    string content = await response.Content.ReadAsStringAsync();
                    var jtoken = JsonConvert.DeserializeObject<JToken>(HttpUtility.HtmlDecode(content))!;
                    var results = jtoken.SelectToken("data.getProductSearchResults.items")!.ToObject<List<Result>>()!;
                    return results;
                }
                else
                {
                    Trace.WriteLine($"Request failed with status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Trace.WriteLine($"Request error: {e.Message}");
            }
            return new List<Result>();
        }

        static public List<Result> Search(string query)
        {
            List<Result> results = SearchAsync(query).Result;
            return results;
        }       

        public class Result
        {
            [JsonProperty("name")]
            public required string name { get; set; }

            [JsonProperty("empiricalFormula")]
            public string empiricalFormula { get; set; }

            [JsonProperty("molecularWeight")]
            public string molecularWeight { get; set; }

            [JsonProperty("products")]
            public List<Product> products { get; set; }

            public class Product
            {
                [JsonProperty("name")]
                public string name { get; set; }

                [JsonProperty("description")]
                public string? description { get; set; }

                public string? fullName { 
                    get {
                        return $"{name} {description}";
                    }
                }

                [JsonProperty("productNumber")]
                public string productNumber { get; set; }

                [JsonProperty("productKey")]
                public string productKey { get; set; }

                [JsonProperty("brand")]
                public Brand brand { get; set; }

                [JsonProperty("sdsLanguages")]
                public List<string>? sdsLanguages { get;set; }

                public string? link{ 
                    get {
                        if (sdsLanguages == null || sdsLanguages.Count == 0) return null;
                        return $"https://www.sigmaaldrich.com/GB/en/sds/{brand.key.ToLower()}/{productNumber}";
                    } 
                }

                public class Brand
                {
                    [JsonProperty("key")]
                    public string key { get; set; }
                       
                    [JsonProperty("erpKey")]
                    public string erpKey { get; set; }

                    [JsonProperty("name")]
                    public string name { get; set; }

                    [JsonProperty("color")]
                    public string color { get; set; }

                    [JsonProperty("__typename")]
                    public string __typename { get; set; }

                }

            }
        }

    }



}
