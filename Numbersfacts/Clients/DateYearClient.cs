using Newtonsoft.Json;
using Numbersfacts.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Numbersfacts.Clients
{
    public class DateYearClient
    {
        private HttpClient _client;
        private static string _adress = "https://api.wikimedia.org";
        public DateYearClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_adress);
        }

        public async Task<List<string>> DateBirths(string date)
        {
            var response = await _client.GetAsync($"/feed/v1/wikipedia/uk/onthisday/births/{date}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var list = JsonConvert.DeserializeObject<Births>(content);

            var result = new List<string>();
            foreach(var item in list.births)
            {
                result.Add($"{item.text} ({item.year})");
            }

            return result;
        }

        public async Task<List<string>> YearEvent(string year)
        {
            var response = await _client.GetAsync($"/core/v1/wikipedia/uk/page/{year}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var text = JsonConvert.DeserializeObject<ModelYearEvent>(content);

            var listTest = new List<string>();
            Regex regextest = new Regex(@"\* \[\[(\w*)");

            foreach (var item in text.source.Split('\n'))
            {
                MatchCollection matchestest = regextest.Matches(item);
                if (matchestest.Count > 0)
                {
                    string pat1 = @"<ref(.*?)<\/ref>";
                    Regex reg = new Regex($"{pat1}");
                    var s = reg.Replace(item, "");
                    string pat2 = @"([[\]\*])*([\*])*([\\])*(&nbsp;)*";
                    reg = new Regex($"{pat2}");
                    s = reg.Replace(s, "");

                    listTest.Add(s);
                } 
            }
            return listTest;
        }

        public async Task<ModelOnThisDay> OnThisDay(string date)
        {
            var response = await _client.GetAsync($"/feed/v1/wikipedia/uk/onthisday/all/{date}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var text = JsonConvert.DeserializeObject<ModelOnThisDay>(content);

            return text;
        }

    }
}
