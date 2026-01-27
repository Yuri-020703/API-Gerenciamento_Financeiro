using Gerenciamento_Financeiro.Models;

namespace Gerenciamento_Financeiro.Repository_Pattern
{
    public interface IUsuariosRepository
    {
        Task <Usuarios>ObterPorId(int usuarioId);
        Task CriarUsuario(Usuarios usuario);
        Task<Usuarios> ObterPorNomeUsuario(string nome);
        Task<bool> NomeUsuarioExiste(string nomeUsuario);
        Task RemoverUsuario(int usuarioId);
        Task <List<Usuarios>> ObterTodos();
    }
}
