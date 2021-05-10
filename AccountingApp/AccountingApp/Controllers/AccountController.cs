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
        public AuthentificationOptions AuthOptions { get; }

        public AccountController(IAccountService accountService,
                                 IMapper mapper,
                                 AuthentificationOptions authOptions)
        {
            AccountService = accountService;
            Mapper = mapper;
            AuthOptions = authOptions;
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
                return BadRequest(new { errorText = "Provided invalid identity information" });
            }

            var now = DateTime.UtcNow;

            var jwtToken = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(
                    AuthOptions.SigningKey, 
                    SecurityAlgorithms.HmacSha256
                ));

            var encodedJwtToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var response = new
            {
                access_token = encodedJwtToken,
                username = identity.Name
            };

            return Ok(response);
        }

        private static ClaimsIdentity GetIdentity(UserDTO user)
        {
            if (user is null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims: claims, 
                authenticationType: "Token", 
                nameType: ClaimsIdentity.DefaultNameClaimType,
                roleType: ClaimsIdentity.DefaultRoleClaimType
            );
            return claimsIdentity;
        }
    }
}
