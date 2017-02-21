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
        public static int length = 1;
        private Vector2 snakeLocation;
        public bool state;
        bool head;

        public bool movingLeft, movingRight, movingUp, movingDown;

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
            // Starts of moving right.
            if (head)
            {
                movingDown = false;
                movingLeft = false;
                movingRight = true;
                movingUp = false;
            }

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
            movingDown = false;
            movingLeft = false;
            movingRight = true;
            movingUp = false;
        }

        public void moveLeft()
        {
            snakeLocation.X = snakeLocation.X - 2.5f;
            movingDown = false;
            movingLeft = true;
            movingRight = false;
            movingUp = false;
        }

        public void moveUp()
        {
            snakeLocation.Y = snakeLocation.Y - 2.5f;
            movingDown = false;
            movingLeft = false;
            movingRight = false;
            movingUp = true;
        }

        public void moveDown()
        {
            snakeLocation.Y = snakeLocation.Y + 2.5f;
            movingDown = true;
            movingLeft = false;
            movingRight = false;
            movingUp = false;
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

        public void backStepRight()
        {
            snakeLocation.X = snakeLocation.X - 2.5f;
        }
        public void backStepLeft()
        {
            snakeLocation.X = snakeLocation.X + 2.5f;
        }
        public void backStepUp()
        {
            snakeLocation.Y = snakeLocation.Y - 2.5f;
        }
        public void backStepDown()
        {
            snakeLocation.Y = snakeLocation.Y + 2.5f;
        }
    }
}
