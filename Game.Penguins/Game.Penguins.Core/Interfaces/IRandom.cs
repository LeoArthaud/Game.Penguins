using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Penguins.Core.Interfaces
{
    /// <summary>
    /// Interface pour simuler des nombres aléatoires
    /// </summary>
    public interface IRandom
    {
        int Next(int min, int max);
    }
}
