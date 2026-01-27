using System.ComponentModel.DataAnnotations;

namespace Gerenciamento_Financeiro.Models
{
    public class Lancamentos
    {
        [Key]      public int        Id        { get; set; }
        [Required] public decimal    Valor { get; set; }
                   public string     ValorGlobal{ get; set; }
        [Required] public string     Tipo      { get; set; }
        [Required] public int        UsuarioId   { get; set; }
        [Required] public int        CategoriaId { get; set; }
        [Required] public DateTime   Data      { get; set; }

    }
}
