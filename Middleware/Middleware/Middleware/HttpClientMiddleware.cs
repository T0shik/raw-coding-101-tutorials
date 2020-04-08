using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Middleware
{
    public class HttpClientMiddleware : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, 
            CancellationToken cancellationToken)
        {
            // do before
            var response = await base.SendAsync(request, cancellationToken);
            // do after
            return response;
        }
    }
}
