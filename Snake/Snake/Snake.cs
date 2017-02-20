using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    class Snake
    {
        private int health = 100;
        public static int length = 3;
        private Vector2 snakeLocation;
        public bool state;
        bool head;

        public Snake(int x, int y)
        {
            snakeLocation.X = x;
            snakeLocation.Y = y;
        }
        public Snake(int x, int y, bool state, bool head)
        {
            snakeLocation.X = x;
            snakeLocation.Y = y;
            this.state = state;
            this.head = head;

        }
        // Take in the postion of a body piece
        public void setLocationRight(Vector2 location)
        {
            // Sets it to the correct spot.
            snakeLocation.X = location.X - 50;
            snakeLocation.Y = location.Y;
        }
        public void setLocationLeft(Vector2 location)
        {
            // Sets it to the correct spot.
            snakeLocation.X = location.X + 50;
            snakeLocation.Y = location.Y;
        }
        public void setLocationUp(Vector2 location)
        {
            // Sets it to the correct spot.
            snakeLocation.X = location.X;
            snakeLocation.Y = location.Y + 50;
        }
        public void setLocationDown(Vector2 location)
        {
            // Sets it to the correct spot.
            snakeLocation.X = location.X;
            snakeLocation.Y = location.Y - 50;
        }
        public Vector2 getLocation()
        {
            return snakeLocation;
        }

        public void moveRight()
        {
            snakeLocation.X = snakeLocation.X + 2.5f;
        }

        public void moveLeft()
        {
            snakeLocation.X = snakeLocation.X - 2.5f;
        }

        public void moveUp()
        {
            snakeLocation.Y = snakeLocation.Y - 2.5f;
        }

        public void moveDown()
        {
            snakeLocation.Y = snakeLocation.Y + 2.5f;
        }

        // Is the piece active? should it be moving?
        public void setStateTrue()
        {
            state = true;
        }
        public bool getState()
        {
            return state;
        }
    }
}
