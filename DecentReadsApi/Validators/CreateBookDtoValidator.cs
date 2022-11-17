using FluentValidation;
using DecentReadsApi.Entities;
using DecentReadsApi.Models;

namespace DecentReadsApi.Validators
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        private readonly DecentReadsDbContext dbContext;
        public CreateBookDtoValidator(DecentReadsDbContext dbContext)
        {
            RuleFor(x => x.Name).MinimumLength(1).MaximumLength(50);
            RuleFor(x => x.AuthorLastName).NotNull();
            RuleFor(x => x.AuthorFirstName).NotNull();
            RuleFor(x => x.Description).NotNull().MaximumLength(250);
        }
    }
}
