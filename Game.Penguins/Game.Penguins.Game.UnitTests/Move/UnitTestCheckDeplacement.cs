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

        [TestMethod]
        public void Test_CheckDeplacement_ResultRight()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;

            // Position of cell
            int xDestination = 2;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 1 && listPossibilities[0].Y == 0);
            Assert.IsTrue(listPossibilities[1].X == 2 && listPossibilities[1].Y == 0);
        }

        [TestMethod]
        public void Test_CheckDeplacement_ResultLeft()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 2;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;

            // Position of cell
            int xDestination = 0;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            Assert.IsTrue(listPossibilities[0].X == 1 && listPossibilities[0].Y == 0);
            Assert.IsTrue(listPossibilities[1].X == 0 && listPossibilities[1].Y == 0);
        }

        [TestMethod]
        public void Test_CheckDeplacement_ResultDownRight()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 1;
            int yOrigin = 4;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;

            // Position of cell
            int xDestination = 2;
            int yDestination = 6;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            Assert.IsTrue(listPossibilities[1].X == 1 && listPossibilities[1].Y == 5);
            Assert.IsTrue(listPossibilities[2].X == 2 && listPossibilities[2].Y == 6);
        }

        [TestMethod]
        public void Test_CheckDeplacement_ResultUpRight()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 3;
            int yOrigin = 7;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];

            // Position of cell
            int xDestination = 7;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            Assert.IsTrue(listPossibilities[4].X == 4 && listPossibilities[4].Y == 6);
            Assert.IsTrue(listPossibilities[5].X == 4 && listPossibilities[5].Y == 5);
            Assert.IsTrue(listPossibilities[6].X == 5 && listPossibilities[6].Y == 4);
            Assert.IsTrue(listPossibilities[7].X == 5 && listPossibilities[7].Y == 3);
            Assert.IsTrue(listPossibilities[8].X == 6 && listPossibilities[8].Y == 2);
            Assert.IsTrue(listPossibilities[9].X == 6 && listPossibilities[9].Y == 1);
            Assert.IsTrue(listPossibilities[10].X == 7 && listPossibilities[10].Y == 0);
        }

        [TestMethod]
        public void Test_CheckDeplacement_ResultDownLeft()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 7;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;

            // Position of cell
            int xDestination = 6;
            int yDestination = 2;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            Assert.IsTrue(listPossibilities[1].X == 6 && listPossibilities[1].Y == 1);
            Assert.IsTrue(listPossibilities[2].X == 6 && listPossibilities[2].Y == 2);
        }

        [TestMethod]
        public void Test_CheckDeplacement_ResultUpLeft()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 2;
            int yOrigin = 7;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;


            // Position of cell
            int xDestination = 1;
            int yDestination = 5;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            Assert.IsTrue(listPossibilities[1].X == 2 && listPossibilities[1].Y == 6);
            Assert.IsTrue(listPossibilities[2].X == 1 && listPossibilities[2].Y == 5);
        }

        [TestMethod]
        public void Test_CheckDeplacement_ResultRightNull()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            
            // Position of cell
            int xDestination = 3;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;
            cellDestination.CellType = CellType.FishWithPenguin;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            foreach (var element in listPossibilities)
            {
                Assert.IsTrue(element.X != xDestination && element.Y != yDestination);
            }
        }

        [TestMethod]
        public void Test_CheckDeplacement_ResultLeftNull()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 3;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            
            // Position of cell
            int xDestination = 0;
            int yDestination = 0;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;
            cellDestination.CellType = CellType.FishWithPenguin;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            foreach (var element in listPossibilities)
            {
                Assert.IsTrue(element.X != xDestination || element.Y != yDestination);
            }
        }

        [TestMethod]
        public void Test_CheckDeplacement_ResultDownRightNull()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 3;
            int yOrigin = 4;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;
            
            // Position of cell
            int xDestination = 4;
            int yDestination = 6;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;
            cellDestination.CellType = CellType.FishWithPenguin;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            foreach (var element in listPossibilities)
            {
                Assert.IsTrue(element.X != xDestination || element.Y != yDestination);
            }
        }

        [TestMethod]
        public void Test_CheckDeplacement_ResultUpRightNull()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 3;
            int yOrigin = 5;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;

            // Position of cell
            int xDestination = 5;
            int yDestination = 2;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;
            cellDestination.CellType = CellType.FishWithPenguin;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            foreach (var element in listPossibilities)
            {
                Assert.IsTrue(element.X != xDestination || element.Y != yDestination);
            }
        }

        [TestMethod]
        public void Test_CheckDeplacement_ResultDownLeftNull()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 7;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;

            // Position of cell
            int xDestination = 6;
            int yDestination = 2;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;
            cellDestination.CellType = CellType.FishWithPenguin;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            foreach (var element in listPossibilities)
            {
                Assert.IsTrue(element.X != xDestination || element.Y != yDestination);
            }
        }

        [TestMethod]
        public void Test_CheckDeplacement_ResultUpLeftNull()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 2;
            int yOrigin = 7;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;


            // Position of cell
            int xDestination = 1;
            int yDestination = 5;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;
            cellDestination.CellType = CellType.FishWithPenguin;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            foreach (var element in listPossibilities)
            {
                Assert.IsTrue(element.X != xDestination || element.Y != yDestination);
            }
        }


        [TestMethod]
        public void Test_CheckDeplacement_ResultNull()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 4;
            int yOrigin = 6;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;


            // Position of cell
            int xDestination = 0;
            int yDestination = 5;

            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;

            Movements moveOfHuman = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = moveOfHuman.GetCoordinates();

            var listPossibilities = moveOfHuman.CheckDeplacement(result["origin"]);
            foreach (var element in listPossibilities)
            {
                Assert.IsTrue(element.X != xDestination || element.Y != yDestination);
            }
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
