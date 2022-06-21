using Newtonsoft.Json;
using Numbersfacts.Models.ApiModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Numbersfacts.Clients
{
    public class NumberClient
    {
        private HttpClient _client;
        private static string _adress = $"http://numbersapi.com";

        public NumberClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_adress);
        }

        public async Task<string> GetInfoMath(string number)
        {
            var response = await _client.GetAsync($"/{number}/math/?json");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<ModelNumber>(content);
            return result.text;

        }


        public async Task<string> GetInfoGeneral(string number)
        {
            var response = await _client.GetAsync($"/{number}/?json");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<ModelNumber>(content);
            return result.text;

        }

    }
}
