﻿using Game.Penguins.AI;
using Game.Penguins.Core.Classes;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Classes.Move;
using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
namespace Game.Penguins.Helper.CustomGame
{
    /// <summary>
    /// Instance of the game
    /// </summary>
    public class CustomGame : IGame
    {
        /// <summary>
        /// Board
        /// </summary>
        public IBoard Board { get; set; }

        /// <summary>
        /// Next action
        /// </summary>
        public NextActionType NextAction { get; set; }

        /// <summary>
        /// Current player
        /// </summary>
        public IPlayer CurrentPlayer { get; set; }

        /// <summary>
        /// List of players
        /// </summary>
        public IList<IPlayer> Players { get; set; }

        /// <summary>
        /// Apply changes
        /// </summary>
        public event EventHandler StateChanged;
        
        /// <summary>
        /// Id of current player
        /// </summary>
        public int IdPlayer { get; set; }

        /// <summary>
        /// Positions of penguins of current player
        /// </summary>
        public List<Coordinates> PossibilitiesOrigin { get; set; }

        /// <summary>
        /// Allow to generate random number
        /// </summary>
        private IRandom random;

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomGame(IRandom random)
        {
            // Initialize list of players
            Players = new List<IPlayer>();

            // Create a random board
            Board = new GameBoard();

            // Initialize the random function
            this.random = random;
        }

        #region Public Functions

        /// <summary>
        /// Add player to list Players
        /// </summary>
        /// <param name="playerName">Name of player</param>
        /// <param name="playerType">Type of player (human or AI)</param>
        /// <returns>return player which is add</returns>
        public IPlayer AddPlayer(string playerName, PlayerType playerType)
        {
            // Create new player with the name and the type
            Player player = new Player(playerName, playerType);

            // Add the player to the list
            Players.Add(player);

            // Return player
            return player;
        }

        /// <summary>
        /// Count the number of penguin of each player
        /// </summary>
        public void StartGame()
        {

            // Get the number of penguins foreach player
            int numberPenguins = 0;
            switch (Players.Count)
            {
                // If there are 2 players
                case 2:
                    numberPenguins = 4;
                    break;
                // If there are 3 players
                case 3:
                    numberPenguins = 3;
                    break;
                // If there are 4 players
                case 4:
                    numberPenguins = 2;
                    break;
            }

            // List of color already assign to players
            List<int> colorOfPlayer = new List<int>();

            // Foreach player
            foreach (var player1 in Players)
            {
                var player = (Player) player1;

                // Give the number of penguins
                player.Penguins = numberPenguins;

                // Get a random color
                int color = random.Next(0, 4);

                // If the color is already assign
                while (colorOfPlayer.Contains(color))
                {
                    // Get an other random color
                    color = random.Next(0, 4);
                }
                
                // Give the color to the player
                player.Color = (PlayerColor)color;

                // Add the color to the list
                colorOfPlayer.Add(color);
            }

            // Choose randomly the first player
            IdPlayer = random.Next(0, Players.Count);
            CurrentPlayer = Players[IdPlayer];

            // Define the next action
            NextAction = NextActionType.PlacePenguin;

            // Apply changes
            StateChanged?.Invoke(this, null);

            // Logging test
            Common.Logging.ILog log = Common.Logging.LogManager.GetLogger(GetType().ToString());
            log.Info("Game successfully started");
            // TODO: Add relevant logging in relevant places
        }

        /// <summary>
        /// The player places a penguin manually on the board
        /// </summary>
        /// <param name="x">x of the selected cell</param>
        /// <param name="y">y of the selected cell</param>
        public void PlacePenguinManual(int x, int y)
        {
            // If selected cell has one fish and has no penguin already
            if (Board.Board[x, y].FishCount == 1 && Board.Board[x,y].CellType != CellType.FishWithPenguin)
            {
                // Apply change
                ChangeStatePlace(x, y);
            }
        }

        /// <summary>
        /// Place penguin for AI
        /// </summary>
        public void PlacePenguin()
        {
            // If AI is easy
            if (CurrentPlayer.PlayerType == PlayerType.AIEasy)
            {
                // Call AIEasy
                AIEasy aiEasy = new AIEasy(Board, random, CurrentPlayer);

                // Get coordinates
                Coordinates coordinates = aiEasy.PlacePenguin();

                // Apply changes
                ChangeStatePlace(coordinates.X, coordinates.Y);
            }
            // If AI is medium
            else if (CurrentPlayer.PlayerType == PlayerType.AIMedium)
            {
                // Call AIMedium
                AIMedium aiMedium = new AIMedium(Board, random, CurrentPlayer);

                // Get coordinates
                Coordinates coordinates = aiMedium.PlacePenguin();

                // Apply changes
                ChangeStatePlace(coordinates.X, coordinates.Y);
            }
            // If AI is hard
            else if (CurrentPlayer.PlayerType == PlayerType.AIHard)
            {
                // Call AIHard
                AIHard aiHard = new AIHard(Board, random, CurrentPlayer);

                // Get coordinates
                Coordinates coordinates = aiHard.PlacePenguin();

                // Apply changes
                ChangeStatePlace(coordinates.X, coordinates.Y);
            }


        }

        /// <summary>
        /// Move penguin for human
        /// </summary>
        /// <param name="origin">Origin Cell</param>
        /// <param name="destination">Destination Cell</param>
        public void MoveManual(ICell origin, ICell destination)
        {
            // If the selected origin cell has a penguin
            // AND If the selected destination cell has fish
            if (origin.CellType == CellType.FishWithPenguin && destination.CellType == CellType.Fish)
            {
                // If the selected origin cell has a penguin which belongs to current player
                if (origin.CurrentPenguin.Player == CurrentPlayer)
                {
                    Movements move = new Movements(origin, destination, Board);

                    // Get coordinates of origin and destination cells
                    Dictionary<string, Coordinates> result = move.GetCoordinates();

                    // Get if the destination cell belongs to the list of possibilities
                    int numberOfResults = move.CheckDeplacement(result["origin"])
                                            .Count(element => element.X == result["destination"].X && element.Y == result["destination"].Y);
                    
                    // If the destination cell belongs to the list
                    if (numberOfResults > 0)
                    {
                        // Apply changes
                        ChangeStateMove(origin, destination);

                        // Check if the player can move his penguins
                        PossibilitiesOfOrigin();
                        IList<Coordinates> coordinates = new List<Coordinates>();
                        foreach (var possibility in PossibilitiesOrigin)
                        {
                            Player playerCurrent = (Player)CurrentPlayer;
                            var list = move.CheckDeplacement(possibility);
                            if (list.Count == 0)
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
                        AffectedCurrentPlayer(ChangeType.Move);
                    }
                }
            }
        }

        /// <summary>
        /// Move penguin for AI
        /// </summary>
        public void Move()
        {
            // If AI is easy
            if (CurrentPlayer.PlayerType == PlayerType.AIEasy)
            {
                // Call an AIEasy
                AIEasy aiEasy = new AIEasy(Board, random, CurrentPlayer);

                // Get positions of penguins of the current player
                PossibilitiesOfOrigin();

                // Get the penguin to move
                Coordinates origin = aiEasy.FindOrigin(PossibilitiesOrigin);

                // Get the destination of the penguin
                Coordinates destination = aiEasy.FindDestination(origin);

                // Apply changes
                ChangeStateMove(Board.Board[origin.X, origin.Y], Board.Board[destination.X, destination.Y]);
                AffectedCurrentPlayer(ChangeType.Move);
                
            }
            // If AI is medium
            else if (CurrentPlayer.PlayerType == PlayerType.AIMedium)
            {
                // Call an AIMedium
                AIMedium aiMedium = new AIMedium(Board, random, CurrentPlayer);

                // Get positions of penguins of the current player
                PossibilitiesOfOrigin();
                var coordinates = aiMedium.FindOriginDestination();

                // Get the penguin to move
                Coordinates origin = coordinates["origin"];
                Coordinates destination = coordinates["destination"];

                // Apply changes
                ChangeStateMove(Board.Board[origin.X, origin.Y], Board.Board[destination.X, destination.Y]);
                AffectedCurrentPlayer(ChangeType.Move);
                
            }
            // If AI is hard
            else if (CurrentPlayer.PlayerType == PlayerType.AIHard)
            {
                // Call an AIHard
                AIHard aiHard = new AIHard(Board, random, CurrentPlayer);

                // Get positions of penguins of the current player
                PossibilitiesOfOrigin();
                var coordinates = aiHard.FindOriginDestination();

                // Get the penguin to move
                Coordinates origin = coordinates["origin"];
                Coordinates destination = coordinates["destination"];

                // Apply changes
                ChangeStateMove(Board.Board[origin.X, origin.Y], Board.Board[destination.X, destination.Y]);
                AffectedCurrentPlayer(ChangeType.Move);
            }
        }
        
        #endregion

        #region Privates Functions

        /// <summary>
        /// Apply placement
        /// </summary>
        /// <param name="x">Position x of placement</param>
        /// <param name="y">Position y of placement</param>
        private void ChangeStatePlace(int x, int y)
        {
            // Modify selected cell
            Cell cell = (Cell)Board.Board[x, y];
            cell.CurrentPenguin = new Penguin(CurrentPlayer);
            cell.CellType = CellType.FishWithPenguin;

            // Define next action
            NextAction = NextActionType.MovePenguin;

            // Apply change
            cell.ChangeState();

            // Remove penguin of the hand of the player
            foreach (var player1 in Players)
            {
                var player = (Player)player1;
                if (player == CurrentPlayer)
                {
                    player.Penguins -= 1;
                }
            }

            // Change current player
            AffectedCurrentPlayer(ChangeType.Place);

            // Define next action
            NextAction = CurrentPlayer.Penguins == 0 ? NextActionType.MovePenguin : NextActionType.PlacePenguin;

            // Apply all changes
            StateChanged?.Invoke(this, null);
        }

        /// <summary>
        /// Apply changes of movements
        /// </summary>
        /// <param name="origin">Origin of movement</param>
        /// <param name="destination">Destination of movement</param>
        private void ChangeStateMove(ICell origin, ICell destination)
        {
            // Get cells
            Cell cellOrigin = (Cell)origin;
            Cell cellDestination = (Cell)destination;

            // Give points to the current player
            Player playerCurrent = (Player)CurrentPlayer;
            playerCurrent.Points += cellOrigin.FishCount;

            // Modify origin cell
            cellOrigin.CellType = CellType.Water;
            cellOrigin.FishCount = 0;
            cellOrigin.CurrentPenguin = null;

            // Modify destination cell
            cellDestination.CellType = CellType.FishWithPenguin;
            cellDestination.CurrentPenguin = new Penguin(CurrentPlayer);
            
            // Apply changes
            cellOrigin.ChangeState();
            cellDestination.ChangeState();
            NextAction = NextActionType.MovePenguin;
        }

        /// <summary>
        /// Affected next player
        /// </summary>
        private void AffectedCurrentPlayer(ChangeType changeType)
        {
            Player playerCurrent = (Player)CurrentPlayer;

            ChangeCurrentPlayer();
            int count = 0;
            if (changeType == ChangeType.Move)
            {
                PossibilitiesOfOrigin();
                while (PossibilitiesOrigin.Count == 0 && count < Players.Count)
                {
                    ChangeCurrentPlayer();
                    PossibilitiesOfOrigin();
                    count++;
                }
            }

            if (count > Players.Count && PossibilitiesOrigin.Count == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Cell cell = (Cell)Board.Board[i, j];
                        cell.CellType = CellType.Water;
                        cell.CurrentPenguin = null;
                        cell.FishCount = 0;
                        cell.ChangeState();
                    }
                }
            }

            // Apply change
            playerCurrent.ChangeState();
        }

        /// <summary>
        /// Determine who is the next player
        /// </summary>
        private void ChangeCurrentPlayer()
        {
            if (IdPlayer + 1 < Players.Count)
            {
                IdPlayer = IdPlayer + 1;
                CurrentPlayer = Players[IdPlayer];
            }
            else
            {
                IdPlayer = 0;
                CurrentPlayer = Players[IdPlayer];
            }
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
        
        #endregion
    }
}
