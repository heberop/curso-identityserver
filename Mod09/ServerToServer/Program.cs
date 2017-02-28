using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerToServerDemo
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        public static async Task MainAsync()
        {
            Console.Title = "Server To Server - Client Credentials Flow";

            var response = await RequestTokenAsync();
            response.Show();

            Console.WriteLine("[ENTER] to api call");
            Console.ReadLine();
            await CallServiceAsync(response.AccessToken);
            Console.ReadLine();
        }

        static async Task<TokenResponse> RequestTokenAsync()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError) throw new Exception(disco.Error);

            var client = new TokenClient(
                disco.TokenEndpoint,
                "server-to-server",
                "segredo");

            return await client.RequestClientCredentialsAsync("financial.read");
        }

        static async Task CallServiceAsync(string token)
        {
            var baseAddress = "http://localhost:9000/api/";

            var client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            client.SetBearerToken(token);
            var response = await client.GetStringAsync("financial");

            "\n\nService claims:".ConsoleGreen();
            Console.WriteLine(JArray.Parse(response));
        }
    }
}