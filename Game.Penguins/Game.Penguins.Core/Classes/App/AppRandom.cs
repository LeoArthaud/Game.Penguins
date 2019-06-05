using System;
using Game.Penguins.Core.Interfaces;

namespace Game.Penguins.Core.Classes.App
{
    /// <summary>
    /// App random helper class.
    /// </summary>
    public class AppRandom : IRandom
    {
        /// <summary>
        /// Returns a random integer between a minimum and a maximum.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Next(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
