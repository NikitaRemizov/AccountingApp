using AccountingApp.Models;
using AccountingApp.Utils;
using AutoMapper;
using BLL.DTO;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AccountingApp.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private List<UserDTO> people = new List<UserDTO>
        {
            new UserDTO { Email="admin@gmail.com", Password="12345", },
            new UserDTO { Email="qwerty@gmail.com", Password="55555", }
        };

        public IAccountService AccountService { get; }
        public IMapper Mapper { get; }

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            AccountService = accountService;
            Mapper = mapper;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(User user)
        {
            var userDTO = Mapper.Map<UserDTO>(user);
            if (await AccountService.IsRegistered(userDTO))
            {
                return Conflict($"The user with this email is already registered");
            }
            await AccountService.Register(userDTO);
            return await Login(user);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(User user)
        {
            var userDTO = Mapper.Map<UserDTO>(user);
            var userId = await AccountService.VerifyCredentials(userDTO);
            if (userId is null)
            {
                return BadRequest($"The Email or password is incorrect");
            }
            return Token(userDTO);
        }


        private IActionResult Token(UserDTO user)
        {
            var identity = GetIdentity(user);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthentificationOptions.ISSUER,
                    audience: AuthentificationOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthentificationOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthentificationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(UserDTO user)
        {
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
