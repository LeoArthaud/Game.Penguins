using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestMovePenguin
    {
        /// <summary>
        /// Test if the AI Easy has move
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIEasy()
        {
            // Init Game
            CustomGame customGame = InitGame();

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
        /// Test if the AI Medium has move
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIMedium()
        {
            // Init Game
            CustomGame customGame = InitGame();

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
        /// Test if the AI Hard has move
        /// </summary>
        [TestMethod]
        public void Test_MovePenguin_AIHard()
        {
            // Init Game
            CustomGame customGame = InitGame();

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

        #region Private Functions

        /// <summary>
        /// Init the game with 3 players (AIEasy, AIMedium, AIHard)
        /// </summary>
        /// <returns>the game with modifications</returns>
        public CustomGame InitGame()
        {
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 3 players
            customGame.AddPlayer("Player1", PlayerType.AIEasy);
            customGame.AddPlayer("Player2", PlayerType.AIMedium);
            customGame.AddPlayer("Player3", PlayerType.AIHard);

            // Launch function
            customGame.StartGame();

            // Set current player
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }

        #endregion
    }
}
