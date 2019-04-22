using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.GameBoard;

namespace Game.Penguins.Core.CustomGame.PlayerHuman
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
        public Dictionary<string, Coordonees> GetCoordonees()
        {
            Dictionary<string, Coordonees> coordonees = new Dictionary<string, Coordonees>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Origin == Board.Board[i, j])
                    {

                        coordonees.Add("origin", new Coordonees(i, j));
                    }
                    if (Destination == Board.Board[i, j])
                    {
                        coordonees.Add("destination", new Coordonees(i, j));
                    }
                }
            }
            return coordonees;
        }

        /// <summary>
        /// Check move of the penguin
        /// </summary>
        /// <param name="coordonees">coordinates origin and destination</param>
        /// <returns>list of coordinates of cells where the penguin can move</returns>
        public IList<Coordonees> CheckDeplacement(Dictionary<string, Coordonees> coordonees)
        {
            IList<Coordonees> result = new List<Coordonees>();
            for (int i = 0; i < 6; i++)
            {
                IList<Coordonees> list = i >= 0 && i < 2
                    // Appelle la fonction CheckCaseRightLeft avec DirectionType.Droite puis DirectionType.Gauche
                    ? CheckCaseRightLeft((DirectionType)i, coordonees)
                    // Appelle la fonction CheckCaseDiago avec DirectionType.BasGauche puis DirectionType.BasDroite, puis DirectionType.HautDroite, puis DirectionType.HautGauche
                    : CheckCaseDiago((DirectionType)i, coordonees);

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
        /// <param name="coordonees">coordinates origin and destination</param>
        /// <returns></returns>
        public IList<Coordonees> CheckCaseDiago(DirectionType directionType, Dictionary<string, Coordonees> coordonees)
        {
            IList<Coordonees> result = new List<Coordonees>();

            // on vérifie que le penguin veut bien aller en diagonale
            var x = coordonees["destination"].X - coordonees["origin"].X;
            var y = coordonees["destination"].Y - coordonees["origin"].Y;
            Console.WriteLine("distance => X : " + x +", Y : "+ y);

            if (x < 0 && y >= 0 && directionType == DirectionType.BasGauche || x >= 0 && y >= 0 && directionType == DirectionType.BasDroite || x < 0 && y < 0 && directionType == DirectionType.HautGauche || x >= 0 && y < 0 && directionType == DirectionType.HautDroite)
            {
                Console.WriteLine("directionType : "+ directionType);
                // si x ou y est négatif alors on les transforme en positif
                x = x < 0 ? x * -1 : x;
                y = y < 0 ? y * -1 : y;

                // on récupère la distance entre la case d'origine et de destination
                int count = x - y > 0 ? x : y;

                int incrementX = 0;
                int incrementY = 1;

                int xDest = coordonees["origin"].X;
                int yDest = coordonees["origin"].Y;

                // on va récupérer toutes les cases entre l'origine et la destination si il n'y a pas d'obstacles
                while (incrementY <= count)
                {
                    Console.WriteLine("(pot) => X : " + yDest + ", Y : " + xDest);
                    // si la derniere case vérifiée n'est pas un modulo de 2 alors on incrémente de 1 (sauf si c'est le premier tour)
                    if (yDest % 2 != 0)
                    {
                        incrementX++;

                    }
                    
                    // si le penguin veut aller en BasGauche et qu'il n'y a pas d'obstacle
                    if (directionType == DirectionType.BasGauche && coordonees["origin"].X - incrementX >= 0 && coordonees["origin"].Y + incrementY <= 7)
                    {
                        Console.WriteLine("(depassement) => X : " + (coordonees["origin"].Y + incrementY) + ", Y : " + (coordonees["origin"].X - incrementX));
                        if (Board.Board[coordonees["origin"].X - incrementX, coordonees["origin"].Y + incrementY].CellType == CellType.Fish)
                        {
                            // alors on ajoute la case possible au résultat
                            yDest = coordonees["origin"].Y + incrementY;
                            xDest = coordonees["origin"].X - incrementX;
                            Console.WriteLine("X : " + yDest + ", Y : " + xDest);
                            result.Add(new Coordonees(xDest, yDest));
                        }
                        else
                        {
                            return null;
                        }
                    }
                    // si le penguin veut aller en BasDroite et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.BasDroite && coordonees["origin"].X + incrementX <= 7 && coordonees["origin"].Y + incrementY <= 7)
                    {
                        Console.WriteLine("(depassement) => X : " + (coordonees["origin"].Y + incrementY) + ", Y : " + (coordonees["origin"].X + incrementX));
                        if (Board.Board[coordonees["origin"].X + incrementX, coordonees["origin"].Y + incrementY].CellType == CellType.Fish)
                        {
                            // alors on ajoute la case possible au résultat
                            yDest = coordonees["origin"].Y + incrementY;
                            xDest = coordonees["origin"].X + incrementX;
                            Console.WriteLine("X : " + yDest + ", Y : " + xDest);
                            result.Add(new Coordonees(xDest, yDest));
                        }
                        else
                        {
                            return null;
                        }
                    }
                    // si le penguin veut aller en HautGauche et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.HautGauche && coordonees["origin"].X - incrementX >= 0 && coordonees["origin"].Y - incrementY >= 0)
                    {
                        Console.WriteLine("(depassement) => X : " + (coordonees["origin"].Y - incrementY) + ", Y : " + (coordonees["origin"].X - incrementX));
                        if (Board.Board[coordonees["origin"].X - incrementX, coordonees["origin"].Y - incrementY].CellType == CellType.Fish)
                        {
                            // alors on ajoute la case possible au résultat
                            yDest = coordonees["origin"].Y - incrementY;
                            xDest = coordonees["origin"].X - incrementX;
                            Console.WriteLine("X : " + yDest + ", Y : " + xDest);
                            result.Add(new Coordonees(xDest, yDest));
                        }
                        else
                        {
                            return null;
                        }
                    }
                    // si le penguin veut aller en HautDroite et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.HautDroite && coordonees["origin"].X + incrementX <= 7 && coordonees["origin"].Y - incrementY >= 0)
                    {
                        Console.WriteLine("(depassement) => X : " + (coordonees["origin"].Y - incrementY) + ", Y : " + (coordonees["origin"].X + incrementX));
                        if (Board.Board[coordonees["origin"].X + incrementX, coordonees["origin"].Y - incrementY].CellType == CellType.Fish)
                        {
                            // alors on ajoute la case possible au résultat
                            yDest = coordonees["origin"].Y - incrementY;
                            xDest = coordonees["origin"].X + incrementX;
                            Console.WriteLine("X : " + yDest + ", Y : " + xDest);
                            result.Add(new Coordonees(xDest, yDest));
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
        /// <param name="coordonees">coordinates origin and destination</param>
        /// <returns></returns>
        public IList<Coordonees> CheckCaseRightLeft(DirectionType directionType, Dictionary<string, Coordonees> coordonees)
        {
            IList<Coordonees> result = new List<Coordonees>();

            //récupère la distance entre l'origine et la destination
            var x = coordonees["destination"].X - coordonees["origin"].X;

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
                    if (directionType == DirectionType.Droite && Board.Board[coordonees["origin"].X + increment, coordonees["origin"].Y].CellType == CellType.Fish)
                    {
                        // alors on ajoute la case possible au résultat
                        result.Add(new Coordonees(coordonees["origin"].X + increment, coordonees["origin"].Y));
                    }
                    // si le penguin veut aller à gauche et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.Gauche && Board.Board[coordonees["origin"].X - increment, coordonees["origin"].Y].CellType == CellType.Fish)
                    {
                        // alors on ajoute la case possible au résultat
                        result.Add(new Coordonees(coordonees["origin"].X - increment, coordonees["origin"].Y));
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
