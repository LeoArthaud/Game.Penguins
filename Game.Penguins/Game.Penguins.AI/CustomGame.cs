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
        public IList<IPlayer> Players { get; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler StateChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerType"></param>
        public void AddPlayer(string playerName, PlayerType playerType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartGame()
        {
            throw new NotImplementedException();
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
