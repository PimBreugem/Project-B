﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace ProjectB
{
    class JsonConverter
    {
        public static List<Movie> getMovieList()
        {
            string jsonFilePath = @"C:\Users\tijme\OneDrive\Bureaublad\Project-B-master\Project-B-master\projectB\movies.json";
            string json = File.ReadAllText(jsonFilePath);
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            return movies;
        }
        public static List<User> getUserList()
        {
            string jsonFilePath = @"C:\Users\tijme\OneDrive\Bureaublad\Project-B-master\Project-B-master\ProjectB\users.json";
            string json = File.ReadAllText(jsonFilePath);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
            return users;
        }
    }
    class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }
        public string[] Genre { get; set; }
        public string Length { get; set; }
        public string[] PlayTimes { get; set; }
        public string Price { get; set; }
        public string[] Version { get; set; }
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
}
