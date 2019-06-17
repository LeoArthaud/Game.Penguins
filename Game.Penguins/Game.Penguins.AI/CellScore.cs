using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.AI
{
    public class CellScore
    {
        public Coordinates Cell { get; set; }
        public float Value { get; set; }

        public Coordinates Origin { get; set; }

        public CellScore(Coordinates cell, float value)
        {
            Cell = cell;
            Value = value;
        }

        public CellScore(Coordinates cell, float value, Coordinates origin)
        {
            Cell = cell;
            Value = value;
            Origin = origin;
        }
    }
}
