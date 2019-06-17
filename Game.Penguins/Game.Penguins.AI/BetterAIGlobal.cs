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
    /// Abstract superclass for other better AI classes. Implements IBetterAI interface.
    /// </summary>
    public abstract class BetterAIGlobal : IBetterAI
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

        /// <summary>
        /// Coefficient of FishCount for CellScores.
        /// </summary>
        public float FishCoefficient { get; set; }

        /// <summary>
        /// Coefficient of cells blocking others for CellScores.
        /// </summary>
        public float BlockOthersCoefficient { get; set; }

        /// <summary>
        /// Coefficient of cells blocking self for CellScores.
        /// </summary>
        public float BlockSelfCoefficient { get; set; }

        /// <summary>
        /// List of CellScores to keep track of best cells.
        /// </summary>
        public List<CellScore> CellScores { get; set; }

        /// <summary>
        /// Positions of penguins of current player
        /// </summary>
        public List<Coordinates> PossibilitiesOrigin { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="board"></param>
        /// <param name="random"></param>
        /// <param name="currentPlayer"></param>
        protected BetterAIGlobal(IBoard board, IRandom random, IPlayer currentPlayer)
        {
            Board = board;
            this.random = random;
            CurrentPlayer = currentPlayer;
        }

        /// <summary>
        /// Check and update CellScores values (= condition1 score*coefficient + condition2 score*coefficient + ...).
        /// </summary>
        public void CheckBoard()
        {
            // Initialize CellScores
            CellScores = new List<CellScore>();
            
            // Add a CellScore for each Cell on the Board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    CellScores.Add(new CellScore(new Coordinates(i, j), 0));
                }
            }

            // Loop through CellScores to calculate their value
            foreach (CellScore cellScore in CellScores)
            {
                Cell cell = (Cell)Board.Board[cellScore.Cell.X, cellScore.Cell.Y]; // Get corresponding cell to get its FishCount
                cellScore.Value += cell.FishCount * FishCoefficient; // Add FishCount score to individual cellScore
                // TODO: Implement other conditions
                // TODO: Change FishCount use in AIEasy (ex: doesn't care about the FishCount of cells)
                //cellScore.Value += blockOthers(cell) * BlockOthersCoefficient;
                //cellScore.Value += blockMyself(cell) * BlockSelfCoefficient;
            }

            CheckBlockOther(); // Add BlockOthersScore to every cellScore
            // CheckBlockSelf() is called when searching for a destination
        }

        /// <summary>
        /// Get coordinates where to place the penguin
        /// </summary>
        /// <returns>Coordinates</returns>
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
        /// Get the best penguin to move from a list of origins
        /// </summary>
        /// <param name="possibilitiesOrigin">List of penguin origins</param>
        /// <returns>Position of the penguin or [-1;-1] if the AI can't move his penguins</returns>
        public Coordinates FindOrigin(List<Coordinates> possibilitiesOrigin)
        {
            //CheckBoard(); // TODO: Delete if wrong
            // List of cell-scores of 1 possible destination per penguin
            List<CellScore> possiblePenguins = new List<CellScore>();

            // Find best destination cell-score for each penguin
            foreach (var possibility in possibilitiesOrigin)
            {
                possiblePenguins.Add(FindBestCellScore(possibility));
            }

            // Order by descending cell-score value
            possiblePenguins = possiblePenguins.OrderByDescending(cellScore => cellScore.Value).ToList();
            
            // Return origin of penguin that can reach the best destination cell-score
            if (possiblePenguins.Count != 0)
            {
                return possiblePenguins[0].Origin;
            }
            /*
            else
            {
                return possibilitiesOrigin[0];
            }
            */

            return new Coordinates(-1, -1);
        }

        /// <summary>
        /// Find the best destination available for a penguin from its origin.
        /// </summary>
        /// <param name="origin">Coordinates of the origin</param>
        /// <returns>Coordinates of the best destination</returns>
        public Coordinates FindDestination(Coordinates origin)
        {
            //CheckBoard(); // TODO: Delete if wrong
            // Singleton to access Movements functions (CheckMove() in this case)
            Movements move = new Movements(null, null, Board);

            // Check possible moves
            IList<Coordinates> result = move.CheckMove(origin);

            // Possible CellScores
            IList<CellScore> possibilities = new List<CellScore>();

            // Check if CellScores are in the possible moves
            foreach (CellScore cellScore in CellScores)
            {
                foreach (Coordinates destination in result)
                {
                    if (cellScore.Cell.X == destination.X && cellScore.Cell.Y == destination.Y)
                    {
                        CheckBlockSelf(origin, cellScore); // Update self-blocking value
                        possibilities.Add(cellScore); // Add them to possibilities if so
                    }
                }
            }

            // Order possibilities by descending value (= "cell score")
            possibilities = possibilities.OrderByDescending(cellScore => cellScore.Value).ToList();

            try
            {
                /*if (possibilities[0].Cell == new Coordinates(-1, -1))
                    return result[0];*/

                // Return best coordinates
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
        /// Get the best CellScore a penguin can reach from its origin. Used for choosing the best penguin to use in FindOrigin().
        /// </summary>
        /// <param name="origin">Coordinates of the origin of the penguin</param>
        /// <returns>Best CellScore reachable from the selected penguin</returns>
        public CellScore FindBestCellScore(Coordinates origin)
        {
            // Movements singleton to access CheckMove()
            Movements move = new Movements(null, null, Board);

            // Possible moves
            IList<Coordinates> result = move.CheckMove(origin);

            // Possible destination cell-scores
            IList<CellScore> possibilities = new List<CellScore>();

            foreach (CellScore cellScore in CellScores)
            {
                foreach (Coordinates destination in result)
                {
                    if (cellScore.Cell.X == destination.X && cellScore.Cell.Y == destination.Y)
                    {
                        CheckBlockSelf(origin, cellScore); // Update self-blocking value
                        cellScore.Origin = origin; // Keep the origin of the penguin that can reach this destination in the cell-score
                        possibilities.Add(cellScore); // Add destination cell-score to possibilities
                    }
                }
            }

            // Sort by descending value
            possibilities = possibilities.OrderByDescending(cellScore => cellScore.Value).ToList();

            try
            {
                // Return best possibility
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

        /// <summary>
        /// Check CellScores that can block enemies and add value to them.
        /// </summary>
        public void CheckBlockOther()
        {
            // Coordinates with penguins in it (all penguins, then minus our penguins)
            List<Coordinates> coordsWithPenguin = new List<Coordinates>();
            // Coordinates of our penguins to remove them from the list of penguins coordinates
            List<Coordinates> coordsToRemove = new List<Coordinates>();
            // Update our penguins origins
            PossibilitiesOfOrigin();

            Movements move = new Movements(null, null, Board);

            // Add every cells with a penguin to the list of coordinates with penguins
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board.Board[i, j].CellType == CellType.FishWithPenguin)
                    {
                        coordsWithPenguin.Add(new Coordinates(i, j));
                    }
                }
            }

            // Add our penguins coordinates to the list of coordinates to remove from the list of penguins coordinates
            foreach (Coordinates coord in coordsWithPenguin)
            {
                foreach (Coordinates origin in PossibilitiesOrigin)
                {
                    if (coord.X == origin.X && coord.Y == origin.Y)
                    {
                        coordsToRemove.Add(coord);
                    }
                }
            }

            // Remove our penguins from the list of penguins coordinates to only keep enemy penguins coordinates
            foreach (Coordinates coordR in coordsToRemove)
            {
                coordsWithPenguin.Remove(coordR);
            }

            // Add value to cells that can block enemy penguins
            foreach (CellScore cellScore in CellScores)
            {
                foreach (Coordinates coordWithPenguin in coordsWithPenguin)
                {
                    // List of free cellScores surrounding enemy penguin
                    List<CellScore> freeCellScores = new List<CellScore>();

                    if (cellScore.Cell.Y == coordWithPenguin.Y && cellScore.Cell.X == coordWithPenguin.X)
                    {
                        int x;
                        int y;

                        // Check left
                        x = cellScore.Cell.X - 1;
                        y = cellScore.Cell.Y;
                        if (cellScore.Cell.X - 1 >= 0)
                        {
                            //GetCellScore(cellScore.Cell.X - 1, cellScore.Cell.Y).Value += 1 * BlockOthersCoefficient;
                            if (!move.CheckFreeCell(x, y))
                            {
                                freeCellScores.Add(GetCellScore(x, y));
                            }
                        }

                        // Check right
                        x = cellScore.Cell.X + 1;
                        y = cellScore.Cell.Y;
                        if (cellScore.Cell.X + 1 < 8)
                        {
                            //GetCellScore(cellScore.Cell.X + 1, cellScore.Cell.Y).Value += 1 * BlockOthersCoefficient;
                            if (!move.CheckFreeCell(x, y))
                            {
                                freeCellScores.Add(GetCellScore(x, y));
                            }
                        }

                        // Check up left
                        x = cellScore.Cell.Y % 2 == 0 ? cellScore.Cell.X - 1 : cellScore.Cell.X;
                        y = cellScore.Cell.Y - 1;
                        if (y < 8 && x < 8 && y >= 0 && x >= 0)
                        {
                            //GetCellScore(x, y).Value += 1 * BlockOthersCoefficient;
                            if (!move.CheckFreeCell(x, y))
                            {
                                freeCellScores.Add(GetCellScore(x, y));
                            }
                        }

                        // Check up right
                        x = cellScore.Cell.Y % 2 != 0 ? cellScore.Cell.X + 1 : cellScore.Cell.X;
                        y = cellScore.Cell.Y - 1;
                        if (y < 8 && x < 8 && y >= 0 && x >= 0)
                        {
                            //GetCellScore(x, y).Value += 1 * BlockOthersCoefficient;
                            if (!move.CheckFreeCell(x, y))
                            {
                                freeCellScores.Add(GetCellScore(x, y));
                            }
                        }

                        // Check down left
                        x = cellScore.Cell.Y % 2 == 0 ? cellScore.Cell.X - 1 : cellScore.Cell.X;
                        y = cellScore.Cell.Y + 1;
                        if (y < 8 && x < 8 && y >= 0 && x >= 0)
                        {
                            //GetCellScore(x, y).Value += 1 * BlockOthersCoefficient;
                            if (!move.CheckFreeCell(x, y))
                            {
                                freeCellScores.Add(GetCellScore(x, y));
                            }
                        }

                        // Check down right
                        x = cellScore.Cell.Y % 2 != 0 ? cellScore.Cell.X + 1 : cellScore.Cell.X;
                        y = cellScore.Cell.Y + 1;
                        if (y < 8 && x < 8 && y >= 0 && x >= 0)
                        {
                            //GetCellScore(x, y).Value += 1 * BlockOthersCoefficient;
                            if (!move.CheckFreeCell(x, y))
                            {
                                freeCellScores.Add(GetCellScore(x, y));
                            }
                        }

                        if (freeCellScores.Count == 1)
                        {
                            freeCellScores[0].Value += 1 * BlockOthersCoefficient;

                            ILog log = LogManager.GetLogger(GetType().ToString());
                            log.Debug("A blocking cell gained value!");
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Check CellScores that can block self and subtract value to them.
        /// </summary>
        /// <param name="origin">Origin of the selected penguin</param>
        /// <param name="cellScore">Destination CellScore</param>
        public void CheckBlockSelf(Coordinates origin, CellScore cellScore)
        {
            // List of free cellScores surrounding enemy penguin
            List<CellScore> freeCellScores = new List<CellScore>();
            // Singleton to access Movements functions (CheckMove() in this case)
            Movements move = new Movements(null, null, Board);
            int x;
            int y;

            // Check left
            x = cellScore.Cell.X - 1;
            y = cellScore.Cell.Y;
            if (cellScore.Cell.X - 1 >= 0)
            {
                //GetCellScore(cellScore.Cell.X - 1, cellScore.Cell.Y).Value += 1 * BlockOthersCoefficient;
                if (!move.CheckFreeCell(x, y) && cellScore.Cell.X != origin.X && cellScore.Cell.Y != origin.Y)
                {
                    freeCellScores.Add(GetCellScore(x, y));
                }
            }

            // Check right
            x = cellScore.Cell.X + 1;
            y = cellScore.Cell.Y;
            if (cellScore.Cell.X + 1 < 8)
            {
                //GetCellScore(cellScore.Cell.X + 1, cellScore.Cell.Y).Value += 1 * BlockOthersCoefficient;
                if (!move.CheckFreeCell(x, y) && cellScore.Cell.X != origin.X && cellScore.Cell.Y != origin.Y)
                {
                    freeCellScores.Add(GetCellScore(x, y));
                }
            }

            // Check up left
            x = cellScore.Cell.Y % 2 == 0 ? cellScore.Cell.X - 1 : cellScore.Cell.X;
            y = cellScore.Cell.Y - 1;
            if (y < 8 && x < 8 && y >= 0 && x >= 0)
            {
                //GetCellScore(x, y).Value += 1 * BlockOthersCoefficient;
                if (!move.CheckFreeCell(x, y) && cellScore.Cell.X != origin.X && cellScore.Cell.Y != origin.Y)
                {
                    freeCellScores.Add(GetCellScore(x, y));
                }
            }

            // Check up right
            x = cellScore.Cell.Y % 2 != 0 ? cellScore.Cell.X + 1 : cellScore.Cell.X;
            y = cellScore.Cell.Y - 1;
            if (y < 8 && x < 8 && y >= 0 && x >= 0)
            {
                //GetCellScore(x, y).Value += 1 * BlockOthersCoefficient;
                if (!move.CheckFreeCell(x, y) && cellScore.Cell.X != origin.X && cellScore.Cell.Y != origin.Y)
                {
                    freeCellScores.Add(GetCellScore(x, y));
                }
            }

            // Check down left
            x = cellScore.Cell.Y % 2 == 0 ? cellScore.Cell.X - 1 : cellScore.Cell.X;
            y = cellScore.Cell.Y + 1;
            if (y < 8 && x < 8 && y >= 0 && x >= 0)
            {
                //GetCellScore(x, y).Value += 1 * BlockOthersCoefficient;
                if (!move.CheckFreeCell(x, y) && cellScore.Cell.X != origin.X && cellScore.Cell.Y != origin.Y)
                {
                    freeCellScores.Add(GetCellScore(x, y));
                }
            }

            // Check down right
            x = cellScore.Cell.Y % 2 != 0 ? cellScore.Cell.X + 1 : cellScore.Cell.X;
            y = cellScore.Cell.Y + 1;
            if (y < 8 && x < 8 && y >= 0 && x >= 0)
            {
                //GetCellScore(x, y).Value += 1 * BlockOthersCoefficient;
                if (!move.CheckFreeCell(x, y) && cellScore.Cell.X != origin.X && cellScore.Cell.Y != origin.Y)
                {
                    freeCellScores.Add(GetCellScore(x, y));
                }
            }

            if (freeCellScores.Count == 0) // If == 1 too?
            {
                // TODO: Make sure this removal, and the added lines below, are correct
                // Removed this line because it removes value to the only available cell near destination, not from destination
                //freeCellScores[0].Value -= 1 * BlockSelfCoefficient;

                // If there's only one or zero cell available near destination, remove value from destination
                cellScore.Value -= 1 * BlockSelfCoefficient;

                // Debug log entry
                ILog log = LogManager.GetLogger(GetType().ToString());
                log.Debug("A blocking cell for your penguin lost value!");
            }

        }

        /// <summary>
        /// Get a CellScore from the CellScores property using its x and y coordinates.
        /// </summary>
        /// <param name="x">x coordinate of the wanted CellScore</param>
        /// <param name="y">y coordinate of the wanted CellScore</param>
        /// <returns>CellScore with corresponding x and y coordinates</returns>
        public CellScore GetCellScore(int x, int y)
        {
            foreach (CellScore cellScore in CellScores)
            {
                if (cellScore.Cell.X == x && cellScore.Cell.Y == y)
                {
                    return cellScore; // Return appropriate CellScore
                }
            }
            return new CellScore(new Coordinates(-1, -1), -1);
        }


        /// <summary>
        /// Get current player penguins
        /// </summary>
        public void PossibilitiesOfOrigin()
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
