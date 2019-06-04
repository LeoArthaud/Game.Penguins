using System;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Classes
{
    /// <summary>
    /// Player class.
    /// </summary>
    public class Player : IPlayer
    {
        public Guid Identifier { get; }
        public PlayerType PlayerType { get; }
        public PlayerColor Color { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int Penguins { get; set; }
        public event EventHandler StateChanged;

        public Player(string playerName, PlayerType playerType)
        {
            Identifier = Guid.NewGuid();
            Name = playerName;
            PlayerType = playerType;
        }

        public void ChangeState()
        {
            StateChanged?.Invoke(this, null);
        }

    }
}
