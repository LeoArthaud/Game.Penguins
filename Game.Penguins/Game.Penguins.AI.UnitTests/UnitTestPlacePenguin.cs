using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.AI.UnitTests
{
    /// <summary>
    /// Test the function PlacePenguin()
    /// </summary>
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestPlacePenguin
    {
        /// <summary>
        /// Test if the AI Easy is place
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_AIEasy()
        {
            // Init game
            CustomGame customGame = InitGame();

            // Launch function
            customGame.PlacePenguin();

            bool isPlace = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // If a penguin has been placed
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin)
                    {
                        // bool is set to true
                        isPlace = true;
                    }
                }
            }

            // Test if a penguin has been placed
            Assert.IsTrue(isPlace);
        }

        /// <summary>
        /// Test if the AI Medium is place
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_AIMedium()
        {
            //Init Game
            CustomGame customGame = InitGame();
            // Current player set to the player medium
            customGame.CurrentPlayer = customGame.Players[1];

            // Launch function
            customGame.PlacePenguin();

            bool isPlace = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // If a penguin has been placed
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin)
                    {
                        // bool is set to true
                        isPlace = true;
                    }
                }
            }

            // Test if a penguin has been placed
            Assert.IsTrue(isPlace);
        }

        /// <summary>
        /// Test if the AI Hard is place
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguin_AIHard()
        {
            //Init Game
            CustomGame customGame = InitGame();
            // Current player set to the player hard
            customGame.CurrentPlayer = customGame.Players[2];

            // Launch function
            customGame.PlacePenguin();

            bool isPlace = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // If a penguin has been placed
                    if (customGame.Board.Board[i, j].CellType == CellType.FishWithPenguin)
                    {
                        // bool is set to true
                        isPlace = true;
                    }
                }
            }

            // Test if a penguin has been placed
            Assert.IsTrue(isPlace);
        }

        #region Private Functions

        /// <summary>
        /// Init the game with 3 players (AIEasy, AIMedium, AIHard)
        /// </summary>
        /// <returns>the game with modifications</returns>
        private CustomGame InitGame()
        {
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 3 players
            customGame.AddPlayer("Player1", PlayerType.AIEasy);
            customGame.AddPlayer("Player2", PlayerType.AIMedium);
            customGame.AddPlayer("Player3", PlayerType.AIHard);

            // Launch function
            customGame.StartGame();

            // Set Current Player
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }

        #endregion
    }
}
