﻿using System;
using System.Collections.Generic;
using System.Linq;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Classes.Move;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.AI
{
    public class AIEasy
    {
        /// <summary>
        /// Plateau
        /// </summary>
        public IBoard Board { get; set; }

        private IRandom random;

        public AIEasy(IBoard board, IRandom random)
        {
            Board = board;
            this.random = random;
        }

        public Coordinates PlacePenguin()
        {
            // Get random x and y
            int randomX = random.Next(0, 8);
            int randomY = random.Next(0, 8);

            // If cell at [x;y] has already a penguin or has more than one fish
            while (Board.Board[randomX, randomY].FishCount != 1 || Board.Board[randomX, randomY].CellType == CellType.FishWithPenguin)
            {
                // Change random x and y
                randomX = random.Next(0, 8);
                randomY = random.Next(0, 8);
            }
            return new Coordinates(randomX,randomY);
        }

        /// <summary>
        /// Choisi aléatoirement la destination du penguin choisi par l'IA
        /// </summary>
        /// <param name="origin">L'origin du penguin choisi</param>
        /// <returns>La destination</returns>
        public Coordinates FindDestination(Coordinates origin)
        {
            Movements move = new Movements(null, null, Board);

            IList<Coordinates> result = move.CheckDeplacement(origin);
            
            result = result.OrderBy(a => Guid.NewGuid()).ToList();

            return result[0];
        }

        /// <summary>
        /// Choisi aléatoirement le penguin que l'IA va déplacer
        /// </summary>
        /// <returns>La position du penguin ou [-1;-1] si l'IA ne peut plus déplacer de penguin</returns>
        public Coordinates FindOrigin(List<Coordinates> possibilitiesOrigin)
        {
            Movements moveOrigin = new Movements(null, null, Board);
            foreach (var possibility in possibilitiesOrigin)
            {
                foreach (var direction in moveOrigin.CanMove(possibility))
                {
                    if (direction.Value == false)
                    {
                        return possibility;
                    }
                }
            }
            return new Coordinates(-1, -1);

        }
    }
}
