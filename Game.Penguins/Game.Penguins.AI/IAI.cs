using System.Collections.Generic;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.AI
{
    /// <summary>
    /// Interface for AI classes
    /// </summary>
    public interface IAI
    {
        /// <summary>
        /// Board
        /// </summary>
        IBoard Board { get; set; }

        /// <summary>
        /// Current player
        /// </summary>
        IPlayer CurrentPlayer { get; set; }

        /// <summary>
        /// Get coordinates where to place the penguin
        /// </summary>
        /// <returns>coordinates</returns>
        Coordinates PlacePenguin();

        /// <summary>
        /// Get randomly the destination of the penguin
        /// </summary>
        /// <param name="origin">coordinates of the origin</param>
        /// <returns>coordinates of the destination</returns>
        Coordinates FindDestination(Coordinates origin);

        /// <summary>
        /// Get randomly the penguin to move
        /// </summary>
        /// <param name="possibilitiesOrigin"></param>
        /// <returns>Position of the penguin or [-1;-1] if the AI can't move his penguins</returns>
        Coordinates FindOrigin(List<Coordinates> possibilitiesOrigin);
    }
}
