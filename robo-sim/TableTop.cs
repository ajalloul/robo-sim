using System;
namespace robosim
{
    public class TableTop
    {
        private bool isPlaced = false;
        private bool?[,] tableTopMatrix;

        private Robot robot = new Robot();
        private Compass compass = new Compass();

        public static int DIMENSION_MAX = 7;

        public TableTop()
        {
            GenerateTableTop();
        }

        private void GenerateTableTop()
        {
            tableTopMatrix = new bool?[DIMENSION_MAX, DIMENSION_MAX];

            for (int i = 0; i < DIMENSION_MAX; i++)
            {
                for (int j = 0; j < DIMENSION_MAX; j++)
                {
                    tableTopMatrix[i, j] = false;
                }
            }
        }

        public void PlaceRobot(int x, int y, string inputtedDirection = "")
        {
            if(x == -1 || y == -1)
            {
                return;
            }

            //is there a valid direction value

            //check if direction is present, if not no changes

            if (compass.CheckValidDirectionString(inputtedDirection))
            {
                int directionIndex = compass.GetDirectionIndex(inputtedDirection);

                robot.SetNewPosition(x, y, directionIndex);

                tableTopMatrix[x, y] = true;

                isPlaced = true;
            }
            else if((inputtedDirection.Equals("") || inputtedDirection.Equals(" ")) && IsPlaced())
            {
                int currentDirection = robot.GetDirectionIndex();

                robot.SetNewPosition(x, y, currentDirection);
            }
            else
            {
                Console.WriteLine("Invalid Direction. Please input one of the following NORTH, WEST, EAST, SOUTH. Please ensure you've placed the robot.");
            }
        }

        public void Avoid(int x, int y)
        {
            if(x == -1 || y == -1)
            {
                return;
            }

            tableTopMatrix[x, y] = null;
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

            if (tableTopMatrix[currentX, currentY] == null)
            {                
                Console.WriteLine("Block has an obstruction. Robot can not move!");
                return;
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
            if(newPosition < 0 || newPosition > (DIMENSION_MAX - 1))
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
