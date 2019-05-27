using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Human.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestMoveManual
    {
        /// <summary>
        /// Test if the function MoveManual place the penguin at the new destination
        /// </summary>
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
        
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_MoveManual_BadCellTypeOrigin_Fish()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            cellOrigin.CurrentPenguin = null;
            cellOrigin.CellType = CellType.Fish;

            int xDestination = 1;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;
            cellDestination.FishCount = 3;

            customGame.MoveManual(cellOrigin, cellDestination);

            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.Fish);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].FishCount == 1);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CellType == CellType.Fish);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_MoveManual_BadCellTypeOrigin_Water()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            cellOrigin.CurrentPenguin = null;
            cellOrigin.CellType = CellType.Water;

            int xDestination = 1;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;
            cellDestination.FishCount = 3;

            customGame.MoveManual(cellOrigin, cellDestination);

            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.Water);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].FishCount == 1);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CellType == CellType.Fish);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_MoveManual_BadCellTypeDestination_Water()
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

            int xDestination = 1;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Water;
            cellDestination.FishCount = 3;

            customGame.MoveManual(cellOrigin, cellDestination);

            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.FishWithPenguin);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CurrentPenguin == penguinCurrentPlayer);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].FishCount == 1);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CellType == CellType.Water);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_MoveManual_BadCellTypeDestination_Penguin()
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

            int xDestination = 1;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.FishWithPenguin;
            cellDestination.FishCount = 3;

            customGame.MoveManual(cellOrigin, cellDestination);

            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.FishWithPenguin);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CurrentPenguin == penguinCurrentPlayer);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].FishCount == 1);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CellType == CellType.FishWithPenguin);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_MoveManual_SupprPenguin()
        {
            CustomGame customGame = InitGame();

            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            var penguinCurrentPlayer = new Penguin(customGame.CurrentPlayer);
            cellOrigin.CurrentPenguin = penguinCurrentPlayer;
            cellOrigin.CellType = CellType.FishWithPenguin;

            Cell cell1 = (Cell)customGame.Board.Board[0,1];
            cell1.CellType = CellType.Water;

            Cell cell2 = (Cell)customGame.Board.Board[1,0];
            cell2.CellType = CellType.Water;

            Cell cellOri = (Cell)customGame.Board.Board[4, 0];
            cellOri.CurrentPenguin = penguinCurrentPlayer;
            cellOri.CellType = CellType.FishWithPenguin;

            Cell cellDesti = (Cell)customGame.Board.Board[5, 0];
            cellDesti.CellType = CellType.Fish;
       
            customGame.MoveManual(cellOri, cellDesti);

            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.Water);
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

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }

        #endregion
    }
}
