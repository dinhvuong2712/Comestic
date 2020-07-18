using Newtonsoft.Json;
using System.Net.Http;

namespace Comestics.Common
{
    public class Utility
    {
        public static HttpResponseMessage CreateJsonResponse(string text)
        {
            HttpResponseMessage rest = new HttpResponseMessage();
            rest.Content = new StringContent(text, System.Text.Encoding.UTF8, "application/json");
            return rest;
        }
        public static HttpResponseMessage CreateJsonTextResponse(string text)
        {
            var result = new
            {
                error = text
            };
            return CreateJsonResponse(JsonConvert.SerializeObject(result));
        }
        public static HttpResponseMessage CreateJsonErrorResponse(string text)
        {
            var result = new
            {
                message = text
            };
            return CreateJsonResponse(JsonConvert.SerializeObject(result));
        }
    }
}