using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMKRestaurantAPI.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public int FoodID { get; set; }
        public int Quantity { get; set; }
        public int SubTotal { get; set; }
        public string StatusOrder { get; set; }
    }
}
