using Gerenciamento_Financeiro.Data;
using Gerenciamento_Financeiro.Models;
using Gerenciamento_Financeiro.Repository_Pattern;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Gerenciamento_Financeiro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuariosRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IUsuariosRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login login)
        {
            var usuario = await _repo.ObterPorNomeUsuario(login.NomeUsuario);
            if (usuario == null || usuario.Senha != login.Senha)
                return Unauthorized("Usuário ou senha inválidos");

            var token = GerarJwt(usuario);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registrar(Usuarios usuario)
        {
            if (await _repo.NomeUsuarioExiste(usuario.NomeUsuario))
                return Conflict("Nome de usuário já existe");

            await _repo.CriarUsuario(usuario);
            return Created("", null);
        }

        private string GerarJwt(Usuarios usuario)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(
                    int.Parse(_config["Jwt:ExpiresInHours"])
                ),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
