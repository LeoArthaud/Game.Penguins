using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.CustomGame;
using Game.Penguins.Core.CustomGame.App;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Game.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestStartGame
    {
        #region Function StartGame

        /// <summary>
        /// Test, in the function StartGame(), if there are 2 players, the number of penguins foreach player is 4
        /// </summary>
        [TestMethod]
        public void Test_StartGame_NumberPenguins2Players()
        {
            // Init game
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 2 players
            Player player1 = new Player("Player1", PlayerType.Human);
            Player player2 = new Player("Player2", PlayerType.Human);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);

            customGame.CountPlayers = 2;

            customGame.StartGame();

            // Verify number of penguins foreach player 
            foreach (var player in customGame.Players)
            {
                Assert.IsTrue(player.Penguins == 4);
            }
        }

        /// <summary>
        /// Test, in the function StartGame(), if there are 3 players, the number of penguins foreach player is 3
        /// </summary>
        [TestMethod]
        public void Test_StartGame_NumberPenguins3Players()
        {
            // Init game
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 3 players
            Player player1 = new Player("Player1", PlayerType.Human);
            Player player2 = new Player("Player2", PlayerType.Human);
            Player player3 = new Player("Player3", PlayerType.Human);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);
            customGame.Players.Add(player3);

            customGame.CountPlayers = 3;

            customGame.StartGame();

            // Verify number of penguins foreach player 
            foreach (var player in customGame.Players)
            {
                Assert.IsTrue(player.Penguins == 3);
            }
        }

        /// <summary>
        /// Test, in the function StartGame(), if there are 4 players, the number of penguins foreach player is 2
        /// </summary>
        [TestMethod]
        public void Test_StartGame_NumberPenguins4Players()
        {
            // Init game
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 4 players
            Player player1 = new Player("Player1", PlayerType.Human);
            Player player2 = new Player("Player2", PlayerType.Human);
            Player player3 = new Player("Player3", PlayerType.Human);
            Player player4 = new Player("Player4", PlayerType.Human);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);
            customGame.Players.Add(player3);
            customGame.Players.Add(player4);

            customGame.CountPlayers = 4;

            // Launch function
            customGame.StartGame();

            // Verify number of penguins foreach player 
            foreach (var player in customGame.Players)
            {
                Assert.IsTrue(player.Penguins == 2);
            }
        }

        /// <summary>
        /// Test, in the function StartGame(), if the current player is assign randomly
        /// </summary>
        [TestMethod]
        public void Test_StartGame_CurrentPlayer()
        {
            // Init game
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add  players
            Player player1 = new Player("Player1", PlayerType.Human);
            Player player2 = new Player("Player2", PlayerType.Human);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);

            customGame.CountPlayers = 2;

            // Launch function
            customGame.StartGame();

            // Verify if currentplayer is assign 
            Assert.IsTrue(customGame.CurrentPlayer == player1 || customGame.CurrentPlayer == player2);
        }

        /// <summary>
        /// Test, in the function StartGame(), if the NextAction is PlacePenguin
        /// </summary>
        [TestMethod]
        public void Test_StartGame_NextActionPlacePenguin()
        {
            // Init game
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 4 players
            Player player1 = new Player("Player1", PlayerType.Human);
            Player player2 = new Player("Player2", PlayerType.Human);
            Player player3 = new Player("Player3", PlayerType.Human);
            Player player4 = new Player("Player4", PlayerType.Human);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);
            customGame.Players.Add(player3);
            customGame.Players.Add(player4);

            customGame.CountPlayers = 4;

            // Launch function
            customGame.StartGame();

            // Verify that the action after StartGame() is PlacePenguin
            Assert.IsTrue(customGame.NextAction == NextActionType.PlacePenguin);
        }

        #endregion

    }
}
