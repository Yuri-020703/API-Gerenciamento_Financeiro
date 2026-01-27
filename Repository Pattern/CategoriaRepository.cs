using Gerenciamento_Financeiro.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_Financeiro.Repository_Pattern
{
    public class CategoriaRepository : ICategoriaRepository
    {

    private readonly AppDbContext _context;

            public CategoriaRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<Categorias> Adicionar(Categorias categoria, int usuarioId)
            {
                categoria.UsuarioId = usuarioId;

                _context.TCategorias.Add(categoria);
                await _context.SaveChangesAsync();
                
                return categoria;
            }

            public async Task<bool> Atualizar(int id, string categoriaAtualizada, int usuarioId)
            {
                var categoria = await _context.TCategorias.FirstOrDefaultAsync(
                    x => x.Id == id && x.UsuarioId == usuarioId);

                if (categoria == null) { return false; }

                categoria.Nome = categoriaAtualizada;

                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<Categorias?> ObterPorId(int id, int usuarioId)
            {
                var categoria = _context.TCategorias.FirstOrDefaultAsync(
                    x => x.Id == id && x.UsuarioId == usuarioId);

                return await categoria;
            }

            public async Task<List<Categorias>> ObterTodas(int usuarioId)
            {
                return await _context.TCategorias.Where(x => x.UsuarioId == usuarioId)
                .ToListAsync();
            }

            public async Task<bool> Remover(int id, int usuarioId)
            {
                var categoria = await _context.TCategorias.FirstOrDefaultAsync(
                    x => x.Id == id && x.UsuarioId == usuarioId);

                if (categoria == null) { return false; };

                _context.TCategorias.Remove(categoria);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
