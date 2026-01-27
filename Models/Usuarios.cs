using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_Financeiro.Models
{
    public class Usuarios
    {
        [Key]       public int Id { get; set; }
        [Required]  public string Nome { get; set; }
        [Required]  public string NomeUsuario { get; set; }
        [Required]  public string Senha { get; set; }
    }
}
