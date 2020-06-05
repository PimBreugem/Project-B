//using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace ProjectB
{


    class JsonConverter
    {
        //TODO create error handler if json file is not found (fixed not implemented yet)

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

        public static void NewUser(string Username, string Password, string Email)
        {
            string jsonFilePath = root + @"json\users.json";
            users.Add(new User(users.Count, Username, Password, Email));
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }

        //Lijsten
        public static List<Movie> movies = GetMovieList();
        private static List<User> users = GetUserList();
        private static List<Order> orders = GetOrderList();
    }


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
        private int Id { get; set; }
        private int MovieTitleId { get; set; }
        private int MoviePlaytimeId { get; set; }
        private int[] SeatAmount { get; set; }
        private int[] MySeats { get; set; }
        private float TotalPrice { get; set; }
        private DateTime OrderDate { get; set; }
        private bool Paid { get; set; }
        public Order(int Id, int MovieTitleId, int MoviePlaytimeId, int[] SeatAmount, int[] MySeats, float TotalPrice, bool Paid)
        {
            this.Id = Id;
            this.MovieTitleId = MovieTitleId;
            this.MoviePlaytimeId = MoviePlaytimeId;
            this.SeatAmount = SeatAmount;
            this.MySeats = MySeats;
            this.TotalPrice = TotalPrice;
            OrderDate = DateTime.Now;
            this.Paid = Paid;
        }
        public int GetId() { return Id; }
        //public int GetMovieTitle() { return MovieTitleId; }

        //Methods toevoegen die nodig zijn voor het programma.
    }
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }
        public string[] Genre { get; set; }
        public int Length { get; set; }
        public string PosterLocation { get; set; }
        public PlayOptions[] PlayOptions { get; set; }

    }
    public class PlayOptions
    {
        private int SubId { get; set; }
        private DateTime Time { get; set; }
        private string ScreenType { get; set; }
        private int Room { get; set; }
        private int[] Reserved { get; set; }
    }
}