using System;

namespace Project_B
{
    class Program
    {
        public static bool currentlylogged = false;
        public static int currentId = 0;
        public static Tuple<string,string>[] users = {
                Tuple.Create("Admin", "12345"),
                Tuple.Create("User", "54321")};
        
        static void login(){
            Console.Clear();
            Console.WriteLine("Please enter your username:");
            string username = Console.ReadLine();
            for(int i = 0; i < Program.users.Length; i++){
                if(Program.users[i].Item1 == username){
                    Console.Clear();
                    Console.WriteLine("Please enter your password:");
                    if(Program.users[i].Item2 == Console.ReadLine()){
                        Program.currentId = i;
                        Program.currentlylogged = true;
                        Program.Main();
                    }else{
                        Console.Clear();
                        Console.WriteLine("Invalid password");
                        Program.Main();
                    }
                }else{
                    Console.Clear();
                    Console.WriteLine("Unknown username");
                    Program.Main();
                }
            }
        }
        static void Main()
        {
            Console.Clear();
            if(Program.currentlylogged == true){
                Console.WriteLine("Reservation system (logged as " + Program.users[Program.currentId].Item1 + ")\n");
                Console.WriteLine("--Version // Shows version of program\n--Exit // Exit the program\n--Logout // logout");
            }else{
                Console.WriteLine("Reservation system\n");
                Console.WriteLine("--Version // Shows version of program\n--Exit // Exit the program\n--Login // Sign in with an account");
            }
            while(true){
                string input = Console.ReadLine();
                if (input == "exit" || input == "Exit"){
                    Environment.Exit(0);
                }else if(input == "version" || input == "Version"){
                    Console.WriteLine("Version 0.2");
                }else if(input == "login" || input == "login"){
                    Program.login();
                }else if(input == "logout" || input =="Logout"){
                    Program.currentlylogged = false;
                    Program.Main();
                }else if(input == "clear" || input =="Clear"){
                    Program.Main();
                }else{
                    Program.Main();
                }
            }
        }
    }
}