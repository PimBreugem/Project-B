using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeatSelection
{
    public partial class Form1 : Form
    {
        int[] Seats = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; /// Array from json reserved seats 40 integers long

        public Form1()
        {
            InitializeComponent();
            SetColor();
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
            int SeatIntAmount = 3; /// Total seats ordered in ticket screen
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

        private void Seat1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[0] <= 1)
            {
                SeatCounter(0, sender);
            }
        }

        private void Seat2_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[1] <= 1)
            {
                SeatCounter(1, sender);
            }
        }

        private void Seat3_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[2] <= 1)
            {
                SeatCounter(2, sender);
            }
        }

        private void Seat4_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[3] <= 1)
            {
                SeatCounter(3, sender);
            }
        }

        private void Seat5_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[4] <= 1)
            {
                SeatCounter(4, sender);
            }
        }

        private void Seat6_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[5] <= 1)
            {
                SeatCounter(5, sender);
            }
        }

        private void Seat7_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[6] <= 1)
            {
                SeatCounter(6, sender);
            }
        }

        private void Seat8_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[7] <= 1)
            {
                SeatCounter(7, sender);
            }
        }

        private void Seat9_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[8] <= 1)
            {
                SeatCounter(8, sender);
            }
        }

        private void Seat10_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[9] <= 1)
            {
                SeatCounter(9, sender);
            }
        }

        private void Seat11_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[10] <= 1)
            {
                SeatCounter(10, sender);
            }
        }

        private void Seat12_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[11] <= 1)
            {
                SeatCounter(11, sender);
            }
        }

        private void Seat13_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[12] <= 1)
            {
                SeatCounter(12, sender);
            }
        }

        private void Seat14_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[13] <= 1)
            {
                SeatCounter(13, sender);
            }
        }

        private void Seat15_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[14] <= 1)
            {
                SeatCounter(14, sender);
            }
        }

        private void Seat16_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[15] <= 1)
            {
                SeatCounter(15, sender);
            }
        }

        private void Seat17_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[16] <= 1)
            {
                SeatCounter(16, sender);
            }
        }

        private void Seat18_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[17] <= 1)
            {
                SeatCounter(17, sender);
            }
        }

        private void Seat19_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[18] <= 1)
            {
                SeatCounter(18, sender);
            }
        }

        private void Seat20_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[19] <= 1)
            {
                SeatCounter(19, sender);
            }
        }

        private void Seat21_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[20] <= 1)
            {
                SeatCounter(20, sender);
            }
        }

        private void Seat22_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[21] <= 1)
            {
                SeatCounter(21, sender);            
            }
        }

        private void Seat23_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[22] <= 1)
            {
                SeatCounter(22, sender);
            }
        }

        private void Seat24_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[23] <= 1)
            {
                SeatCounter(23, sender);
            }
        }

        private void Seat25_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[24] <= 1)
            {
                SeatCounter(24, sender);
            }
        }

        private void Seat26_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[25] <= 1)
            {
                SeatCounter(25, sender);
            }
        }

        private void Seat27_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[26] <= 1)
            {
                SeatCounter(26, sender);
            }
        }

        private void Seat28_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[27] <= 1)
            {
                SeatCounter(27, sender);
            }
        }

        private void Seat29_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[28] <= 1)
            {
                SeatCounter(28, sender);
            }
        }

        private void Seat30_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[29] <= 1)
            {
                SeatCounter(29, sender);
            }
        }

        private void Seat31_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[30] <= 1)
            {
                SeatCounter(30, sender);
            }
        }

        private void Seat32_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[31] <= 1)
            {
                SeatCounter(31, sender);
            }
        }

        private void Seat33_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[32] <= 1)
            {
                SeatCounter(32, sender);
            }
        }

        private void Seat34_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[33] <= 1)
            {
                SeatCounter(33, sender);
            }
        }

        private void Seat35_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[34] <= 1)
            {
                SeatCounter(34, sender);
            }
        }

        private void Seat36_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[35] <= 1)
            {
                SeatCounter(35, sender);
            }
        }

        private void Seat37_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[36] <= 1)
            {
                SeatCounter(36, sender);
            }
        }

        private void Seat38_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[37] <= 1)
            {
                SeatCounter(37, sender);

            }
        }

        private void Seat39_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[38] <= 1)
            {
                SeatCounter(38, sender);

            }
        }

        private void Seat40_MouseClick(object sender, MouseEventArgs e)
        {
            if (Seats[39] <= 1)
            {
                SeatCounter(39, sender);
            }
        }

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
    }
}
