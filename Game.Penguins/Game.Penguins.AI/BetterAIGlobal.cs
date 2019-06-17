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
    /// Abstract superclass for other better AI classes.
    /// </summary>
    public abstract class BetterAIGlobal
    {
        /// <summary>
        /// Board
        /// </summary>
        public IBoard Board { get; set; }

        /// <summary>
        /// Current player
        /// </summary>
        public IPlayer CurrentPlayer { get; set; }

        /// <summary>
        /// Allow to generate random number
        /// </summary>
        protected IRandom random;

        public float FishCoefficient { get; set; }

        public float BlockOthersCoefficient { get; set; }

        public float BlockSelfCoefficient { get; set; }

        public List<CellScore> CellScores { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="board"></param>
        /// <param name="currentPlayer"></param>
        protected BetterAIGlobal(IBoard board, IRandom random, IPlayer currentPlayer)
        {
            Board = board;
            this.random = random;
            FishCoefficient = 1;
            CurrentPlayer = currentPlayer;
        }

        public void CheckBoard()
        {
            CellScores = new List<CellScore>();
            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    CellScores.Add(new CellScore(new Coordinates(i, j), 0));
                }
            }

            foreach (CellScore cellScore in CellScores)
            {
                Cell cell = (Cell)Board.Board[cellScore.Cell.X, cellScore.Cell.Y];
                cellScore.Value += cell.FishCount * FishCoefficient;
                //cellScore.Value += blockOthers(cell) * BlockOthersCoefficient;
                //cellScore.Value += blockMyself(cell) * BlockSelfCoefficient;
            }
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
            Player playerCurrent = (Player)CurrentPlayer;
            Movements moveOrigin = new Movements(null, null, Board);
            List<CellScore> possiblePenguins = new List<CellScore>();
            foreach (var possibility in possibilitiesOrigin)
            {
                foreach (var direction in moveOrigin.CanMove(possibility))
                { 
                    if (direction.Value == false)
                    {
                        possiblePenguins.Add(FindBestPenguin(possibility));
                    } 
                }
            }

            possiblePenguins = possiblePenguins.OrderByDescending(cellScore => cellScore.Value).ToList();
            
            if (possiblePenguins.Count != 0)
                return possiblePenguins[0].Cell;

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

            IList<Coordinates> result = move.CheckMove(origin);

            IList<CellScore> possibilities = new List<CellScore>();

            foreach (CellScore cellScore in CellScores)
            {
                foreach (Coordinates destination in result)
                {
                    if (cellScore.Cell.X == destination.X && cellScore.Cell.Y == destination.Y)
                    {
                        possibilities.Add(cellScore);
                    }
                }
            }
            possibilities = possibilities.OrderByDescending(cellScore => cellScore.Value).ToList();

            try
            {
                return possibilities[0].Cell;
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
        /// Get randomly the destination of the penguin
        /// </summary>
        /// <param name="origin">coordinates of the origin</param>
        /// <returns>coordinates of the destination</returns>
        public CellScore FindBestPenguin(Coordinates origin)
        {
            Movements move = new Movements(null, null, Board);

            IList<Coordinates> result = move.CheckMove(origin);

            IList<CellScore> possibilities = new List<CellScore>();

            foreach (CellScore cellScore in CellScores)
            {
                foreach (Coordinates destination in result)
                {
                    if (cellScore.Cell.X == destination.X && cellScore.Cell.Y == destination.Y)
                    {
                        cellScore.Origin = origin;
                        possibilities.Add(cellScore);
                    }
                }
            }
            possibilities = possibilities.OrderByDescending(cellScore => cellScore.Value).ToList();

            try
            {
                return possibilities[0];
            }
            catch (Exception e)
            {
                // Log penguin movement
                ILog log = LogManager.GetLogger(GetType().ToString());
                log.Info("Your penguins can't move : " + e);
                return new CellScore(new Coordinates(-1, -1), -1);
            }
        }
    }
}
