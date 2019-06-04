using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Human.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestPlacePenguinManual
    {
        #region Public Functions

        /// <summary>
        /// Test, in the function PlacePenguinManual(), if the type of the cell and the current penguin on the cell is change
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_CellStatus()
        {
            //Init Game
            CustomGame customGame = InitGame();

            var playerInitial = customGame.Players[0];

            // Position of cell
            int x = 0;
            int y = 0;

            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;

            // Launch function
            customGame.PlacePenguinManual(x, y);

            // Test si le penguin a bien été placé sur la cellule
            Assert.IsTrue(customGame.Board.Board[x, y].CellType == CellType.FishWithPenguin);
            Assert.IsTrue(customGame.Board.Board[x, y].CurrentPenguin.Player == playerInitial);
        }

        /// <summary>
        /// Test, in the function PlacePenguinManual(), if the number of penguins decrease when a player place a penguin on the board
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_NumberPenguinDecrease()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int x = 0;
            int y = 0;

            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;

            // Launch function
            customGame.PlacePenguinManual(x, y);

            // Test if decrease of number penguins work
            Assert.IsTrue(customGame.Players[0].Penguins == 3);
        }

        /// <summary>
        /// Test, in the function PlacePenguinManual(), if the CurrentPlayer is changed at the end of the function
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_ChangeCurrentPlayer()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int x = 0;
            int y = 0;

            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;

            // Launch function
            customGame.PlacePenguinManual(x, y);

            // Test if CurrentPlayer is changed
            Assert.IsTrue(customGame.CurrentPlayer == customGame.Players[1]);
        }

        /// <summary>
        /// Test, in the function PlacePenguinManual(), if the CurrentPlayer doesn't have all his penguins on the board, the NextAction is to PlacePenguin
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_NextActionPlace()
        {
            CustomGame customGame = InitGame();

            // Position of cell
            int x = 0;
            int y = 0;

            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;

            // Launch function
            customGame.PlacePenguinManual(x, y);

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
        /// Test placement if penguin already existe on placement cell
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
