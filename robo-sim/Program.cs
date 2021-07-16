using System;
using System.Collections.Generic;

namespace robosim
{
    public enum Direction
    {
        LEFT,
        RIGHT,
        NULL
    }

    public enum Command
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT,
        NULL
    }

    class RoboSimMainClass
    {

        private bool isPlaced = false;


        public static void Main(string[] args)
        {
            TableTop tableTop = new TableTop();

            Console.WriteLine("Hello, please PLACE the robot!");
            takeInput(tableTop);
        }

        private static void takeInput(TableTop tableTop)
        {
            InputProcessor processor = new InputProcessor();

            string commandString = Console.ReadLine();

            Command parsedCommand = processor.GetCommand(commandString);

            if(parsedCommand == Command.NULL)
            {
                Console.WriteLine("No valid command found");
            }
            else
            {
                if(parsedCommand == Command.PLACE)
                {
                    tableTop.PlaceRobot(processor.GetX(commandString), processor.GetX(commandString), processor.GetDirectionString(commandString));
                }
                else if(parsedCommand == Command.MOVE && tableTop.IsPlaced())
                {
                    tableTop.MoveRobot();
                }
                else if (parsedCommand == Command.REPORT && tableTop.IsPlaced())
                {
                    //tableTop.RobotReport();
                }
                else if (parsedCommand == Command.LEFT && tableTop.IsPlaced())
                {
                    //tableTop.TurnRobot(Command.LEFT);
                }
                else if (parsedCommand == Command.RIGHT && tableTop.IsPlaced())
                {
                    //tableTop.TurnRobot(Command.LEFT);
                }
            }

            takeInput(tableTop);
        }
    }
}

public class RobotSim
{
    private bool[,] tableTop;

    private bool isPlaced = false;

    private int xPosition = -1;
    private int yPosition = -1;

    private List<string> directionList = new List<string>();
    private string currentDirection = "";
    private int currentDirectionIndex = -1;


    public RobotSim()
    {
        initDirections();
        generateTableTop();
    }

    private void initDirections()
    {
        directionList.Add("NORTH");
        directionList.Add("EAST");
        directionList.Add("SOUTH");
        directionList.Add("WEST");
    }


    //i value represents horizontal axis, j represents vertical axis
    private void generateTableTop()
    {
        tableTop = new bool[6, 6];

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                tableTop[i, j] = false;
            }
        }
    }

    public void IssueCommand(string command)
    {
        if (command.Contains("PLACE"))
        {
            isPlaced = true;

            //substring needed values
            char[] stringAsArray = command.ToCharArray();

            //get cardinal direction
            string cardinalDirection = command.Substring(10);
            string selectedDirection = "";
            selectedDirection = directionList.Find(value => value.Equals(cardinalDirection));

            if (selectedDirection == null || selectedDirection == "")
            {
                Console.WriteLine("Invalid Direction. Please input one of the following NORTH, WEST, EAST, SOUTH");
                isPlaced = false;
                return;

            }
            else
            {
                currentDirection = selectedDirection;
                currentDirectionIndex = directionList.IndexOf(selectedDirection);
            }

            //check range
            //get x
            int xPos = -1;

            xPos = getIntValueAtPosition(stringAsArray, 6);

            if (xPos == -1)
            {
                Console.WriteLine("Bad placement on the vertical axis! Please input a value between 0 & 5");
                isPlaced = false;
                return;
            }

            //get y
            int yPos = -1;

            yPos = getIntValueAtPosition(stringAsArray, 8);

            if (yPos == -1)
            {
                Console.WriteLine("Bad placement on the vertical axis! Please input a value between 0 & 5");
                isPlaced = false;
                return;
            }

            //attempt to place robot
            try
            {
                tableTop[xPos, yPos] = true;
                xPosition = xPos;
                yPosition = yPos;
            }
            catch (Exception e)
            {
                Console.WriteLine("Bad placement, robot would be destroyed!");
                isPlaced = false;
                return;
            }

        }
        else if (command.Contains("MOVE") && isPlaced)
        {
            move();
        }
        else if (command.Contains("LEFT") && isPlaced)
        {
            rotate(Direction.LEFT);
        }
        else if (command.Contains("RIGHT") && isPlaced)
        {
            rotate(Direction.RIGHT);
        }
        else if (command.Contains("REPORT") && isPlaced)
        {
            Console.WriteLine("Output: " + xPosition + "," + yPosition + "," + currentDirection);
        }
        else if (!isPlaced)
        {
            Console.WriteLine("Please place the robot prior to issuing other commands.");
        }
        else
        {
            Console.WriteLine("Please enter a valid command; PLACE, MOVE, LEFT, RIGHT, REPORT");
        }
    }

    private int getIntValueAtPosition(char[] charArray, int charArrayPosition)
    {
        char yPosAsChar = charArray[charArrayPosition];

        int positionValue = -1;

        try
        {
            positionValue = int.Parse(yPosAsChar.ToString());

            if (positionValue > 5 || positionValue < 0)
            {
                positionValue = -1;
            }
        }
        catch (Exception e)
        {
            positionValue = -1;
        }

        return positionValue;
    }

    private void move()
    {

    }

    private void rotate(Direction direction)
    {
        if (direction == Direction.LEFT)
        {
            rotateLeft();
        }
        else if (direction == Direction.RIGHT)
        {
            rotateRight();
        }
    }

    private void rotateRight()
    {
        if (currentDirectionIndex == 3)
        {
            currentDirectionIndex = 0;
            currentDirection = directionList[currentDirectionIndex];
        }
        else
        {
            currentDirectionIndex = currentDirectionIndex++;
            currentDirection = directionList[currentDirectionIndex];
        }
    }

    private void rotateLeft()
    {
        if (currentDirectionIndex == 0)
        {
            currentDirectionIndex = 3;
            currentDirection = directionList[currentDirectionIndex];
        }
        else
        {
            currentDirectionIndex = currentDirectionIndex--;
            currentDirection = directionList[currentDirectionIndex];
        }
    }
}

enum Direction
{
    LEFT,
    RIGHT
}

enum Command
{
    PLACE,
    MOVE,
    LEFT,
    RIGHT,
    REPORT,
    NULL
}