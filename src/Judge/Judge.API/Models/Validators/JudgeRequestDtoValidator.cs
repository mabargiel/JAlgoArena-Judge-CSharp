using FluentValidation;

namespace Judge.API.Models.Validators
{
    public class JudgeRequestDtoValidator : AbstractValidator<JudgeRequestDto>
    {
        protected JudgeRequestDtoValidator()
        {
            RuleFor(dto => dto).NotNull();
            RuleFor(dto => dto.Language).Equal("c#");
            RuleFor(dto => dto.SourceCode)
                .NotEmpty()
                .NotNull();
            RuleFor(dto => dto.UserId)
                .NotEmpty()
                .NotNull();
        }
    }
}