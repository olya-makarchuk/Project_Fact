using Newtonsoft.Json;
using Numbersfacts.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Numbersfacts.Clients
{
	public class TranslateClient
	{
		private HttpClient _client;
		private static string _adress = $"https://languageomega.herokuapp.com";

        public TranslateClient()
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri(_adress);
		}

		public async Task<List<string>> TextTransl(List<string> list)
		{

			HttpResponseMessage response;
			for(int i = 0; i < list.Count; i++)
            {
				response = await _client.GetAsync($"/json?lang_one=en&lang_two=uk&content={list[i]}");
				response.EnsureSuccessStatusCode();
				var content = response.Content.ReadAsStringAsync().Result;
				var result = JsonConvert.DeserializeObject<ModelTranslate>(content);
				list[i] = result.TranslatedContent;

			}
			return list;

		}
		public async Task<string> TextTransl(string text)
		{

			HttpResponseMessage response;
			response = await _client.GetAsync($"/json?lang_one=uk&lang_two=en&content={text}");
			response.EnsureSuccessStatusCode();
			var content = response.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ModelTranslate>(content);
			text = result.TranslatedContent;
			return text;

		}
	}
}
