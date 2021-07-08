using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazingPizza.Shared
{
    public class Address
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Indique quien recibira la orden"), MaxLength(100)]
        public string Name { get; set; }
       [Required(ErrorMessage ="Debe ingresar una dirección"), MaxLength(100)]
        public string Line1 { get; set; }
       [MaxLength(100)]
        public string Line2 { get; set; }
        [Required(ErrorMessage ="Debe ingresar una cuidad"),MaxLength(100)]
        public string City { get; set; }
        [Required(ErrorMessage ="Debe ingresar un barrio"),MaxLength(50)]
        public string Region { get; set; }
        [Required(ErrorMessage ="Debe ingresar el codigo postal"),MaxLength(5)]
        public string PostalCode { get; set; }
    }

}
