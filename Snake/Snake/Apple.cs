using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    class Apple
    {
        // Possible X / Y co-ords for the apple to spawn. It's nicer for it to be a multiple of 50
        float[] randomNumbers = new float[] { 50, 150, 250, 300, 400, 550};
        Vector2 location;
        Random rnd;

        public Apple()
        {
            rnd = new Random();
            location.X = randomNumbers[rnd.Next(0, 5)];
            location.Y = randomNumbers[rnd.Next(0, 5)];
        }

        public void respawn()
        {
            location.X = randomNumbers[rnd.Next(0, 5)];
            location.Y = randomNumbers[rnd.Next(0, 5)];
        }

        public Vector2 getLocation()
        {
            return location;
        }
    }
}
