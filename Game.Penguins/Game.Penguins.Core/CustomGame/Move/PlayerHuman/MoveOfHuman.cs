using System;
using System.Collections.Generic;
using Game.Penguins.Core.CustomGame.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.CustomGame.Move.PlayerHuman
{
    public class MoveOfHuman
    {
        public ICell Origin { get; }
        public ICell Destination { get; }
        public IBoard Board { get; }

        public MoveOfHuman(ICell origin, ICell destination, IBoard board)
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
        public IList<Coordinates> CheckDeplacement(Dictionary<string, Coordinates> coordinates)
        {
            IList<Coordinates> result = new List<Coordinates>();
            for (int i = 0; i < 6; i++)
            {
                IList<Coordinates> list = i >= 0 && i < 2
                    // Appelle la fonction CheckCaseRightLeft avec DirectionType.Droite puis DirectionType.Gauche
                    ? CheckCaseRightLeft((DirectionType)i, coordinates)
                    // Appelle la fonction CheckCaseDiago avec DirectionType.BasGauche puis DirectionType.BasDroite, puis DirectionType.HautDroite, puis DirectionType.HautGauche
                    : CheckCaseDiago((DirectionType)i, coordinates);

                if (list != null)
                {
                    foreach (var element in list)
                    {
                        result.Add(element);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Check if the penguin can move in diago
        /// </summary>
        /// <param name="directionType">direction check</param>
        /// <param name="coordinates">coordinates origin and destination</param>
        /// <returns></returns>
        public IList<Coordinates> CheckCaseDiago(DirectionType directionType, Dictionary<string, Coordinates> coordinates)
        {
            IList<Coordinates> result = new List<Coordinates>();

            // on vérifie que le penguin veut bien aller en diagonale
            var x = coordinates["destination"].X - coordinates["origin"].X;
            var y = coordinates["destination"].Y - coordinates["origin"].Y;
            Console.WriteLine("distance => X : " + x +", Y : "+ y);

            if (x < 0 && y >= 0 && directionType == DirectionType.BasGauche || x >= 0 && y >= 0 && directionType == DirectionType.BasDroite || (x < 0 || x == 0) && y < 0 && directionType == DirectionType.HautGauche || x >= 0 && y < 0 && directionType == DirectionType.HautDroite)
            {
                Console.WriteLine("directionType : "+ directionType);
                // si x ou y est négatif alors on les transforme en positif
                var countX = x < 0 ? x * -1 : x;
                var countY = y < 0 ? y * -1 : y;

                // on récupère la distance entre la case d'origine et de destination
                int count = countX - countY > 0 ? countX : countY;

                int incrementX = 0;
                int incrementY = 1;

                int xDest = coordinates["origin"].X;
                int yDest = coordinates["origin"].Y;

                // on va récupérer toutes les cases entre l'origine et la destination si il n'y a pas d'obstacles
                while (incrementY <= count)
                {
                    Console.WriteLine("(pot) => X : " + yDest + ", Y : " + xDest);
                    Console.WriteLine("yDest % 2 : " + (yDest % 2));
                    Console.WriteLine("directionType : " + directionType);

                    // si on veut aller vers la gauche et que la derniere case vérifiée et paire alors on incrémente
                    if (yDest % 2 == 0 && (directionType == DirectionType.HautGauche || directionType == DirectionType.BasGauche))
                    {
                        incrementX++;
                    }
                    // si on veut aller vers la droite et que la derniere case vérifiée et impaire alors on incrémente
                    else if (yDest % 2 != 0 && (directionType == DirectionType.BasDroite || directionType == DirectionType.HautDroite))
                    {
                        incrementX++;
                    }
                    
                    // si le penguin veut aller en BasGauche et qu'il n'y a pas d'obstacle
                    if (directionType == DirectionType.BasGauche && coordinates["origin"].X - incrementX >= 0 && coordinates["origin"].Y + incrementY <= 7)
                    {
                        Console.WriteLine("(depassement) => X : " + (coordinates["origin"].Y + incrementY) + ", Y : " + (coordinates["origin"].X - incrementX));
                        if (Board.Board[coordinates["origin"].X - incrementX, coordinates["origin"].Y + incrementY].CellType == CellType.Fish)
                        {
                            // alors on ajoute la case possible au résultat
                            yDest = coordinates["origin"].Y + incrementY;
                            xDest = coordinates["origin"].X - incrementX;
                            Console.WriteLine("X : " + yDest + ", Y : " + xDest);
                            result.Add(new Coordinates(xDest, yDest));
                        }
                        else
                        {
                            return null;
                        }
                    }
                    // si le penguin veut aller en BasDroite et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.BasDroite && coordinates["origin"].X + incrementX <= 7 && coordinates["origin"].Y + incrementY <= 7)
                    {
                        Console.WriteLine("(depassement) => X : " + (coordinates["origin"].Y + incrementY) + ", Y : " + (coordinates["origin"].X + incrementX));
                        if (Board.Board[coordinates["origin"].X + incrementX, coordinates["origin"].Y + incrementY].CellType == CellType.Fish)
                        {
                            // alors on ajoute la case possible au résultat
                            yDest = coordinates["origin"].Y + incrementY;
                            xDest = coordinates["origin"].X + incrementX;
                            Console.WriteLine("X : " + yDest + ", Y : " + xDest);
                            result.Add(new Coordinates(xDest, yDest));
                        }
                        else
                        {
                            return null;
                        }
                    }
                    // si le penguin veut aller en HautGauche et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.HautGauche && coordinates["origin"].X - incrementX >= 0 && coordinates["origin"].Y - incrementY >= 0)
                    {
                        Console.WriteLine("(depassement) => X : " + (coordinates["origin"].Y - incrementY) + ", Y : " + (coordinates["origin"].X - incrementX));
                        if (Board.Board[coordinates["origin"].X - incrementX, coordinates["origin"].Y - incrementY].CellType == CellType.Fish)
                        {
                            // alors on ajoute la case possible au résultat
                            yDest = coordinates["origin"].Y - incrementY;
                            xDest = coordinates["origin"].X - incrementX;
                            Console.WriteLine("X : " + yDest + ", Y : " + xDest);
                            result.Add(new Coordinates(xDest, yDest));
                        }
                        else
                        {
                            return null;
                        }
                    }
                    // si le penguin veut aller en HautDroite et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.HautDroite && coordinates["origin"].X + incrementX <= 7 && coordinates["origin"].Y - incrementY >= 0)
                    {
                        Console.WriteLine("(depassement) => X : " + (coordinates["origin"].Y - incrementY) + ", Y : " + (coordinates["origin"].X + incrementX));
                        if (Board.Board[coordinates["origin"].X + incrementX, coordinates["origin"].Y - incrementY].CellType == CellType.Fish)
                        {
                            // alors on ajoute la case possible au résultat
                            yDest = coordinates["origin"].Y - incrementY;
                            xDest = coordinates["origin"].X + incrementX;
                            Console.WriteLine("X : " + yDest + ", Y : " + xDest);
                            result.Add(new Coordinates(xDest, yDest));
                        }
                        else
                        {
                            return null;
                        }
                    }
                    // si un obstacle se trouve sur le chemin du penguin
                    else
                    {
                        // alors le penguin ne peut pas continuer sa course et doit choisir une autre case pour se déplacer
                        return null;
                    }
                    Console.WriteLine("***");
                    incrementY++;
                }
            }

            return result;
        }

        /// <summary>
        /// Check if the penguin can move left/right
        /// </summary>
        /// <param name="directionType">direction check</param>
        /// <param name="coordinates">coordinates origin and destination</param>
        /// <returns></returns>
        public IList<Coordinates> CheckCaseRightLeft(DirectionType directionType, Dictionary<string, Coordinates> coordinates)
        {
            IList<Coordinates> result = new List<Coordinates>();

            //récupère la distance entre l'origine et la destination
            var x = coordinates["destination"].X - coordinates["origin"].X;

            //si elle est positive alors le penguin veut aller à droite sinon il veut aller à gauche
            if (x > 0 && directionType == DirectionType.Droite || x < 0 && directionType == DirectionType.Gauche)
            {
                Console.WriteLine("directionType : " + directionType);
                // si la distance est négative alors on la met en positive
                if (x < 0)
                {
                    x = x * -1;
                }

                // on va récupérer toutes les cases entre l'origine et la destination si il n'y a pas d'obstacle
                int increment = 1;
                while (increment <= x)
                {
                    // si le penguin veut aller à droite et qu'il n'y a pas d'obstacle
                    if (directionType == DirectionType.Droite && Board.Board[coordinates["origin"].X + increment, coordinates["origin"].Y].CellType == CellType.Fish)
                    {
                        // alors on ajoute la case possible au résultat
                        result.Add(new Coordinates(coordinates["origin"].X + increment, coordinates["origin"].Y));
                    }
                    // si le penguin veut aller à gauche et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.Gauche && Board.Board[coordinates["origin"].X - increment, coordinates["origin"].Y].CellType == CellType.Fish)
                    {
                        // alors on ajoute la case possible au résultat
                        result.Add(new Coordinates(coordinates["origin"].X - increment, coordinates["origin"].Y));
                    }
                    // si un obstacle se trouve sur le chemin du penguin
                    else
                    {
                        // alors le penguin ne peut pas continuer sa course et doit choisir une autre case pour se déplacer
                        return null;
                    }
                    increment++;
                }
            }
            return result;
        }
    }
}
