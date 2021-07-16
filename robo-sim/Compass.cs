using System;
using System.Collections.Generic;

namespace robosim
{
    public class Compass
    {
        private List<string> directionList = new List<string>();

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

        public bool CheckValidDirectionString(string cardinalDirection)
        {
            string selectedDirection = "";
            selectedDirection = directionList.Find(value => value.Equals(cardinalDirection));

            if (selectedDirection == null || selectedDirection == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int GetDirectionIndex(string cardinalDirection)
        {
            string selectedDirection = directionList.Find(value => value.Equals(cardinalDirection));
            int currentDirectionIndex = directionList.IndexOf(selectedDirection);

            return currentDirectionIndex;
        }

        public int rotate(Direction direction, int currentIndex)
        {
            int newIndex = -1;

            if (direction == Direction.NULL)
            {
                Console.WriteLine("Failed to rotate");
                return newIndex;
            }

            if(direction == Direction.LEFT)
            {
                newIndex = rotateLeft(currentIndex);
            }
            else if (direction == Direction.RIGHT)
            {
                newIndex = rotateRight(currentIndex);
            }

            return newIndex;
        }

        private int rotateLeft(int currentDirectionIndex)
        {
            int returnedDirection;

            if (currentDirectionIndex == 0)
            {
                returnedDirection = 3;
            }
            else
            {
                returnedDirection = currentDirectionIndex - 1;
                
            }

            return returnedDirection;
        }

        private int rotateRight(int currentDirectionIndex)
        {
            int returnedDirection;

            if (currentDirectionIndex == 3)
            {
                returnedDirection = 0;
            }
            else
            {
                returnedDirection = currentDirectionIndex + 1;
            }

            return returnedDirection;
        }
    }
}
