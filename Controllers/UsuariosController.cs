using Gerenciamento_Financeiro.Models;
using Gerenciamento_Financeiro.Repository_Pattern;
using Gerenciamento_Financeiro.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciamento_Financeiro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // JWT
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var usuario = await _usuariosRepository.ObterPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> ObterPorNomeDeUsuario(string nome)
        {
            var usuario = await _usuariosRepository.ObterPorNomeUsuario(nome);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            int usuarioId = UserContext.GetUsuarioId(User);
            var usuario = await _usuariosRepository.ObterPorId(usuarioId);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CriarUsuario([FromBody] Usuarios usuario)
        {
            await _usuariosRepository.CriarUsuario(usuario);
            return Ok(usuario);
        }

        [HttpDelete("{usuarioId}")]
        public async Task<IActionResult> RemoverUsuario(int usuarioId)
        {
            await _usuariosRepository.RemoverUsuario(usuarioId);
            return NoContent();
        }

        [HttpGet("NomeExiste/{nome}")]
        public async Task<IActionResult> NomeUsuarioExiste(string nome)
        {
            var existe = await _usuariosRepository.NomeUsuarioExiste(nome);
            return Ok(existe);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var lista = await _usuariosRepository.ObterTodos();
            return Ok(lista);
        }
    }
}
