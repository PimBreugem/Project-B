using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Proejct_B
{
    public partial class Patat : Form
    {
        //----------Alle Variable----------//
        int stoelen = 0;
        int tickets_Volwassenen = 0;
        int tickets_Kinderen = 0;
        int tickets_Gehandicapten = 0;
        double volwassen_prijs = 13.95;
        double kinderen_prijs = 8.95;
        double gehandicapten_prijs = 5.95;
        double totaal_prijs = 0.0;
        
        //dit is tijdelijk wordt niet gebruikt though
        string[] film_Tijden = new string[5]
        {
            "01-05-20 12:30","01-05-20 14:30", "02-05-20 13:00","02-05-20 16:00","03-05-20 12:00"
        };

        //----------Einde Alle Variable----------//


        //----------No Clue----------//
        public Patat()
        {
            InitializeComponent();
        }
        //----------End No Clue----------//

        //----------Start JsonConverter Class----------//
        class JsonConverter
        {
            private static readonly string root = Environment.CurrentDirectory + @"\..\..\..\";

            public static List<Order> GetOrderList()
            {
                // Dont forget to add projectname to path
                string jsonFilePath = root + @"json\orders.json";
                string json = File.ReadAllText(jsonFilePath);
                List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(json);
                return orders;
            }
            public static List<Movie> getMovieList()
            {
                string jsonFilePath = root + @"json\movies.json";
                string json = File.ReadAllText(jsonFilePath);
                List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                return movies;
            }
            private static List<Order> orders = GetOrderList();
            private static List<Movie> movies = getMovieList();

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
        class PlayOptions
        {
            public int SubId { get; set; }
            public DateTime Time { get; set; }
            public string ScreenType { get; set; }
            public int Room { get; set; }
            public int[] Reserved { get; set; }
        }
        //----------Start JsonConverter Class----------//

        //----------Menu Scherm----------//
        private void Menu_Login_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatatTabControl.SelectedTab = LoginPage;
        }
        private void Menu_Register_Button_Click(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = RegisterPage;
        }
        private void Menu_Orders_Button_Click(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = OrderPage;
        }
        private void Menu_Admin_Button_Click(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = AdminPage;
        }
        private void Menu_Exit_Button_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void Menu_Center_PictureBox_Click(object sender, EventArgs e)
        {
            //Select center movie to go to order
            PatatTabControl.SelectedTab = TimePage;
        }
        private void Menu_Next_Button_Click(object sender, EventArgs e)
        {
            //choose next movie
        }
        private void Menu_Prev_Button_Click(object sender, EventArgs e)
        {
            //choose prev movie
        }
        //----------Einde Menu Scherm----------//


        //----------Login Scherm----------//
        private void Login_Username_TextBox_Enter(object sender, EventArgs e)
        {
            if (Login_Username_TextBox.Text == "Gebruikersnaam")
            {
                Login_Username_TextBox.Text = "";
                Login_Username_TextBox.ForeColor = Color.Black;
            }
        }
        private void Login_Username_TextBox_Leave(object sender, EventArgs e)
        {
            if (Login_Username_TextBox.Text == "")
            {
                Login_Username_TextBox.Text = "Gebruikersnaam";
                Login_Username_TextBox.ForeColor = Color.Silver;
            }
        }
        private void Login_Password_TextBox_Enter(object sender, EventArgs e)
        {
            if (Login_Password_TextBox.Text == "Wachtwoord")
            {
                Login_Password_TextBox.Text = "";
                Login_Password_TextBox.ForeColor = Color.Black;
            }
        }
        private void Login_Password_TextBox_Leave(object sender, EventArgs e)
        {
            if (Login_Password_TextBox.Text == "")
            {
                Login_Password_TextBox.Text = "Wachtwoord";
                Login_Password_TextBox.ForeColor = Color.Silver;
            }
        }
        private void Login_Login_Button_Click(object sender, EventArgs e)
        {
            //PimLoginFunction
            //Login_Message_RichTextBox.Text
        }
        private void Login_BackToMenu_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatatTabControl.SelectedTab = MenuPage;
        }
        private void Login_Register_Button_Click_1(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = RegisterPage;
        }
        //----------Einde Login Scherm----------//


        //----------Register Scherm----------//
        private void Register_Username_TextBox_Enter(object sender, EventArgs e)
        {
            if (Register_Username_TextBox.Text == "Gebruikersnaam")
            {
                Register_Username_TextBox.Text = "";
                Register_Username_TextBox.ForeColor = Color.Black;
            }
        }
        private void Register_Username_TextBox_Leave(object sender, EventArgs e)
        {
            if (Register_Username_TextBox.Text == "")
            {
                Register_Username_TextBox.Text = "Gebruikersnaam";
                Register_Username_TextBox.ForeColor = Color.Silver;
            }
        }
        private void Register_Password_TextBox_Enter(object sender, EventArgs e)
        {
            if (Register_Password_TextBox.Text == "Wachtwoord")
            {
                Register_Password_TextBox.Text = "";
                Register_Password_TextBox.ForeColor = Color.Black;
            }
        }
        private void Register_Password_TextBox_Leave(object sender, EventArgs e)
        {
            if (Register_Password_TextBox.Text == "")
            {
                Register_Password_TextBox.Text = "Wachtwoord";
                Register_Password_TextBox.ForeColor = Color.Silver;
            }
        }
        private void Register_PasswordCheck_TextBox_Enter(object sender, EventArgs e)
        {
            if (Register_PasswordCheck_TextBox.Text == "Wachtwoord")
            {
                Register_PasswordCheck_TextBox.Text = "";
                Register_PasswordCheck_TextBox.ForeColor = Color.Black;
            }
        }
        private void Register_PasswordCheck_TextBox_Leave(object sender, EventArgs e)
        {
            if (Register_PasswordCheck_TextBox.Text == "")
            {
                Register_PasswordCheck_TextBox.Text = "Wachtwoord";
                Register_PasswordCheck_TextBox.ForeColor = Color.Silver;
            }
        }
        private void Register_Email_TextBox_Enter(object sender, EventArgs e)
        {
            if (Register_Email_TextBox.Text == "Email")
            {
                Register_Email_TextBox.Text = "";
                Register_Email_TextBox.ForeColor = Color.Black;
            }
        }
        private void Register_Email_TextBox_Leave(object sender, EventArgs e)
        {
            if (Register_Email_TextBox.Text == "")
            {
                Register_Email_TextBox.Text = "Email";
                Register_Email_TextBox.ForeColor = Color.Silver;
            }
        }
        private void Register_Register_Button_Click(object sender, EventArgs e)
        {
            string Username = Register_Username_TextBox.Text;
            string Password = Register_Password_TextBox.Text;
            string PasswordCheck = Register_PasswordCheck_TextBox.Text;
            string Email = Register_Email_TextBox.Text;

            if (Username == "Gebruikersnaam" || Password == "Wachtwoord" || Email == "Email") {
                Register_Message_RichTextBox.Text = "U moet bovenstaande velden allemaal invullen om te kunnen registreren!";
                return;
            }

            if (!RegisterCheck.RegisterCheck.IsValidUsername(Username)) {
                Register_Message_RichTextBox.Text = "Gebruikersnaam gebruikt verkeerde character Alleen a-z / A-Z / 0-9!";
                return;
            } 

            if (Password != PasswordCheck) {
                Register_Message_RichTextBox.Text = "Wachtwoorden komen niet met elkaar overeen!";
                return;
            } 

            if (!RegisterCheck.RegisterCheck.IsValidEmail(Email)) {
                Register_Message_RichTextBox.Text = "Dit adress is niet geldig vb. van een goeie email: Patat@gmail.com";
                return;
            }

            //if (!RegisterCheck.RegisterCheck.RegisterAccount(Username, Password, Email)) 
            //{
             //   Register_Message_RichTextBox.Text = "";
             //   return;
            //}

            Register_Message_RichTextBox.Text = "Succescol geregistreerd";
        }
        private void Register_BackToLogin_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatatTabControl.SelectedTab = LoginPage;
        }
        //----------Einde Register Scherm----------//


        //----------Time select Scherm----------//

        //title voor titel film
        //Time_TitleSelectedMovie_Label

        //foto voor foto film
        //Time_PicSelectedMovie_PictureBox

        //info voor info film
        //Time_InfoSelectedMovie_TextBox
        private void Time_ToTickets_Button_Click(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = TicketPage;
        }

        //----------Einde Time select Scherm----------//


        //----------Tickets select Scherm----------//
        private void Ticket_ToSeats_Button_Click(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = SeatsPage;
        }
        private void Ticket_MinVolwassenen_Button_Click(object sender, EventArgs e)
        {
            if (tickets_Volwassenen > 0)
            {
                tickets_Volwassenen -= 1;
                Aantal_volwassenen.Text = tickets_Volwassenen.ToString();
                totaal_prijs -= volwassen_prijs;
                Prijs_Totaal.Text = totaal_prijs.ToString();
            }
        }
        private void Ticket_MinKinderen_Button_Click(object sender, EventArgs e)
        {
            if (tickets_Kinderen > 0)
            {
                tickets_Kinderen -= 1;
                Aantal_kinderen.Text = tickets_Kinderen.ToString();
                totaal_prijs -= kinderen_prijs;
                Prijs_Totaal.Text = totaal_prijs.ToString();
            }
        }
        private void Ticket_MinGehandicapten_Button_Click(object sender, EventArgs e)
        {
            if (tickets_Gehandicapten > 0)
            {
                tickets_Gehandicapten -= 1;
                Aantal_gehandicapten.Text = tickets_Gehandicapten.ToString();
                totaal_prijs -= gehandicapten_prijs;
                Prijs_Totaal.Text = totaal_prijs.ToString();
            }
        }
        private void Ticket_PlusKinderen_Button_Click(object sender, EventArgs e)
        {
            tickets_Kinderen += 1;
            Aantal_kinderen.Text = tickets_Kinderen.ToString();
            totaal_prijs += kinderen_prijs;
            Prijs_Totaal.Text = totaal_prijs.ToString();
        }
        private void Ticket_PlusVolwassen_Button_Click(object sender, EventArgs e)
        {
            tickets_Volwassenen += 1;
            Aantal_volwassenen.Text = tickets_Volwassenen.ToString();
            totaal_prijs += volwassen_prijs;
            Prijs_Totaal.Text = totaal_prijs.ToString();
        }
        private void Ticket_PlusGehandicapten_Button_Click(object sender, EventArgs e)
        {
            tickets_Gehandicapten += 1;
            Aantal_gehandicapten.Text = tickets_Gehandicapten.ToString();
            totaal_prijs += gehandicapten_prijs;
            Prijs_Totaal.Text = totaal_prijs.ToString();
        }
        private void Ticket_BackToTime_LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatatTabControl.SelectedTab = TimePage;
        }
        //---------- Einde Tickets select Scherm----------//


  


        //----------Seats select Scherm----------//
        private void Seats_BackToTicket_LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatatTabControl.SelectedTab = TicketPage;
        }

        private void Seats_ToPayment_Button_Click(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = PaymentPage;
        }
        //----------Einde Seats select Scherm----------//


        //----------Payment Scherm----------//
        //Oude Code Thijmen weet niet of het nog iets belangrijks doet
        private void Pay_Button_Click(object sender, EventArgs e)
        {
            //https://www.youtube.com/watch?v=sRBAv4-G0iw
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\GegevensGebruikers\\" + Firstname_Textbox.Text + " " + Lastname_Textbook.Text + ".txt");
            sw.WriteLine(Firstname_Label.Text + " " + Firstname_Textbox.Text);
            sw.WriteLine(Lastname_Label.Text + " " + Lastname_Textbook.Text);
            sw.WriteLine(Emailadres_Label.Text + " " + Emailadres_Textbox.Text);
            sw.WriteLine(Bank_Label.Text + " " + Bank_Combobox.Text);
            sw.Close();
        }
        private void Payment_BackToSeats_LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatatTabControl.SelectedTab = TicketPage;
        }

        //----------Einde Payment Scherm----------//
       
        //----------Begin Admin Scherm------------//

        

        private void Calender_Data_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateSelected_Textbox.Text = Calender_Data.SelectionStart.ToShortDateString();
        }

        private void SearchDate_Button_Click_1(object sender, EventArgs e)
        {
            if (DateSelected_Textbox.Text != "")
            {
                PatatTabControl.SelectedTab = AdminPage2;
                DateSelectedText.Text = DateSelected_Textbox.Text;
                string dateselect = DateSelected_Textbox.Text;
                Tuple<string, string, string, Tuple<string, string, string>> revenue = JsonConverter.GetData(dateselect);
                AmountOrders.Text = revenue.Item2;
                AmountTickets.Text = revenue.Item3;
                Revenue.Text = "€ " + revenue.Item1;
            }
            else
            {
                MessageBox.Show("Kies een datum");
            }
        }

        private void Admin2_BacktoMenu_LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatatTabControl.SelectedTab = MenuPage;
        }

        private void OtherDate_Button_Click(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = AdminPage;
        }

        private void AmountOrdersInfo_Click(object sender, EventArgs e)
        {
            Tuple<string, string, string> MovieList = JsonConverter.GetMovie();
            string dateselect = DateSelected_Textbox.Text;
            Tuple<string, string, string, Tuple<string, string, string>> revenue = JsonConverter.GetData(dateselect);
            MessageBox.Show(MovieList.Item1 + " = " + revenue.Item4.Item1 + "\n" +
                            MovieList.Item2 + " = " + revenue.Item4.Item2 + "\n" +
                            MovieList.Item3 + " = " + revenue.Item4.Item3);
        }

        private void AmountTicketsInfo_Click(object sender, EventArgs e)
        {
            string dateselect = DateSelected_Textbox.Text;
            Tuple<string, string, string> SortTickets = JsonConverter.GetSortTickets(dateselect);
            MessageBox.Show("Adult tickets: " + SortTickets.Item1 + "\n" +
                            "Child tickets: " + SortTickets.Item2 + "\n" +
                            "Disabled tickets: " + SortTickets.Item3);
        }


        //----------Einde Admin Scherm------------//
    }
}
