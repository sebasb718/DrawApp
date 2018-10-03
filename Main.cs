using System;
using System.Collections.Generic;

namespace Main
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Starting, please wait");
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            DrawingTool.StartProgram();
            Console.Clear();
            Console.WriteLine("End, press enter to exit");
            Console.ReadKey();
        }
    }
    
}