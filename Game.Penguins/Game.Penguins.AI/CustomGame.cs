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
        public IBoard Board { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public NextActionType NextAction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IPlayer CurrentPlayer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<IPlayer> Players { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler StateChanged;

        /// <summary>
        /// 
        /// </summary>
        public int CountPlayers = 0;

        public CustomGame()
        {
            Players = new List<IPlayer>();
            Board = new GameBoard();
        }
        
        /// <summary>
        /// Add player to list Players
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerType"></param>
        /// <returns>return player which is add</returns>
        IPlayer IGame.AddPlayer(string playerName, PlayerType playerType)
        {
            Player player = new Player(playerName, playerType, (PlayerColor)CountPlayers);
            Players.Add(player);
            CountPlayers++;
            return player;
        }

        /// <summary>
        /// COunt the number of penguin of each player
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
            
            CurrentPlayer = Players[0];
            Console.WriteLine("il y a " + Players.Count + "joueurs");
            NextAction = NextActionType.PlacePenguin;

            EventArgs e = new EventArgs();
            StateChanged(NextAction, e);

        }

        public void PlacePenguinManual(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void PlacePenguin()
        {
            throw new NotImplementedException();
        }

        public void MoveManual(ICell origin, ICell destination)
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
