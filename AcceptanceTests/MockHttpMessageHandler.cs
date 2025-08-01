using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanDotnetBlazor.AcceptanceTests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _responseMessage;

        public MockHttpMessageHandler(HttpResponseMessage responseMessage)
        {
            _responseMessage = responseMessage;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_responseMessage);
        }
    }

    public static class Constants
    {
        public const string BaseUrl = "https://localhost:7045";
    }
}
