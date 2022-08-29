using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop.Models
{
    public class OrderUpdateRequest
    {
        public int? OrderId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}