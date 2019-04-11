using System;
using System.Collections.Generic;
using System.Text;
using Game.Penguins.Core.Interfaces.Game.GameBoard;
using Game.Penguins.Core.Interfaces.Game.Players;

namespace Game.Penguins.AI
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
