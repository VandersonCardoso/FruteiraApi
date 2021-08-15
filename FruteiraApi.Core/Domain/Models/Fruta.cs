using FruteiraApi.Core.Validations;
using System.ComponentModel.DataAnnotations;

namespace FruteiraApi.Core.Domain.Models
{
    public class Fruta
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public byte[] Foto { get; set; }
        public int? QuantidadeEstoque { get; set; }
        public decimal? Valor { get; set; }

        public bool IsValid()
        {
            var ValidationResult = new FrutaSelfValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
