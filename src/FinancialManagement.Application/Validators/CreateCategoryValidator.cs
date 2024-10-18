namespace FinancialManagement.Application.Validators;

public class CreateCategoryValidator : AbstractValidator<CategoryPostDTO>
{
    public CreateCategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(c => c.Name)
            .Length(3, 100)
            .WithMessage("Name must be between 3 and 100 characters");
    }
}
