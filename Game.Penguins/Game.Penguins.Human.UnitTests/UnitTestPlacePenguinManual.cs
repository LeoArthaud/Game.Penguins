using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Human.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestPlacePenguinManual : GlobalFunctions
    {
        #region Public Functions

        /// <summary>
        /// Test, in the function PlacePenguinManual(), if the type of the cell and the current penguin on the cell is change
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_CellStatus()
        {
            // Init
            CustomGame customGame = LaunchFunction();

            // Tests
            Assert.IsTrue(customGame.Board.Board[0, 0].CellType == CellType.FishWithPenguin);
            Assert.IsTrue(customGame.Board.Board[0, 0].CurrentPenguin.Player == customGame.Players[0]);
        }

        /// <summary>
        /// Test, in the function PlacePenguinManual(), if the number of penguins decrease when a player place a penguin on the board
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_NumberPenguinDecrease()
        {
            // Init
            CustomGame customGame = LaunchFunction();

            // Test if decrease of number penguins work
            Assert.IsTrue(customGame.Players[0].Penguins == 3);
        }

        /// <summary>
        /// Test, in the function PlacePenguinManual(), if the CurrentPlayer is changed at the end of the function
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_ChangeCurrentPlayer()
        {
            // Init
            CustomGame customGame = LaunchFunction();

            // Test if CurrentPlayer is changed
            Assert.IsTrue(customGame.CurrentPlayer == customGame.Players[1]);
        }

        /// <summary>
        /// Test, in the function PlacePenguinManual(), if the CurrentPlayer doesn't have all his penguins on the board, the NextAction is to PlacePenguin
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_NextActionPlace()
        {
            // Init
            CustomGame customGame = LaunchFunction();

            Assert.IsTrue(customGame.NextAction == NextActionType.PlacePenguin);
        }

        /// <summary>
        /// Test, in the function PlacePenguinManual(), if the CurrentPlayer has all his penguins on the board, the NextAction is to MovePenguin
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_NextActionMove()
        {
            CustomGame customGame = InitGame();

            // Position of cells
            int[,] arrayCoordinates = new int[8,3];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arrayCoordinates[i, j] = i;
                }
                Cell cell = (Cell)customGame.Board.Board[arrayCoordinates[i, 0], arrayCoordinates[i, 1]];
                cell.FishCount = 1;
            }
            
            // Launch function
            var count = 0;
            while (customGame.CurrentPlayer.Penguins != 0)
            {
                customGame.PlacePenguinManual(arrayCoordinates[count, 0], arrayCoordinates[count, 1]);
                count++;
            }

            // Tests
            Assert.IsTrue(customGame.CurrentPlayer.Penguins == 0);
            Assert.IsTrue(customGame.NextAction == NextActionType.MovePenguin);
        }

        /// <summary>
        /// Test, in the function PlacePenguinManual()
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_FishCount()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int x = 0;
            int y = 0;

            for(int i = 2; i< 4; i++)
            {
                Cell cell = (Cell)customGame.Board.Board[x, y];
                cell.FishCount = i;

                customGame.PlacePenguinManual(x, y);

                Assert.IsTrue(cell.CurrentPenguin == null);
            }

        }

        /// <summary>
        /// Test placement if penguin already exist on cell
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_OnCellType_FishWithPenguin()
        {
            //Init game
            CustomGame customGame = InitGame();

            // Position of origin cell
            int x = 0;
            int y = 0;

            // Init Cell with Penguin
            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.CellType = CellType.FishWithPenguin;

            // Launch function
            customGame.PlacePenguinManual(x, y);

            // Test
            Assert.IsTrue(cell.CurrentPenguin == null);

        }
        #endregion

        #region Private Functions

        /// <summary>
        /// Set cell and launch function PlacePenguinManual
        /// </summary>
        private CustomGame LaunchFunction()
        {
            //Init Game
            CustomGame customGame = InitGame();
            
            // Set cell
            Cell cell = (Cell)customGame.Board.Board[0, 0];
            cell.FishCount = 1;

            // Launch function
            customGame.PlacePenguinManual(0, 0);

            return customGame;
        }

        #endregion
    }
}
