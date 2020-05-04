using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ProjectB
{
    internal static class MovieFunctions
    {
        public static List<User> users = JsonConverter.GetUserList();
        public static List<Movie> movies = JsonConverter.getMovieList();
        static void ClearAndWrite(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
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
        internal static void MovieOverview()
        {
            Console.Clear();
            string print = "Available movies:\n";
            foreach (var item in movies)
            {
                print += "\n" + (item.Id + 1) + ". " + (item.Title) +
                "\nGenre: " + (item.Genre[0] + "/" + item.Genre[1]) +
                "\nMovie Length: " + (item.Length) +
                "\nVersion(s): ";
                for (int j = 0; j < item.PlayOptions.Count(); j++)
                {
                    print += item.PlayOptions[j].ScreenType;
                    if (j != item.PlayOptions.Count() - 1)
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
                    ClearAndWrite(print + "\n\nPlease enter a valid movie number:");
                    continue;
                }
                if (result > movies.Count || result <= 0)
                {
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
            print += (movies[queriedId].Title) +
            "\nGenre: ";
            for (int i = 0; i < movies[queriedId].Genre.Count(); i++)
            {
                print += movies[queriedId].Genre[i];
                if (i != movies[queriedId].Genre.Count() - 1)
                {
                    print += ", ";
                }

            }
            print += "\nMovie Length: " + (movies[queriedId].Length) +
            "\nPrice: 7.50" +
            "\n" + (movies[queriedId]) +
            "\nDescription-" +
            "\n" + (movies[queriedId].Bio) +
            "\nPlaytimes:";
            foreach (var item in movies[queriedId].PlayOptions)
            {
                print += "\n" + (item.SubId + 1) + ". " + item.Time.ToString("dd/MM/yyyy HH:mm") + " screen: " + item.Room;
            }
            ClearAndWrite(print + "\n\nPress enter to return to the main menu");
            Console.ReadLine();

        }
        internal static Tuple<int, int> MovieOverviewReserve()
        {
            Console.Clear();
            string print = "Available movies:\n";
            foreach (var item in movies)
            {
                print += "\n" + (item.Id + 1) + ". " + (item.Title) +
                "\nGenre: " + (item.Genre[0] + "/" + item.Genre[1]) +
                "\nMovie Length: " + (item.Length) +
                "\nVersion(s): ";
                for (int j = 0; j < item.PlayOptions.Count(); j++)
                {
                    print += item.PlayOptions[j].ScreenType;
                    if (j != item.PlayOptions.Count() - 1)
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
                    ClearAndWrite(print + "\n\nPlease enter a valid movie number:");
                    continue;
                }
                if (result > movies.Count || result <= 0)
                {
                    ClearAndWrite(print + "\n\nPlease enter a valid movie number:");
                    continue;
                }
                else
                {
                    var tuple = MovieReserve(result - 1);
                    return tuple;
                }
            }

        }
        static Tuple<int, int> MovieReserve(int queriedId)
        {
            string print = "";
            print += (movies[queriedId].Title) +
            "\nGenre: ";
            for (int i = 0; i < movies[queriedId].Genre.Count(); i++)
            {
                print += movies[queriedId].Genre[i];
                if (i != movies[queriedId].Genre.Count() - 1)
                {
                    print += ", ";
                }

            }
            print += "\nMovie Length: " + (movies[queriedId].Length) +
            "\nDescription-" +
            "\n" + (movies[queriedId].Bio) +
            "\nPlaytimes:";
            foreach (var item in movies[queriedId].PlayOptions)
            {
                print += "\n" + (item.SubId + 1) + ". " + item.Time.ToString("dd/MM/yyyy HH:mm") + " screen: " + item.Room;
            }
            ClearAndWrite(print + "\n\nPlease enter the number of the playtime you want to reserve tickets for:");
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
                    ClearAndWrite(print + "\n\nPlease enter a valid movie number:");
                    continue;
                }
                if (result > movies[queriedId].PlayOptions.Length || result <= 0)
                {
                    ClearAndWrite(print + "\n\nPlease enter a valid movie number:");
                    continue;
                }
                else
                {
                    return Tuple.Create(queriedId, result-1);
                }
            }

        }
    }
    static class Program
    {
        public static int currentId = -1;
        public static List<User> users = JsonConverter.GetUserList();
        public static List<Movie> movies = JsonConverter.getMovieList();
        public static List<Order> orders = JsonConverter.GetOrderList();

        static bool IsAdmin() => users[currentId].Admin;
        static bool IsLoggedIn() => currentId >= 0;
        static string GetCurrentUsername() => currentId >= 0 ? GetUsername(currentId) : "";
        static string GetUsername(int index) => users[index].Title;
        static string GetPassword(int index) => users[index].Password;
        static string GetVersion() => "0.6";
        static void ClearAndWrite(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
        }
        static void PrintIntroductionAndOptions()
        {
            if (IsLoggedIn() && IsAdmin())
            {
                Console.WriteLine($"Reservation system (logged in as admin account {GetCurrentUsername()})");
                Console.WriteLine(
                      "--Version // Shows version of program\n" +
                    "--Exit // Exit the program\n" +
                    "--Logout // logout\n" +
                    "--Movies // Show the list of current movies\n" +
                    "--Reservation //reserve seats for a movie\n" +
                    "--Orders // View your orders\n" +
                    "--Data // View all the sales"
                );
            }
            else if (IsLoggedIn())
            {
                Console.WriteLine($"Reservation system (logged in as {GetCurrentUsername()})");
                Console.WriteLine(
                    "--Version // Shows version of program\n" +
                    "--Exit // Exit the program\n" +
                    "--Logout // logout\n" +
                    "--Movies // Show the list of current movies\n" +
                    "--Reservation //reserve seats for a movie\n" +
                    "--Orders // View your orders"
                );
            }
            else
            {
                Console.WriteLine("Reservation system");
                Console.WriteLine(
                    "--Version // Shows version of program\n" +
                    "--Exit // Exit the program\n" +
                    "--Login // Sign in with an account\n" +
                    "--Register // Register an account\n" +
                    "--Movies //Show the list of current movies"
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
        static void Orders()
        {
            int[] userOrders = users[currentId].Orderlist;
            Console.Clear();
            for(int i = 0; i < orders.Count; i++)
            {
                if (userOrders.Contains(i))
                {
                    Console.WriteLine("------------------------------------------------------------------------\n" +
                        movies[orders[i].MovieTitle].Title + "\n" +
                        movies[orders[i].MovieTitle].PlayOptions[orders[i].MoviePlaytimeId].Time + "\n" +
                        "Total seats: " + orders[i].SeatAmount[0] + " (Adult: " + orders[i].SeatAmount[1] + " , Kids: " + orders[i].SeatAmount[2] + " , Disabled: " + orders[i].SeatAmount[3] + ")"
                        );
                    if (orders[i].Paid)
                    {
                        Console.WriteLine("Bill is succesfully paid\n------------------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("Bill is not paid, open amount: " + orders[i].TotalPrice + "\n" + "------------------------------------------------------------------------");
                    }
                    
                }
            }
            Console.WriteLine("press enter to return");
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
                    case "reservation": if (IsLoggedIn()) { Reservation.NewReservation(); break; } else { break; }
                    case "movies": MovieFunctions.MovieOverview(); break;
                    case "data": if (IsAdmin() && IsLoggedIn()) { Data.GetDataToday(); break; } else { break; }
                    case "orders": if (IsLoggedIn()) { Orders(); break; } else { break; }
                }
            }
        }
    }
}