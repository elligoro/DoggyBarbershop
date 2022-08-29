using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoggyBarbershop.Models
{
    public class OrdersResponse : BaseResponse
    {
        public List<OrdersGetModel> Orders { get; set; }
        public int AccountId { get; set; }
    }

    public class OrdersGetModel
    {
        public int OrderId {get; set;}
	    public int AccountId {get; set;}
        public DateTime BookingDate {get; set;}
        public DateTime CreatedDate { get; set; }
	    public string Name {get; set;}
    }
}
