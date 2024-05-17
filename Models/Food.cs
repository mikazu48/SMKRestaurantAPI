using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMKRestaurantAPI.Models
{
    public class Food
    {
        [Key]
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
