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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriasRepository;

        public CategoriasController(ICategoriaRepository categoriasRepository)
        {
            _categoriasRepository = categoriasRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodas()
        {
            int usuarioId = UserContext.GetUsuarioId(User);
            var lista = await _categoriasRepository.ObterTodas(usuarioId);
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            int usuarioId = UserContext.GetUsuarioId(User);
            var categoria = await _categoriasRepository.ObterPorId(id, usuarioId);
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Categorias categoria)
        {
            int usuarioId = UserContext.GetUsuarioId(User);
            var categoriaAtuallizada = await _categoriasRepository.Adicionar(categoria, usuarioId);
            return Ok(categoriaAtuallizada);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(int id, string categoriaAtualizada)
        {
            int usuarioId = UserContext.GetUsuarioId(User);
            bool result = await _categoriasRepository.Atualizar(id, categoriaAtualizada, usuarioId);

            if (result == true)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            int usuarioId = UserContext.GetUsuarioId(User);
            bool result = await _categoriasRepository.Remover(id, usuarioId);

            if (result == true)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
