using System;
using System.Text;
using System.Collections.Generic;
using Game.Penguins.Core.CustomGame;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Game.Penguins.Game.UnitTests
{
    /// <summary>
    /// Description résumée pour UnitTestPlacePenguin
    /// </summary>
    [TestClass]
    public class UnitTestPlacePenguin
    {
        [TestMethod]
        public void Test_PlacePenguin_CellStatus()
        {
            // Init game
            CustomGame customGame = new CustomGame();

            // Add 2 players
            Player player1 = new Player("Player1", PlayerType.AIEasy, (PlayerColor)0);
            Player player2 = new Player("Player2", PlayerType.AIEasy, (PlayerColor)1);
            customGame.Players.Add(player1);
            customGame.Players.Add(player2);

            customGame.CountPlayers = 2;

            customGame.StartGame();
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;
            var playerInitial = customGame.Players[0];

            // Position of cell
            /*int x = 0;
            int y = 0;

            Cell cell = (Cell)customGame.Board.Board[x, y];
            cell.FishCount = 1;*/

            // Launch function
            customGame.PlacePenguin();

            // Test si le penguin a bien été placé sur la cellule
            //...
        }
    }
}
