using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_Financeiro.Data
{
    public class Login
    {
        [Required] public string NomeUsuario { get; set; }

        [Required] public string Senha       { get; set; }
    }
}
