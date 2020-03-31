using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Datingapp.API.Data;
using Datingapp.API.Dtos;
using Datingapp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;

namespace Datingapp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserToRegisterDto userToRegisterDto)
        {
            //We dont need ModelState.IsValid() because [ApiController] takes care of validation

            userToRegisterDto.Username = userToRegisterDto.Username.ToLower();

            if(await _repo.UserExists(userToRegisterDto.Username))
            {
                return BadRequest("User already Exists.");
            }

            var userToCreate = _mapper.Map<User>(userToRegisterDto);

            var createdUser = await _repo.Register(userToCreate, userToRegisterDto.Password);
            var userToReturn = _mapper.Map<UserForDetailedDto>(userToCreate);

            return CreatedAtRoute("GetUser", new {controller = "Users", id = createdUser.Id}, userToReturn);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);

            if(userFromRepo ==  null)
                return Unauthorized();
            
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHander = new JwtSecurityTokenHandler();

            var token= tokenHander.CreateToken(tokenDescriptor);

            var  user = _mapper.Map<UserForListDto>(userFromRepo);

            return Ok(new {
                token = tokenHander.WriteToken(token),
                user
            });
            
        }
    }
}