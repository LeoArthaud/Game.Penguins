using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.Actions;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.AI
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomGame : IGame
    {
        /// <summary>
        /// 
        /// </summary>
        public IBoard Board { get; }

        /// <summary>
        /// 
        /// </summary>
        public NextActionType NextAction { get; }

        /// <summary>
        /// 
        /// </summary>
        public IPlayer CurrentPlayer { get; }

        /// <summary>
        /// 
        /// </summary>
        public IList<IPlayer> Players { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler StateChanged;

        public int CountPlayers = 0;

        public CustomGame()
        {
            Players = new List<IPlayer>();
            Board = new GameBoard();
        }

        /// <summary>
        /// Add a player to the list Players
        /// </summary>
        /// <param name="playerName">name of the player</param>
        /// <param name="playerType">type of the player</param>
        public void AddPlayer(string playerName, PlayerType playerType)
        {
            Player player = new Player(playerName, playerType, (PlayerColor)CountPlayers);
            Players.Add(player);
            CountPlayers++;
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartGame()
        {
            int numberPenguins = 0;
            if (CountPlayers == 2)
            {
                numberPenguins = 4;
            }
            else if (CountPlayers == 3)
            {
                numberPenguins = 3;
            }
            else if (CountPlayers == 4)
            {
                numberPenguins = 2;
            }

            foreach (Player player in Players)
            {
                player.Penguins = numberPenguins;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void PlacePinguinManual(int x, int y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlacePinguin()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void MoveManual(IMove action)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
