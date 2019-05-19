using System;
using System.Collections.Generic;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.Classes.Move
{
    public class Movements
    {
        public ICell Origin { get; }
        public ICell Destination { get; }
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
        /// <returns>coordinate</returns>
        public Dictionary<string, Coordinates> GetCoordinates()
        {
            Dictionary<string, Coordinates> coordinates = new Dictionary<string, Coordinates>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Origin == Board.Board[i, j])
                    {
                        coordinates.Add("origin", new Coordinates(i, j));
                    }
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
        /// <param name="coordinates">coordinates origin and destination</param>
        /// <returns>list of coordinates of cells where the penguin can move</returns>
        public IList<Coordinates> CheckDeplacement(Coordinates origin)
        {
            IList<Coordinates> result = new List<Coordinates>();

            var list = GetCoordinatesRight(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                }
            }
            
            list = GetCoordinatesLeft(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                }
            }

            list = GetCoordinatesDownRight(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                }
            }

            list = GetCoordinatesUpRight(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                }
            }

            list = GetCoordinatesDownLeft(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                }
            }

            list = GetCoordinatesUpLeft(origin);
            if (list.Count != 0)
            {
                foreach (var element in list)
                {
                    result.Add(element);
                }
            }

            return result;
        }

        /// <summary>
        /// Récupère les cellules libres à droite du penguin
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public IList<Coordinates> GetCoordinatesRight(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();
            for (int i = 1; i < 8; i++)
            {
                if (origin.X + i < 8)
                {
                    if (Board.Board[origin.X + i, origin.Y].CellType == CellType.Fish)
                    {
                        //Console.WriteLine("(pot) => X : " + (origin.X + i) + ", " + origin.Y);
                        possibilities.Add(new Coordinates(origin.X + i, origin.Y));
                    }
                    else
                    {
                        return possibilities;
                    }
                }
                else
                {
                    return possibilities;
                }
            }
            return possibilities;
        }

        /// <summary>
        /// Récupère les cellules libres à gauche du penguin
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public IList<Coordinates> GetCoordinatesLeft(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();
            for (int i = 1; i < 8; i++)
            {
                if (origin.X - i >= 0)
                {
                    if (Board.Board[origin.X - i, origin.Y].CellType == CellType.Fish)
                    {
                        //Console.WriteLine("(pot) => X : " + (origin.X - i) + ", " + origin.Y);
                        possibilities.Add(new Coordinates(origin.X - i, origin.Y));
                    }
                    else
                    {
                        return possibilities;
                    }
                }
                else
                {
                    return possibilities;
                }
            }
            return possibilities;
        }

        /// <summary>
        /// Récupère les cellules libres en bas à droite du penguin
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public IList<Coordinates> GetCoordinatesDownRight(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();

            int x = origin.X;
            int y = origin.Y;
            //x = origin.Y % 2 != 0 ? origin.X + 1 : origin.X;
            //y = origin.Y + 1;
            for (int i = 1; i < 8; i++)
            {
                x = y % 2 != 0 ? x + 1 : x;
                y = origin.Y + i;
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    if (Board.Board[x, y].CellType == CellType.Fish)
                    {
                        //Console.WriteLine("(pot) => X : " + x + ", " + y);
                        possibilities.Add(new Coordinates(x, y));
                    }
                    else
                    {
                        return possibilities;
                    }
                }
                else
                {
                    return possibilities;
                }
            }

            return possibilities;
        }

        /// <summary>
        /// Récupère les cellules libres en bas à gauche du penguin
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public IList<Coordinates> GetCoordinatesDownLeft(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();

            int x = origin.X;
            int y = origin.Y;
            //x = origin.Y % 2 == 0 ? origin.X - 1 : origin.X;
            //y = origin.Y + 1;
            for (int i = 1; i < 8; i++)
            {
                x = y % 2 == 0 ? x - 1 : x;
                y = origin.Y + i;
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    if (Board.Board[x, y].CellType == CellType.Fish)
                    {
                        //Console.WriteLine("(pot) => X : " + x + ", " + y);
                        possibilities.Add(new Coordinates(x, y));
                    }
                    else
                    {
                        return possibilities;
                    }
                }
                else
                {
                    return possibilities;
                }
            }

            return possibilities;
        }

        /// <summary>
        /// Récupère les cellules libres en haut à droite du penguin
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public IList<Coordinates> GetCoordinatesUpRight(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();

            int x = origin.X;
            int y = origin.Y;
            //x = origin.Y % 2 != 0 ? origin.X + 1 : origin.X;
            //y = origin.Y - 1;
            for (int i = 1; i < 8; i++)
            {
                x = y % 2 != 0 ? x + 1 : x;
                y = origin.Y - i;
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    if (Board.Board[x, y].CellType == CellType.Fish)
                    {
                        //Console.WriteLine("(pot) => X : " + x + ", " + y);
                        possibilities.Add(new Coordinates(x, y));
                    }
                    else
                    {
                        return possibilities;
                    }
                }
                else
                {
                    return possibilities;
                }
            }

            return possibilities;
        }

        /// <summary>
        /// Récupère les cellules libres en haut à gauche du penguin
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public IList<Coordinates> GetCoordinatesUpLeft(Coordinates origin)
        {
            IList<Coordinates> possibilities = new List<Coordinates>();

            int x = origin.X;
            int y = origin.Y;
            //x = origin.Y % 2 == 0 ? origin.X - 1 : origin.X;
            //y = origin.Y - 1;
            for (int i = 1; i < 8; i++)
            {
                x = y % 2 == 0 ? x - 1 : x;
                y = origin.Y - i;
                if (y < 8 && x < 8 && y >= 0 && x >= 0)
                {
                    if (Board.Board[x, y].CellType == CellType.Fish)
                    {
                        Console.WriteLine("(pot) => X : " + x + ", " + y);
                        possibilities.Add(new Coordinates(x, y));
                    }
                    else
                    {
                        return possibilities;
                    }
                }
                else
                {
                    return possibilities;
                }
            }

            return possibilities;
        }

        public Dictionary<DirectionType, bool> CanMove(Coordinates origin)
        {
            int x, y;
            Dictionary<DirectionType, bool> directions = new Dictionary<DirectionType, bool>();
            // DROITE
            x = origin.X + 1;
            y = origin.Y;
            directions.Add(DirectionType.Droite, CheckCaseWater(x, y));

            // GAUCHE
            x = origin.X - 1;
            y = origin.Y;
            directions.Add(DirectionType.Gauche, CheckCaseWater(x, y));

            // BAS-DROITE
            x = origin.Y % 2 != 0 ? origin.X + 1 : origin.X;
            y = origin.Y + 1;
            directions.Add(DirectionType.BasDroite, CheckCaseWater(x, y));

            // HAUT-DROITE
            x = origin.Y % 2 != 0 ? origin.X + 1 : origin.X;
            y = origin.Y - 1;
            directions.Add(DirectionType.HautDroite, CheckCaseWater(x, y));

            // BAS-GAUCHE
            x = origin.Y % 2 == 0 ? origin.X - 1 : origin.X;
            y = origin.Y + 1;
            directions.Add(DirectionType.BasGauche, CheckCaseWater(x, y));

            // HAUT-GAUCHE
            x = origin.Y % 2 == 0 ? origin.X - 1 : origin.X;
            y = origin.Y - 1;
            directions.Add(DirectionType.HautGauche, CheckCaseWater(x, y));
            
            return directions;
        }

        public bool CheckCaseWater(int originX, int originY)
        {
            if (0 <= originX && 0 <= originY && originX < 8 && originY < 8)
            {
                if (Board.Board[originX, originY].CellType == CellType.Water || Board.Board[originX, originY].CellType == CellType.FishWithPenguin)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            
            return false;
        }
    }
}
