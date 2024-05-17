using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMKRestaurantAPI.Data;
using SMKRestaurantAPI.Models;

namespace SMKRestaurantAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly SMKRestaurantAPIContext _context;

        public FoodController(SMKRestaurantAPIContext context)
        {
            _context = context;
        }

        // GET: api/Food
        [HttpGet]
        public IActionResult GetFood()
        {
            var query = _context.Food.Select(x => new
            {
                food_id = x.FoodID,
                food_name = x.FoodName,
                price = x.Price,
                image_url = x.ImageUrl
            }).ToList();

            if (query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Food Not Found"
                });
            }

            return Ok(new
            {
                code = 200,
                message = "Food Data",
                data = query
            });
        }

        // GET: api/Food/5
        [HttpGet("{id}")]
        public IActionResult GetFood(int id)
        {
            var query = _context.Food.Where(x => x.FoodID == id)
                .Select(x => new
                {
                    food_id = x.FoodID,
                    food_name = x.FoodName,
                    price = x.Price,
                    image_url = x.ImageUrl
                }).ToList();

            if (query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Food Not Found"
                });
            }

            return Ok(new
            {
                code = 200,
                message = "Food Data",
                data = query
            });
        }

        // PUT: api/Food/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutFood(int id, Food food)
        {
            var query = _context.Food.Where(x => x.FoodID == id).FirstOrDefault();

            if (query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Food Not Found"
                });
            }

            if (food.FoodName == null || food.FoodName.Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "FoodName field must be filled"
                });
            }

            if (food.Price.ToString() == null || food.Price.ToString().Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Price field must be filled"
                });
            }

            if (food.ImageUrl == null || food.ImageUrl.Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "ImageUrl field must be filled"
                });
            }

            query.FoodName = food.FoodName;
            query.Price = food.Price;
            query.ImageUrl = food.ImageUrl;

            _context.SaveChanges();

            return Ok(new
            {
                code = 200,
                message = "Data Updated"
            });
        }

        // POST: api/Food
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostFood(Food food)
        {
            if (food.FoodName == null || food.FoodName.Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "FoodName field must be filled"
                });
            }

            if (food.Price == 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Price field must be filled"
                });
            }

            if (food.ImageUrl == null || food.ImageUrl.Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "ImageUrl field must be filled"
                });
            }

            _context.Food.Add(food);
            _context.SaveChanges();

            return CreatedAtAction("Get Foods", new
            {
                code = 201,
                message = "Data Created"
            });
        }

        // DELETE: api/Food/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFood(int id)
        {
            var query = _context.Food.Find(id);
            if (query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Food not found"
                });
            }

            _context.Food.Remove(query);
            _context.SaveChanges();

            return Ok(new
            {
                code = 200,
                message = "Data Deleted"
            });
        }
    }
}
