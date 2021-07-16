using System;
using System.Collections.Generic;

namespace robosim
{
    public class Compass
    {
        private List<string> directionList = new List<string>();
        private string currentDirection = "";
        private int currentDirectionIndex = -1;

        public Compass()
        {
            InitDirections();
        }

        private void InitDirections()
        {
            directionList.Add("NORTH");
            directionList.Add("EAST");
            directionList.Add("SOUTH");
            directionList.Add("WEST");
        }

        public String GetDirectionAtIndex(int index)
        {
            string extractedDirection = "";

            if (index < 0 || index > 3)
                return extractedDirection;

            extractedDirection = directionList[index];

            return extractedDirection;
        }

        public bool CheckSetDirectionString(string cardinalDirection)
        {
            string selectedDirection = "";
            selectedDirection = directionList.Find(value => value.Equals(cardinalDirection));

            if (selectedDirection == null || selectedDirection == "")
            {
                return false;
            }
            else
            {
                currentDirection = selectedDirection;
                currentDirectionIndex = directionList.IndexOf(selectedDirection);
                return true;
            }
        }

        public string GetCurrentDirection()
        {
            return currentDirection;
        }

        public int GetCurrentDirectionIndex()
        {
            return currentDirectionIndex;
        }

        public int rotate(Direction direction, int currentIndex)
        {
            int newIndex = -1;

            if (direction == Direction.NULL)
            {
                Console.WriteLine("Failed to rotate");
                return currentIndex;
            }

            if(direction == Direction.LEFT)
            {
                newIndex = rotateLeft();
            }
            else if (direction == Direction.RIGHT)
            {
                newIndex = rotateRight();
            }

            return newIndex;
        }

        private int rotateLeft()
        {
            if (currentDirectionIndex == 0)
            {
                currentDirectionIndex = 3;
                currentDirection = directionList[currentDirectionIndex];
            }
            else
            {
                currentDirectionIndex = currentDirectionIndex - 1;
                currentDirection = directionList[currentDirectionIndex];
            }

            return currentDirectionIndex;
        }

        private int rotateRight()
        {
            if (currentDirectionIndex == 3)
            {
                currentDirectionIndex = 0;
                currentDirection = directionList[currentDirectionIndex];
            }
            else
            {
                currentDirectionIndex = currentDirectionIndex + 1;
                currentDirection = directionList[currentDirectionIndex];
            }

            return currentDirectionIndex;
        }
    }
}
