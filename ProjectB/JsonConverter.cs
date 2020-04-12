using Newtonsoft.Json;
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
            string jsonFilePath = @"C:/Users/Diedv/Desktop/ProjectB/ProjectB/movies.json";
            string json = File.ReadAllText(jsonFilePath);
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            return movies;
        }
        public static List<User> getUserList()
        {
            string jsonFilePath = @"C:/Users/Diedv/Desktop/ProjectB/ProjectB/users.json";
            string json = File.ReadAllText(jsonFilePath);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
            return users;
        }
        public static List<UserInfo> getUserInfoList()
        {
            string jsonFilePath = @"C:/Users/Diedv/Desktop/ProjectB/ProjectB/usersinfo.json";
            string json = File.ReadAllText(jsonFilePath);
            List<UserInfo> usersinfo = JsonConvert.DeserializeObject<List<UserInfo>>(json);
            return usersinfo;
        }
    }
    class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }
        public string[] Genre { get; set; }
        public int Length { get; set; }
        public string[] PlayTimes { get; set; }
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
    class UserInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserInfo(int id, string title, string firstname, string lastname, string email)
        {
            Id = id;
            Title = title;
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }
    }
}
