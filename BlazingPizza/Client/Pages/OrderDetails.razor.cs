using BlazingPizza.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Pages
{
    public partial class OrderDetails
    {
        [Parameter]
        public int OrderId { get; set; }
        [Inject]
        HttpClient HttpClient { get; set; }
        OrderWithStatus OrderWithStatus;
        bool InvalidOrder;

        CancellationTokenSource PollingCanlationToken;
        private async void PollforUpdate()
        {
            PollingCanlationToken = new CancellationTokenSource();
            while (!PollingCanlationToken.IsCancellationRequested)
            {
                try
                {
                    InvalidOrder = false;
                    OrderWithStatus = await HttpClient.GetFromJsonAsync<OrderWithStatus>($"orders/{OrderId}");
                    if (OrderWithStatus.StatusText == "Entregado")
                    {
                        PollingCanlationToken.Cancel();
                    }
                }
                catch (Exception ex)
                {
                    InvalidOrder = true;
                    PollingCanlationToken.Cancel();
                    Console.Error.WriteLine(ex);
                }
                StateHasChanged();
                await Task.Delay(400);
            }

        }
        //component of the parameter life cycle, it is executed each time a parameter changes value.
        protected override void OnParametersSet()
        {
            PollingCanlationToken?.Cancel();
            PollforUpdate();
        }

        //Used to free up continuous access to the server
        void IDisposable.Dispose()
        {
            PollingCanlationToken?.Cancel();
        }


    }
}
