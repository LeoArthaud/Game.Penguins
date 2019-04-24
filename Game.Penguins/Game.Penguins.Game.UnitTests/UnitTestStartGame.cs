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
            CustomGame customGame = InitGame(2);

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
            CustomGame customGame = InitGame(3);

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
            CustomGame customGame = InitGame(4);

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
            CustomGame customGame = InitGame(2);

            // Verify if currentplayer is assign 
            Assert.IsTrue(customGame.CurrentPlayer == customGame.Players[0] || customGame.CurrentPlayer == customGame.Players[1]);
        }

        /// <summary>
        /// Test, in the function StartGame(), if the NextAction is PlacePenguin
        /// </summary>
        [TestMethod]
        public void Test_StartGame_NextActionPlacePenguin()
        {
            // Init game
            CustomGame customGame = InitGame(2);

            // Verify that the action after StartGame() is PlacePenguin
            Assert.IsTrue(customGame.NextAction == NextActionType.PlacePenguin);
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// Init the game
        /// </summary>
        /// <param name="countPlayer">number of players</param>
        /// <returns>game</returns>
        public CustomGame InitGame(int countPlayer)
        {
            // Init game
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add players
            for (int i = 0; i < countPlayer; i++)
            {
                Player player = new Player("Player"+countPlayer, PlayerType.Human);
                customGame.Players.Add(player);
            }
            customGame.CountPlayers = countPlayer;

            // Launch function
            customGame.StartGame();

            return customGame;
        }

        #endregion
    }
}
