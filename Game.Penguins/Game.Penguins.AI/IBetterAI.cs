using System.Collections.Generic;
using Game.Penguins.Core.Classes.Board;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.AI
{
    /// <summary>
    /// Interface of BetterAI classes. Implemented by BetterAIGlobal superclass.
    /// </summary>
    interface IBetterAI
    {
        /// <summary>
        /// Board
        /// </summary>
        IBoard Board { get; set; }

        /// <summary>
        /// Current player
        /// </summary>
        IPlayer CurrentPlayer { get; set; }

        /// <summary>
        /// Coefficient of FishCount for CellScores.
        /// </summary>
        float FishCoefficient { get; set; }

        /// <summary>
        /// Coefficient of cells blocking others for CellScores.
        /// </summary>
        float BlockOthersCoefficient { get; set; }

        /// <summary>
        /// Coefficient of cells blocking self for CellScores.
        /// </summary>
        float BlockSelfCoefficient { get; set; }

        /// <summary>
        /// List of CellScores to keep track of best cells.
        /// </summary>
        List<CellScore> CellScores { get; set; }

        /// <summary>
        /// Positions of penguins of current player
        /// </summary>
        List<Coordinates> PossibilitiesOrigin { get; set; }

        /// <summary>
        /// Check and update CellScores values (= condition1 score*coefficient + condition2 score*coefficient + ...).
        /// </summary>
        void CheckBoard();

        /// <summary>
        /// Get coordinates where to place the penguin
        /// </summary>
        /// <returns>Coordinates</returns>
        Coordinates PlacePenguin();

        /// <summary>
        /// Get the best penguin to move from a list of origins
        /// </summary>
        /// <param name="possibilitiesOrigin">List of penguin origins</param>
        /// <returns>Position of the penguin or [-1;-1] if the AI can't move his penguins</returns>
        Coordinates FindOrigin(List<Coordinates> possibilitiesOrigin);

        /// <summary>
        /// Find the best destination available for a penguin from its origin.
        /// </summary>
        /// <param name="origin">Coordinates of the origin</param>
        /// <returns>Coordinates of the best destination</returns>
        Coordinates FindDestination(Coordinates origin);

        /// <summary>
        /// Get the best cell-score a penguin can reach from its origin. Used for choosing the best penguin to use in FindOrigin().
        /// </summary>
        /// <param name="origin">Coordinates of the origin of the penguin</param>
        /// <returns>Best cell-score reachable from the selected penguin</returns>
        CellScore FindBestCellScore(Coordinates origin);

        /// <summary>
        /// Check CellScores that can block enemies and add value to them.
        /// </summary>
        void CheckBlockOther();

        /// <summary>
        /// Check CellScores that can block self and subtract value to them.
        /// </summary>
        /// <param name="origin">Origin of the selected penguin</param>
        /// <param name="cellScore">Destination CellScore</param>
        void CheckBlockSelf(Coordinates origin, CellScore cellScore);

        /// <summary>
        /// Get a CellScore from the CellScores of our AI with x and y coordinates.
        /// </summary>
        /// <param name="x">x coordinate of the wanted CellScore</param>
        /// <param name="y">y coordinate of the wanted CellScore</param>
        /// <returns>CellScore with corresponding x and y coordinates</returns>
        CellScore GetCellScore(int x, int y);


        /// <summary>
        /// Get current player penguins
        /// </summary>
        void PossibilitiesOfOrigin();
    }
}
