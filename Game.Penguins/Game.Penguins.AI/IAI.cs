using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.AI
{
    public interface IAI
    {
        IBoard Board { get; set; }

        Coordinates PlacePenguin();

        Coordinates FindDestination(Coordinates origin);

        Coordinates FindOrigin(List<Coordinates> possibilitiesOrigin);
    }
}
