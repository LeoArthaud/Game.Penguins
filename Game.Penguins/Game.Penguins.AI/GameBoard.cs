using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.AI
{
    public class GameBoard : IBoard
    {
        public ICell[,] Board { get; }

        public GameBoard()
        {
            Board = new ICell[8,8];
            List<ICell> listCells = new List<ICell>();

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
            var randomListCells = listCells.OrderBy(a => Guid.NewGuid()).ToList();
        }
    }
}
