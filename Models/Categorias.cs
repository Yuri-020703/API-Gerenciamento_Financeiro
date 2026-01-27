using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_Financeiro.Models
{
    public class Categorias
    {
        [Key]       public int Id { get; set; }       
        [Required]  public string Nome { get; set; }
        [Required]  public int UsuarioId { get; set; }

    }
}
