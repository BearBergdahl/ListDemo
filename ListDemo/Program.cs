using DemoClient;
using Newtonsoft.Json;
using RestSharp;
using System.Dynamic;
using System.Text.Json.Serialization;

namespace ListDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's get ourself properly insulted");
            List<RestResponse> rawInsults = new List<RestResponse>();
            ClientService clientService = new ClientService();
            for (int i = 0; i < 5; i++) 
            {
                var response = clientService.GetInsult();                
                rawInsults.Add(response);  
            }
                        
            List<string> textInsults = DeserializeResponses(rawInsults);
            int hash = textInsults.GetHashCode();
            foreach(var text in textInsults)
            {
                Console.WriteLine(text);
            }
            Console.WriteLine("Press any Key");
            Console.ReadKey();
        }

        /// <summary>
        /// Look at List<T> docs at https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-7.0
        /// </summary>
        /// <param name="rawInsults"></param>
        /// <returns></returns>
        private static List<string> DeserializeResponses(List<RestResponse> rawInsults)
        {
            List<string> insultsOnly = new List<string>();
            foreach (RestResponse response in rawInsults)
            {
                if (response.IsSuccessful)
                {
                    ResponseContent insult = JsonConvert.DeserializeObject<ResponseContent>(response.Content);
                    insultsOnly.Add(insult.Insult);
                }
            }
            int number = rawInsults.Count;
            rawInsults.Clear(); //Well you can guess
            number = rawInsults.Count;
            insultsOnly.ForEach(insult => { Console.WriteLine(insult); }); //returns void, so do whatever necessary in the lambda-function
            insultsOnly.Reverse();
            insultsOnly.ForEach((insult) => { Console.WriteLine(insult); });
            int capacity= insultsOnly.Capacity; // amount of space available before rezising list
            
            var readOnly = insultsOnly.AsReadOnly();
            Console.WriteLine($"What's the type of the readOnly-object? {readOnly.GetType()}");
            
            return insultsOnly;
        }

        public class ResponseContent
        {
            string Number { get; set; }
            string Language { get; set; }   
            public string Insult { get; set; }
            string Created { get; set; }
            string Shown { get; set; }
            string CreatedBy { get; set; }
            string Active { get; set; } 
            string Comment { get; set; }
        }
    }
}