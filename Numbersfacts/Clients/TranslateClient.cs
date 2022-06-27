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
		private static string _adress = $"https://api.mymemory.translated.net";


		public TranslateClient()
		{
			_client = new HttpClient();
			_client.BaseAddress = new Uri(_adress);
		}


		public async Task<List<string>> TextTransl(List<string> list)
		{

			HttpResponseMessage response;
			for (int i = 0; i < list.Count; i++)
			{
				response = await _client.GetAsync($"/get?q={list[i]}&langpair=en|uk");
				response.EnsureSuccessStatusCode();
				var content = response.Content.ReadAsStringAsync().Result;
				var result = JsonConvert.DeserializeObject<ModelTranslate>(content);
				list[i] = result.responseData.translatedText;
			}
			return list;

		}
		public async Task<string> TextTransl(string text)
		{

			HttpResponseMessage response;
			response = await _client.GetAsync($"/get?q={text}&langpair=uk|en");
			response.EnsureSuccessStatusCode();
			var content = response.Content.ReadAsStringAsync().Result;
			var result = JsonConvert.DeserializeObject<ModelTranslate>(content);
			text=result.responseData.translatedText;
			return text;

		}
	}

}

