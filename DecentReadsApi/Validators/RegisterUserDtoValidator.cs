using FluentValidation;
using DecentReadsApi.Entities;
using DecentReadsApi.Models;

namespace DecentReadsApi.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        private readonly GoodreadsDbContext dbContext;

        public RegisterUserDtoValidator(GoodreadsDbContext dbContext)
        {

            RuleFor(x => x.Username).MinimumLength(3).MaximumLength(25);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(1).MaximumLength(15);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emaliUsed = dbContext.Users.Any(u => u.Email == value);
                    if (emaliUsed)
                    {
                        context.AddFailure("Email", "Email is taken");
                    }
                });

            RuleFor(x => x.Username)
               .Custom((value, context) =>
               {
                   var usernameUsed = dbContext.Users.Any(u => u.Username == value);
                   if (usernameUsed)
                   {
                       context.AddFailure("Username", "Username is taken");
                   }
               });
            this.dbContext = dbContext;
        }
    }
}
