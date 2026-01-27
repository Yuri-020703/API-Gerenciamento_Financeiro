using Gerenciamento_Financeiro.Models;

namespace Gerenciamento_Financeiro.Repository_Pattern
{
    public interface ICategoriaRepository
    {
        Task<List<Categorias>> ObterTodas(int usuarioId);
        Task<Categorias> ObterPorId(int id,int usuarioId);
        Task<Categorias> Adicionar(Categorias categoria, int usuarioId);
        Task<bool> Atualizar(int id,string categoriaAtualizada,int usuarioId);
        Task<bool> Remover(int id, int usuarioId);
    }
}
