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
                Console.WriteLine("Valid Command");
                if (parsedCommand == Command.PLACE)
                {
                    Console.WriteLine("Place Command " + processor.GetX(commandString) + processor.GetY(commandString) + processor.GetDirectionString(commandString));
                    tableTop.PlaceRobot(processor.GetX(commandString), processor.GetY(commandString), processor.GetDirectionString(commandString));
                }
                else if(parsedCommand == Command.MOVE && tableTop.IsPlaced())
                {
                    Console.WriteLine("Move Command");
                    tableTop.MoveRobot();
                }
                else if (parsedCommand == Command.REPORT && tableTop.IsPlaced())
                {
                    tableTop.RobotReport();
                }
                else if (parsedCommand == Command.LEFT && tableTop.IsPlaced())
                {
                    //todo test & check bounds
                    tableTop.TurnRobot(Direction.LEFT);
                }
                else if (parsedCommand == Command.RIGHT && tableTop.IsPlaced())
                {
                    //todo test & check bounds
                    tableTop.TurnRobot(Direction.RIGHT);
                }
            }

            takeInput(tableTop);
        }
    }
}