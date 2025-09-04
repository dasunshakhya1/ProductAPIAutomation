using System.Text.Json.Serialization;

namespace ProductAPIAutomation.Core.Endpoints.Products.models
{
    public class ErrorResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ErrorResponse response &&
                   Error == response.Error;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Error);
        }
    }



}
