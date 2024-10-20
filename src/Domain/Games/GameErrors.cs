using SharedKernel;

namespace Domain.Choices;
public static class GameErrors
{
    public static Error PlayerMoveNull() => Error.Problem(
        "Game.Null",
        $"Player move is out of range or null");

    public static Error ComputerMoveNull() => Error.Problem(
       "Game.Null",
       $"Computer move is out of range or null");
}
