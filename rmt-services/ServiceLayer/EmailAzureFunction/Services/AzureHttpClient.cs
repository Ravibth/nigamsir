using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public static class AzureHttpClient
    {
        static AzureHttpClient()
        {

        }

        public static HttpClient GetAzureHttpClient(bool ignoreSSL)
        {
            HttpClient _httpClient;
            if (ignoreSSL)
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                _httpClient = new HttpClient(clientHandler);
            }
            else
            {
                _httpClient = new HttpClient();
            }

            return _httpClient;
        }
    }
}
