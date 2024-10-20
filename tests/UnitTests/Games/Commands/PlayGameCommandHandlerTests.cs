using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Application.Games.Play;
using Domain.Choices;
using Domain.Games;
using Moq;

namespace Application.Games.Commands;
public class PlayGameCommandHandlerTests
{
    private readonly Mock<IRandomNumberService> randomNumberClientMock;
    private readonly Mock<IChoiceRepository> choiceRepositoryMock;
    private readonly Mock<IGameRepository> gameRepositoryMock;
    private readonly Mock<IUnitOfWork> unitOfWorkMock;
    private readonly PlayGameCommandHandler handler;

    public PlayGameCommandHandlerTests()
    {
        randomNumberClientMock = new Mock<IRandomNumberService>();
        choiceRepositoryMock = new Mock<IChoiceRepository>();
        gameRepositoryMock = new Mock<IGameRepository>();
        unitOfWorkMock = new Mock<IUnitOfWork>();

        handler = new PlayGameCommandHandler(
            gameRepositoryMock.Object,
            choiceRepositoryMock.Object,
            unitOfWorkMock.Object,
            randomNumberClientMock.Object
        );
    }

    [Fact]
    public async Task Handle_PlayerMoveIsNull_ReturnsFailure()
    {
        // Arrange
        var request = new PlayGameCommand (1);
        randomNumberClientMock.Setup(r => r.GetRandomNumberAsync()).ReturnsAsync(new RandomNumberResponse { RandomNumber = 2 });
        choiceRepositoryMock.Setup(c => c.GetByChoiceId(1, It.IsAny<CancellationToken>())).ReturnsAsync((Choice)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(GameErrors.PlayerMoveNull(), result.Error);
    }

    [Fact]
    public async Task Handle_ComputerMoveIsNull_ReturnsFailure()
    {
        // Arrange
        var request = new PlayGameCommand (1);
        randomNumberClientMock.Setup(r => r.GetRandomNumberAsync()).ReturnsAsync(new RandomNumberResponse { RandomNumber = 2 });
        choiceRepositoryMock.Setup(c => c.GetByChoiceId(1, It.IsAny<CancellationToken>())).ReturnsAsync(new Choice { ChoiceId = 1 });
        choiceRepositoryMock.Setup(c => c.GetByChoiceId(2, It.IsAny<CancellationToken>())).ReturnsAsync((Choice)null);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(GameErrors.ComputerMoveNull(), result.Error);
    }

    [Fact]
    public async Task Handle_BothMovesAreValid_ReturnsPlayGameCommandResponse()
    {
        // Arrange
        var request = new PlayGameCommand(1);
        var playerChoice = new Choice { ChoiceId = 1, Move = Move.Rock };
        var computerChoice = new Choice { ChoiceId = 2, Move = Move.Paper };
        var randomNumberResponse = new RandomNumberResponse { RandomNumber = 2 };
        var playedGame = Game.Play(playerChoice, computerChoice);

        randomNumberClientMock.Setup(r => r.GetRandomNumberAsync()).ReturnsAsync(randomNumberResponse);
        choiceRepositoryMock.Setup(c => c.GetByChoiceId(1, It.IsAny<CancellationToken>())).ReturnsAsync(playerChoice);
        choiceRepositoryMock.Setup(c => c.GetByChoiceId(2, It.IsAny<CancellationToken>())).ReturnsAsync(computerChoice);
        gameRepositoryMock.Setup(g => g.AddAsync(playedGame)).Returns(Task.CompletedTask);
        unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(playedGame.PlayerChoice.ChoiceId, result.Value.Player);
        Assert.Equal(playedGame.ComputerChoice.ChoiceId, result.Value.Computer);
        Assert.Equal(playedGame.GameResult.ToString(), result.Value.Result);
    }

    [Fact]
    public async Task Handle_SaveChangesFails_ThrowsException()
    {
        // Arrange
        var request = new PlayGameCommand(1);
        var playerChoice = new Choice { ChoiceId = 1, Move = Move.Rock };
        var computerChoice = new Choice { ChoiceId = 2, Move = Move.Paper };
        var randomNumberResponse = new RandomNumberResponse { RandomNumber = 2 };
        var playedGame = Game.Play(playerChoice, computerChoice);

        randomNumberClientMock.Setup(r => r.GetRandomNumberAsync()).ReturnsAsync(randomNumberResponse);
        choiceRepositoryMock.Setup(c => c.GetByChoiceId(1, It.IsAny<CancellationToken>())).ReturnsAsync(playerChoice);
        choiceRepositoryMock.Setup(c => c.GetByChoiceId(2, It.IsAny<CancellationToken>())).ReturnsAsync(computerChoice);
        gameRepositoryMock.Setup(g => g.AddAsync(playedGame)).Returns(Task.CompletedTask);
        unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new System.Exception("Database error"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<System.Exception>(() => handler.Handle(request, CancellationToken.None));
        Assert.Equal("Database error", exception.Message);
    }
}
