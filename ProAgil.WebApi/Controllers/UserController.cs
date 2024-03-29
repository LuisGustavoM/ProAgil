using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ProAgil.Domain.Identity;
using ProAgil.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ProAgil.WebApi.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(IConfiguration config,
                                UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                IMapper mapper)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            return Ok(new UserDto());
        }

        [HttpPost("Registro")]
        [AllowAnonymous]
        public async Task<IActionResult> Registro(UserDto userDto)
        {
          try
          {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.PasswordHash);
                var userToReturn = _mapper.Map<UserDto>(user);

                if(result.Succeeded){
                    return Created("GetUser", userToReturn);
                }
                return BadRequest(result.Errors);
          }
          catch (System.Exception ex)
          {
              
        return this.StatusCode (StatusCodes.Status500InternalServerError, $"Erro ao receber a requisição{ex.Message}");

          }
        }


        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto UserLogin)
        {
           try
           {
               var user = await _userManager.FindByNameAsync(UserLogin.UserName);
               var result = await _signInManager.CheckPasswordSignInAsync
                                    (user, UserLogin.PasswordHash,false);

                if( result.Succeeded)
                {
                    var appUser = await _userManager.Users.FirstOrDefaultAsync
                    (u => u.NormalizedUserName == UserLogin.UserName.ToUpper());
                    
                    var userToReturn = _mapper.Map<UserLoginDto>(appUser);

                    return Ok(new {
                        token = GenerateJwToken(appUser).Result,
                        user = userToReturn
                    });
                }
                return Unauthorized();
           }
           catch (System.Exception ex)
           {
                return this.StatusCode (StatusCodes.Status500InternalServerError, 
                $"PROBLEMA AQUI  {ex.ToString()}");
                

           }
        }
        
        private async Task<string> GenerateJwToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, user.ToString()),
                new Claim (ClaimTypes.Name, user.UserName)
            };  

            var roles  = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

           var key = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                 Subject = new ClaimsIdentity(claims),
                 Expires = DateTime.UtcNow.AddYears(001),
                 SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler(); 
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}