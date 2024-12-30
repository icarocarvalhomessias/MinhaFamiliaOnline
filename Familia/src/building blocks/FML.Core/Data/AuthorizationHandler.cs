using System.Net.Http.Headers;

namespace FML.Core.Data
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private readonly IAspNetUser _user;

        public AuthorizationHandler(IAspNetUser user)
        {
            _user = user;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorization = _user.ObterHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorization))
            {
                request.Headers.Add("Authorization", new List<string>() { authorization });
            }

            var token = _user.ObterUserToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
