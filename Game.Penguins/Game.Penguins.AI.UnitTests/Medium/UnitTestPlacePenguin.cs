using System;
using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Game.Penguins.AI.UnitTests.Medium
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestPlacePenguin
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_FirstTry()
        {
            // Init game
            Mock<IRandom> randomMock = new Mock<IRandom>();
            randomMock.SetupSequence(e => e.Next(0, 8)).Returns(0);

            CustomGame customGame = InitGame(null);

            // Position of cell
            int x = 0;
            int y = 0;

            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;

            // Launch function
            AIMedium aiMedium = new AIMedium(customGame.Board, randomMock.Object, customGame.CurrentPlayer);

            Coordinates coordinates = aiMedium.PlacePenguin();

            Assert.IsTrue(coordinates.X == 0 && coordinates.Y == 0);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_NotFirstTry_FishCount()
        {
            // Init game
            Mock<IRandom> randomMock = new Mock<IRandom>();
            randomMock.SetupSequence(e => e.Next(0, 8))
                .Returns(0).Returns(0)
                .Returns(1).Returns(1);

            CustomGame customGame = InitGame(null);

            // Position of first cell
            int x = 0;
            int y = 0;

            Cell cell1 = (Cell)customGame.Board.Board[x, y];
            cell1.FishCount = 3;

            // Position of second cell
            x = 1;
            y = 1;

            Cell cell2 = (Cell)customGame.Board.Board[x, y];
            cell2.FishCount = 1;

            // Launch function
            AIMedium aiMedium = new AIMedium(customGame.Board, randomMock.Object, customGame.CurrentPlayer);

            Coordinates coordinates = aiMedium.PlacePenguin();

            Assert.IsTrue(coordinates.X == 1 && coordinates.Y == 1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_NotFirstTry_FishWithPenguin()
        {
            // Init game
            Mock<IRandom> randomMock = new Mock<IRandom>();
            randomMock.SetupSequence(e => e.Next(0, 8))
                .Returns(0).Returns(0)
                .Returns(1).Returns(1);

            CustomGame customGame = InitGame(null);

            // Position of first cell
            int x = 0;
            int y = 0;

            Cell cell1 = (Cell)customGame.Board.Board[x, y];
            cell1.CellType = CellType.FishWithPenguin;

            // Position of second cell
            x = 1;
            y = 1;

            Cell cell2 = (Cell)customGame.Board.Board[x, y];
            cell2.FishCount = 1;

            // Launch function
            AIMedium aiMedium = new AIMedium(customGame.Board, randomMock.Object, customGame.CurrentPlayer);

            Coordinates coordinates = aiMedium.PlacePenguin();

            Assert.IsTrue(coordinates.X == 1 && coordinates.Y == 1);
        }

        #region Private Functions

        public CustomGame InitGame(Mock<IRandom> randomMock)
        {
            CustomGame customGame = randomMock == null ? new CustomGame(new AppRandom()) : new CustomGame(randomMock.Object);

            // Add 2 players
            customGame.AddPlayer("Player1", PlayerType.AIMedium);
            customGame.AddPlayer("Player2", PlayerType.AIMedium);

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }

        #endregion
    }
}
