using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace seats
{
    class Seats
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }    
    }

    class Program
    {
        public static List<Seats> getSeatsList()
        {
            string jsonFilePath = @"C:/Users/Diedv/Desktop/seats/seats/seats.json";
            string json = File.ReadAllText(jsonFilePath);
            List<Seats> seats = JsonConvert.DeserializeObject<List<Seats>>(json);
            return seats;
        }

        public static List<Seats> seats = getSeatsList();
        static void Main(string[] args)
        { 
            var table = ConsoleTable.From<Seats>(seats);
            table.Write();
        }
    }
}
