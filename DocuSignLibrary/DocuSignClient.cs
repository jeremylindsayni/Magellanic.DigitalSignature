using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using DocuSignLibrary.Models;

namespace DocuSignLibrary
{
    public class DocuSignClient
    {
        public DocuSignClient(Credentials credentials)
        {
            // initialize client for desired environment (for production change to www)
            ApiClient apiClient = new ApiClient("https://demo.docusign.net/restapi");
            Configuration.Default.ApiClient = apiClient;

            // configure 'X-DocuSign-Authentication' header
            string authHeader = "{\"Username\":\"" + credentials.Username + "\", \"Password\":\"" + credentials.Password + "\", \"IntegratorKey\":\"" + credentials.IntegratorKey + "\"}";

            if (!Configuration.Default.DefaultHeader.ContainsKey("X-DocuSign-Authentication")) {
                Configuration.Default.AddDefaultHeader("X-DocuSign-Authentication", authHeader);
            }

            // login call is available in the authentication api 
            AuthenticationApi authApi = new AuthenticationApi();
            LoginInformation loginInfo = authApi.Login();

            // parse the first account ID that is returned (user might belong to multiple accounts)
            this.AccountId = loginInfo.LoginAccounts[0].AccountId;
        }

        public string AccountId { get; set; }
    }
}
