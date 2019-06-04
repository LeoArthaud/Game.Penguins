namespace Game.Penguins.Core.Classes.Board
{
    /// <summary>
    /// Represents a set of x, y coordinates.
    /// </summary>
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Coordinates(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
