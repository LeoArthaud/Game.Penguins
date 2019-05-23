using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.App;
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
    public class UnitTestCheckDeplacement
    {
        #region Public Functions

        /// <summary>
        /// Test if the function GetCoordinatesRight() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultRight_AllRow()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell
            int xDestination = 7;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesRight(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 1 && listPossibilities[0].Y == 0);
            Assert.IsTrue(listPossibilities[1].X == 2 && listPossibilities[1].Y == 0);
            Assert.IsTrue(listPossibilities[2].X == 3 && listPossibilities[2].Y == 0);
            Assert.IsTrue(listPossibilities[3].X == 4 && listPossibilities[3].Y == 0);
            Assert.IsTrue(listPossibilities[4].X == 5 && listPossibilities[4].Y == 0);
            Assert.IsTrue(listPossibilities[5].X == 6 && listPossibilities[5].Y == 0);
            Assert.IsTrue(listPossibilities[6].X == 7 && listPossibilities[6].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesLeft() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultLeft_AllRow()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 7;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell
            int xDestination = 0;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;
            
            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesLeft(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 6 && listPossibilities[0].Y == 0);
            Assert.IsTrue(listPossibilities[1].X == 5 && listPossibilities[1].Y == 0);
            Assert.IsTrue(listPossibilities[2].X == 4 && listPossibilities[2].Y == 0);
            Assert.IsTrue(listPossibilities[3].X == 3 && listPossibilities[3].Y == 0);
            Assert.IsTrue(listPossibilities[4].X == 2 && listPossibilities[4].Y == 0);
            Assert.IsTrue(listPossibilities[5].X == 1 && listPossibilities[5].Y == 0);
            Assert.IsTrue(listPossibilities[6].X == 0 && listPossibilities[6].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesDownRight() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultDownRight_AllRow()
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


            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesDownRight(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 0 && listPossibilities[0].Y == 1);
            Assert.IsTrue(listPossibilities[1].X == 1 && listPossibilities[1].Y == 2);
            Assert.IsTrue(listPossibilities[2].X == 1 && listPossibilities[2].Y == 3);
            Assert.IsTrue(listPossibilities[3].X == 2 && listPossibilities[3].Y == 4);
            Assert.IsTrue(listPossibilities[4].X == 2 && listPossibilities[4].Y == 5);
            Assert.IsTrue(listPossibilities[5].X == 3 && listPossibilities[5].Y == 6);
            Assert.IsTrue(listPossibilities[6].X == 3 && listPossibilities[6].Y == 7);
        }

        /// <summary>
        /// Test if the function GetCoordinatesUpRight() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultUpRight_AllRow()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 7;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell
            int xDestination = 4;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesUpRight(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 1 && listPossibilities[0].Y == 6);
            Assert.IsTrue(listPossibilities[1].X == 1 && listPossibilities[1].Y == 5);
            Assert.IsTrue(listPossibilities[2].X == 2 && listPossibilities[2].Y == 4);
            Assert.IsTrue(listPossibilities[3].X == 2 && listPossibilities[3].Y == 3);
            Assert.IsTrue(listPossibilities[4].X == 3 && listPossibilities[4].Y == 2);
            Assert.IsTrue(listPossibilities[5].X == 3 && listPossibilities[5].Y == 1);
            Assert.IsTrue(listPossibilities[6].X == 4 && listPossibilities[6].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesDownLeft() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultDownLeft_AllRow()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 7;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell
            int xDestination = 3;
            int yDestination = 7;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;
            
            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesDownLeft(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 6 && listPossibilities[0].Y == 1);
            Assert.IsTrue(listPossibilities[1].X == 6 && listPossibilities[1].Y == 2);
            Assert.IsTrue(listPossibilities[2].X == 5 && listPossibilities[2].Y == 3);
            Assert.IsTrue(listPossibilities[3].X == 5 && listPossibilities[3].Y == 4);
            Assert.IsTrue(listPossibilities[4].X == 4 && listPossibilities[4].Y == 5);
            Assert.IsTrue(listPossibilities[5].X == 4 && listPossibilities[5].Y == 6);
            Assert.IsTrue(listPossibilities[6].X == 3 && listPossibilities[6].Y == 7);
        }

        /// <summary>
        /// Test if the function GetCoordinatesUpLeft() returns the good coordinates (all the row)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultUpLeft_AllRow()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 7;
            int yOrigin = 7;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell
            int xDestination = 4;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;
            
            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesUpLeft(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 7 && listPossibilities[0].Y == 6);
            Assert.IsTrue(listPossibilities[1].X == 6 && listPossibilities[1].Y == 5);
            Assert.IsTrue(listPossibilities[2].X == 6 && listPossibilities[2].Y == 4);
            Assert.IsTrue(listPossibilities[3].X == 5 && listPossibilities[3].Y == 3);
            Assert.IsTrue(listPossibilities[4].X == 5 && listPossibilities[4].Y == 2);
            Assert.IsTrue(listPossibilities[5].X == 4 && listPossibilities[5].Y == 1);
            Assert.IsTrue(listPossibilities[6].X == 4 && listPossibilities[6].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesRight() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultRight_BlockBy()
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

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesRight(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 1 && listPossibilities[0].Y == 0);
            Assert.IsTrue(listPossibilities[1].X == 2 && listPossibilities[1].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesLeft() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultLeft_BlockBy()
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

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesLeft(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 6 && listPossibilities[0].Y == 0);
            Assert.IsTrue(listPossibilities[1].X == 5 && listPossibilities[1].Y == 0);
        }

        /// <summary>
        /// Test if the function GetCoordinatesDownRight() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultDownRight_BlockBy()
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

            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesDownRight(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 0 && listPossibilities[0].Y == 1);
            Assert.IsTrue(listPossibilities[1].X == 1 && listPossibilities[1].Y == 2);
        }

        /// <summary>
        /// Test if the function GetCoordinatesUpRight() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultUpRight_BlockBy()
        {
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 0;
            int yOrigin = 7;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination
            int xDestination = 1;
            int yDestination = 5;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Position of cell after
            int xAfter = 2;
            int yAfter = 4;

            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesUpRight(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 1 && listPossibilities[0].Y == 6);
            Assert.IsTrue(listPossibilities[1].X == 1 && listPossibilities[1].Y == 5);
        }

        /// <summary>
        /// Test if the function GetCoordinatesDownLeft() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultDownLeft_BlockBy()
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
            int yDestination = 3;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Position of cell after
            int xAfter = 5;
            int yAfter = 4;

            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesDownLeft(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 6 && listPossibilities[0].Y == 1);
            Assert.IsTrue(listPossibilities[1].X == 6 && listPossibilities[1].Y == 2);
            Assert.IsTrue(listPossibilities[2].X == 5 && listPossibilities[2].Y == 3);
        }

        /// <summary>
        /// Test if the function GetCoordinatesUpLeft() returns the good coordinates (2 or 3 cells -> block by cell water)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultUpLeft_BlockBy()
        {
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 7;
            int yOrigin = 7;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination
            int xDestination = 6;
            int yDestination = 5;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Position of cell after
            int xAfter = 6;
            int yAfter = 4;

            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.GetCoordinatesUpLeft(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 7 && listPossibilities[0].Y == 6);
            Assert.IsTrue(listPossibilities[1].X == 6 && listPossibilities[1].Y == 5);
        }

        /// <summary>
        /// Test if the function CheckDeplacement() returns the good coordinates (with up-right)
        /// </summary>
        [TestMethod]
        public void Test_CheckDeplacement_ResultUpRight()
        {
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 0;
            int yOrigin = 7;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell destination
            int xDestination = 1;
            int yDestination = 5;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.CellType = CellType.Fish;

            // Position of cell after
            int xAfter = 2;
            int yAfter = 4;

            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            Assert.IsTrue(listPossibilities[7].X == 1 && listPossibilities[7].Y == 6);
            Assert.IsTrue(listPossibilities[8].X == 1 && listPossibilities[8].Y == 5);
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
