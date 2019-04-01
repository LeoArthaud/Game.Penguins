using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.AI
{
    public class Cell : ICell
    {
        public CellType CellType { get; }
        public int FishCount { get; }
        public IPenguin CurrentPenguin { get; }
        public event EventHandler StateChanged;

        public Cell(int fishCount)
        {
            FishCount = fishCount;
            CellType = CellType.Fish;
        }
    }
}
