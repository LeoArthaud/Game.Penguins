using System;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.CustomGame.Board
{
    public class Cell : ICell
    {
        public CellType CellType { get; set; }
        public int FishCount { get; set; }
        public IPenguin CurrentPenguin { get; set; }
        public event EventHandler StateChanged;

        public Cell(int fishCount)
        {
            FishCount = fishCount;
            CellType = CellType.Fish;
        }
        
        public void ChangeState()
        {
            StateChanged?.Invoke(this, null);
        }
    }
}
