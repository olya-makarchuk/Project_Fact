using Newtonsoft.Json;
using Numbersfacts.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Numbersfacts.Clients
{
    public class WordClient
    {

        private HttpClient _client;
        private static string _adress = "https://api.wordnik.com";
        private static string key = "gq034f1jgewp3eol1b7rm4paz9c15nmbk8ajgajn3982r97fz";
        public WordClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_adress);
        }

        public async Task<List<ModelDefinition>> Definitions(string word, string wordinput)
        {
            var response = await _client.GetAsync($"/v4/word.json/{word}/definitions?limit=10&api_key={key}");
            var list = new List<ModelDefinition>();
            if (response.IsSuccessStatusCode == false)
            {
                var model = new ModelDefinition();
                model.text = $"Слово \"{wordinput}\" незнайдено";
                list.Add(model);
                return list;
            }
            var content = response.Content.ReadAsStringAsync().Result;

            list = JsonConvert.DeserializeObject<List<ModelDefinition>>(content);
            var newlist = new List<ModelDefinition>();
            foreach (var item in list)
            {
                string pat1 = @"<(.*?)>";
                Regex reg = new Regex($"{pat1}");
                if(item.text != null)
                {
                    item.text = reg.Replace(item.text, "");
                    newlist.Add(item);
                }
            }
            return newlist;
        }

    }
}
