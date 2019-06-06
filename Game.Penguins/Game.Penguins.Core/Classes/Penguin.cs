using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Classes
{
    /// <summary>
    /// Represents a penguin belonging to a player. Implements IPenguin interface.
    /// </summary>
    public class Penguin : IPenguin
    {
        /// <summary>
        /// Player
        /// </summary>
        public IPlayer Player { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="player"></param>
        public Penguin(IPlayer player)
        {
            Player = player;
        }
    }
}
