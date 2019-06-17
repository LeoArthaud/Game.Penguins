using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Classes.Move;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Game.UnitTests.Move
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestCheckMove : GlobalFunction
    {
        #region Public Functions

        /// <summary>
        /// Test if the function GetCoordinatesRight() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultRight_AllRow()
        {
            // Init Game
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            // Set Origin cell
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination 
            int xDestination = 7;
            int yDestination = 0;

            // Set destination cell
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            //Launch movement
            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            // Tests
            move.GetCoordinatesRight(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 1 && move.Possibilities[0].Y == 0);
            Assert.IsTrue(move.Possibilities[1].X == 2 && move.Possibilities[1].Y == 0);
            Assert.IsTrue(move.Possibilities[2].X == 3 && move.Possibilities[2].Y == 0);
            Assert.IsTrue(move.Possibilities[3].X == 4 && move.Possibilities[3].Y == 0);
            Assert.IsTrue(move.Possibilities[4].X == 5 && move.Possibilities[4].Y == 0);
            Assert.IsTrue(move.Possibilities[5].X == 6 && move.Possibilities[5].Y == 0);
            Assert.IsTrue(move.Possibilities[6].X == 7 && move.Possibilities[6].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesLeft() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultLeft_AllRow()
        {
            CustomGame customGame = InitGame();

             // Position of cell origin
            int xOrigin = 7;
            int yOrigin = 0;

            // Set Origin cell
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination 
            int xDestination = 0;
            int yDestination = 0;

            // Set destination cell
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;
            
            //launch movement
            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            // Tests
            move.GetCoordinatesLeft(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 6 && move.Possibilities[0].Y == 0);
            Assert.IsTrue(move.Possibilities[1].X == 5 && move.Possibilities[1].Y == 0);
            Assert.IsTrue(move.Possibilities[2].X == 4 && move.Possibilities[2].Y == 0);
            Assert.IsTrue(move.Possibilities[3].X == 3 && move.Possibilities[3].Y == 0);
            Assert.IsTrue(move.Possibilities[4].X == 2 && move.Possibilities[4].Y == 0);
            Assert.IsTrue(move.Possibilities[5].X == 1 && move.Possibilities[5].Y == 0);
            Assert.IsTrue(move.Possibilities[6].X == 0 && move.Possibilities[6].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesDownRight() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultDownRight_AllRow()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell
            int xDestination = 3;
            int yDestination = 7;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;


            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            move.GetCoordinatesDownRight(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 0 && move.Possibilities[0].Y == 1);
            Assert.IsTrue(move.Possibilities[1].X == 1 && move.Possibilities[1].Y == 2);
            Assert.IsTrue(move.Possibilities[2].X == 1 && move.Possibilities[2].Y == 3);
            Assert.IsTrue(move.Possibilities[3].X == 2 && move.Possibilities[3].Y == 4);
            Assert.IsTrue(move.Possibilities[4].X == 2 && move.Possibilities[4].Y == 5);
            Assert.IsTrue(move.Possibilities[5].X == 3 && move.Possibilities[5].Y == 6);
            Assert.IsTrue(move.Possibilities[6].X == 3 && move.Possibilities[6].Y == 7);
        }

        /// <summary>
        /// Test if the function GetCoordinatesUpRight() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultUpRight_AllRow()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 7;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination 
            int xDestination = 4;
            int yDestination = 0;

            // Set destination
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Launch move
            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            // Tests
            move.GetCoordinatesUpRight(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 1 && move.Possibilities[0].Y == 6);
            Assert.IsTrue(move.Possibilities[1].X == 1 && move.Possibilities[1].Y == 5);
            Assert.IsTrue(move.Possibilities[2].X == 2 && move.Possibilities[2].Y == 4);
            Assert.IsTrue(move.Possibilities[3].X == 2 && move.Possibilities[3].Y == 3);
            Assert.IsTrue(move.Possibilities[4].X == 3 && move.Possibilities[4].Y == 2);
            Assert.IsTrue(move.Possibilities[5].X == 3 && move.Possibilities[5].Y == 1);
            Assert.IsTrue(move.Possibilities[6].X == 4 && move.Possibilities[6].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesDownLeft() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultDownLeft_AllRow()
        {
            CustomGame customGame = InitGame();

             // Position of cell origin
            int xOrigin = 7;
            int yOrigin = 0;

            // Set Origin 
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination 
            int xDestination = 3;
            int yDestination = 7;

            // Set Destination
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;
            
            // Launch move
            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            // Tests
            move.GetCoordinatesDownLeft(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 6 && move.Possibilities[0].Y == 1);
            Assert.IsTrue(move.Possibilities[1].X == 6 && move.Possibilities[1].Y == 2);
            Assert.IsTrue(move.Possibilities[2].X == 5 && move.Possibilities[2].Y == 3);
            Assert.IsTrue(move.Possibilities[3].X == 5 && move.Possibilities[3].Y == 4);
            Assert.IsTrue(move.Possibilities[4].X == 4 && move.Possibilities[4].Y == 5);
            Assert.IsTrue(move.Possibilities[5].X == 4 && move.Possibilities[5].Y == 6);
            Assert.IsTrue(move.Possibilities[6].X == 3 && move.Possibilities[6].Y == 7);
        }

        /// <summary>
        /// Test if the function GetCoordinatesUpLeft() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultUpLeft_AllRow()
        {
            CustomGame customGame = InitGame();

             // Position of cell origin
            int xOrigin = 7;
            int yOrigin = 7;

            // Set Origin 

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination 
            int xDestination = 4;
            int yDestination = 0;

            // Set Destination
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Launch move
            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            // Tests
            move.GetCoordinatesUpLeft(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 7 && move.Possibilities[0].Y == 6);
            Assert.IsTrue(move.Possibilities[1].X == 6 && move.Possibilities[1].Y == 5);
            Assert.IsTrue(move.Possibilities[2].X == 6 && move.Possibilities[2].Y == 4);
            Assert.IsTrue(move.Possibilities[3].X == 5 && move.Possibilities[3].Y == 3);
            Assert.IsTrue(move.Possibilities[4].X == 5 && move.Possibilities[4].Y == 2);
            Assert.IsTrue(move.Possibilities[5].X == 4 && move.Possibilities[5].Y == 1);
            Assert.IsTrue(move.Possibilities[6].X == 4 && move.Possibilities[6].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesRight() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultRight_BlockBy()
        {
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination
            int xDestination = 2;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Position of cell after
            int xAfter = 3;
            int yAfter = 0;

            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            move.GetCoordinatesRight(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 1 && move.Possibilities[0].Y == 0);
            Assert.IsTrue(move.Possibilities[1].X == 2 && move.Possibilities[1].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesLeft() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultLeft_BlockBy()
        {
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 7;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination
            int xDestination = 5;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Position of cell after
            int xAfter = 4;
            int yAfter = 0;

            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            move.GetCoordinatesLeft(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 6 && move.Possibilities[0].Y == 0);
            Assert.IsTrue(move.Possibilities[1].X == 5 && move.Possibilities[1].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesDownRight() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultDownRight_BlockBy()
        {
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination
            int xDestination = 1;
            int yDestination = 2;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;
            
            // Position of cell after
            int xAfter = 1;
            int yAfter = 3;

            // set After
            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            // launch move
            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            // Tests
            move.GetCoordinatesDownRight(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 0 && move.Possibilities[0].Y == 1);
            Assert.IsTrue(move.Possibilities[1].X == 1 && move.Possibilities[1].Y == 2);
        }

        /// <summary>
        /// Test if the function GetCoordinatesUpRight() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultUpRight_BlockBy()
        {
            // Init Game
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 0;
            int yOrigin = 7;

            // Set Origin
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination
            int xDestination = 1;
            int yDestination = 5;

            // Set destination
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Position of cell after
            int xAfter = 2;
            int yAfter = 4;

            // Set After
            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            //Launch move
            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            // Tests
            move.GetCoordinatesUpRight(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 1 && move.Possibilities[0].Y == 6);
            Assert.IsTrue(move.Possibilities[1].X == 1 && move.Possibilities[1].Y == 5);
        }

        /// <summary>
        /// Test if the function GetCoordinatesDownLeft() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultDownLeft_BlockBy()
        {
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 7;
            int yOrigin = 0;
            
            // Set origin
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination
            int xDestination = 5;
            int yDestination = 3;

            // Set Destination
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Position of cell after
            int xAfter = 5;
            int yAfter = 4;

            // Set After
            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            //Launch move
            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            // Tests
            move.GetCoordinatesDownLeft(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 6 && move.Possibilities[0].Y == 1);
            Assert.IsTrue(move.Possibilities[1].X == 6 && move.Possibilities[1].Y == 2);
            Assert.IsTrue(move.Possibilities[2].X == 5 && move.Possibilities[2].Y == 3);
        }

        /// <summary>
        /// Test if the function GetCoordinatesUpLeft() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultUpLeft_BlockBy()
        {
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 7;
            int yOrigin = 7;

            // Set origin
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination
            int xDestination = 6;
            int yDestination = 5;

            // Set After
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Position of cell after
            int xAfter = 6;
            int yAfter = 4;

            // Set After
            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            // Launch move
            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            // Tests
            move.GetCoordinatesUpLeft(result["origin"]);
            Assert.IsTrue(move.Possibilities[0].X == 7 && move.Possibilities[0].Y == 6);
            Assert.IsTrue(move.Possibilities[1].X == 6 && move.Possibilities[1].Y == 5);
        }

        /// <summary>
        /// Test if the function CheckMove() returns the good coordinates (with up-right)
        /// </summary>
        [TestMethod]
        public void Test_CheckMove_ResultUpRight()
        {
            // Init Game
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 0;
            int yOrigin = 7;

            // Set Origin
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination
            int xDestination = 1;
            int yDestination = 5;

            // Set Destination
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Position of cell after
            int xAfter = 2;
            int yAfter = 4;

            // Set After
            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            // Launch function 
            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            // Tests
            move.CheckMove(result["origin"]);
            Assert.IsTrue(move.Possibilities[7].X == 1 && move.Possibilities[7].Y == 6);
            Assert.IsTrue(move.Possibilities[8].X == 1 && move.Possibilities[8].Y == 5);
        }


        #endregion

    }
}
