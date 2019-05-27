﻿using System.Diagnostics.CodeAnalysis;
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
    public class UnitTestCheckCaseWater
    {
        #region Public Functions

        /// <summary>
        /// Test in the function CheckCaseWater(), if cell after is Water returns "true" 
        /// </summary>
        [TestMethod]
        public void Test_CheckCaseWater_CellTypeWater()
        {
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell after
            int xAfter = 1;
            int yAfter = 0;

            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Water;

            Movements move = new Movements(cellOrigin, null, customGame.Board);
            bool result = move.CheckCaseWater(xAfter, yAfter);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test in the function CheckCaseWater(), if cell after exceed board returns "true"
        /// </summary>
        [TestMethod]
        public void Test_CheckCaseWater_CellTypeWater_ExceedBoard()
        {
            CustomGame customGame = InitGame();
            
            Movements move = new Movements(null, null, customGame.Board);
            bool result = move.CheckCaseWater(8, 8);

            Assert.IsTrue(result);

            result = move.CheckCaseWater(-1, -1);

            Assert.IsTrue(result);
        }


        /// <summary>
        /// Test in the function CheckCaseWater(), if cell after is Penguin returns "true" 
        /// </summary>
        [TestMethod]
        public void Test_CheckCaseWater_CellTypePenguin()
        {
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell after
            int xAfter = 1;
            int yAfter = 0;

            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.FishWithPenguin;
            cellAfter.CurrentPenguin = new Penguin(new Player("Player2", PlayerType.Human));

            Movements move = new Movements(cellOrigin, null, customGame.Board);
            bool result = move.CheckCaseWater(xAfter, yAfter);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Test in the function CheckCaseWater(), if cell after is Fish returns "false" 
        /// </summary>
        [TestMethod]
        public void Test_CheckCaseWater_CellTypeFish()
        {
            CustomGame customGame = InitGame();

            // Position of cell origin
            int xOrigin = 0;
            int yOrigin = 0;

            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(new Player("Player1", PlayerType.Human));

            // Position of cell after
            int xAfter = 1;
            int yAfter = 0;

            Cell cellAfter = (Cell)customGame.Board.Board[xAfter, yAfter];
            cellAfter.CellType = CellType.Fish;

            Movements move = new Movements(cellOrigin, null, customGame.Board);
            bool result = move.CheckCaseWater(xAfter, yAfter);

            Assert.IsTrue(!result);
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
