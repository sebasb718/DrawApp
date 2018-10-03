using System;
using System.Collections.Generic;
using Main;
 class DrawingTool
    {
        public static void StartProgram()
        {
            do
            {
                try
                {
                    Command oCommandReceived = CommandReader.ReadCommand();
                    if (oCommandReceived.CommandIdentifier == 'Q')
                    {
                        break;
                    }
                    DrawCommandProcessor.Execute(oCommandReceived);
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Error: " + ex.Message + ". Press enter to continue");
                    Console.ReadKey(true);
                }
            } while (true);
        }
    }
