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
    public class LancamentosController : ControllerBase
    {
        private readonly ILancamentosRepository _lancamentosRepository;

        public LancamentosController(ILancamentosRepository lancamentosRepository)
        {
            _lancamentosRepository = lancamentosRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterLancamentos()
        {
            int usuarioId = UserContext.GetUsuarioId(User);
            var lancamentos = await _lancamentosRepository.ObterLancamentos(usuarioId);
            return Ok(lancamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            int usuarioId = UserContext.GetUsuarioId(User);
            var lancamento = await _lancamentosRepository.ObterPorId(id, usuarioId);
            return Ok(lancamento);
        }

        [HttpPost]
        public async Task<Lancamentos> Adicionar(Lancamentos lancamento)
        {
            int usuarioId = UserContext.GetUsuarioId(User);
            return await _lancamentosRepository.Adicionar(lancamento, usuarioId);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Lancamentos lancamentoAtualizado)
        {
            int usuarioId = UserContext.GetUsuarioId(User);
            var _lancamentoAtualizado =
                await _lancamentosRepository.Atualizar(id, usuarioId, lancamentoAtualizado);

            return Ok(_lancamentoAtualizado);
        }

        [HttpDelete]
        public async Task<IActionResult> Remover(int id)
        {
            int usuarioId = UserContext.GetUsuarioId(User);
            var result = await _lancamentosRepository.Remover(id, usuarioId);

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
