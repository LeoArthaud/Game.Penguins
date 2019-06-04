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
    /// <summary>
    /// Hard difficulty AI. Subclass of AIGlobal.
    /// </summary>
    public class AIHard : AIGlobal
    {
        /// <summary>
        /// Positions of penguins of current player
        /// </summary>
        public List<Coordinates> PossibilitiesOrigin { get; set; }

        /// <summary>
        /// Get a list of cells with 3 points
        /// </summary>
        public IList<Coordinates> ThreePoints { get; set; }

        /// <summary>
        /// Get a list of solo fish near 3 points cells
        /// </summary>
        public IList<Coordinates> SoloFish { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="board"></param>
        /// <param name="random"></param>
        /// <param name="currentPlayer"></param>
        public AIHard(IBoard board, IRandom random, IPlayer currentPlayer) : base(board, random, currentPlayer)
        {
            Board = board;
            this.random = random;
            CurrentPlayer = currentPlayer;
        }

        /// <summary>
        /// Get coordinates where to place the penguin
        /// </summary>
        /// <returns>coordinates</returns>
        public new Coordinates PlacePenguin()
        {
            
            ThreePoints = getThreePoints();
            SoloFish = getFishNear(ThreePoints);
            // Get random x and y
            bool isNear = false;
            int placeX;
            int placeY;

            if (SoloFish.Count != 0)
            {
                isNear = true;
            }

            if (isNear)
            {
                return SoloFish[random.Next(0, SoloFish.Count)];
            }

            Console.Write("Random position");
            placeX = random.Next(0, 8);
            placeY = random.Next(0, 8);

            // If cell at [x;y] has already a penguin or has more than one fish
            while ((Board.Board[placeX, placeY].FishCount != 1 || Board.Board[placeX, placeY].CellType == CellType.FishWithPenguin) && isNear == false)
            {
                // Change random x and y
                placeX = random.Next(0, 8);
                placeY = random.Next(0, 8);
            }
            return new Coordinates(placeX, placeY);
        }

        public IList<Coordinates> getThreePoints()
        {
            int k = 0;
            List<Coordinates> coordinatesList = new List<Coordinates>();
            for(int i = 0 ; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(Board.Board[i,j].FishCount == 3)
                    {
                        Coordinates coordinates = new Coordinates(i, j);
                        coordinatesList.Add(coordinates);
                        k++;
                    }
                }
            }
            Console.WriteLine("il y a " + k + " case(s) de 3 points");

            return coordinatesList;
        }

        public IList<Coordinates> getFishNear(IList<Coordinates> threePoints)
        {
            int k = 0;
            List<Coordinates> soloFishList = new List<Coordinates>();
            
            foreach (Coordinates cells in threePoints)
            {
                int x;
                int y;
                //check left
                if (cells.X - 1 >= 0)
                {
                    // If the cell = 1
                    if (Board.Board[cells.X - 1, cells.Y].FishCount == 1 && Board.Board[cells.X - 1, cells.Y].CellType == CellType.Fish)
                    {
                        soloFishList.Add(new Coordinates(cells.X - 1, cells.Y));
                        Console.WriteLine("fish on left " + cells.X + ", " + cells.Y);
                    }
                }

                //check right
                if (cells.X + 1 < 8)
                {
                    // If the cell = 1
                    if (Board.Board[cells.X + 1, cells.Y].FishCount == 1 && Board.Board[cells.X + 1, cells.Y].CellType == CellType.Fish)
                    {
                        soloFishList.Add(new Coordinates(cells.X + 1, cells.Y));
                        Console.WriteLine("fish on right " + cells.X +", "+ cells.Y);
                    }
                }

                //check up left
                x = cells.Y % 2 == 0 ? cells.X - 1 : cells.X;
                y = cells.Y - 1;
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    if (Board.Board[x, y].CellType == CellType.Fish && Board.Board[x, y].FishCount == 1)
                    {
                        soloFishList.Add(new Coordinates(x, y));
                    }
                }

                //check up right
                x = cells.Y % 2 != 0 ? cells.X + 1 : cells.X;
                y = cells.Y - 1;
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    if (Board.Board[x, y].CellType == CellType.Fish && Board.Board[x, y].FishCount == 1)
                    {
                        soloFishList.Add(new Coordinates(x, y));
                    }
                }

                //check down left
                x = cells.Y % 2 == 0 ? cells.X - 1 : cells.X;
                y = cells.Y + 1;
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    if (Board.Board[x, y].CellType == CellType.Fish && Board.Board[x, y].FishCount == 1)
                    {
                        soloFishList.Add(new Coordinates(x, y));
                    }
                }

                //check down right
                x = cells.Y % 2 != 0 ? cells.X + 1 : cells.X;
                y = cells.Y + 1;
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    if (Board.Board[x, y].CellType == CellType.Fish && Board.Board[x, y].FishCount == 1)
                    {
                        soloFishList.Add(new Coordinates(x, y));
                    }
                }

                k++;
            }
            Console.WriteLine("il y a " + k + " soloFish autour d'une case de 3 points :)");

            return soloFishList;
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
