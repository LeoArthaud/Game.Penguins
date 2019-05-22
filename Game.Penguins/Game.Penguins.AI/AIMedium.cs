using System;
using System.Collections.Generic;
using System.Linq;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Classes.Move;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.AI
{
    public class AIMedium : IAI
    {
        /// <summary>
        /// Plateau
        /// </summary>
        public IBoard Board { get; set; }

        /// <summary>
        /// Allow to generate random number
        /// </summary>
        private IRandom random;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="board"></param>
        /// <param name="random"></param>
        public AIMedium(IBoard board, IRandom random)
        {
            Board = board;
            this.random = random;
        }

        /// <summary>
        /// Get coordinates where to place the penguin
        /// </summary>
        /// <returns>coordinates</returns>
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
            return new Coordinates(randomX, randomY);
        }

        /// <summary>
        /// Get randomly the penguin to move
        /// </summary>
        /// <param name="possibilitiesOrigin"></param>
        /// <returns>Position of the penguin or [-1;-1] if the AI can't move his penguins</returns>
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

        /// <summary>
        /// Get randomly the destination of the penguin
        /// </summary>
        /// <param name="origin">coordinates of the origin</param>
        /// <returns>coordinates of the destination</returns>
        public Coordinates FindDestination(Coordinates origin)
        {
            Movements move = new Movements(null, null, Board);

            IList<Coordinates> result = move.CheckDeplacement(origin); 

            IList<ICell> resultCells = new List<ICell>();
            foreach (var element in result)
            {
                resultCells.Add(Board.Board[element.X, element.Y]);
            }

            var resultOrderBy = resultCells.OrderByDescending(cell => cell.FishCount).ToList();

            Movements getCells = new Movements(Board.Board[origin.X, origin.Y], resultOrderBy[0], Board);
            var coordinates = getCells.GetCoordinates();

            return coordinates["destination"];
        }

    }
}
