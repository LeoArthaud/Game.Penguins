namespace Game.Penguins.Core.Classes.Board
{
    /// <summary>
    /// Represents a set of x, y coordinates.
    /// </summary>
    public class Coordinates
    {
        /// <summary>
        /// x coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// y coordinate
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Coordinates(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
