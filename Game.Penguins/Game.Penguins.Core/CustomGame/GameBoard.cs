using System;
using System.Collections.Generic;
using System.Linq;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.CustomGame
{
    public class GameBoard : IBoard
    {
        public ICell[,] Board { get; set; }

        public GameBoard()
        {
            // Initialisation du tableau
            Board = new Cell[8,8];

            // Initialisation de la liste qui va permettre de mélanger les cases
            List<Cell> listCells = new List<Cell>();

            //Envoie dans la liste des 3 types de cases (1 poisson, 2 poissons et 3 poissons)
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

            // Mélange de la liste
            var randomListCells = listCells.OrderBy(a => Guid.NewGuid()).ToList();

            // Envoie des 64 cases dans le tableau
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
