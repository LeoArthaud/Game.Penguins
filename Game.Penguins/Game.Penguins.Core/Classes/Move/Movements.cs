using System;
using System.Collections.Generic;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.Classes.Move
{
    /// <summary>
    /// Represents movements on the board.
    /// </summary>
    public class Movements
    {
        /// <summary>
        /// Cell of origin
        /// </summary>
        public ICell Origin { get; }

        /// <summary>
        /// Cell of destination
        /// </summary>
        public ICell Destination { get; }

        /// <summary>
        /// Board
        /// </summary>
        public IBoard Board { get; }

        /// <summary>
        /// List of possibilities cells
        /// </summary>
        public IList<Coordinates> Possibilities { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="board"></param>
        public Movements(ICell origin, ICell destination, IBoard board)
        {
            Origin = origin;
            Destination = destination;
            Board = board;
            Possibilities = new List<Coordinates>();
        }

        /// <summary>
        /// Get coordinate of the origin and destination
        /// </summary>
        /// <returns>coordinates</returns>
        public Dictionary<string, Coordinates> GetCoordinates()
        {
            Dictionary<string, Coordinates> coordinates = new Dictionary<string, Coordinates>();

            // For each cell of the board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Check if the origin cell correspond to the cell of the board
                    if (Origin == Board.Board[i, j])
                    {
                        coordinates.Add("origin", new Coordinates(i, j));
                    }
                    // Check if the destination cell correspond to the cell of the board
                    if (Destination == Board.Board[i, j])
                    {
                        coordinates.Add("destination", new Coordinates(i, j));
                    }
                }
            }
            return coordinates;
        }

        /// <summary>
        /// Check move of the penguin
        /// </summary>
        /// <param name="origin">coordinates origin and destination</param>
        /// <returns>list of coordinates of cells where the penguin can move</returns>
        public IList<Coordinates> CheckMove(Coordinates origin)
        {
            // Get cells at the right of the penguin
            GetCoordinatesRight(origin);

            // Get cells at the left of the penguin
            GetCoordinatesLeft(origin);

            // Get cells at the down-right of the penguin
            GetCoordinatesDownRight(origin);

            // Get cells at the up-right of the penguin
            GetCoordinatesUpRight(origin);

            // Get cells at the down-left of the penguin
            GetCoordinatesDownLeft(origin);

            // Get cells at the up-left of the penguin
            GetCoordinatesUpLeft(origin);

            return Possibilities;
        }

        /// <summary>
        /// Get cells at the right of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public void GetCoordinatesRight(Coordinates origin)
        {
            RecursiveCellRightLeft(origin.X + 1, origin.Y, 1,0);
        }

        /// <summary>
        /// Check if the cell is free at the right or left
        /// </summary>
        /// <param name="x">Coordinate x of cell</param>
        /// <param name="y">Coordinate y of cell</param>
        /// <param name="i">Incrementation x</param>
        /// <param name="j">Incrementation y</param>
        public void RecursiveCellRightLeft(int x, int y, int i, int j)
        {
            // If the cell is in the board
            if (x < 8 && x >= 0 && y < 8 && y >= 0)
            {
                // If the cell is free
                if (Board.Board[x, y].CellType == CellType.Fish)
                {
                    // Add cell to the list
                    Possibilities.Add(new Coordinates(x, y));
                    // Call the function
                    RecursiveCellRightLeft(x + i, y + j, i , j);
                }
            }
        }

        /// <summary>
        /// Get cells at the left of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public void GetCoordinatesLeft(Coordinates origin)
        {
            RecursiveCellRightLeft(origin.X - 1, origin.Y, -1, 0);
        }

        /// <summary>
        /// Check if the cell is free at the down-right
        /// </summary>
        /// <param name="x">Coordinate x of cell</param>
        /// <param name="y">Coordinate y of cell</param>
        /// <param name="i">Incrementation x</param>
        /// <param name="j">Incrementation y</param>
        public void RecursiveCellDownRight(int x, int y, int i, int j)
        {
            if (x < 8 && x >= 0 && y < 8 && y >= 0)
            {
                // If the cell is free
                if (Board.Board[x, y].CellType == CellType.Fish)
                {
                    Possibilities.Add(new Coordinates(x, y));
                    i = y % 2 != 0 ? 1 : 0;
                    RecursiveCellDownRight(x + i, y + j, i, j);
                }
            }
        }

        /// <summary>
        /// Get cells at the down-right of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public void GetCoordinatesDownRight(Coordinates origin)
        {
            int i = origin.Y % 2 != 0 ? 1 : 0;
            int j = 1;
            RecursiveCellDownRight(origin.X + i, origin.Y + j, i, j);
        }

        /// <summary>
        /// Check if the cell is free at the down-left
        /// </summary>
        /// <param name="x">Coordinate x of cell</param>
        /// <param name="y">Coordinate y of cell</param>
        /// <param name="i">Incrementation x</param>
        /// <param name="j">Incrementation y</param>
        public void RecursiveCellDownLeft(int x, int y, int i, int j)
        {
            if (x < 8 && x >= 0 && y < 8 && y >= 0)
            {
                // If the cell is free
                if (Board.Board[x, y].CellType == CellType.Fish)
                {
                    Possibilities.Add(new Coordinates(x, y));
                    i = y % 2 == 0 ? - 1 : 0;
                    RecursiveCellDownLeft(x + i, y + j, i, j);
                }
            }
        }

        /// <summary>
        /// Get cells at the down-left of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public void GetCoordinatesDownLeft(Coordinates origin)
        {
            int i = origin.Y % 2 == 0 ? -1 : 0;
            int j = 1;
            RecursiveCellDownLeft(origin.X + i, origin.Y + j, i, j);
        }

        /// <summary>
        /// Check if the cell is free at the up-right
        /// </summary>
        /// <param name="x">Coordinate x of cell</param>
        /// <param name="y">Coordinate y of cell</param>
        /// <param name="i">Incrementation x</param>
        /// <param name="j">Incrementation y</param>
        public void RecursiveCellUpRight(int x, int y, int i, int j)
        {
            if (x < 8 && x >= 0 && y < 8 && y >= 0)
            {
                // If the cell is free
                if (Board.Board[x, y].CellType == CellType.Fish)
                {
                    Possibilities.Add(new Coordinates(x, y));
                    i = y % 2 != 0 ? 1 : 0;
                    RecursiveCellUpRight(x + i, y + j, i, j);
                }
            }
        }

        /// <summary>
        /// Get cells at the up-right of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public void GetCoordinatesUpRight(Coordinates origin)
        {
            int i = origin.Y % 2 != 0 ? 1 : 0;
            int j = -1;
            RecursiveCellUpRight(origin.X + i, origin.Y + j, i, j);
        }

        /// <summary>
        /// Check if the cell is free at the up-left
        /// </summary>
        /// <param name="x">Coordinate x of cell</param>
        /// <param name="y">Coordinate y of cell</param>
        /// <param name="i">Incrementation x</param>
        /// <param name="j">Incrementation y</param>
        public void RecursiveCellUpLeft(int x, int y, int i, int j)
        {
            if (x < 8 && x >= 0 && y < 8 && y >= 0)
            {
                // If the cell is free
                if (Board.Board[x, y].CellType == CellType.Fish)
                {
                    Possibilities.Add(new Coordinates(x, y));
                    i = y % 2 == 0 ? - 1 : 0;
                    RecursiveCellUpLeft(x + i, y + j, i, j);
                }
            }
        }

        /// <summary>
        /// Get cells at the up-left of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public void GetCoordinatesUpLeft(Coordinates origin)
        {
            int i = origin.Y % 2 == 0 ? - 1 : 0;
            int j = -1;
            RecursiveCellUpLeft(origin.X + i, origin.Y + j, i, j);
        }

        /// <summary>
        /// Get in which direction the penguin can move or not
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>directions</returns>
        public Dictionary<DirectionType, bool> CanMove(Coordinates origin)
        {
            Dictionary<DirectionType, bool> directions = new Dictionary<DirectionType, bool>();

            // Direction right
            int x = origin.X + 1;
            int y = origin.Y;
            directions.Add(DirectionType.Right, CheckFreeCell(x, y));

            // Direction left
            x = origin.X - 1;
            y = origin.Y;
            directions.Add(DirectionType.Left, CheckFreeCell(x, y));

            // Direction down-right
            x = origin.Y % 2 != 0 ? origin.X + 1 : origin.X;
            y = origin.Y + 1;
            directions.Add(DirectionType.DownRight, CheckFreeCell(x, y));

            // Direction up-right
            x = origin.Y % 2 != 0 ? origin.X + 1 : origin.X;
            y = origin.Y - 1;
            directions.Add(DirectionType.UpRight, CheckFreeCell(x, y));

            // Direction down-left
            x = origin.Y % 2 == 0 ? origin.X - 1 : origin.X;
            y = origin.Y + 1;
            directions.Add(DirectionType.DownLeft, CheckFreeCell(x, y));

            // Direction up-left
            x = origin.Y % 2 == 0 ? origin.X - 1 : origin.X;
            y = origin.Y - 1;
            directions.Add(DirectionType.UpLeft, CheckFreeCell(x, y));
            
            return directions;
        }

        /// <summary>
        /// Check if the penguin can move in a direction
        /// </summary>
        /// <param name="x">Coordinate X</param>
        /// <param name="y">Coordinate Y</param>
        /// <returns>true if the penguin can't move</returns>
        public bool CheckFreeCell(int x, int y)
        {
            // If the cell not exceed the border of the board
            if (0 <= x && 0 <= y && x < 8 && y < 8)
            {
                // If the type of the cell is equal to Fish
                if (Board.Board[x, y].CellType == CellType.Fish)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
