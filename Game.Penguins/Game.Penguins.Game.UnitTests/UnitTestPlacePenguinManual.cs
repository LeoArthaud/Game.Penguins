using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.CustomGame;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Game.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestPlacePenguinManual
    {
        #region Function PlacePenguinManual

        /// <summary>
        /// Test, in the function PlacePenguinManual(), if the type of the cell and the current penguin on the cell is change
        /// </summary>
        [TestMethod]
        public void Test_PlacePenguinManual_CellStatus()
        {
            // Init game
            CustomGame customGame = new CustomGame();

            // Add 2 players
            Player player1 = new Player("Player1", PlayerType.Human, (PlayerColor)0);
            Player player2 = new Player("Player2", PlayerType.Human, (PlayerColor)1);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);

            customGame.CountPlayers = 2;

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;
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
            // Init game
            CustomGame customGame = new CustomGame();

            // Add 2 players
            Player player1 = new Player("Player1", PlayerType.Human, (PlayerColor)0);
            Player player2 = new Player("Player2", PlayerType.Human, (PlayerColor)1);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);

            customGame.CountPlayers = 2;

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

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
            // Init game
            CustomGame customGame = new CustomGame();

            // Add 2 players
            Player player1 = new Player("Player1", PlayerType.Human, (PlayerColor)0);
            Player player2 = new Player("Player2", PlayerType.Human, (PlayerColor)1);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);

            customGame.CountPlayers = 2;

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

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
            // Init game
            CustomGame customGame = new CustomGame();

            // Add 2 players
            Player player1 = new Player("Player1", PlayerType.Human, (PlayerColor)0);
            Player player2 = new Player("Player2", PlayerType.Human, (PlayerColor)1);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);

            customGame.CountPlayers = 2;

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

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
            // Init game
            CustomGame customGame = new CustomGame();

            // Add 2 players
            Player player1 = new Player("Player1", PlayerType.Human, (PlayerColor)0);
            Player player2 = new Player("Player2", PlayerType.Human, (PlayerColor)1);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);

            customGame.CountPlayers = 2;

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            // Position of cell
            int x = 0;
            int y = 0;

            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;

            // Launch function
            while (customGame.CurrentPlayer.Penguins != 0)
            {
                customGame.PlacePenguinManual(x, y);
            }

            Assert.IsTrue(customGame.CurrentPlayer.Penguins == 0);
            Assert.IsTrue(customGame.NextAction == NextActionType.MovePenguin);
        }

        #endregion

    }
}
