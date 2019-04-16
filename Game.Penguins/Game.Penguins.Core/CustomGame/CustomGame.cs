using System;
using System.Collections.Generic;
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

                        coordonees.Add("origin", new Coordonees(j,i) );
                    }
                    if (destination == Board.Board[i, j])
                    {
                        coordonees.Add("destination", new Coordonees(j, i));
                    }
                }
            }
            return coordonees;
        }

        public bool CheckDeplacement(Dictionary<string, Coordonees> coordonees)
        {
            //get origin

            //check angle

            //verif pingouins

            //

            return true;
        }

        /// <summary>
        /// Add player to list Players
        /// </summary>
        /// <param name="playerName">Nom du joueur</param>
        /// <param name="playerType">Type du joueur (human ou AI)</param>
        /// <returns>return player which is add</returns>
        IPlayer IGame.AddPlayer(string playerName, PlayerType playerType)
        {
            Player player = new Player(playerName, playerType, (PlayerColor)CountPlayers);
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

            //Attribut à chaque joueur le nombre de penguins et sa couleur
            foreach (Player player in Players)
            {
                player.Penguins = numberPenguins;
            }

            // Le premier joueur est choisi aléatoirement
            Random random = new Random();
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
            throw new NotImplementedException();
        }

        public void MoveManual(ICell origin, ICell destination)
        {
            if (origin.CellType == CellType.FishWithPenguin && destination.CellType != CellType.Water)
            {
                if (origin.CurrentPenguin.Player == CurrentPlayer)
                {

                    Dictionary<string, Coordonees> result = GetCoordonees(origin, destination);
                    Console.WriteLine(result["origin"].X + "," + result["origin"].Y);
                    Console.WriteLine(result["destination"].X + "," + result["destination"].Y);

                    if (CheckDeplacement(result))
                    {
                        //On définit les cellules
                        Cell cellOrigine = (Cell)origin;
                        Cell cellDestination = (Cell)destination;

                        //On donne les points au Joueur
                        Player PlayerCurrent = (Player)CurrentPlayer;
                        PlayerCurrent.Points += cellOrigine.FishCount;
                        Console.WriteLine(CurrentPlayer.Points);

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
