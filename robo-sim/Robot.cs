using System;
namespace robosim
{
    public class Robot
    {
        private int currentDirectionIndex = -1;

        private int xPosition = -1;
        private int yPosition = -1;

        private Compass compass = new Compass();

        public Robot()
        {

        }

        public void setNewPosition(int x, int y, int directionIndex)
        {
            xPosition = x;
            yPosition = y;
            currentDirectionIndex = directionIndex;
        }

        public int GetX()
        {
            return xPosition;
        }

        public int GetY()
        {
            return yPosition;
        }

        public int GetDirectionIndex()
        {
            return currentDirectionIndex;
        }
    }
}
