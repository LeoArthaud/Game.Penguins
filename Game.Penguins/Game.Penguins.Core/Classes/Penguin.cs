using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Classes
{
    /// <summary>
    /// Represents a penguin belonging to a player. Implements IPenguin interface.
    /// </summary>
    public class Penguin : IPenguin
    {
        public IPlayer Player { get; set; }

        public Penguin(IPlayer player)
        {
            Player = player;
        }
    }
}
