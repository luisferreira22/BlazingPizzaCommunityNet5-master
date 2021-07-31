using BlazingPizza.Componentslibrary.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizza.Shared
{
    public class OrderWithStatus
    {
        const string Preparing = "Preparando";
        const string OutForDelivery = "En camino";
        const string Delivered = "Entregado";

        public readonly static TimeSpan PreparationDuration =
                              TimeSpan.FromSeconds(25);
        public readonly static TimeSpan DeliveryDuration =
                               TimeSpan.FromMinutes(1);

        public Order Order { get; set; }
        public string StatusText { get; set; }
        public bool IsDelivered => StatusText == Delivered;
        public List<Marker> MapMarkers { get; set; }

        public static OrderWithStatus FromOrder(Order order)
        {
            string Message;
            List<Marker> Markers;
            var DispatchTime = order.CreatedTime.Add(PreparationDuration);
            if (DateTime.Now < DispatchTime)
            {
                Message = "Su orden se esta preparando";
                Markers = new List<Marker>
                {
                    ToMapMarker("Usted",order.DeliveryLocation,showPopup:true)
                };

            }
            else if (DateTime.Now < DispatchTime + DeliveryDuration)
            {
                Message = "En camino";
                var StartPosition = ComputeStartPosition(order);
                var ProportionOfDeliveryCompleted = Math.Min(1, (DateTime.Now - DispatchTime).TotalMilliseconds /
                                                                                           DeliveryDuration.TotalMilliseconds);
                var DriverPosition = LatLong.Interpolate(StartPosition, order.DeliveryLocation, ProportionOfDeliveryCompleted);
                Markers = new List<Marker>
                {
                    ToMapMarker("Usted",order.DeliveryLocation),
                    ToMapMarker("Repartidor",DriverPosition,showPopup:true)
                };
            }
            else {
                Message = "Entregado";
                Markers = new List<Marker>
                {
                    ToMapMarker("Ubicacion de entrega",order.DeliveryLocation, showPopup:true),
                   
                };
            }
            return new OrderWithStatus
            {
                Order = order,
                StatusText = Message,
                MapMarkers = Markers
            };

        }

        static Marker ToMapMarker(string description, LatLong coords, bool showPopup = false) => new Marker
                                                                                                    {
                                                                                                        Description = description,
                                                                                                        X = coords.Longitude,
                                                                                                        Y = coords.Latitude,
                                                                                                        ShowPopup = showPopup
                                                                                                    };
       static LatLong ComputeStartPosition(Order order)
        {
            //En una apliacion real este metodo recibiria los datos del GPS
            //del repartidor, a modo de ejemplo esto toma la direccion del pedido y de forma aleatoria genera una ruta para efectos de simulacion
            var Random = new Random(order.OrderId);
            var Distance = 0.01 + Random.NextDouble() * 0.02;
            var Angle = Random.NextDouble() * Math.PI * 2;
            var offset =
                      (Distance * Math.Cos(Angle),
                       Distance * Math.Sin(Angle));
            return new LatLong(
                               order.DeliveryLocation.Latitude + offset.Item1,
                               order.DeliveryLocation.Longitude + offset.Item2);

        }

    }

    
}
