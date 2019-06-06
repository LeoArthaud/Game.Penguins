using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestMovePenguin : GlobalFunctions
    {
        /// <summary>
        /// Test if the AI Easy has move
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIEasy()
        {
            // Init Game
            CustomGame customGame = InitGame(null);

            // Launch function to place a penguin
            customGame.PlacePenguin();

            // Set current player
            customGame.CurrentPlayer = customGame.Players[0];

            // Launch function to move a penguin
            customGame.Move();

            bool isMove = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // If a penguin has been moved
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin && customGame.Board.Board[i, j].CurrentPenguin.Player == customGame.Players[0])
                    {
                        // bool is set to true
                        isMove = true;
                    }
                }
            }

            // Test if a penguin has been moved
            Assert.IsTrue(isMove);
        }

        /// <summary>
        /// Test if the AI Easy cannot move
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIEasy_EndGame()
        {
            // Init Game
            CustomGame customGame = InitGame(null);

            // Position of cell with penguin 0;0
            int x = 0;
            int y = 0;
            // Set cell
            Cell cellOrigin = (Cell)customGame.Board.Board[x, y];
            cellOrigin.FishCount = 1;
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(customGame.Players[0]);

            // Position of cell water 1;0
            x = 1;
            y = 0;
            // Set cell
            Cell cell1 = (Cell)customGame.Board.Board[x, y];
            cell1.CellType = CellType.Water;

            // Position of cell water 0;1
            x = 0;
            y = 1;
            // Set cell
            Cell cell2 = (Cell)customGame.Board.Board[x, y];
            cell2.CellType = CellType.Water;

            // End Game
            customGame.Move();

            // Test
            Assert.IsTrue(customGame.NextAction == NextActionType.Nothing);
        }

        /// <summary>
        /// Test if the AI Medium has move
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIMedium()
        {
            // Init Game
            CustomGame customGame = InitGame(null);

            // Set Current player
            customGame.CurrentPlayer = customGame.Players[1];

            // Launch function to place a penguin
            customGame.PlacePenguin();

            // Set Current player
            customGame.CurrentPlayer = customGame.Players[1];

            // Launch function to move a penguin
            customGame.Move();

            bool isMove = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // If a penguin has been moved
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin && customGame.Board.Board[i,j].CurrentPenguin.Player == customGame.Players[1])
                    {
                        // bool is set to true
                        isMove = true;
                    }
                }
            }

            // Test if a penguin has been moved
            Assert.IsTrue(isMove);
        }

        /// <summary>
        /// Test if the AI Medium cannot move
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIMedium_EndGame()
        {
            // Init Game
            CustomGame customGame = InitGame(null);
            
            // Set Current player
            customGame.CurrentPlayer = customGame.Players[1];

            // Position of cell with penguin 0;0
            int x = 0;
            int y = 0;
            // Set cell
            Cell cellOrigin = (Cell)customGame.Board.Board[x, y];
            cellOrigin.FishCount = 1;
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(customGame.Players[1]);

            // Position of cell water 1;0
            x = 1;
            y = 0;
            // Set cell
            Cell cell1 = (Cell)customGame.Board.Board[x, y];
            cell1.CellType = CellType.Water;

            // Position of cell water 0;1
            x = 0;
            y = 1;
            // Set cell
            Cell cell2 = (Cell)customGame.Board.Board[x, y];
            cell2.CellType = CellType.Water;

            // End Game
            customGame.Move();

            // Test
            Assert.IsTrue(customGame.NextAction == NextActionType.Nothing);
        }

        /// <summary>
        /// Test if the AI Hard has move
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIHard()
        {
            // Init Game
            CustomGame customGame = InitGame(null);

            // Set current player
            customGame.CurrentPlayer = customGame.Players[2];

            // Launch function to place a penguin
            customGame.PlacePenguin();

            // Set current player
            customGame.CurrentPlayer = customGame.Players[2];

            // Launch function to move a penguin
            customGame.Move();

            bool isMove = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // If a penguin has been moved
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin && customGame.Board.Board[i, j].CurrentPenguin.Player == customGame.Players[2])
                    {
                        // bool is set to true
                        isMove = true;
                    }
                }
            }

            // Test if a penguin has been moved
            Assert.IsTrue(isMove);
        }

        /// <summary>
        /// Test if the AI Hard cannot move
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIHard_EndGame()
        {
            // Init Game
            CustomGame customGame = InitGame(null);

            // Set Current player
            customGame.CurrentPlayer = customGame.Players[2];

            // Position of cell with penguin 0;0
            int x = 0;
            int y = 0;
            // Set cell
            Cell cellOrigin = (Cell)customGame.Board.Board[x, y];
            cellOrigin.FishCount = 1;
            cellOrigin.CellType = CellType.FishWithPenguin;
            cellOrigin.CurrentPenguin = new Penguin(customGame.Players[2]);

            // Position of cell water 1;0
            x = 1;
            y = 0;
            // Set cell
            Cell cell1 = (Cell)customGame.Board.Board[x, y];
            cell1.CellType = CellType.Water;

            // Position of cell water 0;1
            x = 0;
            y = 1;
            // Set cell
            Cell cell2 = (Cell)customGame.Board.Board[x, y];
            cell2.CellType = CellType.Water;

            // End Game
            customGame.Move();

            // Test
            Assert.IsTrue(customGame.NextAction == NextActionType.Nothing);
        }

    }
}
