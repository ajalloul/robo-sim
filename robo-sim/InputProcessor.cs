using System;
namespace robosim
{
    public class InputProcessor
    {
        public InputProcessor()
        {
        }

        public Command GetCommand(string commandString)
        {
            Command setCommand = Command.NULL;

            if (commandString.Contains("PLACE"))
            {
                setCommand = Command.PLACE;
            }
            else if (commandString.Contains("MOVE"))
            {
                setCommand = Command.MOVE;
            }
            else if (commandString.Contains("LEFT"))
            {
                setCommand = Command.LEFT;
            }
            else if (commandString.Contains("RIGHT"))
            {
                setCommand = Command.RIGHT;
            }
            else if (commandString.Contains("REPORT"))
            {
                setCommand = Command.REPORT;
            }
            else if (commandString.Contains("AVOID"))
            {
                setCommand = Command.AVOID;
            }
            else
            {
                setCommand = Command.NULL;
            }

            return setCommand;
        }

        public int GetX(string command)
        {
            //get x
            int xPos = -1;

            xPos = GetIntValueAtPosition(command.ToCharArray(), 6);

            if (xPos == -1)
            {
                Console.WriteLine("Bad placement on the horizontal axis! Please input a value between 0 & " + (TableTop.DIMENSION_MAX - 1));
            }

            return xPos;
        }

        public int GetY(string command)
        {
            //get y
            int yPos = -1;

            yPos = GetIntValueAtPosition(command.ToCharArray(), 8);

            if (yPos == -1)
            {
                Console.WriteLine("Bad placement on the vertical axis! Please input a value between 0 & " + (TableTop.DIMENSION_MAX - 1));
            }

            return yPos;
        }

        public string GetDirectionString(string command)
        {
            return command.Substring(10);
        }

        private int GetIntValueAtPosition(char[] charArray, int charArrayPosition)
        {
            char yPosAsChar = charArray[charArrayPosition];

            int positionValue = -1;

            try
            {
                positionValue = int.Parse(yPosAsChar.ToString());

                if (positionValue > (TableTop.DIMENSION_MAX - 1) || positionValue < 0)
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
    }
}
