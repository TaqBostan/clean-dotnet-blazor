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
        private readonly Func<HttpRequestMessage, HttpResponseMessage> _handlerFunc;

        public MockHttpMessageHandler(HttpResponseMessage responseMessage)
        {
            _responseMessage = responseMessage;
        }
        public MockHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(_responseMessage != null) return Task.FromResult(_responseMessage);
            else return Task.FromResult(_handlerFunc(request));
        }
    }

    public static class Constants
    {
        public const string BaseUrl = "https://localhost:7045";
    }
}
