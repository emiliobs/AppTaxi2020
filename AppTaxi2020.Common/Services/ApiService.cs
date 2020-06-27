using AppTaxi2020.Common.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppTaxi2020.Common.Services
{
    public class ApiService : IApiService
    {
        [Obsolete]
        public async Task<bool> CheckConnectionAsync(string url)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return false;
            }

            return await CrossConnectivity.Current.IsRemoteReachable(url);
        }

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

        public async Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request)
        {
            try
            {
                var requestString = JsonConvert.SerializeObject(request);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };
                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var token = JsonConvert.DeserializeObject<TokenResponse>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = token,
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = $"Error : {ex.Message}",
                };
            }
        }

        public async Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request)
        {
            try
            {
                var requestString = JsonConvert.SerializeObject(request);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var userResponse = JsonConvert.DeserializeObject<UserResponse>(result);

                return new Response
                {
                    IsSuccess = false,
                    Result = userResponse,
                };
            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                };
            }
        }

        public async Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest)
        {
            try
            {
                var request = JsonConvert.SerializeObject(userRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<Response>(answer);
                return obj;
            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccess = false,
                    Message = $"Error: {ex.Message}",
                };
            }
        }
    }
}

