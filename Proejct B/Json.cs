﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Proejct_B
{
    public class JsonToObjectLists
    {
        private static readonly string root = Environment.CurrentDirectory + @"\..\..\";
        public static List<Movie> GetMovieList()
        {
            string jsonFilePath = root + @"json\movies.json";
            string json = File.ReadAllText(jsonFilePath);
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            return movies;
        }
        public static List<User> GetUserList()
        {
            string jsonFilePath = root + @"json\users.json";
            string json = File.ReadAllText(jsonFilePath);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
            return users;
        }
        public static List<Order> GetOrderList()
        {
            string jsonFilePath = root + @"json\orders.json";
            string json = File.ReadAllText(jsonFilePath);
            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(json);
            return orders;
        }
        private static readonly List<Order> orders = GetOrderList();
        public static List<Order> GetFilteredOrderList(int[] IdArray)
        {
            List<Order> userOrders = new List<Order>();
            foreach (var item in orders)
            {
                if (IdArray.Contains(item.Id)) { userOrders.Add(item); }
            }
            return userOrders;
        }
        private static List<User> users = GetUserList();
        public static List<Movie> movies = GetMovieList();
        public static void NewUser(string Username, string Password, string Email)
        {
            string jsonFilePath = root + @"json\users.json";
            users.Add(new User(users.Count, Username, Password, Email));
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
            ItemFromJsonList.Reload();
        }
        public static Tuple<string, string, string, Tuple<string, string, string>> GetData(string inputdate)
        {
            int OrdersCountToday = 0;
            int TicketsCountToday = 0;
            float TotalPriceToday = 0.00f;
            int Movie1 = 0;
            int Movie2 = 0;
            int Movie3 = 0;

            foreach (var item in orders)
            {
                if (item.OrderDate.ToString("d") == inputdate)
                {
                    OrdersCountToday += 1;
                    TotalPriceToday += item.TotalPrice;
                    TicketsCountToday += item.SeatAmount[0];
                    if (item.MovieTitle == 0)
                    {
                        Movie1 += 1;
                    }
                    else if (item.MovieTitle == 1)
                    {
                        Movie2 += 1;
                    }
                    else if (item.MovieTitle == 2)
                    {
                        Movie3 += 1;
                    }
                }
            }
            var DifferentMovies = Tuple.Create(Movie1.ToString(), Movie2.ToString(), Movie3.ToString());

            TotalPriceToday = (float)Math.Round(TotalPriceToday * 100f) / 100f;
            var DataOrder = Tuple.Create(TotalPriceToday.ToString(), OrdersCountToday.ToString(), TicketsCountToday.ToString(), DifferentMovies);
            return DataOrder;
        }

        public static Tuple<string, string, string> GetMovie()
        {
            string movie1 = "";
            string movie2 = "";
            string movie3 = "";

            foreach (var item in movies)
            {
                if (item.Id == 0)
                {
                    movie1 = item.Title;
                }
                else if (item.Id == 1)
                {
                    movie2 = item.Title;
                }
                else if (item.Id == 2)
                {
                    movie3 = item.Title;
                }
            }
            var MovieList = Tuple.Create(movie1, movie2, movie3);
            return MovieList;
        }

        public static Tuple<string, string, string> GetSortTickets(string inputdate)
        {
            int Adult = 0;
            int Child = 0;
            int Disabled = 0;

            foreach (var item in orders)
            {
                if (item.OrderDate.ToString("d") == inputdate)
                {
                    Adult += item.SeatAmount[1];
                    Child += item.SeatAmount[2];
                    Disabled += item.SeatAmount[3];
                }
            }

            var SortTickets = Tuple.Create(Adult.ToString(), Child.ToString(), Disabled.ToString());
            return SortTickets;
        }
    }
    public class ItemFromJsonList
    {
        //Lijsten
        private static List<Movie> movies = JsonToObjectLists.GetMovieList();
        private static List<User> users = JsonToObjectLists.GetUserList();
        private static List<Order> orders = JsonToObjectLists.GetOrderList();
        public static void Reload()
        {
            movies = JsonToObjectLists.GetMovieList();
            users = JsonToObjectLists.GetUserList();
            orders = JsonToObjectLists.GetOrderList();
        }

        public static bool TryLogin(string username, string password)
        {
            foreach (var item in users)
            {
                if (username == item.Username && password == item.Password) { return true; }
            }
            return false;
        }
        public static int GetId(string username)
        {
            foreach (var item in users)
            {
                if (username == item.Username) { return item.Id; }
            }
            return -1;
        }
        public static string GetUsername(int id)
        {
            foreach (var item in users)
            {
                if (item.Id == id) { return item.Username; }
            }
            return "";
        }
        public static int[] GetIntArray(int id)
        {
            foreach (var item in users)
            {
                if (item.Id == id) { return item.OrderArray; }
            }
            return new int[0];
        }
        public static string GetMovieTitle(int MovieId)
        {
            foreach (var item in movies)
            {
                if (item.Id == MovieId) { return item.Title; }
            }
            return "";
        }
        public static string GetPlayTime(int MovieId, int playtimeId)
        {
            foreach (var item in movies)
            {
                if (item.Id == MovieId) { return item.PlayOptions[playtimeId].Time.ToString("d"); }
            }
            return "";
        }
    }
    //Data classes all public for json
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
        public int[] OrderArray { get; set; }
        public User(int Id, string Username, string Password, string Email)
        {
            this.Id = Id;
            this.Username = Username;
            this.Password = Password;
            this.Email = Email;
            Admin = false;
            OrderArray = new int[] { };
        }
    }
    public class Order
    {
        public int Id { get; set; }
        public int MovieTitle { get; set; }
        public int MoviePlaytimeId { get; set; }
        public int[] SeatAmount { get; set; }
        public int[] MySeats { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Paid { get; set; }
        public Order(int Id, int MovieTitle, int MoviePlaytimeId, int[] SeatAmount, int[] MySeats, float TotalPrice, bool Paid)
        {
            this.Id = Id;
            this.MovieTitle = MovieTitle;
            this.MoviePlaytimeId = MoviePlaytimeId;
            this.SeatAmount = SeatAmount;
            this.MySeats = MySeats;
            this.TotalPrice = TotalPrice;
            OrderDate = DateTime.Now;
            this.Paid = Paid;
        }
    }
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }
        public string[] Genre { get; set; }
        public int Length { get; set; }
        public PlayOptions[] PlayOptions { get; set; }
        public int GetId() { return Id; }
        public string GetTitle() { return Title; }
        public string PosterLocation { get; set; }
    }
    public class PlayOptions
    {
        public int SubId { get; set; }
        public DateTime Time { get; set; }
        public string ScreenType { get; set; }
        public int Room { get; set; }
        public int[] Reserved { get; set; }
    }
}
