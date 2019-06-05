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

        public Movements(ICell origin, ICell destination, IBoard board)
        {
            Origin = origin;
            Destination = destination;
            Board = board;
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
            IList<Coordinates> result = new List<Coordinates>();
            Console.WriteLine("**************************************************");
            Console.WriteLine("New turn : Origin : " + origin.X + ", " + origin.Y);

            // Get cells at the right of the penguin
            var list = GetCoordinatesRight(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                    Console.WriteLine("Right");
                    Console.WriteLine(element.X+", "+element.Y);
                }
            }

            // Get cells at the left of the penguin
            list = GetCoordinatesLeft(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                    Console.WriteLine("Left");
                    Console.WriteLine(element.X + ", " + element.Y);
                }
            }

            // Get cells at the down-right of the penguin
            list = GetCoordinatesDownRight(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                    Console.WriteLine("Down-Right");
                    Console.WriteLine(element.X + ", " + element.Y);
                }
            }

            // Get cells at the up-right of the penguin
            list = GetCoordinatesUpRight(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                    Console.WriteLine("Up-Right");
                    Console.WriteLine(element.X + ", " + element.Y);
                }
            }

            // Get cells at the down-left of the penguin
            list = GetCoordinatesDownLeft(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                    Console.WriteLine("Down-Left");
                    Console.WriteLine(element.X + ", " + element.Y);
                }
            }

            // Get cells at the up-left of the penguin
            list = GetCoordinatesUpLeft(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                    Console.WriteLine("Up-Left");
                    Console.WriteLine(element.X + ", " + element.Y);
                }
            }

            return result;
        }

        /// <summary>
        /// Get cells at the right of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public IList<Coordinates> GetCoordinatesRight(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();
            for (int i = 1; i < 8; i++)
            {
                // If the cell not exceed the border of the board
                if (origin.X + i < 8 && origin.Y >= 0 && origin.Y < 8 && origin.X + i >= 0)
                {
                    // If the cell is free
                    if (Board.Board[origin.X + i, origin.Y].CellType == CellType.Fish)
                    {
                        possibilities.Add(new Coordinates(origin.X + i, origin.Y));
                    }
                    // Else, stop the research
                    else
                    {
                        return possibilities;
                    }
                }
                // Else, stop the research
                else
                {
                    return possibilities;
                }
            }
            return possibilities;
        }

        /// <summary>
        /// Get cells at the left of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public IList<Coordinates> GetCoordinatesLeft(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();
            for (int i = 1; i < 8; i++)
            {
                // If the cell not exceed the border of the board
                if (origin.X - i >= 0 && origin.Y >= 0 && origin.Y < 8 && origin.X - i < 8)
                {
                    // If the cell is free
                    if (Board.Board[origin.X - i, origin.Y].CellType == CellType.Fish)
                    {
                        possibilities.Add(new Coordinates(origin.X - i, origin.Y));
                    }
                    // Else, stop the research
                    else
                    {
                        return possibilities;
                    }
                }
                // Else, stop the research
                else
                {
                    return possibilities;
                }
            }
            return possibilities;
        }

        /// <summary>
        /// Get cells at the down-right of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public IList<Coordinates> GetCoordinatesDownRight(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();

            int x = origin.X;
            int y = origin.Y;
            for (int i = 1; i < 8; i++)
            {
                x = y % 2 != 0 ? x + 1 : x;
                y = origin.Y + i;

                // If the cell not exceed the border of the board
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    // If the cell is free
                    if (Board.Board[x, y].CellType == CellType.Fish)
                    {
                        possibilities.Add(new Coordinates(x, y));
                    }
                    // Else, stop the research
                    else
                    {
                        return possibilities;
                    }
                }
                // Else, stop the research
                else
                {
                    return possibilities;
                }
            }

            return possibilities;
        }

        /// <summary>
        /// Get cells at the down-left of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public IList<Coordinates> GetCoordinatesDownLeft(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();

            int x = origin.X;
            int y = origin.Y;
            for (int i = 1; i < 8; i++)
            {
                x = y % 2 == 0 ? x - 1 : x;
                y = origin.Y + i;

                // If the cell not exceed the border of the board
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    // If the cell is free
                    if (Board.Board[x, y].CellType == CellType.Fish)
                    {
                        possibilities.Add(new Coordinates(x, y));
                    }
                    // Else, stop the research
                    else
                    {
                        return possibilities;
                    }
                }
                // Else, stop the research
                else
                {
                    return possibilities;
                }
            }

            return possibilities;
        }

        /// <summary>
        /// Get cells at the up-right of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public IList<Coordinates> GetCoordinatesUpRight(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();

            int x = origin.X;
            int y = origin.Y;
            for (int i = 1; i < 8; i++)
            {
                x = y % 2 != 0 ? x + 1 : x;
                y = origin.Y - i;

                // If the cell not exceed the border of the board
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    // If the cell is free
                    if (Board.Board[x, y].CellType == CellType.Fish)
                    {
                        possibilities.Add(new Coordinates(x, y));
                    }
                    // Else, stop the research
                    else
                    {
                        return possibilities;
                    }
                }
                // Else, stop the research
                else
                {
                    return possibilities;
                }
            }

            return possibilities;
        }

        /// <summary>
        /// Get cells at the up-left of the penguin
        /// </summary>
        /// <param name="origin">coordinates of origin</param>
        /// <returns>possibilities of move</returns>
        public IList<Coordinates> GetCoordinatesUpLeft(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();

            int x = origin.X;
            int y = origin.Y;
            for (int i = 1; i < 8; i++)
            {
                x = y % 2 == 0 ? x - 1 : x;
                y = origin.Y - i;

                // If the cell not exceed the border of the board
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    // If the cell is free
                    if (Board.Board[x, y].CellType == CellType.Fish)
                    {
                        possibilities.Add(new Coordinates(x, y));
                    }
                    // Else, stop the research
                    else
                    {
                        return possibilities;
                    }
                }
                // Else, stop the research
                else
                {
                    return possibilities;
                }
            }

            return possibilities;
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
            directions.Add(DirectionType.Droite, CheckFreeCell(x, y));

            // Direction left
            x = origin.X - 1;
            y = origin.Y;
            directions.Add(DirectionType.Gauche, CheckFreeCell(x, y));

            // Direction down-right
            x = origin.Y % 2 != 0 ? origin.X + 1 : origin.X;
            y = origin.Y + 1;
            directions.Add(DirectionType.BasDroite, CheckFreeCell(x, y));

            // Direction up-right
            x = origin.Y % 2 != 0 ? origin.X + 1 : origin.X;
            y = origin.Y - 1;
            directions.Add(DirectionType.HautDroite, CheckFreeCell(x, y));

            // Direction down-left
            x = origin.Y % 2 == 0 ? origin.X - 1 : origin.X;
            y = origin.Y + 1;
            directions.Add(DirectionType.BasGauche, CheckFreeCell(x, y));

            // Direction up-left
            x = origin.Y % 2 == 0 ? origin.X - 1 : origin.X;
            y = origin.Y - 1;
            directions.Add(DirectionType.HautGauche, CheckFreeCell(x, y));
            
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
