using System;
using System.Collections.Generic;
using System.Linq;
using Game.Penguins.Core.CustomGame.Board;
using Game.Penguins.Core.CustomGame.Move.PlayerHuman;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.CustomGame
{
    /// <summary>
    /// Instance du jeu
    /// </summary>
    public class CustomGame : IGame
    {
        /// <summary>
        /// Plateau
        /// </summary>
        public IBoard Board { get; set; }

        /// <summary>
        /// L'action qui va suivre
        /// </summary>
        public NextActionType NextAction { get; set; }

        /// <summary>
        /// Le joueur actuel
        /// </summary>
        public IPlayer CurrentPlayer { get; set; }

        /// <summary>
        /// La liste des joueurs
        /// </summary>
        public IList<IPlayer> Players { get; set; }

        /// <summary>
        /// Active les changements apportés
        /// </summary>
        public event EventHandler StateChanged;

        /// <summary>
        /// Nombres de joueurs
        /// </summary>
        public int CountPlayers;
        
        /// <summary>
        /// Id qui va servir à déterminer l'ordre des joueurs
        /// </summary>
        public int IdPlayer;

        private IRandom random;

        /// <summary>
        /// Constructeur
        /// </summary>
        public CustomGame(IRandom random)
        {
            Players = new List<IPlayer>();
            Board = new GameBoard();
            this.random = random;
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

            //Random rdm = new Random();
            List<int> colorOfPlayer = new List<int>();

            //Attribut à chaque joueur le nombre de penguins et sa couleur
            foreach (var player1 in Players)
            {
                var player = (Player) player1;
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
            StateChanged?.Invoke(this, null);
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

                foreach (var player1 in Players)
                {
                    var player = (Player) player1;
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
                StateChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Place penguin for AI
        /// </summary>
        public void PlacePenguin()
        {
            // if AI is easy
            if (CurrentPlayer.PlayerType == PlayerType.AIEasy)
            {
                //Random rnd = new Random();
                int randomX;
                int randomY;

                randomX = random.Next(0, 8); //7
                randomY = random.Next(0, 8); //5
                // 7, 5

                while (Board.Board[randomX, randomY].FishCount != 1 || Board.Board[randomX, randomY].CellType == CellType.FishWithPenguin)
                {
                    randomX = random.Next(0, 8); //7
                    randomY = random.Next(0, 8); //5
                }

                //On change le type de la cellule choisi
                Cell cell = (Cell)Board.Board[randomX, randomY];
                cell.CurrentPenguin = new Penguin(CurrentPlayer);
                cell.CellType = CellType.FishWithPenguin;

                //On défini la nouvelle action à faire
                NextAction = NextActionType.MovePenguin;

                //On déclare le changement d'état de la cellule
                cell.ChangeState();

                foreach (var player1 in Players)
                {
                    var player = (Player)player1;
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
            }
            // if AI is medium
            else if (CurrentPlayer.PlayerType == PlayerType.AIMedium)
            {
                throw new NotImplementedException();
            }
            // if AI is hard
            else if (CurrentPlayer.PlayerType == PlayerType.AIHard)
            {
                throw new NotImplementedException();
            }

            StateChanged?.Invoke(this, null);

        }

        /// <summary>
        /// Move penguin for human
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        public void MoveManual(ICell origin, ICell destination)
        {
            if (origin.CellType == CellType.FishWithPenguin && destination.CellType != CellType.Water && destination.CellType != CellType.FishWithPenguin)
            {
                if (origin.CurrentPenguin.Player == CurrentPlayer)
                {
                    MoveOfHuman moveOfHuman = new MoveOfHuman(origin, destination,Board);
                    Dictionary<string, Coordinates> result = moveOfHuman.GetCoordinates();

                    Console.WriteLine("ORIGIN => X : "+result["origin"].Y+", Y : "+result["origin"].X);
                    Console.WriteLine("DESTINATION => X : "+result["destination"].Y+", Y : "+result["destination"].X);
                    Console.WriteLine("-------------");

                    // Vérifie qu'il n'y a pas d'obstacles entre la case destination et origine
                    int numberOfResults = moveOfHuman.CheckDeplacement(result)
                                            .Count(element => element.X == result["destination"].X && element.Y == result["destination"].Y);
                    
                    Console.WriteLine("numberOfResults : "+ numberOfResults);

                    if (numberOfResults > 0)
                    {
                        //On définit les cellules
                        Cell cellOrigine = (Cell)origin;
                        Cell cellDestination = (Cell)destination;

                        //On donne les points au Joueur
                        Player playerCurrent = (Player)CurrentPlayer;
                        playerCurrent.Points += cellOrigine.FishCount;

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
                        playerCurrent.ChangeState();
                        NextAction = NextActionType.MovePenguin;
                    }
                }
            }
            StateChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Move penguin for AI
        /// </summary>
        public void Move()
        {
            // if AI is easy
            if (CurrentPlayer.PlayerType == PlayerType.AIEasy)
            {
                throw new NotImplementedException();
            }
            // if AI is medium
            else if (CurrentPlayer.PlayerType == PlayerType.AIMedium)
            {
                throw new NotImplementedException();
            }
            // if AI is hard
            else if (CurrentPlayer.PlayerType == PlayerType.AIHard)
            {
                throw new NotImplementedException();
            }
        }
    }
}
