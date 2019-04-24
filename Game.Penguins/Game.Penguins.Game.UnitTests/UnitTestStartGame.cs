using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.CustomGame;
using Game.Penguins.Core.CustomGame.App;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
            CustomGame customGame = InitGame(2, null);

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
            CustomGame customGame = InitGame(3, null);

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
            CustomGame customGame = InitGame(4, null);

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
            CustomGame customGame = InitGame(2, null);

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
            CustomGame customGame = InitGame(2, null);

            // Verify that the action after StartGame() is PlacePenguin
            Assert.IsTrue(customGame.NextAction == NextActionType.PlacePenguin);
        }

        /// <summary>
        /// Test, in the function StartGame(), if the NextAction is PlacePenguin
        /// </summary>
        [TestMethod]
        public void Test_StartGame_Name()
        {
            // Init game
            CustomGame customGame = InitGame(2, null);

            // Test Name of the players
            Assert.IsTrue(customGame.Players[0].Name == "Player0");
            Assert.IsTrue(customGame.Players[1].Name == "Player1");
        }

        /// <summary>
        /// Test, in the function StartGame(), if the NextAction is PlacePenguin
        /// </summary>
        [TestMethod]
        public void Test_StartGame_Identifier()
        {
            // Init game
            CustomGame customGame = InitGame(2, null);

            // Test Name of the players
            foreach (var player in customGame.Players)
            {
                Assert.IsNotNull(player.Identifier);
            }
        }

        /// <summary>
        /// Test, in the function StartGame(), if the NextAction is PlacePenguin
        /// </summary>
        [TestMethod]
        public void Test_StartGame_PlayerType()
        {
            // Init game
            CustomGame customGame = InitGame(2, null);

            // Test type of the players
            Assert.IsTrue(customGame.Players[0].PlayerType == PlayerType.Human);
            Assert.IsTrue(customGame.Players[1].PlayerType == PlayerType.Human);
        }

        /// <summary>
        /// Test, in the function StartGame(), if the color is random
        /// </summary>
        [TestMethod]
        public void Test_StartGame_Color()
        {
            Mock<IRandom> randomMock = new Mock<IRandom>();
            randomMock.SetupSequence(e => e.Next(0, 4)).Returns(0).Returns(1);

            // Init game
            CustomGame customGame = InitGame(2, randomMock);

            // Verify that the action after StartGame() is PlacePenguin
            Assert.IsTrue(customGame.Players[0].Color == PlayerColor.Blue);
            Assert.IsTrue(customGame.Players[1].Color == PlayerColor.Yellow);
        }
        
        #endregion

        #region Private Functions

        /// <summary>
        /// Init the game
        /// </summary>
        /// <param name="countPlayer">number of players</param>
        /// <returns>game</returns>
        public CustomGame InitGame(int countPlayer, Mock<IRandom> randomMock)
        {
            // Init game
            CustomGame customGame;
            if (randomMock != null)
            {
                customGame = new CustomGame(randomMock.Object);
            }
            else
            {
                customGame = new CustomGame(new AppRandom());
            }

            // Add players
            for (int i = 0; i < countPlayer; i++)
            {
                Player player = new Player("Player"+i, PlayerType.Human);
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
