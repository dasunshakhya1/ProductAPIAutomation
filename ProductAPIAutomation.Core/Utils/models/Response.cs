using RestSharp;

namespace ProductAPIAutomation.Core.Utils.models
{
    public class Response
    {
        private readonly string? content;
        private readonly int statusCode;
        private readonly bool? isError;
        private readonly IReadOnlyCollection<HeaderParameter>? headerParameters;

        public Response(string resContent, int resStatusCode, IReadOnlyCollection<HeaderParameter>? headers, bool error)
        {
            content = resContent;

            statusCode = resStatusCode;

            headerParameters = headers;

            isError = error;
        }

        public string? Content { get { return content; } }
        public int StatusCode { get { return statusCode; } }
        public bool? Success { get { return isError; } }
        public IReadOnlyCollection<HeaderParameter>? HeaderParameters
        {
            get { return headerParameters; }
        }
    }
}

