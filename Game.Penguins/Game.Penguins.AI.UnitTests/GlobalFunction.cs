using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Moq;

namespace Game.Penguins.AI.UnitTests
{
    [ExcludeFromCodeCoverage]
    public abstract class GlobalFunction
    {
        /// <summary>
        /// Init the game with 3 players (AIEasy, AIMedium, AIHard)
        /// </summary>
        /// <param name="randomMock">mock for the random</param>
        /// <returns>the game with modifications</returns>
        public CustomGame InitGame(Mock<IRandom> randomMock)
        {
            CustomGame customGame = randomMock == null ? new CustomGame(new AppRandom()) : new CustomGame(randomMock.Object);

            // Add 3 players
            customGame.AddPlayer("Easy", PlayerType.AIEasy);
            customGame.AddPlayer("Medium", PlayerType.AIMedium);
            customGame.AddPlayer("Hard", PlayerType.AIHard);

            // Launch function
            customGame.StartGame();

            // Set current player
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }
    }
}
