using System;
using System.Collections.Generic;
using System.Linq;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Classes.Move;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.AI
{
    public class AIHard
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
        /// Positions of penguins of current player
        /// </summary>
        public List<Coordinates> PossibilitiesOrigin { get; set; }

        /// <summary>
        /// Current player
        /// </summary>
        public IPlayer CurrentPlayer { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="board"></param>
        /// <param name="random"></param>
        /// <param name="currentPlayer"></param>
        public AIHard(IBoard board, IRandom random, IPlayer currentPlayer)
        {
            Board = board;
            this.random = random;
            CurrentPlayer = currentPlayer;
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
        /// Get randomly the destination of the penguin
        /// </summary>
        /// <returns>coordinates of the destination</returns>
        public Dictionary<string, Coordinates> FindOriginDestination()
        {
            Movements move = new Movements(null, null, Board);
            Player playerCurrent = (Player)CurrentPlayer;

            PossibilitiesOfOrigin();
            Dictionary<Coordinates, ICell> bestCells = new Dictionary<Coordinates, ICell>();

            foreach (var possibility in PossibilitiesOrigin)
            {
                var list = move.CheckDeplacement(possibility);
                IList<ICell> resultCells = new List<ICell>();

                foreach (var element in list)
                {
                    resultCells.Add(Board.Board[element.X, element.Y]);
                }
                if (resultCells.Count != 0)
                {
                    var resultOrderBy = resultCells.OrderByDescending(cell => cell.FishCount).ToList();
                    bestCells.Add(possibility, resultOrderBy[0]);
                }
                else
                {
                    // Get cell
                    Cell cell = (Cell)Board.Board[possibility.X, possibility.Y];

                    // Add to the player number of point of the cell
                    playerCurrent.Points += cell.FishCount;

                    // Cell become water
                    cell.CellType = CellType.Water;

                    // Cell have no fish
                    cell.FishCount = 0;

                    // Cell have no penguin
                    cell.CurrentPenguin = null;

                    // Apply change
                    cell.ChangeState();
                }
            }


            Coordinates greatOrigin = new Coordinates(0, 0);
            ICell greatDestination = new Cell(0);
            foreach (var couple in bestCells)
            {
                if (couple.Value.FishCount > greatDestination.FishCount)
                {
                    greatDestination = couple.Value;
                    greatOrigin = couple.Key;
                }
            }

            Movements getCells = new Movements(Board.Board[greatOrigin.X, greatOrigin.Y], greatDestination, Board);
            var coordinates = getCells.GetCoordinates();

            return coordinates;
        }

        /// <summary>
        /// Get current player penguins
        /// </summary>
        private void PossibilitiesOfOrigin()
        {
            PossibilitiesOrigin = new List<Coordinates>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // If a penguin is on the cell
                    if (Board.Board[i, j].CurrentPenguin != null)
                    {
                        // If the penguin belongs to the current player
                        if (Board.Board[i, j].CurrentPenguin.Player == CurrentPlayer)
                        {
                            PossibilitiesOrigin.Add(new Coordinates(i, j));
                        }
                    }
                }
            }
            // Mixed the list
            PossibilitiesOrigin = PossibilitiesOrigin.OrderBy(a => Guid.NewGuid()).ToList();
        }

    }
}
