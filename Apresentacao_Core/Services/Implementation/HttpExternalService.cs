using Apresentacao_Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Apresentacao_Core.Services.Implementation
{
    public class HttpExternalService : IHttpExternalService
    {
        private string Api = "https://localhost:7066/api/";
        private readonly HttpClient _httpClient;
        public HttpExternalService(HttpClient httpClient, HttpRequestMessage httpRequestMessage)
        {
            _httpClient = httpClient;
            
        }

        public async Task<object> CriarTokenIdentity(object jObject)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, Api + "CriarTokenIdentity");
            var content = new StringContent(jObject.ToString(), null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);

            return response.Content;
        }

        public async Task<object> AdicionatUsarioIdentity(object JOject)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Api + "AdicionatUsarioIdentity");
            var content = new StringContent(JOject.ToString(), null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);

            return response.Content;

        }

        public async Task<object> ConfirmarEmailIdentity(object JOject)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Api + "ConfirmarEmailIdentity");
            var content = new StringContent(JOject.ToString(), null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);

            return response.Content;
        }

        public async Task<object> EmailResetDeSenha(string EmailUser)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Api + $"EmailResetDeSenha?EmailUser={EmailUser}");
            var response = await _httpClient.SendAsync(request);

            return response.Content;
        }

        public async Task<object> ResetarSenhaIdentity(object JOject)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Api + "ConfirmarEmailIdentity");
            var content = new StringContent(JOject.ToString(), null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);

            return response.Content;
        }
    }
}
