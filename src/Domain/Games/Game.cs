using Domain.Choices;
using SharedKernel;

namespace Domain.Games;
public class Game : Entity
{
    public Choice PlayerChoice { get; private set; }
    public Choice ComputerChoice { get; private set; }
    public GameResult GameResult { get; private set; }
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Encapsulation over game. Method that automatically play game. This approach used so game state cannot be modified.
    /// </summary>
    /// <param name="playerMove">Player choice</param>
    /// <param name="computerMove">Computer choice</param>
    /// <returns></returns>
    public static Game Play(Choice playerMove, Choice computerMove)
    {
        var game = new Game(playerMove, computerMove);
        return game;
    }

    private Game() { }

    /// <summary>
    /// Crate played Game object
    /// </summary>
    /// <param name="playerMove"></param>
    /// <param name="computerMove"></param>
    private Game(Choice playerMove, Choice computerMove)
    {
        PlayerChoice = playerMove;
        ComputerChoice = computerMove;
        GameResult = GetGameResult(playerMove, computerMove);
        CreatedAt = DateTime.UtcNow;
    }


    /// <summary>
    /// Play game and get result
    /// </summary>
    /// <param name="playerMove">Player choice</param>
    /// <param name="computerMove">Computer choice</param>
    /// <returns></returns>
    private GameResult GetGameResult(Choice playerMove , Choice computerMove)
    {
        if (playerMove.ChoiceId == computerMove.ChoiceId)
        {
            return GameResult.Tie;
        }

        var winningMoves = new Dictionary<Move, List<Move>>()
        {
            { Move.Rock, new List<Move> { Move.Scissors, Move.Lizard } },
            { Move.Paper, new List<Move> { Move.Rock, Move.Spock } },
            { Move.Scissors, new List<Move> { Move.Paper, Move.Lizard } },
            { Move.Lizard, new List<Move> { Move.Paper, Move.Spock } },
            { Move.Spock, new List<Move> { Move.Scissors, Move.Rock } }
        };

        return winningMoves[playerMove.Move].Contains(computerMove.Move) ? GameResult.Win : GameResult.Lose;
    }
}


