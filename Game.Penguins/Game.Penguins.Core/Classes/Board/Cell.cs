using System;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.Classes.Board
{
    /// <summary>
    /// Represents a cell on the board. Implements ICell interface.
    /// </summary>
    public class Cell : ICell
    {
        /// <summary>
        /// Type of the cell
        /// </summary>
        public CellType CellType { get; set; }

        /// <summary>
        /// Number of fish on this cell
        /// (0 by default if it's not a cell with some fish)
        /// </summary>
        public int FishCount { get; set; }

        /// <summary>
        /// Current penguin that stay on the cell
        /// Can be null
        /// </summary>
        public IPenguin CurrentPenguin { get; set; }

        /// <summary>
        /// Fired when the state has changed (Type, FishCount, Penguin, ...)
        /// </summary>
        public event EventHandler StateChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fishCount"></param>
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
