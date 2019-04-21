using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.CustomGame
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomGame : IGame
    {
        /// <summary>
        /// 
        /// </summary>
        public IBoard Board { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public NextActionType NextAction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IPlayer CurrentPlayer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<IPlayer> Players { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler StateChanged;

        /// <summary>
        /// 
        /// </summary>
        public int CountPlayers = 0;

        public int IdPlayer;

        public CustomGame()
        {
            Players = new List<IPlayer>();
            Board = new GameBoard();
        }

        public Dictionary<string, Coordonees> GetCoordonees(ICell origin, ICell destination)
        {
            Dictionary<string, Coordonees> coordonees = new Dictionary<string, Coordonees>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (origin == Board.Board[i, j])
                    {

                        coordonees.Add("origin", new Coordonees(i,j) );
                    }
                    if (destination == Board.Board[i, j])
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
        /// <param name="coordonees"></param>
        /// <returns></returns>
        public IList<Coordonees> CheckDeplacement(Dictionary<string, Coordonees> coordonees)
        {
            IList<Coordonees> result = new List<Coordonees>();
            for (int i = 0; i < 6; i++)
            {
                IList<Coordonees> list = i >= 0 && i < 2
                    // Appelle la fonction CheckCaseRightLeft avec DirectionType.Droite puis DirectionType.Gauche
                    ? CheckCaseRightLeft((DirectionType) i, coordonees)
                    // Appelle la fonction CheckCaseDiago avec DirectionType.BasGauche puis DirectionType.BasDroite, puis DirectionType.HautDroite, puis DirectionType.HautGauche
                    : CheckCaseDiago((DirectionType) i, coordonees);

                if (list != null)
                {
                    foreach (var element in list)
                    {
                        result.Add(element);
                    }
                }
            }

            Console.WriteLine("--------");
            return result;
        }

        /// <summary>
        /// Check if the penguin can move in diago
        /// </summary>
        /// <param name="directionType"></param>
        /// <param name="coordonees"></param>
        /// <returns></returns>
        public IList<Coordonees> CheckCaseDiago(DirectionType directionType, Dictionary<string, Coordonees> coordonees)
        {
            IList<Coordonees> result = new List<Coordonees>();

            // on vérifie que le penguin veut bien aller en diagonale
            var x = coordonees["destination"].X - coordonees["origin"].X;
            var y = coordonees["destination"].Y - coordonees["origin"].Y;

            if (x < 0 && y > 0 && directionType == DirectionType.BasGauche || x > 0 && y > 0 && directionType == DirectionType.BasDroite || x < 0 && y < 0 && directionType == DirectionType.HautGauche || x > 0 && y < 0 && directionType == DirectionType.HautDroite)
            {
                Console.WriteLine(directionType);

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
                    // si y est un modulo 2 alors on incrémente de 1
                    if (yDest % 2 == 0)
                    {
                        incrementX++;
                    }

                    // si le penguin veut aller en BasGauche et qu'il n'y a pas d'obstacle
                    if (directionType == DirectionType.BasGauche && Board.Board[coordonees["origin"].X - incrementX, coordonees["origin"].Y + incrementY].CellType == CellType.Fish)
                    {
                        // alors on ajoute la case possible au résultat
                        yDest = coordonees["origin"].Y + incrementY;
                        xDest = coordonees["origin"].X - incrementX;
                        result.Add(new Coordonees(xDest, yDest));
                        Console.WriteLine("Y : " + (xDest) + ", X : " + (yDest));
                    }
                    // si le penguin veut aller en BasDroite et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.BasDroite && Board.Board[coordonees["origin"].X + incrementX, coordonees["origin"].Y + incrementY].CellType == CellType.Fish)
                    {
                        // alors on ajoute la case possible au résultat
                        yDest = coordonees["origin"].Y + incrementY;
                        xDest = coordonees["origin"].X + incrementX;
                        result.Add(new Coordonees(xDest, yDest));
                        Console.WriteLine("Y : " + (xDest) + ", X : " + (yDest));
                    }
                    // si le penguin veut aller en HautGauche et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.HautGauche && Board.Board[coordonees["origin"].X - incrementX, coordonees["origin"].Y - incrementY].CellType == CellType.Fish)
                    {
                        // alors on ajoute la case possible au résultat
                        yDest = coordonees["origin"].Y - incrementY;
                        xDest = coordonees["origin"].X - incrementX;
                        result.Add(new Coordonees(xDest, yDest));
                        Console.WriteLine("Y : " + (xDest) + ", X : " + (yDest));
                    }
                    // si le penguin veut aller en HautDroite et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.HautDroite && Board.Board[coordonees["origin"].X + incrementX, coordonees["origin"].Y - incrementY].CellType == CellType.Fish)
                    {
                        // alors on ajoute la case possible au résultat
                        yDest = coordonees["origin"].Y - incrementY;
                        xDest = coordonees["origin"].X + incrementX;
                        result.Add(new Coordonees(xDest, yDest));
                        Console.WriteLine("Y : " + (xDest) + ", X : " + (yDest));
                    }
                    // si un obstacle se trouve sur le chemin du penguin
                    else
                    {
                        // alors le penguin ne peut pas continuer sa course et doit choisir une autre case pour se déplacer
                        return null;
                    }

                    incrementY++;
                }
            }

            return result;
        }

        /// <summary>
        /// Check if the penguin can move left/right
        /// </summary>
        /// <param name="directionType"></param>
        /// <param name="coordonees"></param>
        /// <returns></returns>
        public IList<Coordonees> CheckCaseRightLeft(DirectionType directionType, Dictionary<string, Coordonees> coordonees)
        {
            IList<Coordonees> result = new List<Coordonees>();

            //récupère la distance entre l'origine et la destination
            var x = coordonees["destination"].X - coordonees["origin"].X;

            //si elle est positive alors le penguin veut aller à droite sinon il veut aller à gauche
            if (x > 0 && directionType == DirectionType.Droite || x < 0 && directionType == DirectionType.Gauche)
            {
                Console.WriteLine(directionType);

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
                        Console.WriteLine("Y : " + (coordonees["origin"].X + increment) + ", X : " + (coordonees["origin"].Y));
                    }
                    // si le penguin veut aller à gauche et qu'il n'y a pas d'obstacle
                    else if (directionType == DirectionType.Gauche && Board.Board[coordonees["origin"].X - increment, coordonees["origin"].Y].CellType == CellType.Fish)
                    {
                        // alors on ajoute la case possible au résultat
                        result.Add(new Coordonees(coordonees["origin"].X - increment, coordonees["origin"].Y));
                        Console.WriteLine("Y : " + (coordonees["origin"].X - increment) + ", X : " + (coordonees["origin"].Y));
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

        /// <summary>
        /// Add player to list Players
        /// </summary>
        /// <param name="playerName">Nom du joueur</param>
        /// <param name="playerType">Type du joueur (human ou AI)</param>
        /// <returns>return player which is add</returns>
        IPlayer IGame.AddPlayer(string playerName, PlayerType playerType)
        {
            Player player = new Player(playerName, playerType);
            Players.Add(player);
            CountPlayers++;
            return player;
        }

        /// <summary>
        /// Count the number of penguin of each player
        /// </summary>
        public void StartGame()
        {
            // On détermine le nombre de penguin par joueur
            int numberPenguins = 0;
            if (CountPlayers == 2)
            {
                numberPenguins = 4;
            }
            else if (CountPlayers == 3)
            {
                numberPenguins = 3;
            }
            else if (CountPlayers == 4)
            {
                numberPenguins = 2;
            }

            Random random = new Random();
            List<int> colorOfPlayer = new List<int>();

            //Attribut à chaque joueur le nombre de penguins et sa couleur
            foreach (Player player in Players)
            {
                //number of penguin
                player.Penguins = numberPenguins;

                //color of player
                int color = random.Next(0, 4);
                while (colorOfPlayer.Contains(color))
                {
                    color = random.Next(0, 4);
                }
                
                player.Color = (PlayerColor)color;
                colorOfPlayer.Add(color);
            }

            // Le premier joueur est choisi aléatoirement
            IdPlayer = random.Next(0, Players.Count);
            CurrentPlayer = Players[IdPlayer];

            //On défini la nouvelle action à faire
            NextAction = NextActionType.PlacePenguin;
            StateChanged(this, null);
        }

        /// <summary>
        /// Le joueur place un penguin manuellement sur le plateau
        /// </summary>
        /// <param name="x">La position x de la case choisie</param>
        /// <param name="y">La position y de la case choisie</param>
        public void PlacePenguinManual(int x, int y)
        {
            //on rentre dans la boucle si le joueur se place sur 1 seul poisson
            if (Board.Board[x, y].FishCount == 1 && Board.Board[x,y].CellType != CellType.FishWithPenguin)
            {
                //On change le type de la cellule choisi
                Cell cell = (Cell)Board.Board[x, y];
                cell.CurrentPenguin = new Penguin(CurrentPlayer);
                cell.CellType = CellType.FishWithPenguin;
                
                //On défini la nouvelle action à faire
                NextAction = NextActionType.MovePenguin;

                //On déclare le changement d'état de la cellule
                cell.ChangeState();

                foreach (Player player in Players)
                {
                    if (player == CurrentPlayer)
                    {
                        player.Penguins = player.Penguins - 1;
                    }
                }

                // Lorsque le penguin a été posé, on change de CurrentPlayer
                if (IdPlayer + 1 < CountPlayers)
                {
                    IdPlayer = IdPlayer + 1;
                    CurrentPlayer = Players[IdPlayer];
                }
                else
                {
                    IdPlayer = 0;
                    CurrentPlayer = Players[IdPlayer];
                }

                // On défini la nouvelle action à faire
                if (CurrentPlayer.Penguins == 0)
                {
                    NextAction = NextActionType.MovePenguin;
                }
                else
                {
                    NextAction = NextActionType.PlacePenguin;
                }
                StateChanged(this, null);
            }
        }

        public void PlacePenguin()
        {
            Random rnd = new Random();
            int randomX;
            int randomY;

            randomX = rnd.Next(0, 8); //7
            randomY = rnd.Next(0, 8); //5
            // 7, 5

            while (Board.Board[randomX, randomY].FishCount != 1 || Board.Board[randomX, randomY].CellType == CellType.FishWithPenguin)
            {
                randomX = rnd.Next(0, 8); //7
                randomY = rnd.Next(0, 8); //5
            }

            //On change le type de la cellule choisi
            Cell cell = (Cell)Board.Board[randomX, randomY];
            cell.CurrentPenguin = new Penguin(CurrentPlayer);
            cell.CellType = CellType.FishWithPenguin;

            //On défini la nouvelle action à faire
            NextAction = NextActionType.MovePenguin;

            //On déclare le changement d'état de la cellule
            cell.ChangeState();

            foreach (Player player in Players)
            {
                if (player == CurrentPlayer)
                {
                    player.Penguins = player.Penguins - 1;
                }
            }

            // Lorsque le penguin a été posé, on change de CurrentPlayer
            if (IdPlayer + 1 < CountPlayers)
            {
                IdPlayer = IdPlayer + 1;
                CurrentPlayer = Players[IdPlayer];
            }
            else
            {
                IdPlayer = 0;
                CurrentPlayer = Players[IdPlayer];
            }

            // On défini la nouvelle action à faire
            if (CurrentPlayer.Penguins == 0)
            {
                NextAction = NextActionType.MovePenguin;
            }
            else
            {
                NextAction = NextActionType.PlacePenguin;
            }
            StateChanged(this, null);

        }

        public void MoveManual(ICell origin, ICell destination)
        {
            if (origin.CellType == CellType.FishWithPenguin && destination.CellType != CellType.Water)
            {
                if (origin.CurrentPenguin.Player == CurrentPlayer)
                {
                    Dictionary<string, Coordonees> result = GetCoordonees(origin, destination);

                    Console.WriteLine("-----------------");
                    Console.WriteLine("ORIGIN = Y : " + result["origin"].X + ", X : " + result["origin"].Y);
                    Console.WriteLine("DESTINATION = Y : " + result["destination"].X + ", X : " + result["destination"].Y);
                    Console.WriteLine("-----------------");

                    int numberOfResults = CheckDeplacement(result)
                                            .Count(element => element.X == result["destination"].X && element.Y == result["destination"].Y);
                    
                    Console.WriteLine("numberOfResults : " + numberOfResults);

                    if (numberOfResults == 1)
                    {
                        //On définit les cellules
                        Cell cellOrigine = (Cell)origin;
                        Cell cellDestination = (Cell)destination;

                        //On donne les points au Joueur
                        Player PlayerCurrent = (Player)CurrentPlayer;
                        PlayerCurrent.Points += cellOrigine.FishCount;
                        //Console.WriteLine(CurrentPlayer.Points);

                        //On modifie la cellule d'origine en cellule vide
                        cellOrigine.CellType = CellType.Water;
                        cellOrigine.FishCount = 0;
                        cellOrigine.CurrentPenguin = null;

                        //On modifie la cellule de destination en cellule avec un pingouins
                        cellDestination.CellType = CellType.FishWithPenguin;
                        cellDestination.CurrentPenguin = new Penguin(CurrentPlayer);


                        // Lorsque le penguin a été déplacé, on change de CurrentPlayer
                        if (IdPlayer + 1 < CountPlayers)
                        {
                            IdPlayer = IdPlayer + 1;
                            CurrentPlayer = Players[IdPlayer];
                        }
                        else
                        {
                            IdPlayer = 0;
                            CurrentPlayer = Players[IdPlayer];
                        }

                        //On actualise tout
                        cellOrigine.ChangeState();
                        cellDestination.ChangeState();
                        PlayerCurrent.ChangeState();
                        NextAction = NextActionType.MovePenguin;
                    }
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
