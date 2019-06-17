using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Human.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestMoveManual : GlobalFunction
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

            // Position cell destination
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

            //Launch function
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

            // Launch function
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
        public void Test_MoveManual_DeletePenguin()
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

            // Create a second penguin
            Cell cellOrigin2 = (Cell)customGame.Board.Board[4, 0];
            cellOrigin2.CurrentPenguin = penguinCurrentPlayer;
            cellOrigin2.CellType = CellType.FishWithPenguin;

            // Set destination for the second penguin
            Cell cellDestination2 = (Cell)customGame.Board.Board[5, 0];
            cellDestination2.CellType = CellType.Fish;

            // Simulate movement to activate the suppression
            customGame.MoveManual(cellOrigin2, cellDestination2);

            // Test
            Assert.IsTrue(customGame.Board.Board[xOrigin, yOrigin].CellType == CellType.Water);
        }

    }
}
