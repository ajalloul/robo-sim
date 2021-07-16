using System;
namespace robosim
{
    public class TableTop
    {
        private bool isPlaced = false;
        private bool[,] tableTopMatrix;

        private Robot robot = new Robot();
        private Compass compass = new Compass();

        public TableTop()
        {
            generateTableTop();
        }

        private void generateTableTop()
        {
            tableTopMatrix = new bool[6, 6];

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    tableTopMatrix[i, j] = false;
                }
            }
        }

        public void PlaceRobot(int x, int y, string inputedDirection)
        {
            if(x == -1 || y == -1)
            {
                Console.WriteLine("Please make sure placement values are between 0 & 5");
                return;
            }

            if (compass.CheckSetDirectionString(inputedDirection))
            {
                string direction = compass.GetCurrentDirection();
                int directionIndex = compass.GetCurrentDirectionIndex();

                robot.setNewPosition(x, y, directionIndex);

                tableTopMatrix[x, y] = true;

                isPlaced = true;
            }
            else
            {
                Console.WriteLine("Invalid Direction. Please input one of the following NORTH, WEST, EAST, SOUTH");
            }
        }

        public void MoveRobot()
        {
            robot.GetX();
            robot.GetY();

            compass.GetDirectionAtIndex(robot.GetDirectionIndex());
        }

        public bool IsPlaced()
        {
            return isPlaced;
        }
    }
}
