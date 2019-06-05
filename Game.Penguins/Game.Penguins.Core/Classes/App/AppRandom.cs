﻿using System;
using Game.Penguins.Core.Interfaces;

namespace Game.Penguins.Core.Classes.App
{
    /// <summary>
    /// App random helper class.
    /// </summary>
    public class AppRandom : IRandom
    {
        public int Next(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
