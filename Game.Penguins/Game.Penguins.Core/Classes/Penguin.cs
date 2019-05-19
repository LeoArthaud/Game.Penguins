using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.Core.Classes
{
    public class Penguin : IPenguin
    {
        public IPlayer Player { get; set; }

        public Penguin(IPlayer player)
        {
            Player = player;
        }
    }
}
