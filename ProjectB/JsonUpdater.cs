using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ProjectB
{
    class EreaAssembler
    {
        public static bool IntContains(int[] list, int input)
        {
            if(list == null) { return false; }
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == input)
                {
                    return true;
                }
            }
            return false;
        }
        public static void Assembler(int[] not, int[] selected)
        {
            string result = "   1    2    3    4    5\n";
            for (int i = 0, j = 0; j < 4; j++)
            {
                i = 0;
                result += (j + 1) + " ";
                while (i < 5)
                {
                    if (IntContains(not, i + j * 5 + 1))
                    {
                        result += "[X ] ";
                    }
                    else if (selected != null && IntContains(selected, i + j * 5 + 1))
                    {
                        result += "[S ] ";
                    }

                    else
                    {
                        string total = (i + j * 5).ToString();
                        if (total.Length == 1) { result += "[" + (i + j * 5 + 1) + " ] "; }
                        else { result += "[" + (i + j * 5 + 1) + "] "; }
                    }
                    i++;
                }
                result += "\n";
            }
            Console.WriteLine(result);
        }
    }
    class Reservation
    {
        static void ClearAndWrite(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
        }
        private static List<Movie> movies = JsonConverter.getMovieList();
        private static int SelectMovie()
        {
            List<string> list = new List<string>();
            Console.Clear();
            string print = "Available movies:\n";
            foreach(var item in movies) { print += "\n" + (item.Title); list.Add(item.Title.ToLower()); }
            Console.WriteLine(print + "\n\nPlease enter a movie:");
            while (true)
            {
                string result = Console.ReadLine();
                if(list.Contains(result.ToLower()))
                {
                    foreach(var item in movies) { if(result.ToLower() == item.Title.ToLower()) { return item.Id; } } 
                }
                else { ClearAndWrite(print + "\n\nPlease enter a valid movie name:"); }
            }
        }
        public static int GetPlayTimes(int MovieId)
        {
            string Resultstring = "";
            foreach (var item in movies[MovieId].PlayOptions)
            {
                Resultstring += item.SubId + ". " + item.Time + "(" + item.Room + ")"  + "\n";
            }
            Console.WriteLine(Resultstring + "\nPlease enter wanted time.");
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "0": return 0;
                    case "1": return 1;
                }
            }
        }
        public static int[] getSeats(int movie, int playtime)
        {
            int[] not = movies[movie].PlayOptions[playtime].Reserved;
            int[] selected = new int[1];
            int index = 0;
            string print = "Enter seats, enter next to contine";
            while (true)
            {
                Console.Clear();
                EreaAssembler.Assembler(not, selected);
                Console.WriteLine(print);
                string input = Console.ReadLine();

                if (input.ToLower() == "next") { break; }
                int number;
                bool succes = Int32.TryParse(input, out number);
                if (succes)
                {
                    if (index == selected.Length)
                    {
                        int[] newInt = new int[selected.Length + 1];
                        for (int i = 0; i < selected.Length; i++) { newInt[i] = selected[i]; }
                        selected = newInt;
                    }
                    if (EreaAssembler.IntContains(not, number)) { print = "This seat is occupied, please enter seat or next to contine"; }
                    else
                    {
                        selected[index] = number;
                        index++;
                        print = "Enter seats, enter next to contine";
                    }
                }
                else
                {
                    print = "Please enter valid number or type next";
                }
            }
            return selected;
        }
        public static int GetSeatAmount(string name, float price)
        {
            string before = "";
            while (true) {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                ClearAndWrite(before + "How many " + name + " tickets do you want to order " + HexToChar("20AC") + price + " per ticket.");
                bool succes = Int32.TryParse(Console.ReadLine(), out int number);
                if (succes) { return number; }
                else { before = "Please enter valid number\n"; }
            }
        }
        public static char HexToChar(string hex)
        {
            return (char)ushort.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }
        public static void newReservation()
        {
            int selectedMovie = SelectMovie();
            int time = GetPlayTimes(selectedMovie);
            int[] stoelen = getSeats(selectedMovie, time);
            int total = 0;
            while (total != stoelen.Length)
            {
                int adult = GetSeatAmount("Adult", 14.99f);
                int child = GetSeatAmount("Child", 9.99f);
                int disabled = GetSeatAmount("disabled", 4.99f);
                total = adult + child + disabled;
            }
            
            string wait = Console.ReadLine();
        }
    }
    class RegisterAccount
    {
        private static List<User> users = JsonConverter.getUserList();
        private static void newUsers(int id, string title, string password)
        {
            int[] orders = new int[0];
            User newuser = new User(id, title, password, false, orders);
            users.Add(newuser);
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            string jsonFilePath = Environment.CurrentDirectory + @"\..\..\..\json\users.json";
            File.WriteAllText(jsonFilePath, json);
        }
        public static void registerAccount()
        {
            string username = getUsername();
            string password = getPassword();
            newUsers(users.Count, username, password);
        }
        private static string getPassword()
        {
            bool newPass = false;
            string message = "Please enter a password:";
            string inputpass = "";
            while(newPass == false)
            {
                Console.Clear();
                Console.WriteLine(message);
                inputpass = Console.ReadLine();
                Console.WriteLine("Enter your password again:");
                string inputpass2 = Console.ReadLine();
                if(inputpass == inputpass2) { newPass = true; }
                else { message = "Passwords are not the same, enter password:"; }
            }
            Console.Clear();
            return inputpass;
        }
        private static string getUsername()
        {
            bool newUser = false;
            string message = "Please enter a username:";
            string inputname = "";
            while (newUser == false)
            {
                Console.Clear();
                Console.WriteLine(message);
                inputname = Console.ReadLine();
                newUser = true;
                foreach(var item in users)
                {
                    if(item.Title == inputname)
                    {
                        newUser = false;
                        message = "Username has been taken, please enter another one:";
                    }
                }
            }
            Console.Clear();
            return inputname;
        }
    }
}
