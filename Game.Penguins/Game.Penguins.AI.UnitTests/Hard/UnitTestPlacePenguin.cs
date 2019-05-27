using System;
using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Game.Penguins.AI.UnitTests.Hard
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestPlacePenguin
    {
        [TestMethod]
        public void Test_PlacePenguin_isNear()
        {
            CustomGame customGame = InitGame();
            customGame.PlacePenguin();

            AIHard aiHard = new AIHard(customGame.Board, new AppRandom(), customGame.CurrentPlayer );

            aiHard.ThreePoints = aiHard.getFishNear(aiHard.getThreePoints());

            bool isNear = false;

            if (aiHard.ThreePoints.Count != 0)
            {
                isNear = true;
            }

            Assert.IsTrue(isNear);
        }

        #region Private Functions

        public CustomGame InitGame()
        {
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 2 IA hard
            customGame.AddPlayer("Player1", PlayerType.AIHard);
            customGame.AddPlayer("Player2", PlayerType.AIHard);

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }

        #endregion
    }
}
