﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml.XPath;
using Newtonsoft.Json;

namespace ProjectB
{
    class EreaAssembler
    {
        public static bool IntContains(int[] list, int input)
        {
            if(list == null) { return false; }
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == input)
                {
                    return true;
                }
            }
            return false;
        }
        public static void Assembler(int[] not, int[] selected)
        {
            string result = "   1    2    3    4    5\n";
            for (int i = 0, j = 0; j < 4; j++)
            {
                i = 0;
                result += (j + 1) + " ";
                while (i < 5)
                {
                    if (IntContains(not, i + j * 5 + 1))
                    {
                        result += "[X ] ";
                    }
                    else if (selected != null && IntContains(selected, i + j * 5 + 1))
                    {
                        result += "[S ] ";
                    }

                    else
                    {
                        string total = (i + j * 5).ToString();
                        if (total.Length == 1) { result += "[" + (i + j * 5 + 1) + " ] "; }
                        else { result += "[" + (i + j * 5 + 1) + "] "; }
                    }
                    i++;
                }
                result += "\n";
            }
            Console.WriteLine(result);
        }
    }
    
    class Reservation
    {
        static void ClearAndWrite(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
        }
        private static List<Movie> movies = JsonConverter.getMovieList();
        
        
        public static int[] getSeats(int movie, int playtime)
        {
            int[] not = movies[movie].PlayOptions[playtime].Reserved;
            int[] selected = new int[1];
            int index = 0;
            string print = "Enter seats, enter next to contine";
            while (true)
            {
                Console.Clear();
                EreaAssembler.Assembler(not, selected); //BUG not value to be the reservered int array from json / class
                Console.WriteLine(print);
                string input = Console.ReadLine();

                if (input.ToLower() == "next") { break; }
                int number;
                bool succes = Int32.TryParse(input, out number);
                if (succes)
                {
                    if (index == selected.Length)
                    {
                        int[] newInt = new int[selected.Length + 1];
                        for (int i = 0; i < selected.Length; i++) { newInt[i] = selected[i]; }
                        selected = newInt;
                    }
                    if (EreaAssembler.IntContains(not, number)) { print = "This seat is occupied, please enter seat or next to contine"; }
                    else
                    {
                        selected[index] = number;
                        index++;
                        print = "Enter seats, enter next to contine";
                    }
                }
                else
                {
                    print = "Please enter valid number or type next";
                }
            }
            return selected;
        }
        public static int GetSeatAmount(string name, float price)
        {
            string before = "";
            while (true) {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                ClearAndWrite(before + "How many " + name + " tickets do you want to order " + HexToChar("20AC") + price + " per ticket.");
                bool succes = Int32.TryParse(Console.ReadLine(), out int number);
                if (succes) { return number; }
                else { before = "Please enter valid number\n"; }
            }
        }
        public static char HexToChar(string hex)
        {
            return (char)ushort.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }
        private static List<Order> orders = JsonConverter.GetOrderList();
        public static void NewReservation()
        {
            Tuple<int, int> tuple = MovieFunctions.MovieOverviewReserve();
            int selectedMovie = tuple.Item1;
            int time = tuple.Item2;
            int[] seats = getSeats(selectedMovie, time);
            int total = 0, adult = 0, child = 0, disabled = 0;
            float pricetotal = 0.00f;
            while (total != seats.Length)
            {
                adult = GetSeatAmount("Adult", 14.99f);
                child = GetSeatAmount("Child", 9.99f);
                disabled = GetSeatAmount("disabled", 4.99f);
                total = adult + child + disabled;
                pricetotal = (adult * 14.99f) + (child * 9.99f) + (disabled * 4.99f);
            }
            bool paid = false;
            ClearAndWrite("Your order is almost complete, your total is " + HexToChar("20AC") + pricetotal + "\nWould you like to pay online or at the cinema."); //FIX max float is unlimited, need to be 2
            Console.WriteLine("Online / Cinema:");
            string result = "";
            while (true)
            {
                string input = Console.ReadLine().ToLower();
                if (input == "online")
                {
                    paid = true;
                    result = "You have succesfully paid!";
                    break;
                }
                else if (input == "cinema")
                {
                    paid = false;
                    result = "Be aware that you have to be around 15 minutes early to validate you ticket(s).";
                    break;
                }
            }
            ClearAndWrite("Order succesfully, check my orders for order details\n" + result);
            string wait = Console.ReadLine();
            int[] seatamount = new int[4] { total, adult, child, disabled };
            Order neworder = new Order(orders.Count, selectedMovie, time, seatamount, seats, pricetotal, DateTime.Now, paid);
            orders.Add(neworder);
            string json = JsonConvert.SerializeObject(orders, Formatting.Indented);
            string jsonFilePath = Environment.CurrentDirectory + @"\..\..\..\json\orders.json";
            File.WriteAllText(jsonFilePath, json);
            //update de gereserveerde stoelen van een film
        }
    }
    class RegisterAccount
    {
        private static List<User> users = JsonConverter.GetUserList();
        private static void newUsers(int id, string title, string password)
        {
            int[] orders = new int[0];
            User newuser = new User(id, title, password, false, orders);
            users.Add(newuser);
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            string jsonFilePath = Environment.CurrentDirectory + @"\..\..\..\json\users.json";
            File.WriteAllText(jsonFilePath, json);
        }
        public static void registerAccount()
        {
            string username = getUsername();
            string password = getPassword();
            newUsers(users.Count, username, password);
        }
        private static string getPassword()
        {
            bool newPass = false;
            string message = "Please enter a password:";
            string inputpass = "";
            while(newPass == false)
            {
                Console.Clear();
                Console.WriteLine(message);
                inputpass = Console.ReadLine();
                Console.WriteLine("Enter your password again:");
                string inputpass2 = Console.ReadLine();
                if(inputpass == inputpass2) { newPass = true; }
                else { message = "Passwords are not the same, enter password:"; }
            }
            Console.Clear();
            return inputpass;
        }
        private static string getUsername()
        {
            bool newUser = false;
            string message = "Please enter a username:";
            string inputname = "";
            while (newUser == false)
            {
                Console.Clear();
                Console.WriteLine(message);
                inputname = Console.ReadLine();
                newUser = true;
                foreach(var item in users)
                {
                    if(item.Title == inputname)
                    {
                        newUser = false;
                        message = "Username has been taken, please enter another one:";
                    }
                }
            }
            Console.Clear();
            return inputname;
        }
    }
}
