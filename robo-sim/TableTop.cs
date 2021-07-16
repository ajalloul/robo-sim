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
            GenerateTableTop();
        }

        private void GenerateTableTop()
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

        public void PlaceRobot(int x, int y, string inputtedDirection)
        {
            if(x == -1 || y == -1)
            {
                return;
            }

            if (compass.CheckValidDirectionString(inputtedDirection))
            {
                int directionIndex = compass.GetDirectionIndex(inputtedDirection);

                robot.SetNewPosition(x, y, directionIndex);

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
                validMove = CheckBounds(newPosition);
                if (validMove)
                {
                    currentY = newPosition;
                }
            }
            else if(direction.ToLower() == "east")
            {
                newPosition = currentX + 1;
                validMove = CheckBounds(newPosition);
                if (validMove)
                {
                    currentX = newPosition;
                }
            }
            else if (direction.ToLower() == "south")
            {
                newPosition = currentY - 1;
                validMove = CheckBounds(newPosition);
                if (validMove)
                {
                    currentY = newPosition;
                }
            }
            else if (direction.ToLower() == "west")
            {
                newPosition = currentX - 1;
                validMove = CheckBounds(newPosition);
                if (validMove)
                {
                    currentX = newPosition;
                }
                
            }

            if (validMove)
            {
                tableTopMatrix[robot.GetX(), robot.GetY()] = false;
                tableTopMatrix[currentX, currentY] = false;
                robot.SetNewPosition(currentX, currentY, directionIndex);
            }
            else
            {
                Console.WriteLine("Invalid Move. The robot would be destroyed!");
            }
        }

        private bool CheckBounds(int newPosition)
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

            int newDirection = compass.Rotate(direction, currentDirection);

            compass.CheckValidDirectionString(compass.GetDirectionAtIndex(newDirection));

            robot.SetNewPosition(robot.GetX(), robot.GetY(), newDirection);
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
