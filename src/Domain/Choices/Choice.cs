using SharedKernel;

namespace Domain.Choices;
public sealed class Choice : Entity
{
    public int ChoiceId { get; set; }
    public Move Move { get; set; }
}
