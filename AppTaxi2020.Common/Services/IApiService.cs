using AppTaxi2020.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTaxi2020.Common.Services
{
    public interface IApiService
    {
        Task<Response> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest);

        Task<Response> GetTaxiAsync(string plaque, string urlBase, string servicePrefix, string controller);

        Task<bool> CheckConnectionAsync(string url);
        
        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<Response> GetUserByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, EmailRequest request);

    }
}
