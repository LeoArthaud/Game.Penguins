using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Game.UnitTests
{
    /// <summary>
    /// Test the function PlacePenguin()
    /// </summary>
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestAddPlayers
    {
        /// <summary>
        /// Test initialization with human player and AI player
        /// </summary>
        [TestMethod]
        public void Test_AddPlayers()
        {
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 2 players
            customGame.AddPlayer("AI", PlayerType.AIEasy);
            customGame.AddPlayer("Human", PlayerType.Human);

            // Tests
            Assert.IsTrue(customGame.Players[0].PlayerType == PlayerType.AIEasy && customGame.Players[0].Name == "AI");
            Assert.IsTrue(customGame.Players[1].PlayerType == PlayerType.Human && customGame.Players[1].Name == "Human");
            Assert.IsTrue(customGame.Players[0].Identifier != customGame.Players[1].Identifier);
        }
    }
}
