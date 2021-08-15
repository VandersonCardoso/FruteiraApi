using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruteiraApi.Data.Repositories
{
    [Table("Frutas")]
    public class Frutas
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        public byte[] Foto { get; set; }
        [Required]
        public int QuantidadeEstoque { get; set; }
        [Required]
        public decimal Valor { get; set; }
    }
}
