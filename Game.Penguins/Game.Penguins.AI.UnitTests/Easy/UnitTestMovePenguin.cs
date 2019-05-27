using System.Collections.Generic;
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

namespace Game.Penguins.AI.UnitTests.Easy
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestMovePenguin
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Test_FindOrigin_ApplyChange()
        {
            //Init Game
            CustomGame customGame = InitGame(null);
            List<Coordinates> list = new List<Coordinates>();

            // Penguin in 0;0
            int x = 0;
            int y = 0;

            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;
            cell.CellType = CellType.FishWithPenguin;
            cell.CurrentPenguin = new Penguin(customGame.CurrentPlayer);
            list.Add(new Coordinates(x,y));

            // Cell water in 1;0
            x = 1;
            y = 0;

            cell = (Cell)customGame.Board.Board[x, y];
            cell.CellType = CellType.Water;

            // Cell water in 0;1
            x = 0;
            y = 1;

            cell = (Cell)customGame.Board.Board[x, y];
            cell.CellType = CellType.Water;

            // Launch function
            AIEasy aiEasy = new AIEasy(customGame.Board, new AppRandom(), customGame.CurrentPlayer);
            Coordinates coordinates = aiEasy.FindOrigin(list);

            // Tests
            Assert.IsTrue(customGame.Board.Board[0,0].CellType == CellType.Water);
            Assert.IsTrue(customGame.Board.Board[0,0].FishCount == 0);
            Assert.IsTrue(customGame.Board.Board[0,0].CurrentPenguin == null);
            Assert.IsTrue(customGame.CurrentPlayer.Points == 1);
        }

        #region Private Functions

        public CustomGame InitGame(Mock<IRandom> randomMock)
        {
            CustomGame customGame = randomMock == null ? new CustomGame(new AppRandom()) : new CustomGame(randomMock.Object);

            // Add 2 players
            customGame.AddPlayer("Player1", PlayerType.AIEasy);
            customGame.AddPlayer("Player2", PlayerType.AIEasy);

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }

        #endregion
    }
}
