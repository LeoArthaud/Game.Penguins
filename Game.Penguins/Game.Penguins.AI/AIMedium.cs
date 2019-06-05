﻿using System;
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
    /// Medium difficulty AI. Subclass of AIGlobal.
    /// </summary>
    public class AIMedium : AIGlobal
    {
        /// <summary>
        /// Positions of penguins of current player
        /// </summary>
        public List<Coordinates> PossibilitiesOrigin { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="board"></param>
        /// <param name="random"></param>
        /// <param name="currentPlayer"></param>
        public AIMedium(IBoard board, IRandom random, IPlayer currentPlayer) : base(board, random, currentPlayer)
        {
            Board = board;
            this.random = random;
            CurrentPlayer = currentPlayer;
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
                var list = move.CheckMove(possibility);
                IList<ICell> resultCells = new List<ICell>();

                foreach (var element in list)
                {
                    resultCells.Add(Board.Board[element.X, element.Y]);
                }
                if (resultCells.Count != 0)
                {
                    var resultOrderBy = resultCells.Where(cell => cell.FishCount == 3).ToList();
                    if (resultOrderBy.Count == 0)
                    {
                        bestCells.Add(possibility, resultCells[random.Next(0, resultCells.Count)]);
                    }
                    else
                    {
                        bestCells.Add(possibility, resultOrderBy[0]);
                    }
                }
            }
            
            
            Coordinates greatOrigin = new Coordinates(0,0);
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
