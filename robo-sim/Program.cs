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
        AVOID,
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT,
        NULL
    }

    class RoboSimMainClass
    {

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
                Console.WriteLine("No valid command found. Please use the following PLACE, REPORT, LEFT, RIGHT, or MOVE");
            }
            else
            {
                if (parsedCommand == Command.PLACE)
                {
                    tableTop.PlaceRobot(processor.GetX(commandString), processor.GetY(commandString), processor.GetDirectionString(commandString));
                }
                else if(parsedCommand == Command.MOVE && tableTop.IsPlaced())
                {
                    tableTop.MoveRobot();
                }
                else if (parsedCommand == Command.REPORT && tableTop.IsPlaced())
                {
                    tableTop.RobotReport();
                }
                else if (parsedCommand == Command.LEFT && tableTop.IsPlaced())
                {
                    tableTop.TurnRobot(Direction.LEFT);
                }
                else if (parsedCommand == Command.RIGHT && tableTop.IsPlaced())
                {
                    tableTop.TurnRobot(Direction.RIGHT);
                }
                else if (parsedCommand == Command.AVOID && tableTop.IsPlaced())
                {
                    //todo create new method AVOID
                    tableTop.Avoid(processor.GetX(commandString), processor.GetY(commandString));
                }
                else if (!tableTop.IsPlaced())
                {
                    Console.WriteLine("PLACE the robot first!");
                }
            }

            takeInput(tableTop);
        }
    }
}