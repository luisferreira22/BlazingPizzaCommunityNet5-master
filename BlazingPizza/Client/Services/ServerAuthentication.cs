using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using BlazingPizza.Shared;
using System.Net.Http.Json;

namespace BlazingPizza.Client.Services
{
    public class ServerAuthentication : AuthenticationStateProvider
    {
        private readonly HttpClient HttpClient;
       /* public ServerAuthentication(HttpClient httpClient) 
        {
            HttpClient = httpClient;
        }*/
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            /*var UserInfo = await HttpClient.GetFromJsonAsync<UserInfo>("User");
            var Identity = UserInfo.IsAuthenticated ?
                                        new ClaimsIdentity(new[]
                                                                 {
                                                                     new Claim(ClaimTypes.Name,UserInfo.Name)
                                                                   }, "serverauth")
                                        : new ClaimsIdentity();*/
            var Claim = new Claim(ClaimTypes.Name, "Test User");
            var Identity = new ClaimsIdentity(new[] { Claim},"serverauth");
            return new AuthenticationState(new ClaimsPrincipal(Identity));
        }


    }
}
