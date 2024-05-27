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

namespace COSHH_Generator.Core
{

    public static class SigmaAldrich
    {
        private readonly static HttpClient Client = new HttpClient(new HttpClientHandler
        {
            UseProxy = false,
        })
        {
            Timeout = TimeSpan.FromSeconds(3)
        };

        static public async Task<List<Result>> SearchAsync(string query, CancellationToken cancelToken = default)
        {
            Trace.WriteLine("Searching " + query);
            string encodedQuery = WebUtility.UrlEncode(query.Trim().ToLower().Replace(" ", "-"));
            using (var request = new HttpRequestMessage(HttpMethod.Post, "https://www.sigmaaldrich.com/api?operation=ProductSearch"))
            {
                var requestContent = new
                {
                    operationName = "ProductSearch",
                    variables = new
                    {
                        searchTerm = encodedQuery,
                        page = 1,
                        group = "substance",
                        selectedFacets = new object[] { },
                        sort = "relevance",
                        type = "PRODUCT"
                    },
                    query = "query ProductSearch($searchTerm: String, $page: Int!, $sort: Sort, $group: ProductSearchGroup, $selectedFacets: [FacetInput!], $type: ProductSearchType, $catalogType: CatalogType, $orgId: String, $region: String, $facetSet: [String], $filter: String) {\n  getProductSearchResults(input: {searchTerm: $searchTerm, pagination: {page: $page}, sort: $sort, group: $group, facets: $selectedFacets, type: $type, catalogType: $catalogType, orgId: $orgId, region: $region, facetSet: $facetSet, filter: $filter}) {\n    ...ProductSearchFields\n    __typename\n  }\n}\n\nfragment ProductSearchFields on ProductSearchResults {\n  metadata {\n    itemCount\n    setsCount\n    page\n    perPage\n    numPages\n    redirect\n    suggestedType\n    __typename\n  }\n  items {\n    ... on Substance {\n      ...SubstanceFields\n      __typename\n    }\n    ... on Product {\n      ...SubstanceProductFields\n      __typename\n    }\n    __typename\n  }\n  facets {\n    key\n    numToDisplay\n    isHidden\n    isCollapsed\n    multiSelect\n    prefix\n    options {\n      value\n      count\n      __typename\n    }\n    __typename\n  }\n  didYouMeanTerms {\n    term\n    count\n    __typename\n  }\n  __typename\n}\n\nfragment SubstanceFields on Substance {\n  _id\n  id\n  name\n  synonyms\n  empiricalFormula\n  linearFormula\n  molecularWeight\n  aliases {\n    key\n    label\n    value\n    __typename\n  }\n  images {\n    sequence\n    altText\n    smallUrl\n    mediumUrl\n    largeUrl\n    brandKey\n    productKey\n    label\n    videoUrl\n    __typename\n  }\n  casNumber\n  products {\n    ...SubstanceProductFields\n    __typename\n  }\n  __typename\n}\n\nfragment SubstanceProductFields on Product {\n  name\n  displaySellerName\n  productNumber\n  productKey\n  isSial\n  isMarketplace\n  marketplaceSellerId\n  marketplaceOfferId\n  cardCategory\n  cardAttribute {\n    citationCount\n    application\n    __typename\n  }\n  attributes {\n    key\n    label\n    values\n    __typename\n  }\n  speciesReactivity\n  brand {\n    key\n    erpKey\n    name\n    color\n    __typename\n  }\n  images {\n    altText\n    smallUrl\n    mediumUrl\n    largeUrl\n    __typename\n  }\n  description\n  sdsLanguages\n  sdsPnoKey\n  similarity\n  paMessage\n  features\n  catalogId\n  materialIds\n  erp_type\n  __typename\n}\n"
                };
                request.Content = new StringContent(JsonSerializer.Serialize(requestContent));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                request.Headers.Add("authority", "www.sigmaaldrich.com");
                request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
                request.Headers.Add("x-gql-access-token", "3a2beba1-fe5b-11ee-ad83-9527b9ed3d42");
                request.Headers.Add("x-gql-country", "GB");
                request.Headers.Add("x-gql-language", "en");
                request.Headers.Add("x-gql-operation-name", "ProductSearch");
                request.Headers.Add("x-gql-user-erp-type", "ANONYMOUS");
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
                        return results;
                    }
                }
                catch (HttpRequestException e)
                {
                    Trace.WriteLine($"Request error: {e.Message}");
                }
            }
            return new List<SigmaAldrich.Result>();
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
