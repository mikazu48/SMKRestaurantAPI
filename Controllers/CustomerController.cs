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
    public class CustomerController : ControllerBase
    {
        private readonly SMKRestaurantAPIContext _context;

        public CustomerController(SMKRestaurantAPIContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public IActionResult GetCustomer()
        {
            var query = _context.Customer.Select(x=>new
            {
                customer_id = x.CustomerID,
                customer_name = x.FullName,
                birthdate = x.BirthDate,
                email = x.Email
            }).ToList();

            if(query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Data Empty"
                });
            }

            return Ok(new
            {
                code = 200,
                message = "Customers Data",
                data = query
            });
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var query = _context.Customer.Where(x=>x.CustomerID == id)
                .Select(x=>new
                {
                    customer_id = x.CustomerID,
                    customer_name = x.FullName,
                    birthdate = x.BirthDate,
                    email = x.Email
                }).ToList();

            if (query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Customer Not Found"
                });
            }

            return Ok(new
            {
                code = 200,
                message = "Customer Data",
                data = query
            });
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, Customer customers)
        {
            var query = _context.Customer.Where(x => x.CustomerID == id).FirstOrDefault();

            if(query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Customer Not Found"
                });
            }

            if (customers.FullName == null || customers.FullName.Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "FullName field must be filled"
                });
            }

            if (customers.BirthDate.ToString() == null || customers.BirthDate.ToString().Equals("01-Jan-01 12:00:00 AM"))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "BirthDate field must be filled"
                });
            }

            if (customers.Email == null || customers.Email.Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Email field must be filled"
                });
            }

            query.FullName = customers.FullName;
            query.BirthDate = customers.BirthDate;
            query.Email = customers.Email;

            _context.SaveChanges();

            return Ok(new
            {
                code = 200,
                message = "Data Updated"
            });
        }

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostCustomer(Customer customers)
        {
            if (customers.FullName == null || customers.FullName.Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "FullName field must be filled"
                });
            }

            if (customers.BirthDate.ToString() == null || customers.BirthDate.ToString().Equals("01-Jan-01 12:00:00 AM"))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "BirthDate field must be filled"
                });
            }

            if (customers.Email == null || customers.Email.Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Email field must be filled"
                });
            }

            if (customers.Password == null || customers.Password.Equals(String.Empty))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Password field must be filled"
                });
            }
            _context.Customer.Add(customers);
            _context.SaveChanges();

            return CreatedAtAction("Get Customers", new 
            { 
                code = 201,
                message = "Data Created"
            });
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var query = _context.Customer.Find(id);
            if (query == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "Customer not found"
                });
            }

            _context.Customer.Remove(query);
            _context.SaveChanges();

            return Ok(new
            {
                code = 200,
                message = "Data Deleted"
            });
        }
    }
}
