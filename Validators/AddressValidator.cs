using ETicaret.Models;
using FluentValidation;

namespace ETicaret.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(c => c.Title).NotEmpty().WithMessage("Başlık boş olamaz!");
            RuleFor(c => c.Name).NotEmpty().WithMessage("İsim boş olamaz!");
            RuleFor(c => c.Surname).NotEmpty().WithMessage("Soyisim boş olamaz!");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email adresi boş bırakılamaz!").EmailAddress().WithMessage("Lütfen geçerli bir Email adresi giriniz.");
            RuleFor(c => c.Phone).NotEmpty().WithMessage("Telefon numarası boş bırakılamaz!").Length(11, 11).WithMessage("Telefon numarası 11 haneli olmalıdır!"); ;
            RuleFor(c => c.FullAddress).NotEmpty().WithMessage("Adres boş bırakılamaz!").Length(15, 100).WithMessage("Adres 15 ile 100 karakter arasında olmalıdır!");
            RuleFor(c => c.Country).NotEmpty().WithMessage("Ülke boş bırakılamaz!");
            RuleFor(c => c.City).NotEmpty().WithMessage("İl boş bırakılamaz!");
            RuleFor(c => c.Appartment).NotEmpty().WithMessage("Apartman boş bırakılamaz!");
            RuleFor(c => c.District).NotEmpty().WithMessage("İlçe boş bırakılamaz!");
            RuleFor(c => c.PostalCode).NotEmpty().WithMessage("Posta kodu boş bırakılamaz!").Length(5,5).WithMessage("Posta kodu 5 haneli olmalıdır!");
        }
    }
}