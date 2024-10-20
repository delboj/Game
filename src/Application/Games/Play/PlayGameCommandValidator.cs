using FluentValidation;

namespace Application.Games.Play;
public sealed class PlayGameCommandValidator : AbstractValidator<PlayGameCommand>
{
    public PlayGameCommandValidator()
    {
        RuleFor(c => c.Player)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(5)
            .WithMessage("Choice must be between 1 and 5")
            .WithErrorCode("400");
    }
}
