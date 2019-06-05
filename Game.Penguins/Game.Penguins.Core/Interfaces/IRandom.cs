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
        /// <summary>
        /// Returns a random integer between a minimum and a maximum.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        int Next(int min, int max);
    }
}
