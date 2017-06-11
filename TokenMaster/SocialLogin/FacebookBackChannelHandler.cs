using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace TokenMaster.SocialLogin
{
    public class FacebookOauthResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
    public class FacebookBackChannelHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (!request.RequestUri.AbsolutePath.Contains("/oauth"))
            {
                request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token", "&access_token"));
            }

            //if (request.RequestUri.AbsolutePath.Contains("me"))
            //{
            //    request.RequestUri = new Uri(request.RequestUri.AbsolutePath.Replace("me?access_token=", "me?fields=email,id,first_name,last_name&access_token="));
            //}

            //https://graph.facebook.com/v2.8/oauth/access_token?grant_type=authorization_code&code=AQBjdzJHnkPOPKPrCPUiQWXEm6MGiE_ejgUwhoFSuUlwtfvEZ3BPCwKKMrwkygNx1pfo8e4WAdduDZD_PMLqkh78wFJZH_0rGctVW2iAdCKrShMpPcQOw6BYJlB6VEkZjVchBHr88pYp8xhoaqGccPkW-GHt-UINPibH4NNSW4sZUHUpedQu2uB9bueZ5weQ4Azv95akELlFVSX-O5K-2Q8w8-7VgbjmQb0CCS1KjrNiYXvCn3wGfJ-Idlg_TryzaRAR7Xhxf27dXGsXIfLhfOUufC-z9gU8yjsr5v94iZmfZ2JCovHLwxbxN3UjofP8GTg&redirect_uri=http%3A%2F%2Flocalhost%3A16193%2Fsignin-facebook&client_id=649949561872494&client_secret=3b884225bbbaaeed29d84abeaaa5cc36

            return await base.SendAsync(request, cancellationToken);


            var result = await base.SendAsync(request, cancellationToken);
            if (!request.RequestUri.AbsolutePath.Contains("access_token"))
                return result;

            // For the access token we need to now deal with the fact that the response is now in JSON format, not form values. Owin looks for form values.
            var content = await result.Content.ReadAsStringAsync();
            var facebookOauthResponse = JsonConvert.DeserializeObject<FacebookOauthResponse>(content);

            var outgoingQueryString = HttpUtility.ParseQueryString(string.Empty);
            outgoingQueryString.Add(nameof(facebookOauthResponse.access_token), facebookOauthResponse.access_token);
            outgoingQueryString.Add(nameof(facebookOauthResponse.expires_in), facebookOauthResponse.expires_in + string.Empty);
            outgoingQueryString.Add(nameof(facebookOauthResponse.token_type), facebookOauthResponse.token_type);
            var postdata = outgoingQueryString.ToString();

            var modifiedResult = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(postdata)
            };

            return modifiedResult;



        }
    }
}
