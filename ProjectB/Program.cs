using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace ProjectB
{
    class Program
    {
        public static int currentId = -1;
        public static List<User> users = JsonConverter.getUserList();
        public static List<Movie> movies = JsonConverter.getMovieList();
        public static List<UserInfo> usersinfo = JsonConverter.getUserInfoList();

        static bool isAdmin() => users[currentId].Admin;
        static bool IsLoggedIn() => currentId >= 0;
        static string GetCurrentUsername() => currentId >= 0 ? GetUsername(currentId) : "";
        static string GetUsername(int index) => users[index].Title;
        static string GetPassword(int index) => users[index].Password;
        static string GetVersion() => "0.4";
        static void ClearAndWrite(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
        }
       
        static void PrintIntroductionAndOptions()
        {
            if (IsLoggedIn() && isAdmin())
            {
                Console.WriteLine($"Reservation system (logged as admin account {GetCurrentUsername()})");
                Console.WriteLine(
                    "--Version // Shows version of program\n" +
                    "--Exit // Exit the program\n" +
                    "--Logout // logout\n" +
                    "--NewMovie // Create a new movie (not working)"

                );
            }
            else if (IsLoggedIn())
            {
                Console.WriteLine($"Reservation system (logged as {GetCurrentUsername()})");
                Console.WriteLine(
                    "--Version // Shows version of program\n" +
                    "--Exit // Exit the program\n" +
                    "--Logout // logout\n" + 
                    "--Userinfo // Enter your personal information"
                );
            }
            else
            {
                Console.WriteLine("Reservation system");
                Console.WriteLine(
                    "--Version // Shows version of program\n" +
                    "--Exit // Exit the program\n" +
                    "--Login // Sign in with an account\n" +
                    "--Register // Register an account"
                );
            }
        }
        static void Login()
        {
            ClearAndWrite("Please enter your username:");
            string username = Console.ReadLine();

            for (int i = 0; i < Program.users.Count; i++)
            {
                if (GetUsername(i).Equals(username))
                {
                    ClearAndWrite("Please enter your password:");
                    string password = Console.ReadLine();
                    currentId = password == GetPassword(i) ? i : -1;
                }
            }
            if (!IsLoggedIn())
            {
                ClearAndWrite("Incorrect username or password");
            }
        }
        static void Register()
        {
            RegisterAccount.registerAccount();
            users = JsonConverter.getUserList();
            currentId = Program.users.Count - 1;
        }
        static void printMovies()
        {
            ClearAndWrite("Enter Genre, movie title or all:");
            string search = Console.ReadLine();
            Console.Clear();
            string result = "Search results:\n";
            foreach (var item in movies)
            {
                if (item.Title == search || item.Genre.Contains(search) || search == "all")
                {
                    result += "\n" + item.Title + " : " + item.Genre[0];
                    for (int i = 1; i < item.Genre.Length; i++)
                    {
                        result += ", " + item.Genre[i];
                    }
                    result += "\n" + item.Bio;
                    foreach (var time in item.PlayTimes)
                    {
                        result += "\n" + DateTime.UtcNow.ToString(time);
                    }
                    result += "\n";
                }
            }
            if (result.Length < 20)
            {
                result = "There are no results for this search";
            }
            result += "\nClick enter to return to the main menu";
            Console.WriteLine(result);
            Console.ReadLine();
        }
        
        public static void userinfo()
        {
            List<UserInfo> usersinfo = JsonConverter.getUserInfoList();
            if (usersinfo[currentId].FirstName == "")
            {
                ClearAndWrite("Please enter your personal information:");
                Console.WriteLine("First name:");
                string firstname = Console.ReadLine();
                usersinfo[currentId].FirstName = firstname;
                Console.WriteLine("Last name:");
                string lastname = Console.ReadLine();
                usersinfo[currentId].LastName = lastname;
                Console.WriteLine("E-mailadres:");
                string email = Console.ReadLine();
                usersinfo[currentId].Email = email;
                string json = JsonConvert.SerializeObject(usersinfo, Formatting.Indented);
                string jsonFilePath = @"C:/Users/Diedv/Desktop/ProjectB/ProjectB/usersinfo.json";
                File.WriteAllText(jsonFilePath, json);
                string CurrentUserInfo = "Your information:" +
                                         "\nUsername   = " + usersinfo[currentId].Title +
                                         "\nFirst Name = " + usersinfo[currentId].FirstName +
                                         "\nLast Name  = " + usersinfo[currentId].LastName +
                                         "\nEmail      = " + usersinfo[currentId].Email;
                Console.ReadLine();
                Console.WriteLine(CurrentUserInfo);
                Console.ReadLine();    
            }
            else
            {
                ClearAndWrite("We already have your information!");
                string CurrentUserInfo = "Username   = " + usersinfo[currentId].Title +
                                         "\nFirst Name = " + usersinfo[currentId].FirstName +
                                         "\nLast Name  = " + usersinfo[currentId].LastName +
                                         "\nEmail      = " + usersinfo[currentId].Email;
                Console.WriteLine(CurrentUserInfo);
                Console.ReadLine();
            }
        }
      
        
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                PrintIntroductionAndOptions();
                
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "exit": Environment.Exit(0); break;
                    case "version": Console.WriteLine($"Version {GetVersion()}"); Console.ReadLine(); break;
                    case "login": Login(); break;
                    case "logout": currentId = -1; break;
                    case "register": Register(); break;
                    case "movies": printMovies(); break;
                    case "reservation": if (IsLoggedIn()) { Reservation.newReservation(); break; } else { break; }
                    case "userinfo": if (IsLoggedIn()) { userinfo(); break; } else { break; }
                }
            }
        }
    }
}