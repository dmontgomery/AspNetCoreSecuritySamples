using System;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Api
{
    public class PermissionsEnforcer: IEnforcePermissions
    {
        private string _rootAddress;
        private TokenResponse _clientAccessToken;
        
        public PermissionsEnforcer()
        {
            // IRL we pass the NetFileConfiguration object here
            _rootAddress = "https://localhost:8001";
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync(_rootAddress).Result;
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
            // request token
            var tokenResponse = client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "bffdemo.backend",
                    ClientSecret = "secret",
                    Scope = "domain-capsule-api"
                }).Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            
            Console.WriteLine(tokenResponse.Json);
            _clientAccessToken = tokenResponse;
        }

        public void AssertAuthorized(string sessionIdFromClaim, string permissionName)
        {
            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(_clientAccessToken.AccessToken);

            var response = apiClient.GetAsync($"{_rootAddress}/api/permissions").Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
            }
            
            
            
            
        }
    }
}