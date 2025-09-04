using System.Text.Json.Serialization;

namespace ProductAPIAutomation.Core.Endpoints.Products.models
{
    public class Product
    {


        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("data")]
        public ProductData? Data { get; set; }


        public Product()
        {
        }
        public Product(string? name, ProductData? data)
        {
            Name = name;
            Data = data;
        }

        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   Id == product.Id &&
                   Name == product.Name &&
                   EqualityComparer<ProductData>.Default.Equals(Data, product.Data);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Data);
        }
    }
}
