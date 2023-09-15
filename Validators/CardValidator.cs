using ETicaret.Models;
using FluentValidation;

namespace ETicaret.Validators
{
    public class CardValidator : AbstractValidator<Card>
    {
        public CardValidator() 
        {
            RuleFor(c => c.Number).NotEmpty().WithMessage("test1").Length(16, 16).WithMessage("test2");
            RuleFor(c => c.Owner).NotEmpty().WithMessage("Kart Sahibi boş olamaz!");
            RuleFor(c => c.SecurityCode).InclusiveBetween(100,999).WithMessage("CVV 3 haneli olmalıdır.");
        }
    }
}
