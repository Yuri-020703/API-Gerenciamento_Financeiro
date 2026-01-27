using Gerenciamento_Financeiro.Models;

namespace Gerenciamento_Financeiro.Repository_Pattern
{
    public interface ILancamentosRepository
    {
        Task<List<Lancamentos>> ObterLancamentos(int usuarioId);
        Task<Lancamentos> ObterPorId(int id, int usuarioId);
        Task<Lancamentos> Adicionar(Lancamentos lancamento, int usuarioId);
        Task<Lancamentos> Atualizar(int id, int usuarioId, Lancamentos lancamentoAtualizado);
        Task<bool> Remover(int id,int usuarioId);
    }
}
