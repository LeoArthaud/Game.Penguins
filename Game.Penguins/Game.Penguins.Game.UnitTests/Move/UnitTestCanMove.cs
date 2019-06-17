using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Classes.Move;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Game.UnitTests.Move
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestCanMove : GlobalFunction
    {
        #region Public Functions

        /// <summary>
        /// Test in the function CanMove(), if all the possibilities are tests
        /// </summary>
        [TestMethod]
        public void Test_CanMove()
        {
            // Init Game
            CustomGame customGame = InitGame();

            // launch move function
            Movements move = new Movements(null, null, customGame.Board);
            var result = move.CanMove(new Coordinates(4, 4));

            // Tests
            Assert.IsFalse(result[DirectionType.Right]);
            Assert.IsFalse(result[DirectionType.Left]);
            Assert.IsFalse(result[DirectionType.DownRight]);
            Assert.IsFalse(result[DirectionType.DownLeft]);
            Assert.IsFalse(result[DirectionType.UpRight]);
            Assert.IsFalse(result[DirectionType.UpLeft]);


            result = move.CanMove(new Coordinates(3, 5));

            // Tests
            Assert.IsFalse(result[DirectionType.Right]);
            Assert.IsFalse(result[DirectionType.Left]);
            Assert.IsFalse(result[DirectionType.DownRight]);
            Assert.IsFalse(result[DirectionType.DownLeft]);
            Assert.IsFalse(result[DirectionType.UpRight]);
            Assert.IsFalse(result[DirectionType.UpLeft]);
        }

        #endregion

    }
}
