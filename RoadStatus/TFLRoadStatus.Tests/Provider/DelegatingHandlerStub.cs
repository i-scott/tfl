﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace TFLRoadStatus.Tests.Provider
{
    internal class DelegatingHandlerStub : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;

        public string? RequestUrlUsed { get; set; }

        public DelegatingHandlerStub()
        {
            _handlerFunc = (request, cancellationToken) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }

        public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestUrlUsed = request.RequestUri.OriginalString;
            return _handlerFunc(request, cancellationToken);
        }
    }
}
