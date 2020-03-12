using System;

namespace Project_B
{
    class Program
    {
        static void exit(){
            Environment.Exit(0);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Reservation system");
            while(true){
                string input = Console.ReadLine();
                if (input == "exit" || input == "Exit"){
                    Environment.Exit(0);
                }else if(input == "version" || input == "Version"){
                    Console.WriteLine("Version 0.1");
                }else if(input == "help" || input == "Help"){
                    Console.WriteLine("--Version // Shows version of program");
                    Console.WriteLine("--Exit // Exit the program");
                    Console.WriteLine("--Help // Shows all the commands in the program");
                }
            }
        }
    }
}
