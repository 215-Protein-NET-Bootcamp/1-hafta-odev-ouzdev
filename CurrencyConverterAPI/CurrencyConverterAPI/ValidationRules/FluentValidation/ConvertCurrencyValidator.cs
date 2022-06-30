using CurrencyConverterAPI.Models;
using FluentValidation;

namespace CurrencyConverterAPI.ValidationRules.FluentValidation
{
    public class ConvertCurrencyValidator:AbstractValidator<GetConvertCurrencyQueryObject>
    {
        public ConvertCurrencyValidator()
        {
            RuleFor(c => c.Amount).NotEmpty().Matches(@"^-?[0-9\.]+$");
            RuleFor(c => c.From).NotEmpty().Length(3).WithMessage("Kur Formatı Hatalı.");
            RuleFor(c=> c.To).NotEmpty().Length(3).WithMessage("Kur Formatı Hatalı.");
        }
    }
}
