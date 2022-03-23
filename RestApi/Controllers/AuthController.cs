using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using ApplicationCore.Utils;
using Infraestructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _options;
        public AuthController(IOptions<AppSettings> options, IUnitOfWork unitOfWork)
        {
            _options = options.Value;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var user = new User
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = registerDTO.Password,
            };

            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "User registered.",
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _unitOfWork.Users.Authenticate(loginDTO.Username, loginDTO.password);
         
            if(user == null)
            {
                return BadRequest(new ResponseDTO
                {
                    StatusCode = 400,
                    Message = "Username or password incorrect."
                });
            }
            var authResponse = GenerateToken(user);

            return Ok(new ResponseDTO
            {
                StatusCode = 200,
                Message = "User authenticate.",
                Results = authResponse
            });
        }

        private AuthResponseDTO GenerateToken(User user)
        {
            var tokenHanlder = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_options.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHanlder.CreateToken(tokenDescriptor);
            return new AuthResponseDTO
            {
                UserName = user.Username,
                Token = tokenHanlder.WriteToken(token)
            };
        }
    }
}
