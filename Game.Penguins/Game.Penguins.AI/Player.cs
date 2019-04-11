using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.AI
{
    public class Player : IPlayer
    {
        public Guid Identifier { get; }
        public PlayerType PlayerType { get; }
        public PlayerColor Color { get; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int Penguins { get; set; }
        public event EventHandler StateChanged;

        public Player(string playerName, PlayerType playerType, PlayerColor playerColor)
        {
            Identifier = Guid.NewGuid();
            Name = playerName;
            PlayerType = playerType;
            Color = playerColor;
        }
        
    }
}
