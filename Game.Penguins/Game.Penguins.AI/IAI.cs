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
        IBoard Board { get; set; }

        IPlayer CurrentPlayer { get; set; }

        Coordinates PlacePenguin();

        Coordinates FindDestination(Coordinates origin);

        Coordinates FindOrigin(List<Coordinates> possibilitiesOrigin);
    }
}
