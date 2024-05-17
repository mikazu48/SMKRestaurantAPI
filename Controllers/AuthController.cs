using Microsoft.AspNetCore.Mvc;
using SMKRestaurantAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using SMKRestaurantAPI.Models;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMKRestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SMKRestaurantAPIContext _context;

        public AuthController(SMKRestaurantAPIContext context)
        {
            _context = context;
        }

        // POST api/<AuthControllers>
        [HttpPost]
        public IActionResult Post([FromBody] Auth authModel)
        {
            var token = "";
            var query = _context.Customer.Where(x => x.Email.Equals(authModel.Email)
            && x.Password.Equals(authModel.Password)).FirstOrDefault();
            if(query == null)
            {
                return Unauthorized(new
                {
                    code = 401,
                    message = "Wrong email or password"
                });
            }

            token = GenerateToken(authModel);
            return Ok(new
            {
                code = 200,
                message = "Login Success",
                data = new
                {
                    id = query.CustomerID,
                    expired = 30*60,
                    token = token
                }
            });
        }

        private string GenerateToken(Auth authModel)
        {
            string secrets = "SMKRestaurant2021SMKRestaurant";
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secrets);
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("email", authModel.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = secrets,
                Audience = secrets,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
