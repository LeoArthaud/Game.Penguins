using System;
using System.Collections.Generic;
using System.Linq;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.Classes.Board
{
    /// <summary>
    /// Represents the game board. Implements IBoard interface.
    /// </summary>
    public class GameBoard : IBoard
    {
        /// <summary>
        /// Board
        /// </summary>
        public ICell[,] Board { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GameBoard()
        {
            // Board initialization
            Board = new ICell[8,8];

            // Initialization of the list that will allow the shuffling of cells
            List<Cell> listCells = new List<Cell>();

            // Add 3 types of cells to list (1 fish, 2 fishes, 3 fishes)
            for (int i = 0; i < 34; i++)
            {
                listCells.Add(new Cell(1));
            }
            for (int i = 0; i < 20; i++)
            {
                listCells.Add(new Cell(2));
            }
            for (int i = 0; i < 10; i++)
            {
                listCells.Add(new Cell(3));
            }

            // Shuffle list
            var randomListCells = listCells.OrderBy(a => Guid.NewGuid()).ToList();

            // Add 64 cells to the board
            int index = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Board[i, j] = randomListCells[index];
                    index++;
                }
            }
        }
    }
}
