using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Classes.Move;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Game.UnitTests.Move
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestCanMove
    {
        #region Public Functions

        /// <summary>
        /// Test in the function CanMove(), if all the possibilities are tests
        /// </summary>
        [TestMethod]
        public void Test_CanMove()
        {
            // Init Game
            CustomGame customGame = InitGame();

            // launch move function
            Movements move = new Movements(null, null, customGame.Board);
            var result = move.CanMove(new Coordinates(4, 4));

            // Tests
            Assert.IsFalse(result[DirectionType.Right]);
            Assert.IsFalse(result[DirectionType.Left]);
            Assert.IsFalse(result[DirectionType.DownRight]);
            Assert.IsFalse(result[DirectionType.DownLeft]);
            Assert.IsFalse(result[DirectionType.UpRight]);
            Assert.IsFalse(result[DirectionType.UpLeft]);


            result = move.CanMove(new Coordinates(3, 5));

            // Tests
            Assert.IsFalse(result[DirectionType.Right]);
            Assert.IsFalse(result[DirectionType.Left]);
            Assert.IsFalse(result[DirectionType.DownRight]);
            Assert.IsFalse(result[DirectionType.DownLeft]);
            Assert.IsFalse(result[DirectionType.UpRight]);
            Assert.IsFalse(result[DirectionType.UpLeft]);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Init the game
        /// </summary>
        /// <returns>game</returns>
        private CustomGame InitGame()
        {
            // Init game
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 2 players
            Player player1 = new Player("Player1", PlayerType.Human);
            Player player2 = new Player("Player2", PlayerType.Human);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }

        #endregion
    }
}
