﻿using BlazingPizza.Shared;
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

        #region Variables
        List<PizzaSpecial> Specials;

        Pizza ConfiguringPizza;
        bool ShowingConfigureDialog;
        Order Order = new();
        #endregion

        #region Overrides
        protected async override Task OnInitializedAsync()
        {
            Specials = await HttpClient.GetFromJsonAsync<List<PizzaSpecial>>("specials");
        }
        #endregion

        #region Method
        void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new()
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new()
            };
            ShowingConfigureDialog = true;
        }
        #endregion

        #region event handler
        void CancelConfigurePizzaDialog()
        {
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;
        }

        void ConfirmConfigurePizzaDialog()
        {
            Order.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;
            ShowingConfigureDialog = false;
        }


        void RemoveConfiguredPizza(Pizza pizza)
        {
            Order.Pizzas.Remove(pizza);
        }

        async Task PlaceOrder()
        {
            await HttpClient.PostAsJsonAsync("orders", Order);
            Order = new Order();
        }
        #endregion
    }
}
