using AccountingApp.Models;
using AccountingApp.Models.Validation;
using AccountingApp.Utils;
using AutoMapper;
using AccountingApp.BLL.DTO;
using AccountingApp.BLL.Services.Interfaces;
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
    [ValidateModel]
    public class AccountController : AccountingController
    {
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
            var userDto = Mapper.Map<UserDTO>(user);
            if (await AccountService.IsRegistered(userDto))
            {
                return Conflict(WrapError($"The user with this email is already registered"));
            }
            if (await AccountService.Register(userDto) == Guid.Empty)
            {
                return StatusCode(500, WrapError("Couldn't register new user"));
            }
            return await Login(user);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(User user)
        {
            var userDto = Mapper.Map<UserDTO>(user);
            var userId = await AccountService.VerifyCredentials(userDto);
            if (userId is null)
            {
                return BadRequest(WrapError($"The Email or password is incorrect"));
            }
            return Token(user);
        }

        private IActionResult Token(User user)
        {
            var identity = GetIdentity(user);
            if (identity == null)
            {
                return BadRequest(WrapError("Provided invalid identity information"));
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

        private static ClaimsIdentity GetIdentity(User user)
        {
            if (user is null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
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
