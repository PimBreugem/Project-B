using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Transactions;

namespace ProjectB
{
    class JsonConverter
    {
        //TODO create error handler if json file is not found (fixed not implemented yet)
        private static readonly string root = Environment.CurrentDirectory + @"\..\..\..\";
        public static List<Movie> getMovieList()
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
        private static List<User> users = JsonConverter.GetUserList();
        public static void OrderUpdate(int NewOrder)
        {
            int user = Program.currentId;

            int[] update = new int[users[user].Orderlist.Length + 1];
            for(int i = 0; i < users[user].Orderlist.Length; i++)
            {
                update[i] = users[user].Orderlist[i];
            }
            update[users[user].Orderlist.Length] = NewOrder;

            users[user].Orderlist = update;
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            string jsonFilePath = Environment.CurrentDirectory + @"\..\..\..\json\users.json";
            File.WriteAllText(jsonFilePath, json);
        }
    }
    class Order
    {
        public int Id { get; set; }
        public int MovieTitle { get; set; }
        public int MoviePlaytimeId { get; set; }    
        public int[] SeatAmount { get; set; } //Int Array with 4 values -> total seats, Adult seats, child seats & disabled seats
        public int[] MySeats { get; set; }
        public float TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Paid { get; set; }
        public Order(int id, int movietitle, int movieplaytimeid, int[] seatamount, int[] myseats, float totalPrice, DateTime orderdate, bool paid)
        {
            Id = id;
            MovieTitle = movietitle;
            MoviePlaytimeId = movieplaytimeid;
            SeatAmount = seatamount;
            MySeats = myseats;
            TotalPrice = totalPrice;
            OrderDate = orderdate;
            Paid = paid;
        }
    }
    class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }
        public string[] Genre { get; set; }
        public int Length { get; set; }
        public PlayOptions[] PlayOptions { get; set; }

    }
    class User
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int[] Orderlist { get; set; }
        public User(int id, string title, string password, bool admin, int[] orderlist)
        {
            Id = id;
            Title = title;
            Password = password;
            Admin = admin;
            Orderlist = orderlist;
        }
    }
    class PlayOptions
    {
        public int SubId { get; set; }
        public DateTime Time { get; set; }
        public string ScreenType { get; set; }
        public int Room { get; set; }
        public int[] Reserved { get; set; }
    }
}