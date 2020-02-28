using AppTaxi2020.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppTaxi2020.Common.Services
{
    public class ApiService : IApiService
    {
        public async Task<Response> GetTaxiAsync(string plaque, string urlBase, string servicePrefix, string controller)
        {
			try
			{
				var client = new HttpClient
				{
					BaseAddress = new Uri(urlBase),
				};

				var url = $"{servicePrefix}{controller}/{plaque}";
				var response = await client.GetAsync(url);
				var result = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
					   IsSuccess = false,
					   Message = result,
					   

					};
				}

				var model = JsonConvert.DeserializeObject<TaxiResponse>(result);
				return new Response 
				{
				 IsSuccess = true,
				 Result = model,
				};
			}
			catch (Exception ex)
			{

				return new Response 
				{ 
				   IsSuccess = false,
				   Message = ex.Message,
				};
			}
        }
    }
}
