﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiApp.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ApiApp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context)
        {
            _context = context;
        }
        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Name == username && x.Password == password);
            if(user != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name)
                };
                foreach(var role in user.UserRoles)
                {
                    claims.Add(new Claim("role", role.ToString()));
                }
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if(username == null || password == null)
            {
                return BadRequest("Данные неверны");
            }
            ClaimsIdentity identity = await GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest("Данные неверны");
            }
            DateTime now = DateTime.Now;
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer:AuthOptions.ISSUER,
                audience:AuthOptions.AUDIENCE,
                notBefore:now,
                claims:identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }

        [Route("info")]
        [Authorize(Roles ="User")]
        [HttpGet]
        public IActionResult Info()
        {
            var request = User.IsInRole(Role.Client.ToString());
            
            return Ok(new {
                message = "Вы авторизованы",
                obj = request
            });
        }

    }
}