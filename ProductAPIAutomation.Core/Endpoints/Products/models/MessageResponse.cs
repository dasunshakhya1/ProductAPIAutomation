using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductAPIAutomation.Core.Endpoints.Products.models
{
    public class MessageResponse
    {

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MessageResponse response &&
                   Message == response.Message;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Message);
        }
    }
}
