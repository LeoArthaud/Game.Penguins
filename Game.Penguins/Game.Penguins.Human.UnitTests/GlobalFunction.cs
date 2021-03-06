﻿using System.Diagnostics.CodeAnalysis;
using Game.Penguins.Core.Classes.App;
using Game.Penguins.Core.Interfaces.Game.Players;
using Game.Penguins.Helper.CustomGame;

namespace Game.Penguins.Human.UnitTests
{
    [ExcludeFromCodeCoverage]
    public abstract class GlobalFunction
    {
        /// <summary>
        /// Init the game with 2 players human
        /// </summary>
        /// <returns>game</returns>
        public CustomGame InitGame()
        {
            // Init game
            CustomGame customGame = new CustomGame(new AppRandom());

            // Add 2 players
            customGame.AddPlayer("Player1", PlayerType.Human);
            customGame.AddPlayer("Player2", PlayerType.Human);

            // Launch function
            customGame.StartGame();

            // Set current player
            customGame.CurrentPlayer = customGame.Players[0];
            customGame.IdPlayer = 0;

            return customGame;
        }
    }
}
