using Gerenciamento_Financeiro.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gerenciamento_Financeiro.Repository_Pattern
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly AppDbContext _context;
        public UsuariosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuarios?> ObterPorId(int usuarioId)
        {
            return await _context.TUsuarios.FirstOrDefaultAsync(x => x.Id == usuarioId);
        }
        public async Task CriarUsuario(Usuarios usuario)
        {
            _context.TUsuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> NomeUsuarioExiste(string nomeUsuario)
        {
            return await _context.TUsuarios.AnyAsync(u => u.NomeUsuario == nomeUsuario);
        }


        public async Task<Usuarios?> ObterPorNomeUsuario(string nome)
        {
            return await _context.TUsuarios.FirstOrDefaultAsync(x => x.NomeUsuario == nome);
        }

        public async Task RemoverUsuario(int usuarioId)
        {
            var usuario = await _context.TUsuarios
                .FirstOrDefaultAsync(x => x.Id == usuarioId);

            if (usuario != null)
            {
                _context.TUsuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<Usuarios>> ObterTodos()
        {
            var lista = await _context.TUsuarios.ToListAsync();
            return lista;
        }
    }
}
 

 
