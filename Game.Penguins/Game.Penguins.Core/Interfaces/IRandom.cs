using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Penguins.Core.Interfaces
{
    /// <summary>
    /// Interface for simulating random numbers.
    /// </summary>
    public interface IRandom
    {
        int Next(int min, int max);
    }
}
