//using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
//using System.Transactions;

namespace ProjectB
{


    class JsonConverter
    {
        //TODO create error handler if json file is not found (fixed not implemented yet)
       
        public static List<User> GetUserList()
        {
            string jsonFilePath = root + @"json\users.json";
            string json = File.ReadAllText(jsonFilePath);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
            return users;
        }

        private static List<User> users = JsonConverter.GetUserList();

        private static readonly string root = Environment.CurrentDirectory + @"\..\..\..\";
        public static void NewUser(string Username, string Password, string Email)
        {
            string jsonFilePath = root + @"json\users.json";
            users.Add(new User(users.Count, Username, Password, Email));
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
        }
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
}