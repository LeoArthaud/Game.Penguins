using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.AI
{
    public class Cell : ICell
    {
        public CellType CellType { get; set; }
        public int FishCount { get; }
        public IPenguin CurrentPenguin { get; set; }
        public event EventHandler StateChanged;

        public Cell(int fishCount)
        {
            FishCount = fishCount;
            CellType = CellType.Fish;
        }

        public Cell(int fishCount, CellType cellType, IPenguin currentPenguin)
        {
            FishCount = fishCount;
            CellType = cellType;
            CurrentPenguin = currentPenguin;
        }
    }
}
