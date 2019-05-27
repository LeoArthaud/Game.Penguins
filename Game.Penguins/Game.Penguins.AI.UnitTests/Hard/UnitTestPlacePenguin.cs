using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Game.Penguins.AI.UnitTests.Hard
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestPlacePenguin
    {
        [TestMethod]
        public void Test_PlacePenguin_getThreePoints()
        {
            CustomGame customGame = InitGame();

            AIHard aiHard = new AIHard(customGame.Board, new AppRandom(), customGame.CurrentPlayer );
            Assert.IsTrue(aiHard.getThreePoints().Count == 10);
        }

        [TestMethod]
        public void Test_PlacePenguin_getFishNear()
        {
            CustomGame customGame = InitGame();

            AIHard aiHard = new AIHard(customGame.Board, new AppRandom(), customGame.CurrentPlayer);
            IList<Coordinates> fishThreePointsList = new List<Coordinates>();

            Cell cellThreePoints = (Cell)customGame.Board.Board[1, 1];
            cellThreePoints.FishCount = 3;
            Coordinates coordinates = new Coordinates(1, 1);

            Cell cellUpRight = (Cell)customGame.Board.Board[2, 0];
            cellUpRight.FishCount = 1;

            Cell cellRight = (Cell)customGame.Board.Board[2, 1];
            cellRight.FishCount = 1;

            Cell cellDownRight = (Cell)customGame.Board.Board[2, 2];
            cellDownRight.FishCount = 1;

            Cell cellDownLeft = (Cell)customGame.Board.Board[1, 2];
            cellDownLeft.FishCount = 1;

            Cell cellLeft = (Cell)customGame.Board.Board[0, 1];
            cellLeft.FishCount = 1;

            Cell cellUpLeft = (Cell)customGame.Board.Board[1, 0];
            cellUpLeft.FishCount = 1;


            fishThreePointsList.Add(coordinates);
            IList<Coordinates> list = aiHard.getFishNear(fishThreePointsList);
            Assert.IsTrue(list[0].X == 0 && list[0].Y == 1);
            Assert.IsTrue(list[1].X == 2 && list[1].Y == 1);
            Assert.IsTrue(list[2].X == 1 && list[2].Y == 0);
            Assert.IsTrue(list[3].X == 2 && list[3].Y == 0);
            Assert.IsTrue(list[4].X == 1 && list[4].Y == 2);
            Assert.IsTrue(list[5].X == 2 && list[5].Y == 2);
        }

        [TestMethod]
        public void Test_PlacePenguin_getFishNearEmpty()
        {
            CustomGame customGame = InitGame();

            AIHard aiHard = new AIHard(customGame.Board, new AppRandom(), customGame.CurrentPlayer);
            IList<Coordinates> fishThreePointsList = new List<Coordinates>();

            Cell cellThreePoints = (Cell)customGame.Board.Board[1, 1];
            cellThreePoints.FishCount = 3;
            Coordinates coordinates = new Coordinates(1, 1);
            customGame = SetFish(customGame);

            fishThreePointsList.Add(coordinates);
            IList<Coordinates> list = aiHard.getFishNear(fishThreePointsList);
            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void Test_PlacePenguin_isNearFalse()
        {
            Mock<IRandom> mock = new Mock<IRandom>();
            mock.SetupSequence(e => e.Next(0, 8))
                .Returns(1)
                .Returns(0)
                .Returns(2)
                .Returns(2);

            CustomGame customGame = InitGame();
            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Cell cell2 = (Cell)customGame.Board.Board[i,j];
                    cell2.FishCount = 2;
                }
                Cell cell = (Cell)customGame.Board.Board[i, i];
                cell.FishCount = 1;
            }

            AIHard aiHard = new AIHard(customGame.Board, mock.Object, customGame.CurrentPlayer);
            Coordinates coordinates = aiHard.PlacePenguin();
            Assert.IsTrue(coordinates.X == 2 && coordinates.Y == 2);

        }
        #region Private Functions
        private CustomGame SetFish(CustomGame customGame)
        {
            Cell cellUpRight = (Cell)customGame.Board.Board[2, 0];
            cellUpRight.FishCount = 2;
            cellUpRight.CellType = CellType.FishWithPenguin;

            Cell cellRight = (Cell)customGame.Board.Board[2, 1];
            cellRight.FishCount = 2;
            cellRight.CellType = CellType.FishWithPenguin;

            Cell cellDownRight = (Cell)customGame.Board.Board[2, 2];
            cellDownRight.FishCount = 2;
            cellDownRight.CellType = CellType.FishWithPenguin;

            Cell cellDownLeft = (Cell)customGame.Board.Board[1, 2];
            cellDownLeft.FishCount = 2;
            cellDownLeft.CellType = CellType.FishWithPenguin;

            Cell cellLeft = (Cell)customGame.Board.Board[0, 1];
            cellLeft.FishCount = 2;
            cellLeft.CellType = CellType.FishWithPenguin;

            Cell cellUpLeft = (Cell)customGame.Board.Board[1, 0];
            cellUpLeft.FishCount = 2;
            cellUpLeft.CellType = CellType.FishWithPenguin;

            return customGame;
        }

        public CustomGame InitGame()
        {
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 2 IA hard
            customGame.AddPlayer("Player1", PlayerType.AIHard);
            customGame.AddPlayer("Player2", PlayerType.AIHard);

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }

        #endregion
    }
}
