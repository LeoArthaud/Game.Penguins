using System.Collections.Generic;
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
    public class UnitTestCustomGameConstructor
    {
        #region Function CustomGame

        /// <summary>
        /// Test if the constructor CustomGame() initialize correctly the Board and the Players list
        /// </summary>
        [TestMethod]
        public void Test_CustomGame_Initialize()
        {
            // Init game
            CustomGame customGame = new CustomGame(new AppRandom());

            // Tests if Players and Board are initialized
            Assert.IsFalse(customGame.Players == default(IList<IPlayer>));
            Assert.IsFalse(customGame.Board == default(IBoard));
        }
        
        #endregion
    }
}
