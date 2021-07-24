using Microsoft.AspNetCore.Components;
using BlazingPizza.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingPizza.Client.Shared
{
    public partial class AddressEditor
    {
        [Parameter]
        public Address Address { get; set; }
    }
}
