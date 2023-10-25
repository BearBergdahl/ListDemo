using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using static System.Net.WebRequestMethods;

namespace DemoClient
{
    public class ClientService
    {
        public RestResponse GetInsult()
        {
            RestClient client = CreateClient($"https://evilinsult.com/generate_insult.php?lang=en&type=json");
            return MakeRestCall(client);
        }

        private RestResponse MakeRestCall(RestClient restClient)
        {
            using (RestClient client = restClient)
            {
                var request = new RestRequest();
                var response = client.Get(request);
                return response;
            }
        }

        private RestClient CreateClient(string url)
        {
            var options = new RestClientOptions(url);
            var client = new RestClient(options);
            return client;
        }
    }
}
