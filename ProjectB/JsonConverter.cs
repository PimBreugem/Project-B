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
            string jsonFilePath = @"C:\Users\freek\OneDrive\Documenten\School\Project B\Project-B\ProjectB\movies.json";
            string json = File.ReadAllText(jsonFilePath);
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            return movies;
        }
        public static List<User> getUserList()
        {
            string jsonFilePath = @"C:\Users\freek\OneDrive\Documenten\School\Project B\Project-B\ProjectB\users.json";
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
        public string[] Type { get; set; }
        public string Screen { get; set; }
        public Movie(int id, string title, string bio, string[] genre, string length, string[] playTimes, string price, string[] type, string screen)
        {
            Id = id;
            Title = title;
            Bio = bio;
            Genre = genre;
            Length = length;
            PlayTimes = playTimes;
            Price = price;
            Type = type;
            Screen = screen;
        }
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