using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Proejct_B
{
    public partial class Form1 : Form
    {
        //Alle variabelen
        int stoelen = 0;
        int tickets_Volwassenen = 0;
        int tickets_Kinderen = 0;
        int tickets_Gehandicapten = 0;
        double volwassen_prijs = 13.95;
        double kinderen_prijs = 8.95;
        double gehandicapten_prijs = 5.95;
        double totaal_prijs = 0.0;
        int volgende_Tijd = 0;
        string[] film_Tijden = new string[5]
        {
            "01-05-20 12:30","01-05-20 14:30", "02-05-20 13:00","02-05-20 16:00","03-05-20 12:00"
        };
        public Form1()
        {
            InitializeComponent();
        }


        //Seats scherm


        //Alle vooruit en terugknoppen
        private void To_Paymentscreen_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = BetaalPage;
            Hoeveelheid_tickets_label.Text = "U heeft " + stoelen + " stoelen geselecteerd.";
        }

        private void Back_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = StoelenPage;
        }

        private void Back_To_BestelPage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = BestelPage;
        }


        //Alle Seats knoppen
        private void button1_Click(object sender, EventArgs e)
        {
            Seat1.BackColor = Color.Blue;
            stoelen += 1;
            Aantal_stoelen.Text = "Aantal stoelen:\n\t\t" + stoelen.ToString();
            Seat1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Seat2.BackColor = Color.Blue;
            stoelen += 1;
            Aantal_stoelen.Text = "Aantal stoelen:\n\t\t" + stoelen.ToString();
            Seat2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Seat3.BackColor = Color.Blue;
            stoelen += 1;
            Aantal_stoelen.Text = "Aantal stoelen:\n\t\t" + stoelen.ToString();
            Seat3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Seat4.BackColor = Color.Blue;
            stoelen += 1;
            Aantal_stoelen.Text = "Aantal stoelen:\n\t\t" + stoelen.ToString();
            Seat4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Seat5.BackColor = Color.Blue;
            stoelen += 1;
            Aantal_stoelen.Text = "Aantal stoelen:\n\t\t" + stoelen.ToString();
            Seat5.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Seat6.BackColor = Color.Blue;
            stoelen += 1;
            Aantal_stoelen.Text = "Aantal stoelen:\n\t\t" + stoelen.ToString();
            Seat6.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Seat7.BackColor = Color.Blue;
            stoelen += 1;
            Aantal_stoelen.Text = "Aantal stoelen:\n\t\t" + stoelen.ToString();
            Seat7.Enabled = false;
        }

        //Alle Legenda knoppen
        private void Non_Click_Button_Click(object sender, EventArgs e)
        {
            Non_Click_Button.Enabled = false;
        }

        private void Non_Click_Button2_Click(object sender, EventArgs e)
        {
            Non_Click_Button2.Enabled = false;
        }
        private void Non_Click_Button3_Click(object sender, EventArgs e)
        {
            Non_Click_Button3.Enabled = false;
        }


        //Bestel scherm


        private void label10_Click(object sender, EventArgs e)
        {

        }
        //Min en plus knoppen
        //Min knoppen
        private void Minbutton_volwassenen_Click(object sender, EventArgs e)
        {
            tickets_Volwassenen -= 1;
            Aantal_volwassenen.Text = tickets_Volwassenen.ToString();
            totaal_prijs -= volwassen_prijs;
            Prijs_Totaal.Text = totaal_prijs.ToString();
        }

        private void MinButton_Kinderen_Click(object sender, EventArgs e)
        {
            tickets_Kinderen -= 1;
            Aantal_kinderen.Text = tickets_Kinderen.ToString();
            totaal_prijs -= kinderen_prijs;
            Prijs_Totaal.Text = totaal_prijs.ToString();
        }

        private void MinButton_Gehandicapten_Click(object sender, EventArgs e)
        {
            tickets_Gehandicapten -= 1;
            Aantal_gehandicapten.Text = tickets_Gehandicapten.ToString();
            totaal_prijs -= gehandicapten_prijs;
            Prijs_Totaal.Text = totaal_prijs.ToString();
        }

        //Plus knoppen
        private void PlusButton_Volwassenen_Click(object sender, EventArgs e)
        {
            tickets_Volwassenen += 1;
            Aantal_volwassenen.Text = tickets_Volwassenen.ToString();
            totaal_prijs += volwassen_prijs;
            Prijs_Totaal.Text = totaal_prijs.ToString();
        }
        private void PlusButton_Kinderen_Click(object sender, EventArgs e)
        {
            tickets_Kinderen += 1;
            Aantal_kinderen.Text = tickets_Kinderen.ToString();
            totaal_prijs += kinderen_prijs;
            Prijs_Totaal.Text = totaal_prijs.ToString();
        }

        private void PlusButton_Gehandicapten_Click(object sender, EventArgs e)
        {
            tickets_Gehandicapten += 1;
            Aantal_gehandicapten.Text = tickets_Gehandicapten.ToString();
            totaal_prijs += gehandicapten_prijs;
            Prijs_Totaal.Text = totaal_prijs.ToString();
        }

        //Bestel scherm vooruit en terugknoppen
        private void Next__Time_Click(object sender, EventArgs e)
        {
            volgende_Tijd += 1;
            Time_of_movie_label.Text = film_Tijden[volgende_Tijd];
        }

        private void Back_Time_Click(object sender, EventArgs e)
        {
            volgende_Tijd -= 1;
            Time_of_movie_label.Text = film_Tijden[volgende_Tijd];
        }
        private void To_Seats_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = StoelenPage;
        }
        private void Back_to_menu1_Button_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Menu;
        }



        //PayingPage scherm


        //Slaat de gegevens die je invult op in een map
        //https://www.youtube.com/watch?v=sRBAv4-G0iw
        private void Pay_Button_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\GegevensGebruikers\\" + Firstname_Textbox.Text + " " + Lastname_Textbook.Text + ".txt");
            sw.WriteLine(Firstname_Label.Text + " " + Firstname_Textbox.Text);
            sw.WriteLine(Lastname_Label.Text + " " + Lastname_Textbook.Text);
            sw.WriteLine(Emailadres_Label.Text + " " + Emailadres_Textbox.Text);
            sw.WriteLine(Bank_Label.Text + " " + Bank_Combobox.Text);
            sw.Close();
        }

        //Menu scherm


        //Knop om naar login scherm te gaan
        private void Login_Button_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Login;
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = BestelPage;
        }

        //Login scherm


        //Knop om naar menu te gaan
        private void Back_to_menu_Button_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Menu;
        }

        private void Log_in_Button_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\tijme\source\repos\Proejct B\Proejct B\Database1.mdf; Integrated Security = True");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Login2 where UserName='" + Username_TextBox.Text + "' and Password='" + Password_TextBox.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();

                LoggedIn ss = new LoggedIn();
                ss.Show();
            }
            else
            {
                MessageBox.Show("Please check your username and password");
            }

        }
    }
}
