using System;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Classes
{
    /// <summary>
    /// Represents a player. Implements IPlayer interface.
    /// </summary>
    public class Player : IPlayer
    {
        /// <summary>
        /// Unique identifier of the player in this game
        /// </summary>
        public Guid Identifier { get; }

        /// <summary>
        /// Information about the user type (human or AI)
        /// </summary>
        public PlayerType PlayerType { get; }

        /// <summary>
        /// Define the color for the player
        /// </summary>
        public PlayerColor Color { get; set; }

        /// <summary>
        /// Name of the current player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current score for this player
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Number of available penguins
        /// </summary>
        public int Penguins { get; set; }

        /// <summary>
        /// Fired when the state has changed (Points, Penguins count ...)
        /// </summary>
        public event EventHandler StateChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerType"></param>
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
