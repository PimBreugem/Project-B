using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace ProjectB
{
    class Reservation
    {
        static void ClearAndWrite(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
        }
        private static List<Movie> movies = JsonConverter.getMovieList();
        private static string selectMovie()
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
                    return result;
                }
                else { ClearAndWrite(print + "\n\nPlease enter a valid movie name:"); }
            }
        }
        
        public static void newReservation()
        {
            string selectedMovie = selectMovie();
            ClearAndWrite(selectedMovie);
            Console.ReadLine();
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
            string jsonFilePath = @"C:/Users/Diedv/Desktop/ProjectB/ProjectB/users.json";
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
                        getUsername();
                    }
                }
            }
            Console.Clear();
            return inputname;
        }
    }
}
