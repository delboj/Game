using SharedKernel;

namespace Domain.Choices;
public static class ChoiceErrors
{
    public static Error NotFound(int id) => Error.NotFound(
        "Choice.NotFound",
        $"ChoiceId with {id} is not found");
}
