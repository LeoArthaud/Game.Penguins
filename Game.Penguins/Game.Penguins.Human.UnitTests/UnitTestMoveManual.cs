using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.CustomGame;
using Game.Penguins.Core.CustomGame.App;
using Game.Penguins.Core.CustomGame.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Human.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestMoveManual
    {
        [TestMethod]
        public void Test_MoveManual_CheckGood()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            var penguinCurrentPlayer = new Penguin(customGame.CurrentPlayer);
            cellOrigin.CurrentPenguin = penguinCurrentPlayer;
            cellOrigin.CellType = CellType.FishWithPenguin;
            
            // Position of cell
            int xDestination = 2;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;

            // Launch function
            customGame.MoveManual(cellOrigin,cellDestination);

            // Tests
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.Water);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].FishCount == 0);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CurrentPenguin.Player == penguinCurrentPlayer.Player);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CellType == CellType.FishWithPenguin);
        }

        [TestMethod]
        public void Test_MoveManual_CheckGoodWithOtherCurrentPlayer()
        {
            CustomGame customGame = InitGame();

            // Change CurrentPlayer
            customGame.CurrentPlayer = customGame.Players[1];
            customGame.IdPlayer = 1;

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            var penguinCurrentPlayer = new Penguin(customGame.CurrentPlayer);
            cellOrigin.CurrentPenguin = penguinCurrentPlayer;
            cellOrigin.CellType = CellType.FishWithPenguin;
            
            // Position of cell
            int xDestination = 2;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;

            // Launch function
            customGame.MoveManual(cellOrigin,cellDestination);

            // Tests
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.Water);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].FishCount == 0);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CurrentPenguin.Player == penguinCurrentPlayer.Player);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CellType == CellType.FishWithPenguin);
        }

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

            customGame.CountPlayers = 2;

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }

        #endregion
    }
}
