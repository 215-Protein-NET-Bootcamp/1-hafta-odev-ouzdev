using CurrencyConverterAPI.Models;
using FluentValidation;

namespace CurrencyConverterAPI.ValidationRules.FluentValidation
{
    public class ConvertCurrencyValidator:AbstractValidator<ConvertCurrencyRate>
    {
        public ConvertCurrencyValidator()
        {
            RuleFor(c => c.Amount).NotEmpty().Matches(@"^-?[0-9][0-9\.]+$");
            RuleFor(c => c.From).NotEmpty().MinimumLength(3).MaximumLength(3).WithMessage("Kur Formatı Hatalı.");
            RuleFor(c=> c.To).NotEmpty().MinimumLength(3).MaximumLength(3).WithMessage("Kur Formatı Hatalı.");
        }
    }
}
