using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    class Apple
    {
        float[] randomNumbers = new float[] { 50, 100, 150, 300, 400, 600, 450, 700 };
        Vector2 location;

        public Apple()
        {
            Random rnd = new Random();
            rnd.Next(0, 7);
            location.X = randomNumbers[rnd.Next(0, 7)];
            location.Y = randomNumbers[rnd.Next(0, 7)];
        }

        public void respawn()
        {

        }

        public Vector2 returnLocation()
        {
            return location;
        }
    }
}
