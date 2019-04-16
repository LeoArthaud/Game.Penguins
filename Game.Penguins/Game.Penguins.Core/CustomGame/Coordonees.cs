using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Penguins.Core.CustomGame
{
    public class Coordonees
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Coordonees(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
