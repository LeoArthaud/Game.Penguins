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
            // Init Game
            CustomGame customGame = InitGame();

            // Position of origin cell
            int xOrigin = 0;
            int yOrigin = 0;

            // Set Origin cell
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            var penguinCurrentPlayer = new Penguin(customGame.CurrentPlayer);
            cellOrigin.CurrentPenguin = penguinCurrentPlayer;
            cellOrigin.CellType = CellType.FishWithPenguin;
            
            // Position of destination cell
            int xDestination = 2;
            int yDestination = 0;

            // Set destination cell
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
        /// Test if the function MoveManual place the penguin at a bad destination
        /// </summary>
        [TestMethod]
        public void Test_MoveManual_BadCellTypeOrigin_Fish()
        {
            //Init Game
            CustomGame customGame = InitGame();

            // Position of origin cell
            int xOrigin = 0;
            int yOrigin = 0;

            // Set origin cell
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            cellOrigin.CurrentPenguin = null;
            cellOrigin.CellType = CellType.Fish;

            // Coord cell destination
            int xDestination = 1;
            int yDestination = 0;

            // Set destination cell
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;
            cellDestination.FishCount = 3;

            // Launch function
            customGame.MoveManual(cellOrigin, cellDestination);

            // Tests
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.Fish);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].FishCount == 1);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CellType == CellType.Fish);
        }

        /// <summary>
        /// Test if player select water cell origin
        /// </summary>
        [TestMethod]
        public void Test_MoveManual_BadCellTypeOrigin_Water()
        {
            //Init Game
            CustomGame customGame = InitGame();

            // Position of origin cell
            int xOrigin = 0;
            int yOrigin = 0;

            // Set origin cell
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            cellOrigin.CurrentPenguin = null;
            cellOrigin.CellType = CellType.Water;

            // Position of destination cell
            int xDestination = 1;
            int yDestination = 0;

            // Set destination cell
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;
            cellDestination.FishCount = 3;

            // Launch function
            customGame.MoveManual(cellOrigin, cellDestination);

            //Tests
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.Water);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].FishCount == 1);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CellType == CellType.Fish);
        }

        /// <summary>
        /// Test if player choose water destination
        /// </summary>
        [TestMethod]
        public void Test_MoveManual_BadCellTypeDestination_Water()
        {
            // Init game
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            // Set origin
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            var penguinCurrentPlayer = new Penguin(customGame.CurrentPlayer);
            cellOrigin.CurrentPenguin = penguinCurrentPlayer;
            cellOrigin.CellType = CellType.FishWithPenguin;

            // Position Destination cell
            int xDestination = 1;
            int yDestination = 0;

            // Set Destination
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Water;
            cellDestination.FishCount = 3;

            //Launche function
            customGame.MoveManual(cellOrigin, cellDestination);

            // Tests
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.FishWithPenguin);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CurrentPenguin == penguinCurrentPlayer);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].FishCount == 1);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CellType == CellType.Water);
        }

        /// <summary>
        /// Test if player select choose cell with penguin
        /// </summary>
        [TestMethod]
        public void Test_MoveManual_BadCellTypeDestination_Penguin()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            // Set origin cell
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            var penguinCurrentPlayer = new Penguin(customGame.CurrentPlayer);
            cellOrigin.CurrentPenguin = penguinCurrentPlayer;
            cellOrigin.CellType = CellType.FishWithPenguin;

            // Position destination cell
            int xDestination = 1;
            int yDestination = 0;

            // Set Destination cell
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.FishWithPenguin;
            cellDestination.FishCount = 3;

            // Launche function
            customGame.MoveManual(cellOrigin, cellDestination);

            // Test
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.FishWithPenguin);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CurrentPenguin == penguinCurrentPlayer);
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].FishCount == 1);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CurrentPenguin == null);
            Assert.IsTrue(customGame.Board.Board[xDestination, yDestination].CellType == CellType.FishWithPenguin);
        }

        /// <summary>
        /// Test the suppression penguin
        /// </summary>
        [TestMethod]
        public void Test_MoveManual_SupprPenguin()
        {
            // Init game
            CustomGame customGame = InitGame();

            // Position of origin cell
            int xOrigin = 0;
            int yOrigin = 0;

            // Set origin cell
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            var penguinCurrentPlayer = new Penguin(customGame.CurrentPlayer);
            cellOrigin.CurrentPenguin = penguinCurrentPlayer;
            cellOrigin.CellType = CellType.FishWithPenguin;

            // Set water cells around origin
            Cell cell1 = (Cell)customGame.Board.Board[0,1];
            cell1.CellType = CellType.Water;

            Cell cell2 = (Cell)customGame.Board.Board[1,0];
            cell2.CellType = CellType.Water;

            // Create a seconde penguin
            Cell cell2ndOri = (Cell)customGame.Board.Board[4, 0];
            cell2ndOri.CurrentPenguin = penguinCurrentPlayer;
            cell2ndOri.CellType = CellType.FishWithPenguin;

            // Set destination for the seconde penguin
            Cell cell2ndDesti = (Cell)customGame.Board.Board[5, 0];
            cell2ndDesti.CellType = CellType.Fish;

            // Simulate movement to activate the suppression
            customGame.MoveManual(cell2ndOri, cell2ndDesti);

            // Test
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
