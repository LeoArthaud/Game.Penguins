using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Classes.Move;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Game.UnitTests.Move
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestGetCoordinates : GlobalFunction
    {
        /// <summary>
        /// Check if the function get the right coordinates of origin and destination of cells
        /// </summary>
        [TestMethod]
        public void Test_GetCoordinates_GetOriginDestination()
        {
            // Init Game
            CustomGame customGame = InitGame();

            // Position of cell
            int xOrigin = 0;
            int yOrigin = 0;

            // Set 
            Cell cellOrigin = (Cell)customGame.Board.Board[xOrigin, yOrigin];
            cellOrigin.FishCount = 1;

            // Position of cell
            int xDestination = 1;
            int yDestination = 1;

            // Set Destination 
            Cell cellDestination = (Cell)customGame.Board.Board[xDestination, yDestination];
            cellDestination.FishCount = 3;

            // Launch function
            Movements move = new Movements(cellOrigin, cellDestination, customGame.Board);
            var result = move.GetCoordinates();

            // Tests
            Assert.IsTrue(result["origin"].X == xOrigin);
            Assert.IsTrue(result["origin"].Y == yOrigin);
            Assert.IsTrue(result["destination"].X == xDestination);
            Assert.IsTrue(result["destination"].Y == yDestination);
            
        }

    }
}
