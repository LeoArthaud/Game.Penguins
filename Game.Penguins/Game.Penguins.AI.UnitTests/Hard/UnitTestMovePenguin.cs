﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests.Hard
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestMovePenguin : GlobalFunction
    {
        /// <summary>
        /// Test if the penguin can move
        /// </summary>
        [TestMethod]
        public void Test_FindOriginDestination_CanMove()
        {
            //Init Game
            CustomGame customGame = InitGame(null);

            // Penguin in 0;0
            int x = 0;
            int y = 0;
            // Set cell with penguin
            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;
            cell.CellType = CellType.FishWithPenguin;
            cell.CurrentPenguin = new Penguin(customGame.CurrentPlayer);

            // Cell 1 fish in 1;0
            x = 1;
            y = 0;
            // Set cell with 1 fish
            cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;

            // Cell 3 fish in 2;0
            x = 2;
            y = 0;
            // Set cell with 3 fish
            cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 3;

            // Cell water in 3;0
            x = 3;
            y = 0;
            // Set cell water
            cell = (Cell)customGame.Board.Board[x, y];
            cell.CellType = CellType.Water;

            // Cell 1 fish in 0;1
            x = 0;
            y = 1;
            // Set cell with 1 fish
            cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;

            // Cell 2 fish in 1;2
            x = 1;
            y = 2;
            // Set cell with 2 fish
            cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 2;

            // Cell water in 1;3
            x = 1;
            y = 3;
            // Set cell water
            cell = (Cell)customGame.Board.Board[x, y];
            cell.CellType = CellType.Water;

            // Launch function
            AIHard aiHard = new AIHard(customGame.Board, new AppRandom(), customGame.CurrentPlayer);
            Dictionary<string, Coordinates> coordinates = aiHard.FindOriginDestination();

            // Test
            Assert.IsTrue(coordinates["destination"].X == 2 && coordinates["destination"].Y == 0);
        }

        /// <summary>
        /// Test if the a penguin of player can't move
        /// </summary>
        [TestMethod]
        public void Test_FindOrigin_ApplyChange()
        {
            //Init Game
            CustomGame customGame = InitGame(null);

            // Penguin in 0;0
            int x = 0;
            int y = 0;
            // Set cell with penguin
            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;
            cell.CellType = CellType.FishWithPenguin;
            cell.CurrentPenguin = new Penguin(customGame.CurrentPlayer);

            // Cell water in 1;0
            x = 1;
            y = 0;
            // Set cell water
            cell = (Cell)customGame.Board.Board[x, y];
            cell.CellType = CellType.Water;

            // Cell water in 0;1
            x = 0;
            y = 1;
            // Set cell water
            cell = (Cell)customGame.Board.Board[x, y];
            cell.CellType = CellType.Water;

            // Launch function
            AIHard aiHard = new AIHard(customGame.Board, new AppRandom(), customGame.CurrentPlayer);
            Dictionary<string, Coordinates> coordinates = aiHard.FindOriginDestination();

            // Tests
            Assert.IsTrue(coordinates.Count == 1);
        }
    }
}
