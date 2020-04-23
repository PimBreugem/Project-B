using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectB
{
    class Program
    {
        public static int currentId = 0;
        public static List<User> users = JsonConverter.GetUserList();
        public static List<Movie> movies = JsonConverter.GetMovieList();

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
                Console.WriteLine($"Reservation system (logged in as admin account {GetCurrentUsername()})");
                Console.WriteLine(
                    "--Version // Shows version of program\n" +
                    "--Exit // Exit the program\n" +
                    "--Logout // logout\n" +
                    "--NewMovie // Create a new movie (not working)"
                );
            }
            else if (IsLoggedIn())
            {
                Console.WriteLine($"Reservation system (logged in as {GetCurrentUsername()})");
                Console.WriteLine(
                    "--Version // Shows version of program\n" +
                    "--Exit // Exit the program\n" +
                    "--Logout // logout\n" +
                    "--Movies // Show the list of current movies"
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
            users = JsonConverter.GetUserList();
            currentId = Program.users.Count - 1;
        }
        static void PrintMovies()
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
                    result += "\n\n" + item.Bio + "\n";
                    foreach (var option in item.PlayOptions)
                    {
                        result += "\n" + option.Time.ToString("yyyy/MM/dd HH:mm") + " (" + option.ScreenType + ")";
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
        static void MovieOverview()
        {
            Console.Clear();
            string print = "Available movies:\n";
            foreach (var item in movies)
            {
                print += "\n" + (item.Id) + ". " + (item.Title) +
                "\n" + "Genre: " + (item.Genre[0] + "/" + item.Genre[1]) +
                "\n" + "Movie Length: " + (item.Length) +
                "\n" + "Price: " + (item.Price) +
                "\n" + (item.Screen) +
                "\n" + "Version(s): ";
                for (int j = 0; j < item.Type.Count(); j++)
                {
                    print += item.Type[j];
                    if (j != item.Type.Count() - 1)
                    {
                        print += ", ";
                    }

                }
                print += "\nDescription-" +
                "\n" + (item.Bio) + "\n";
            }


            Console.WriteLine(print + "\n\nPlease enter the number of the movie:");
            while (true)
            {
                string input = Console.ReadLine();
                int result;
                try
                {
                    result = Int32.Parse(input);
                }
                catch (FormatException)
                {
                    input = null;
                    ClearAndWrite(print + "\n\nPlease enter a valid movie number:");
                    continue;
                }

                Console.WriteLine(movies[result - 1].Bio);
                if (result > movies.Count || result < 0)
                {
                    input = null;
                    ClearAndWrite(print + "\n\nPlease enter a valid movie number:");
                    continue;
                }
                else
                {
                    Movie(result - 1);
                    break;
                }
            }
        }
        static void Movie(int queriedId)
        {
            string print = "";
            print += "\n" + (movies[queriedId].Title) +
            "\n" + "Genre: ";
            for (int i = 0; i < movies[queriedId].Genre.Count(); i++)
            {
                print += movies[queriedId].Genre[i];
                if (i != movies[queriedId].Genre.Count() - 1)
                {
                    print += ", ";
                }

            }
            print += "\n" + "Movie Length: " + (movies[queriedId].Length) +
            "\n" + "Price: " + (movies[queriedId].Price) +
            "\n" + (movies[queriedId].Screen) +
            "\n" + "Version(s): ";
            for (int j = 0; j < movies[queriedId].Type.Count(); j++)
            {
                print += movies[queriedId].Type[j];
                if (j != movies[queriedId].Type.Count() - 1)
                {
                    print += ", ";
                }

            }
            print += "\nDescription-" +
            "\n" + (movies[queriedId].Bio) +
            "\nPlaytimes:";
            foreach (var time in movies[queriedId].PlayTimes)
            {
                print += "\n\t" + DateTime.UtcNow.ToString(time);
            }
            ClearAndWrite(print);
            Console.ReadLine();
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
                    case "reservation": if (IsLoggedIn()) { Reservation.newReservation(); break; } else { break; } 
                    case "movies": PrintMovies(); break;
                }
            }
        }
    }
}