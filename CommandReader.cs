using System;
using System.Collections.Generic;
using Main;

class CommandReader
    {
        public static Command ReadCommand()
        {
            try
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 0);
                Console.Write("Enter command and press enter: ");
                Command oCommandRead = new Command(Console.ReadLine());
                return oCommandRead;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
