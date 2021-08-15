using FluentValidation;
using FruteiraApi.Core.Domain.Models;

namespace FruteiraApi.Core.Validations
{
    public class FrutaSelfValidation : AbstractValidator<Fruta>
    {
        public FrutaSelfValidation()
        {
            RuleFor(r => r.Nome)
                .Length(2, 60).WithMessage("O atributo [Nome] deve ter entre 2 e 60 caracteres.");

            RuleFor(r => r.Descricao)
                .Length(2, 60).WithMessage("O atributo [Descricao] deve ter entre 2 e 60 caracteres.");

            RuleFor(r => r.QuantidadeEstoque)
                .GreaterThanOrEqualTo(0).WithMessage("O atributo [QuantidadeEstoque] deve ser igual ou maior que zero.");

            RuleFor(r => r.Valor)
                .GreaterThan(0).WithMessage("O atributo [Valor] deve ser maior que zero.");
        }
    }
}
