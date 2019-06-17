using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Classes.Move;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Game.UnitTests.Move
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UnitTestGetCoordinates
    {
        /// <summary>
        /// Vérifie que la fonction récupère les bonnes coordonnées d'origine et de destination
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
