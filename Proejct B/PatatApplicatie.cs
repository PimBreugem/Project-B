using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Proejct_B
{
    public partial class Patat : Form
    {
        //----------Alle Variablen----------//
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
        int selectedTime;
        int totalTicketAmount;
        private static readonly string root = Environment.CurrentDirectory + @"\..\..\";
        public static List<Movie> movies = JsonToObjectLists.GetMovieList();
        private int currentImage = 1;

        //dit is tijdelijk wordt niet gebruikt though
        string[] film_Tijden = new string[5]
        {
            "01-05-20 12:30","01-05-20 14:30", "02-05-20 13:00","02-05-20 16:00","03-05-20 12:00"
        };
        int[] Seats = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; /// Array from json reserved seats 40 integers long
        //----------Einde Alle Variable----------//


        #region Intializing the form
        public Patat()
        {
            InitializeComponent();
            Menu_Movie_Label.Text = movies[currentImage].Title;
            Menu_Left_PictureBox.Image = Image.FromFile(root + movies[currentImage - 1].PosterLocation);
            Menu_Center_PictureBox.Image = Image.FromFile(root + movies[currentImage].PosterLocation);
            Menu_Right_PictureBox.Image = Image.FromFile(root + movies[currentImage + 1].PosterLocation);
            HideAllTabsOnTabControl(PatatTabControl);
        }

        private void HideAllTabsOnTabControl(TabControl theTabControl)
        {
            theTabControl.Appearance = TabAppearance.FlatButtons;
            theTabControl.ItemSize = new Size(0, 1);
            theTabControl.SizeMode = TabSizeMode.Fixed;
        }
        #endregion

        #region Menuscherm
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
            for (int i = 0; i < movies[quirriedId].PlayOptions.Length; i++)
            {
                Time_CheckboxList.Items.Add(movies[quirriedId].PlayOptions[i].Time.ToString("dddd d/M HH:mm"));
            }
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
        #endregion

        #region log in scherm
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
            if (ItemFromJsonList.TryLogin(Login_Username_TextBox.Text, Login_Password_TextBox.Text))
            {
                Login_Message_RichTextBox.Text = "U bent ingelogd";
            }
        }
        private void Login_BackToMenu_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatatTabControl.SelectedTab = MenuPage;
        }
        private void Login_Register_Button_Click_1(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = RegisterPage;
        }
        #endregion

        #region registreer scherm
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

            JsonToObjectLists.NewUser(Username, Password, Email);
            Register_Message_RichTextBox.Text = "Succescol geregistreerd";
        }
        private void Register_BackToLogin_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatatTabControl.SelectedTab = LoginPage;
        }
        #endregion


        #region tijd selectie
        private void Time_ToTickets_Button_Click(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = TicketPage;
        }
        private void Time_CheckboxList_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTime = Time_CheckboxList.SelectedIndex;
        }

        private void Time_CheckboxList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < Time_CheckboxList.Items.Count; ++ix)
                if (ix != e.Index) Time_CheckboxList.SetItemChecked(ix, false);
        }
        #endregion


        #region tickets selectie en voedsel scherm
        private void Ticket_ToSeats_Button_Click(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = SeatsPage;
            totalTicketAmount = tickets_Volwassenen + tickets_Kinderen + tickets_Gehandicapten;
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
        #endregion

        #region stoelen scherm
        private void Seats_BackToTicket_LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatatTabControl.SelectedTab = TicketPage;
        }

        private void Seats_ToPayment_Button_Click(object sender, EventArgs e)
        {
            PatatTabControl.SelectedTab = PaymentPage;
            Totaal_Prijs_Alles_Label.Text = (totaal_Prijs_Snacks + totaal_prijs).ToString();
        }

        public void SetColor()
        {
            if (Seats[0] == 2) { Seat1.BackColor = Color.Crimson; }
            if (Seats[1] == 2) { Seat2.BackColor = Color.Crimson; }
            if (Seats[2] == 2) { Seat3.BackColor = Color.Crimson; }
            if (Seats[3] == 2) { Seat4.BackColor = Color.Crimson; }
            if (Seats[4] == 2) { Seat5.BackColor = Color.Crimson; }
            if (Seats[5] == 2) { Seat6.BackColor = Color.Crimson; }
            if (Seats[6] == 2) { Seat7.BackColor = Color.Crimson; }
            if (Seats[7] == 2) { Seat8.BackColor = Color.Crimson; }
            if (Seats[8] == 2) { Seat9.BackColor = Color.Crimson; }
            if (Seats[9] == 2) { Seat10.BackColor = Color.Crimson; }
            if (Seats[10] == 2) { Seat11.BackColor = Color.Crimson; }
            if (Seats[11] == 2) { Seat12.BackColor = Color.Crimson; }
            if (Seats[12] == 2) { Seat13.BackColor = Color.Crimson; }
            if (Seats[13] == 2) { Seat14.BackColor = Color.Crimson; }
            if (Seats[14] == 2) { Seat15.BackColor = Color.Crimson; }
            if (Seats[15] == 2) { Seat16.BackColor = Color.Crimson; }
            if (Seats[16] == 2) { Seat17.BackColor = Color.Crimson; }
            if (Seats[17] == 2) { Seat18.BackColor = Color.Crimson; }
            if (Seats[18] == 2) { Seat19.BackColor = Color.Crimson; }
            if (Seats[19] == 2) { Seat20.BackColor = Color.Crimson; }
            if (Seats[20] == 2) { Seat21.BackColor = Color.Crimson; }
            if (Seats[21] == 2) { Seat22.BackColor = Color.Crimson; }
            if (Seats[22] == 2) { Seat23.BackColor = Color.Crimson; }
            if (Seats[23] == 2) { Seat24.BackColor = Color.Crimson; }
            if (Seats[24] == 2) { Seat25.BackColor = Color.Crimson; }
            if (Seats[25] == 2) { Seat26.BackColor = Color.Crimson; }
            if (Seats[26] == 2) { Seat27.BackColor = Color.Crimson; }
            if (Seats[27] == 2) { Seat28.BackColor = Color.Crimson; }
            if (Seats[28] == 2) { Seat29.BackColor = Color.Crimson; }
            if (Seats[29] == 2) { Seat30.BackColor = Color.Crimson; }
            if (Seats[30] == 2) { Seat31.BackColor = Color.Crimson; }
            if (Seats[31] == 2) { Seat32.BackColor = Color.Crimson; }
            if (Seats[32] == 2) { Seat33.BackColor = Color.Crimson; }
            if (Seats[33] == 2) { Seat34.BackColor = Color.Crimson; }
            if (Seats[34] == 2) { Seat35.BackColor = Color.Crimson; }
            if (Seats[35] == 2) { Seat36.BackColor = Color.Crimson; }
            if (Seats[36] == 2) { Seat37.BackColor = Color.Crimson; }
            if (Seats[37] == 2) { Seat38.BackColor = Color.Crimson; }
            if (Seats[38] == 2) { Seat39.BackColor = Color.Crimson; }
            if (Seats[39] == 2) { Seat40.BackColor = Color.Crimson; }
        }

        public void SeatCounter(int SeatInt, object sender)
        {
            int SeatIntAmount = totalTicketAmount; /// Total seats ordered in ticket screen
            Button btn = sender as Button;
            if (Seats[SeatInt] == 0 && Seats.Count(x => x == 1) < SeatIntAmount)
            {
                Seats[SeatInt] = 1;
                btn.BackColor = Color.Yellow;
            }
            else if (Seats[SeatInt] == 1)
            {
                Seats[SeatInt] = 0;
                btn.BackColor = Color.RoyalBlue;
            }
            int SeatIntTotal = Seats.Count(x => x == 1);
            SeatAmount.Text = SeatIntTotal.ToString();
        }
        #region seat buttons
        private void Seat1_Click(object sender, EventArgs e)
        {
            if (Seats[0] <= 1)
            {
                SeatCounter(0, sender);
            }
        }

        private void Seat2_Click(object sender, EventArgs e)
        {
            if (Seats[1] <= 1)
            {
                SeatCounter(1, sender);
            }
        }

        private void Seat3_Click(object sender, EventArgs e)
        {
            if (Seats[2] <= 1)
            {
                SeatCounter(2, sender);
            }
        }

        private void Seat4_Click(object sender, EventArgs e)
        {
            if (Seats[3] <= 1)
            {
                SeatCounter(3, sender);
            }

        }

        private void Seat5_Click(object sender, EventArgs e)
        {
            if (Seats[4] <= 1)
            {
                SeatCounter(4, sender);
            }
        }

        private void Seat6_Click(object sender, EventArgs e)
        {
            if (Seats[5] <= 1)
            {
                SeatCounter(5, sender);
            }
        }

        private void Seat7_Click(object sender, EventArgs e)
        {
            if (Seats[6] <= 1)
            {
                SeatCounter(6, sender);
            }
        }

        private void Seat8_Click(object sender, EventArgs e)
        {
            if (Seats[7] <= 1)
            {
                SeatCounter(7, sender);
            }
        }

        private void Seat9_Click(object sender, EventArgs e)
        {
            if (Seats[8] <= 1)
            {
                SeatCounter(8, sender);
            }
        }

        private void Seat10_Click(object sender, EventArgs e)
        {
            if (Seats[9] <= 1)
            {
                SeatCounter(9, sender);
            }
        }

        private void Seat11_Click(object sender, EventArgs e)
        {
            if (Seats[10] <= 1)
            {
                SeatCounter(10, sender);
            }
        }

        private void Seat12_Click(object sender, EventArgs e)
        {
            if (Seats[11] <= 1)
            {
                SeatCounter(11, sender);
            }
        }

        private void Seat13_Click(object sender, EventArgs e)
        {
            if (Seats[12] <= 1)
            {
                SeatCounter(12, sender);
            }
        }

        private void Seat14_Click(object sender, EventArgs e)
        {
            if (Seats[13] <= 1)
            {
                SeatCounter(13, sender);
            }
        }

        private void Seat15_Click(object sender, EventArgs e)
        {
            if (Seats[14] <= 1)
            {
                SeatCounter(14, sender);
            }
        }

        private void Seat16_Click(object sender, EventArgs e)
        {
            if (Seats[15] <= 1)
            {
                SeatCounter(15, sender);
            }
        }

        private void Seat17_Click(object sender, EventArgs e)
        {
            if (Seats[16] <= 1)
            {
                SeatCounter(16, sender);
            }
        }

        private void Seat18_Click(object sender, EventArgs e)
        {
            if (Seats[17] <= 1)
            {
                SeatCounter(17, sender);
            }
        }

        private void Seat19_Click(object sender, EventArgs e)
        {
            if (Seats[18] <= 1)
            {
                SeatCounter(18, sender);
            }
        }

        private void Seat20_Click(object sender, EventArgs e)
        {
            if (Seats[19] <= 1)
            {
                SeatCounter(19, sender);
            }
        }

        private void Seat21_Click(object sender, EventArgs e)
        {
            if (Seats[20] <= 1)
            {
                SeatCounter(20, sender);
            }
        }

        private void Seat22_Click(object sender, EventArgs e)
        {
            if (Seats[21] <= 1)
            {
                SeatCounter(21, sender);
            }
        }

        private void Seat23_Click(object sender, EventArgs e)
        {
            if (Seats[22] <= 1)
            {
                SeatCounter(22, sender);
            }
        }

        private void Seat24_Click(object sender, EventArgs e)
        {
            if (Seats[23] <= 1)
            {
                SeatCounter(23, sender);
            }
        }

        private void Seat25_Click(object sender, EventArgs e)
        {
            if (Seats[24] <= 1)
            {
                SeatCounter(24, sender);
            }
        }

        private void Seat26_Click(object sender, EventArgs e)
        {
            if (Seats[25] <= 1)
            {
                SeatCounter(25, sender);
            }
        }

        private void Seat27_Click(object sender, EventArgs e)
        {
            if (Seats[26] <= 1)
            {
                SeatCounter(26, sender);
            }
        }

        private void Seat28_Click(object sender, EventArgs e)
        {
            if (Seats[27] <= 1)
            {
                SeatCounter(27, sender);
            }
        }

        private void Seat29_Click(object sender, EventArgs e)
        {
            if (Seats[28] <= 1)
            {
                SeatCounter(28, sender);
            }
        }

        private void Seat30_Click(object sender, EventArgs e)
        {
            if (Seats[29] <= 1)
            {
                SeatCounter(29, sender);
            }
        }

        private void Seat31_Click(object sender, EventArgs e)
        {
            if (Seats[30] <= 1)
            {
                SeatCounter(30, sender);
            }
        }

        private void Seat32_Click(object sender, EventArgs e)
        {
            if (Seats[31] <= 1)
            {
                SeatCounter(31, sender);
            }
        }

        private void Seat33_Click(object sender, EventArgs e)
        {
            if (Seats[32] <= 1)
            {
                SeatCounter(32, sender);
            }
        }

        private void Seat34_Click(object sender, EventArgs e)
        {
            if (Seats[33] <= 1)
            {
                SeatCounter(33, sender);
            }
        }

        private void Seat35_Click(object sender, EventArgs e)
        {
            if (Seats[34] <= 1)
            {
                SeatCounter(34, sender);
            }
        }

        private void Seat36_Click(object sender, EventArgs e)
        {
            if (Seats[35] <= 1)
            {
                SeatCounter(35, sender);
            }
        }

        private void Seat37_Click(object sender, EventArgs e)
        {
            if (Seats[36] <= 1)
            {
                SeatCounter(36, sender);
            }
        }

        private void Seat38_Click(object sender, EventArgs e)
        {
            if (Seats[37] <= 1)
            {
                SeatCounter(37, sender);
            }
        }

        private void Seat39_Click(object sender, EventArgs e)
        {
            if (Seats[38] <= 1)
            {
                SeatCounter(38, sender);
            }
        }

        private void Seat40_Click(object sender, EventArgs e)
        {
            if (Seats[39] <= 1)
            {
                SeatCounter(39, sender);
            }
        }
        #endregion

        private void OrderButton_Click(object sender, EventArgs e)
        {
            var MySeats = new List<int>(); /// For loop that creates a list of ordered seats for this specific order
            for (int x = 0; x < Seats.Length; x++)
            {
                if (Seats[x] == 1)
                {
                    MySeats.Add(x + 1);
                }
            }


            for (int x = 0; x < Seats.Length; x++) /// For loop that turns selected seats into reserved seats in the array that need to be written to the json in the payment screen
            {
                if (Seats[x] == 1)
                {
                    Seats[x] = 2;
                }
            }
        }
        #endregion

        #region payment scherm
        private void Pay_Button_Click(object sender, EventArgs e)
        {
            
        }
        private void Payment_BackToSeats_LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PatatTabControl.SelectedTab = SeatsPage;
        }
        #endregion

        #region Admin scherm
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
        #endregion

 
    }
}
