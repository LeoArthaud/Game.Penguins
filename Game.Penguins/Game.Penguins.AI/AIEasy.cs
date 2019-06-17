using Game.Penguins.Core.Interfaces;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;
namespace Game.Penguins.AI
{
    /// <summary>
    /// Easy difficulty AI. Subclass of AIGlobal.
    /// </summary>
    public class AIEasy : BetterAIGlobal
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="board"></param>
        /// <param name="random"></param>
        /// <param name="currentPlayer"></param>
        public AIEasy(IBoard board, IRandom random, IPlayer currentPlayer) : base(board, random, currentPlayer)
        {
            Board = board;
            this.random = random;
            CurrentPlayer = currentPlayer;
            FishCoefficient = 0;
            BlockOthersCoefficient = 0;
            BlockSelfCoefficient = 0;
        }
    }
}
