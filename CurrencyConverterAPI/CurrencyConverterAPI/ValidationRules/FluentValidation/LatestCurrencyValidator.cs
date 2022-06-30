using CurrencyConverterAPI.Models;
using FluentValidation;

namespace CurrencyConverterAPI.ValidationRules.FluentValidation
{
    public class LatestCurrencyValidator:AbstractValidator<LatestCurrencyRate>
    {
        public LatestCurrencyValidator()
        {
            RuleFor(c => c.BaseCurrency).NotEmpty().Length(3);
            RuleFor(c => c.Currencies).Matches("^[a-zA-Z]+(,[a-zA-Z]+)*$");
        }
    }
}
