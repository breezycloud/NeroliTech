using NeroliTech.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NeroliTech.Client.Services
{
    public class UserService : IUserService
    {
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient _httpClient { get; }
        

        public async Task<User> LoginAsync(User user)
        {
            user.Password = Utility.PasswordService.Encrypt(user.Password);
            string serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/Login");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }

        public Task<User> RegisterUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
