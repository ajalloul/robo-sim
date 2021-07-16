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
            int currentX = robot.GetX();
            int currentY = robot.GetY();
            int directionIndex = robot.GetDirectionIndex();
            int newPosition;

            bool validMove = false;

            string direction = compass.GetDirectionAtIndex(directionIndex);

            if(direction.ToLower() == "north")
            {
                newPosition = currentY + 1;
                validMove = checkBounds(newPosition);
                if (validMove)
                {
                    currentY = newPosition;
                }
            }
            else if(direction.ToLower() == "east")
            {
                newPosition = currentX + 1;
                validMove = checkBounds(newPosition);
                if (validMove)
                {
                    currentX = newPosition;
                }
            }
            else if (direction.ToLower() == "south")
            {
                newPosition = currentY - 1;
                validMove = checkBounds(newPosition);
                if (validMove)
                {
                    currentY = newPosition;
                }
            }
            else if (direction.ToLower() == "west")
            {
                newPosition = currentX - 1;
                validMove = checkBounds(newPosition);
                if (validMove)
                {
                    currentX = newPosition;
                }
                
            }

            if (validMove)
            {
                tableTopMatrix[robot.GetX(), robot.GetY()] = false;
                tableTopMatrix[currentX, currentY] = false;
                robot.setNewPosition(currentX, currentY, directionIndex);
            }
            else
            {
                Console.WriteLine("Invalid Move. The robot would be destroyed!");
            }
        }

        private bool checkBounds(int newPosition)
        {
            if(newPosition < 0 || newPosition > 5)
            {
                return false;
            }

            return true;
        }

        public void TurnRobot(Direction direction)
        {
            int currentDirection = robot.GetDirectionIndex();

            int newDirection = compass.rotate(direction, currentDirection);

            compass.CheckSetDirectionString(compass.GetDirectionAtIndex(newDirection));

            robot.setNewPosition(robot.GetX(), robot.GetY(), newDirection);
        }

        public void RobotReport()
        {
            int x = robot.GetX();
            int y = robot.GetY();
            int directionIndex = robot.GetDirectionIndex();

            string direction = compass.GetDirectionAtIndex(directionIndex);

            Console.WriteLine("Output: " + x + "," + y + "," + direction);
        }

        public bool IsPlaced()
        {
            return isPlaced;
        }
    }
}
