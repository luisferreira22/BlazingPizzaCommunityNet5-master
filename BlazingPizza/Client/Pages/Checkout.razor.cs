using BlazingPizza.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class Checkout
    {
        [Inject]
        public HttpClient HttpClient { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public OrderState OrderState { get; set; }
        public bool IsTaskRunning { get; set; }

        async Task PlaceOrder()
        {
            IsTaskRunning = true;
           
            var Response = await HttpClient.PostAsJsonAsync("orders", OrderState.Order);
            var NewOrderId = await Response.Content.ReadFromJsonAsync<int>();
            OrderState.ResetOrder();
            NavigationManager.NavigateTo($"myorders/{NewOrderId}");

            IsTaskRunning = false;
           
            }
    }
}
