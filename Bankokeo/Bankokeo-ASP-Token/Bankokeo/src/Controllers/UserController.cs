// Essa classe é responsável por gerenciar as requisições de login

using Bankokeo.Models;
using Bankokeo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bankokeo.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    // http://localhost:5197/api/user/login
    public class UserController : ControllerBase
    {
        // Esse controlador depende de um tokenService para gerar o token JWT
        private readonly TokenService _tokenService;

        public UserController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // O método Login é responsável por gerar o token JWT para o usuário logado.
        [HttpPost(template: "login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(token);
        }
    }