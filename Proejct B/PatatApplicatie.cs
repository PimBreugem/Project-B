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
        double volwassen_prijs = 13.50;
        double kinderen_prijs = 8.50;
        double gehandicapten_prijs = 5.50;
        double totaal_prijs = 0.0;
        double popcorn_Prijs = 3.50;
        double frisdrank_Prijs = 1.50;
        double snoep_Prijs = 1.0;
        double totaal_Prijs_Snacks = 0.0;
        int popcorn_Aantal = 0;
        int frisdrank_Aantal = 0;
        int snoep_Aantal = 0;
        private static readonly string root = Environment.CurrentDirectory + @"\..\..\";
        public static List<Movie> movies = JsonToObjectLists.GetMovieList();
        private int currentImage = 1;

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
            Menu_Movie_Label.Text = movies[currentImage].Title;
            Menu_Left_PictureBox.Image = Image.FromFile(root + movies[currentImage - 1].PosterLocation);
            Menu_Center_PictureBox.Image = Image.FromFile(root + movies[currentImage].PosterLocation);
            Menu_Right_PictureBox.Image = Image.FromFile(root + movies[currentImage + 1].PosterLocation);
        }
        //----------End No Clue----------//

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
            int quirriedId = currentImage;
            Time_PicSelectedMovie_PictureBox.Image = Image.FromFile(root + movies[quirriedId].PosterLocation);
            TitleSelectedMovie_Label.Text = movies[quirriedId].Title;
            Movie_Playtime_Label.Text = "Speeltijd: " + movies[quirriedId].Length + " minuten";
            string genres = "Genres: ";
            for (int i = 0; i < movies[quirriedId].Genre.Length; i++)
            {
                genres += movies[quirriedId].Genre[i] + " ";
            }
            Movie_Genre_Label.Text = genres;
            Movie_Description_Label.Text = movies[quirriedId].Bio;
            PatatTabControl.SelectedTab = TimePage;
        }
        private void Menu_Next_Button_Click(object sender, EventArgs e)
        {
            //choose next movie
            currentImage++;
            switch (currentImage)
            {
                case 1:
                    Menu_Left_PictureBox.Image = Image.FromFile(root + movies[currentImage - 1].PosterLocation);
                    Menu_Center_PictureBox.Image = Image.FromFile(root + movies[currentImage].PosterLocation);
                    Menu_Right_PictureBox.Image = Image.FromFile(root + movies[currentImage + 1].PosterLocation);
                    Menu_Movie_Label.Text = movies[currentImage].Title;
                    break;
                case 2:
                    Menu_Left_PictureBox.Image = Image.FromFile(root + movies[currentImage - 1].PosterLocation);
                    Menu_Center_PictureBox.Image = Image.FromFile(root + movies[currentImage].PosterLocation);
                    Menu_Right_PictureBox.Image = Image.FromFile(root + movies[0].PosterLocation);
                    Menu_Movie_Label.Text = movies[currentImage].Title;
                    break;
                case 3:
                    currentImage = 0;
                    Menu_Left_PictureBox.Image = Image.FromFile(root + movies[movies.Count - 1].PosterLocation);
                    Menu_Center_PictureBox.Image = Image.FromFile(root + movies[currentImage].PosterLocation);
                    Menu_Right_PictureBox.Image = Image.FromFile(root + movies[currentImage + 1].PosterLocation);
                    Menu_Movie_Label.Text = movies[currentImage].Title;
                    break;
            }
            GC.Collect();
        }
        private void Menu_Prev_Button_Click(object sender, EventArgs e)
        {
            //choose prev movie
            currentImage--;
            switch (currentImage)
            {
                case -1:
                    currentImage = movies.Count - 1;
                    Menu_Left_PictureBox.Image = Image.FromFile(root + movies[currentImage - 1].PosterLocation);
                    Menu_Center_PictureBox.Image = Image.FromFile(root + movies[currentImage].PosterLocation);
                    Menu_Right_PictureBox.Image = Image.FromFile(root + movies[0].PosterLocation);
                    Menu_Movie_Label.Text = movies[currentImage].Title;
                    break;
                case 0:
                    Menu_Left_PictureBox.Image = Image.FromFile(root + movies[movies.Count - 1].PosterLocation);
                    Menu_Center_PictureBox.Image = Image.FromFile(root + movies[currentImage].PosterLocation);
                    Menu_Right_PictureBox.Image = Image.FromFile(root + movies[currentImage + 1].PosterLocation);
                    Menu_Movie_Label.Text = movies[currentImage].Title;
                    break;
                case 1:
                    Menu_Left_PictureBox.Image = Image.FromFile(root + movies[currentImage - 1].PosterLocation);
                    Menu_Center_PictureBox.Image = Image.FromFile(root + movies[currentImage].PosterLocation);
                    Menu_Right_PictureBox.Image = Image.FromFile(root + movies[currentImage + 1].PosterLocation);
                    Menu_Movie_Label.Text = movies[currentImage].Title;
                    break;
            }
            GC.Collect();
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
            tickets_Volwassenen += 1;
            Aantal_volwassenen.Text = tickets_Volwassenen.ToString();
            totaal_prijs += volwassen_prijs;
            Prijs_Totaal.Text = totaal_prijs.ToString();
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
            if (tickets_Volwassenen > 0)
            {
                tickets_Volwassenen -= 1;
                Aantal_volwassenen.Text = tickets_Volwassenen.ToString();
                totaal_prijs -= volwassen_prijs;
                Prijs_Totaal.Text = totaal_prijs.ToString();
            }
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
        private void Plus_Popcorn_Button_Click(object sender, EventArgs e)
        {
            popcorn_Aantal += 1;
            Popcorn_Aantal_Label.Text = popcorn_Aantal.ToString();
            totaal_Prijs_Snacks += popcorn_Prijs;
            Totaal_Snacks_Label.Text = totaal_Prijs_Snacks.ToString();
        }
        private void Plus_Frisdrank_Button_Click(object sender, EventArgs e)
        {
            frisdrank_Aantal += 1;
            Frisdrank_Aantal_Label.Text = frisdrank_Aantal.ToString();
            totaal_Prijs_Snacks += frisdrank_Prijs;
            Totaal_Snacks_Label.Text = totaal_Prijs_Snacks.ToString();
        }
        private void Plus_Snoep_Button_Click(object sender, EventArgs e)
        {
            snoep_Aantal += 1;
            Snoep_Aantal_Label.Text = snoep_Aantal.ToString();
            totaal_Prijs_Snacks += snoep_Prijs;
            Totaal_Snacks_Label.Text = totaal_Prijs_Snacks.ToString();
        }
        private void Min_Popcorn_Button_Click(object sender, EventArgs e)
        {
            if(popcorn_Aantal > 0)
            {
                popcorn_Aantal -= 1;
                Popcorn_Aantal_Label.Text = popcorn_Aantal.ToString();
                totaal_Prijs_Snacks -= popcorn_Prijs;
                Totaal_Snacks_Label.Text = totaal_Prijs_Snacks.ToString();
            }
        }
        private void Min_Frisdrank_Button_Click(object sender, EventArgs e)
        {
            if(frisdrank_Aantal > 0)
            {
                frisdrank_Aantal -= 1;
                Frisdrank_Aantal_Label.Text = frisdrank_Aantal.ToString();
                totaal_Prijs_Snacks -= frisdrank_Prijs;
                Totaal_Snacks_Label.Text = totaal_Prijs_Snacks.ToString();
            }
        }
        private void Min_Snoep_Button_Click(object sender, EventArgs e)
        {
            if (snoep_Aantal > 0)
            {
                snoep_Aantal -= 1;
                Snoep_Aantal_Label.Text = snoep_Aantal.ToString();
                totaal_Prijs_Snacks -= snoep_Prijs;
                Totaal_Snacks_Label.Text = totaal_Prijs_Snacks.ToString();
            }
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
            Totaal_Prijs_Alles_Label.Text = (totaal_Prijs_Snacks + totaal_prijs).ToString();
        }
        //----------Einde Seats select Scherm----------//


        //----------Payment Scherm----------//
        //Oude Code Tijmen weet niet of het nog iets belangrijks doet
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
                Tuple<string, string, string, Tuple<string, string, string>> revenue = JsonToObjectLists.GetData(dateselect);
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
            Tuple<string, string, string> MovieList = JsonToObjectLists.GetMovie();
            string dateselect = DateSelected_Textbox.Text;
            Tuple<string, string, string, Tuple<string, string, string>> revenue = JsonToObjectLists.GetData(dateselect);
            MessageBox.Show(MovieList.Item1 + " = " + revenue.Item4.Item1 + "\n" +
                            MovieList.Item2 + " = " + revenue.Item4.Item2 + "\n" +
                            MovieList.Item3 + " = " + revenue.Item4.Item3);
        }

        private void AmountTicketsInfo_Click(object sender, EventArgs e)
        {
            string dateselect = DateSelected_Textbox.Text;
            Tuple<string, string, string> SortTickets = JsonToObjectLists.GetSortTickets(dateselect);
            MessageBox.Show("Adult tickets: " + SortTickets.Item1 + "\n" +
                            "Child tickets: " + SortTickets.Item2 + "\n" +
                            "Disabled tickets: " + SortTickets.Item3);
        }


        //----------Einde Admin Scherm------------//
    }
}
