using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.Actions;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.AI
{
    public class CustomGame : IGame
    {
        public IBoard Board { get; }
        public NextActionType NextAction { get; }
        public IPlayer CurrentPlayer { get; }
        public IList<IPlayer> Players { get; }
        public event EventHandler StateChanged;
        public void AddPlayer(string playerName, PlayerType playerType)
        {
            throw new NotImplementedException();
        }

        public void StartGame()
        {
            throw new NotImplementedException();
        }

        public void PlacePinguinManual(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void PlacePinguin()
        {
            throw new NotImplementedException();
        }

        public void MoveManual(IMove action)
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
