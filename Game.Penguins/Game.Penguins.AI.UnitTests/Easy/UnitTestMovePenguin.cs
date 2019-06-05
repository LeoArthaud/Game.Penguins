using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests.Easy
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestMovePenguin : GlobalFunctions
    {
        /// <summary>
        /// If all the penguin of the AI can move
        /// </summary>
        [TestMethod]
        public void Test_FindOrigin_CanMove()
        {
            // Init Game
            CustomGame customGame = InitGame(null);
            List<Coordinates> list = new List<Coordinates>();

            // Penguin in 0;0
            int x = 0;
            int y = 0;
            // Set cell with penguin
            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;
            cell.CellType = CellType.FishWithPenguin;
            cell.CurrentPenguin = new Penguin(customGame.CurrentPlayer);
            list.Add(new Coordinates(x, y));
            
            // Launch function
            AIEasy aiEasy = new AIEasy(customGame.Board, new AppRandom(), customGame.CurrentPlayer);
            Coordinates coordinates = aiEasy.FindOrigin(list);

            // Tests
            Assert.IsTrue(customGame.Board.Board[0, 0].CellType == CellType.FishWithPenguin);
            Assert.IsTrue(customGame.Board.Board[0, 0].FishCount == 1);
            Assert.IsTrue(customGame.Board.Board[0, 0].CurrentPenguin.Player == customGame.CurrentPlayer);
            Assert.IsTrue(customGame.CurrentPlayer.Points == 0);
            Assert.IsTrue(coordinates.X == 0 && coordinates.Y == 0);
        }
        
        /// <summary>
        /// If a penguin of the AI can't move
        /// </summary>
        [TestMethod]
        public void Test_FindOrigin_ApplyChange()
        {
            // Init Game
            CustomGame customGame = InitGame(null);
            List<Coordinates> list = new List<Coordinates>();

            // Penguin in 0;0
            int x = 0;
            int y = 0;
            // Set cell with penguin
            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;
            cell.CellType = CellType.FishWithPenguin;
            cell.CurrentPenguin = new Penguin(customGame.CurrentPlayer);
            list.Add(new Coordinates(x,y));

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
            AIEasy aiEasy = new AIEasy(customGame.Board, new AppRandom(), customGame.CurrentPlayer);
            Coordinates coordinates = aiEasy.FindOrigin(list);

            // Tests
            Assert.IsTrue(coordinates.X == -1 && coordinates.Y == -1);
        }

        /// <summary>
        /// Get coordinates if the penguin can move
        /// </summary>
        [TestMethod]
        public void Test_FindDestination()
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

            // Cell water in 0;1
            x = 0;
            y = 1;
            // Set cell water
            cell = (Cell)customGame.Board.Board[x, y];
            cell.CellType = CellType.Water;

            // Cell water in 2;0
            x = 2;
            y = 0;
            // Set cell water
            cell = (Cell)customGame.Board.Board[x, y];
            cell.CellType = CellType.Water;

            // Launch function
            AIEasy aiEasy = new AIEasy(customGame.Board, new AppRandom(), customGame.CurrentPlayer);
            Coordinates coordinates = aiEasy.FindDestination(new Coordinates(0,0));

            // Test
            Assert.IsTrue(coordinates.X == 1 && coordinates.Y == 0);

        }

        /// <summary>
        /// Get coordinates if the penguin can't move
        /// </summary>
        [TestMethod]
        public void Test_FindDestination_CannotMove()
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

            // Cell water in 0;1
            x = 0;
            y = 1;
            // Set cell water
            cell = (Cell)customGame.Board.Board[x, y];
            cell.CellType = CellType.Water;

            // Cell water in 1;0
            x = 1;
            y = 0;
            // Set cell water
            cell = (Cell)customGame.Board.Board[x, y];
            cell.CellType = CellType.Water;

            // Launch function
            AIEasy aiEasy = new AIEasy(customGame.Board, new AppRandom(), customGame.CurrentPlayer);
            Coordinates coordinates = aiEasy.FindDestination(new Coordinates(0, 0));

            // Test
            Assert.IsTrue(coordinates.X == -1 && coordinates.Y == -1);

        }

    }
}
