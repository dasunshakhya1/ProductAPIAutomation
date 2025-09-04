using ProductAPIAutomation.Core.Endpoints.Products.models;
using ProductAPIAutomation.Core.Utils;
using ProductAPIAutomation.Core.Utils.models;

namespace ProductAPIAutomation.Core.Endpoints.Products
{
    public class ProductController
    {

        private static readonly string _uri = "objects";

        public static async Task<Response> GetProductById(string productId)
        {
            string uri = $"{_uri}/{productId}";
            return await HttpClientImpl.HttpGet(uri);
        }

        public static async Task<Response> GetProducts()
        {
            string uri = $"{_uri}";
            return await HttpClientImpl.HttpGet(uri);
        }


        public static async Task<Response> AddProduct(Product product)
        {
            string uri = $"{_uri}";
            string payload = JsonParser.SerializeJson(product);
            return await HttpClientImpl.HttpPost(uri, payload);

        }


        public static async Task<Response> UpdateProductById(string productId, Product product)
        {
            string uri = $"{_uri}/{productId}";
            return await HttpClientImpl.HttpPut(uri, product);
        }


        public static async Task<Response> UpdatePartOfProductById(string productId, Product product)
        {
            string uri = $"{_uri}/{productId}";
            return await HttpClientImpl.HttpPatch(uri, product);
        }


        public static async Task<Response> DeleteProductById(string productId)
        {
            string uri = $"{_uri}/{productId}";
            return await HttpClientImpl.HttpDelete(uri);
        }

    }
}

