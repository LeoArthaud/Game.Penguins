using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Classes.Move;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
namespace Game.Penguins.AI
{
    /// <summary>
    /// Abstract superclass for other AI classes. Implements IAI interface.
    /// </summary>
    public abstract class AIGlobal : IAI
    {
        /// <summary>
        /// Board
        /// </summary>
        public IBoard Board { get; set; }

        /// <summary>
        /// Allow to generate random number
        /// </summary>
        protected IRandom random;

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
        protected AIGlobal(IBoard board, IRandom random, IPlayer currentPlayer)
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
        /// <param name="origin">coordinates of the origin</param>
        /// <returns>coordinates of the destination</returns>
        public Coordinates FindDestination(Coordinates origin)
        {
            Movements move = new Movements(null, null, Board);

            IList<Coordinates> result = move.CheckMove(origin);

            // Shuffle
            result = result.OrderBy(a => Guid.NewGuid()).ToList();

            try
            {
                return result[0];
            }
            catch (Exception e)
            {
                // Log penguin movement
                ILog log = LogManager.GetLogger(GetType().ToString());
                log.Info("Your penguins can't move : " + e);
                return new Coordinates(-1, -1);
            }
        }

        /// <summary>
        /// Get randomly the penguin to move
        /// </summary>
        /// <param name="possibilitiesOrigin"></param>
        /// <returns>Position of the penguin or [-1;-1] if the AI can't move his penguins</returns>
        public Coordinates FindOrigin(List<Coordinates> possibilitiesOrigin)
        {
            Player playerCurrent = (Player)CurrentPlayer;
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
            return new Coordinates(-1, -1);

        }
    }
}
