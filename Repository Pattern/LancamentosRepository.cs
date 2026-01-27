using Gerenciamento_Financeiro.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Globalization;

namespace Gerenciamento_Financeiro.Repository_Pattern
{
    public class LancamentosRepository : ILancamentosRepository
    {
        private readonly AppDbContext _context;

        public LancamentosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Lancamentos> Adicionar(Lancamentos lancamento, int usuarioId)
        {
            lancamento.UsuarioId = usuarioId;
            _context.TLancamentos.Add(lancamento);
            await _context.SaveChangesAsync();

            return lancamento;
        }

        public async Task<Lancamentos> Atualizar(int id, int usuarioId, Lancamentos lancamentoAtualizado)
        {
            var lancamento = await _context.TLancamentos.FirstOrDefaultAsync(x => x.Id == id && x.UsuarioId == usuarioId);

            if (lancamento == null)
                throw new ("Lançamento não encontrado.");
            else
            {
                lancamento.Valor = lancamentoAtualizado.Valor;
                lancamento.ValorGlobal = lancamento.Valor.ToString("C", CultureInfo.InvariantCulture);
                lancamento.Tipo = lancamentoAtualizado.Tipo;
                lancamento.Data = lancamentoAtualizado.Data;
                lancamento.CategoriaId = lancamentoAtualizado.CategoriaId;

                await _context.SaveChangesAsync();
                return lancamentoAtualizado;
            }

        }

        public async Task<List<Lancamentos>> ObterLancamentos(int usuarioId)
        {
            var lista = await _context.TLancamentos.Where(x => x.UsuarioId == usuarioId).ToListAsync();
            return lista;
        }

        public async Task<Lancamentos> ObterPorId(int id, int usuarioId)
        {
            return await _context.TLancamentos.FirstOrDefaultAsync(x => x.Id == id && x.UsuarioId == usuarioId);
        }

        public async Task<bool> Remover(int id, int usuarioId)
        {
            var lancamento = await _context.TLancamentos.FirstOrDefaultAsync(
                    x => x.Id == id && x.UsuarioId == usuarioId);

            if (lancamento == null) { return false; }
            else
            {
                _context.TLancamentos.Remove(lancamento);
                await _context.SaveChangesAsync();
                return true;
            }

                
        }
    }
}
