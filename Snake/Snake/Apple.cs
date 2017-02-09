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
        float[] randomNumbers = new float[] { 50, 100, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600 };
        Vector2 location;
        Random rnd;

        public Apple()
        {
            rnd = new Random();
            rnd.Next(0, 7);
            location.X = randomNumbers[rnd.Next(0, 7)];
            location.Y = randomNumbers[rnd.Next(0, 7)];
        }

        public void respawn()
        {
            rnd.Next(0, 7);
            location.X = randomNumbers[rnd.Next(0, 7)];
            location.Y = randomNumbers[rnd.Next(0, 7)];
        }

        public Vector2 getLocation()
        {
            return location;
        }
    }
}
