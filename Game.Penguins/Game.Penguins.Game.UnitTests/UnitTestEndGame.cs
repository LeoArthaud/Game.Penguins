using System;
using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Game.Penguins.Game.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestEndGame
    {
        /// <summary>
        /// Test EndGame if there is no penguin on the board
        /// </summary>
        [TestMethod]
        public void Test_EndGame_NoPenguinOnBoard()
        {
            // Init game
            CustomGame customGame = InitGame(null);

            // End Game
            customGame.EndGame();

            // Test
            Assert.IsTrue(customGame.NextAction == NextActionType.Nothing);
        }

        /// <summary>
        /// Test EndGame if there is no winner
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_EndGame_WinnerNull()
        {
            // Init game
            CustomGame customGame = new CustomGame(new AppRandom());

            // End Game
            customGame.EndGame();
        }

        /// <summary>
        /// Test EndGame if there is always penguin on the board
        /// </summary>
        [TestMethod]
        public void Test_EndGame_WithPenguinOnBoard()
        {
            // Init game
            CustomGame customGame = InitGame(null);

            // Position of cell with penguin 0;0
            int x = 0;
            int y = 0;
            // Set cell
            Cell cellOrigin = (Cell)customGame.Board.Board[x, y];
            cellOrigin.FishCount = 1;
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(customGame.Players[0]);

            // Position of cell water 1;0
            x = 1;
            y = 0;
            // Set cell
            Cell cell1 = (Cell)customGame.Board.Board[x, y];
            cell1.CellType = CellType.Water;

            // Position of cell water 0;1
            x = 0;
            y = 1;
            // Set cell
            Cell cell2 = (Cell)customGame.Board.Board[x, y];
            cell2.CellType = CellType.Water;

            // End Game
            customGame.EndGame();

            // Test
            Assert.IsTrue(customGame.NextAction == NextActionType.MovePenguin);
        }

        #region Private Functions

        /// <summary>
        /// Init the game with 2 players AIEasy
        /// </summary>
        /// <param name="randomMock">mock for the random</param>
        /// <returns>the game with modifications</returns>
        public CustomGame InitGame(Mock<IRandom> randomMock)
        {
            CustomGame customGame = randomMock == null ? new CustomGame(new AppRandom()) : new CustomGame(randomMock.Object);

            // Add 2 players
            customGame.AddPlayer("Player1", PlayerType.AIEasy);
            // Set point
            Player player = (Player)customGame.Players[0];
            player.Points = 10;
            customGame.AddPlayer("Player2", PlayerType.AIEasy);
            // Set point
            player = (Player)customGame.Players[1];
            player.Points = 5;

            // Launch function
            customGame.StartGame();

            // Set current player
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            // Set Next Action
            customGame.NextAction = NextActionType.MovePenguin;

            return customGame;
        }

        #endregion
    }
}
