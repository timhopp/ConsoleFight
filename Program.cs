using System;
using System.Collections.Generic;
using Demo.Controllers;

namespace Demo
{
  class Program
    {
        static void Main(string[] args)
        {
            new GameController();
            Console.WriteLine("Thank you for playing! Press Any Key To Exit");
            Console.ReadKey();
        }
    }
}

