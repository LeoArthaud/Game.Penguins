namespace Game.Penguins.Core.Interfaces.Game.GameBoard
{
    /// <summary>
    /// Interface for Board objects.
    /// </summary>
    public interface IBoard
    {
        /// <summary>
        /// Current board
        /// </summary>
        ICell[,] Board { get; }
    }
}
