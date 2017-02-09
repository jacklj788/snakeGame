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
        // not currently using 
        public void incLength()
        {
            
        }
        // damages the snake
        public void hurtSnake20()
        {
            health = health - 20;
        }
        // Take in the postion of a body piece
        public void setLocation(Vector2 location)
        {
            // Sets it to the correct spot.
            snakeLocation.X = location.X - 50;
            snakeLocation.Y = location.Y;
        }
        public Vector2 getLocation()
        {
            return snakeLocation;
        }

        public void moveRight()
        {
            snakeLocation.X = snakeLocation.X + 5f;
        }

        public void moveLeft()
        {
            snakeLocation.X = snakeLocation.X - 5f;
        }

        public void moveUp()
        {
            snakeLocation.Y = snakeLocation.Y - 5;
        }

        public void moveDown()
        {
            snakeLocation.Y = snakeLocation.Y + 5;
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
