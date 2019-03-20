using System;
using System.Collections.Generic;
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
        }
    }
}
