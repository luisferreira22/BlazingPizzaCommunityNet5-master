using BlazingPizza.Client.Services;
using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class Index
    {
        #region Servivcios
        [Inject]
        HttpClient HttpClient { get; set; }
        #endregion
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Inject]
        OrderState OrderState { get; set; }
        #region Variables
        List<PizzaSpecial> Specials;


        #endregion

        #region Overrides
        protected async override Task OnInitializedAsync()
        {
            Specials = await HttpClient.GetFromJsonAsync<List<PizzaSpecial>>("specials");
        }
        #endregion



        #region event handler


        async Task PlaceOrder()
        {
           var Response= await HttpClient.PostAsJsonAsync("orders", OrderState.Order);
            var NewOrderId = await Response.Content.ReadFromJsonAsync<int>();
            OrderState.ResetOrder();
            navigationManager.NavigateTo($"myorders/{NewOrderId}");
        }
        #endregion
    }
}
