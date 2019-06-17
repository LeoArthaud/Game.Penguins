using System;
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
    /// Easy difficulty AI. Subclass of AIGlobal.
    /// </summary>
    public class AIHard : BetterAIGlobal
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="board"></param>
        /// <param name="random"></param>
        /// <param name="currentPlayer"></param>
        public AIHard(IBoard board, IRandom random, IPlayer currentPlayer) : base(board, random, currentPlayer)
        {
            Board = board;
            this.random = random;
            CurrentPlayer = currentPlayer;
            FishCoefficient = 3;
            BlockOthersCoefficient = 1;
            BlockSelfCoefficient = 2;
        }
    }
}
