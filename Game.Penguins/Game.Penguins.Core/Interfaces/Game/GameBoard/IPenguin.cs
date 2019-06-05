using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Interfaces.Game.GameBoard
{
    /// <summary>
    /// Interface for Penguin objects.
    /// </summary>
    public interface IPenguin
    {
        /// <summary>
        /// Linked player object
        /// </summary>
        IPlayer Player { get; }
    }
}
