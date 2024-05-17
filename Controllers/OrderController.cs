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
    public class OrderController : ControllerBase
    {
        private readonly SMKRestaurantAPIContext _context;

        public OrderController(SMKRestaurantAPIContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public IActionResult GetOrder()
        {
            var query = (from o in _context.Order 
                        join c in _context.Customer on o.CustomerID equals c.CustomerID
                        join f in _context.Food on o.FoodID equals f.FoodID 
                        select new
                        {
                            order_id = o.OrderID,
                            order_date = o.OrderDate,
                            customer_id = o.CustomerID,
                            food_id = o.FoodID,
                            qty = o.Quantity,
                            subtotal = o.SubTotal,
                            status = o.StatusOrder,
                            customer = new
                            {
                                customer_id = c.CustomerID,
                                customer_name = c.FullName,
                                birthdate = c.BirthDate,
                                email = c.Email
                            },
                            food = new
                            {
                                food_id = f.FoodID,
                                food_name = f.FoodName,
                                price = f.Price,
                                image_url = f.ImageUrl
                            }
                        }).ToList();

            if (query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Order Not Found"
                });
            }

            return Ok(new
            {
                code = 200,
                message = "Order Data",
                data = query
            });
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            var query = (from o in _context.Order
                         join c in _context.Customer on o.CustomerID equals c.CustomerID
                         join f in _context.Food on o.FoodID equals f.FoodID where o.OrderID == id
                         select new
                         {
                             order_id = o.OrderID,
                             order_date = o.OrderDate,
                             customer_id = o.CustomerID,
                             food_id = o.FoodID,
                             qty = o.Quantity,
                             subtotal = o.SubTotal,
                             status = o.StatusOrder,
                             customer = new
                             {
                                 customer_id = c.CustomerID,
                                 customer_name = c.FullName,
                                 birthdate = c.BirthDate,
                                 email = c.Email
                             },
                             food = new
                             {
                                 food_id = f.FoodID,
                                 food_name = f.FoodName,
                                 price = f.Price,
                                 image_url = f.ImageUrl
                             }
                         }).ToList();

            if (query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Order Not Found"
                });
            }

            return Ok(new
            {
                code = 200,
                message = "Order Data",
                data = query
            });
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, Order order)
        {
            var query = _context.Order.Where(x => x.OrderID == id).FirstOrDefault();

            if (query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Order Not Found"
                });
            }

            if (order.OrderDate.ToString() == null || order.OrderDate.ToString().Equals("01-Jan-01 12:00:00 AM"))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "OrderDate field must be filled"
                });
            }

            if (order.CustomerID == 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "CustomerID field must be filled"
                });
            }

            if (order.FoodID == 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "FoodID field must be filled"
                });
            }

            if (order.Quantity == 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Quantity field must be filled"
                });
            }

            if (order.SubTotal == 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "SubTotal field must be filled"
                });
            }

            if (order.StatusOrder == null || order.StatusOrder.Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "StatusOrder field must be filled"
                });
            }

            query.OrderDate = order.OrderDate;
            query.CustomerID = order.CustomerID;
            query.FoodID = order.FoodID;
            query.Quantity = order.Quantity;
            query.SubTotal = order.SubTotal;
            query.StatusOrder = order.StatusOrder;

            _context.SaveChanges();

            return Ok(new
            {
                code = 200,
                message = "Data Updated"
            });
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostOrder(Order order)
        { 
            if (order.OrderDate.ToString() == null || order.OrderDate.ToString().Equals("01-Jan-01 12:00:00 AM"))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "OrderDate field must be filled"
                });
            }

            if (order.CustomerID == 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "CustomerID field must be filled"
                });
            }

            if (order.FoodID == 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "FoodID field must be filled"
                });
            }

            if (order.Quantity == 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Quantity field must be filled"
                });
            }

            if (order.SubTotal == 0)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "SubTotal field must be filled"
                });
            }

            if (order.StatusOrder == null || order.StatusOrder.Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "StatusOrder field must be filled"
                });
            }

            _context.Order.Add(order);
            _context.SaveChanges();

            return CreatedAtAction("Get Orders", new
            {
                code = 201,
                message = "Data Created"
            });
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var query = _context.Order.Find(id);
            if (query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Order not found"
                });
            }

            _context.Order.Remove(query);
            _context.SaveChanges();

            return Ok(new
            {
                code = 200,
                message = "Data Deleted"
            });
        }
    }
}
