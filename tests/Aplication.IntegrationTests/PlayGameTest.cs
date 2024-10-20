using Application.Games.Play;
using Microsoft.EntityFrameworkCore;

namespace Application.IntegrationTests
{
    public class PlayGameTest : BaseIntegrationTest
    {
        public PlayGameTest(IntegrationTestWebAppFactory integrationTestWebAppFactory) : base(integrationTestWebAppFactory)
        {
        }

        [Fact]
        public async Task PlayGame_ShouldAdd_NewGameToDatabase()
        {
            // Arrange
            var command = new PlayGameCommand(1);

            // Act
            var gameResult = await _sender.Send(command, default);

            // Assert
            var game = await _applicationDbContext.Games.OrderByDescending(x => x.CreatedAt).Include(x => x.ComputerChoice).Include(x => x.PlayerChoice).FirstOrDefaultAsync();
            Assert.True(game.PlayerChoice.ChoiceId == gameResult.Value.Player && game.ComputerChoice.ChoiceId == gameResult.Value.Computer);
        }
    }
}
