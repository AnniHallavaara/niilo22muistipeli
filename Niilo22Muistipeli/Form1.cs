using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Niilo22Muistipeli
{
    public partial class Form1 : Form
    {
        bool[] nakyvissa = new bool[8];
        Bitmap[] kuvat = new Bitmap[8];
        Bitmap[] niilot = new Bitmap[]
        {
            Niilo22Muistipeli.Properties.Resources.Niilo1_pieni,
            Niilo22Muistipeli.Properties.Resources.Niilo2_pieni,
            Niilo22Muistipeli.Properties.Resources.Niilo3_pieni,
            Niilo22Muistipeli.Properties.Resources.Niilo4_pieni,
        };

        public Form1()
        {
            InitializeComponent();

            // Täytetään kuvat- taulukkoon tarvittava määrä kuvapareja
            for (int kuvatI = 0, niiloI = 0; kuvatI < kuvat.Length; kuvatI++)
            {
                kuvat[kuvatI] = niilot[niiloI];
                kuvatI++;
                kuvat[kuvatI] = niilot[niiloI];
                if (niiloI == niilot.Length - 1)  // Ollaanko viimeisessä?
                {
                    // Seuraavaksi laitetaan taas ensimmäistä niiloa
                    niiloI = 0;
                }
                else
                {
                    niiloI++;
                }
            }



            

        }

        private void pictureBox_Click(object sender, EventArgs e, int boksinNumero)
        {
            PictureBox pbox = (PictureBox)sender;
            if (nakyvissa[boksinNumero])
                pbox.Image = Niilo22Muistipeli.Properties.Resources.NiilonKysymysmerkki;
            else
                pbox.Image = kuvat[boksinNumero];
            nakyvissa[boksinNumero] = !nakyvissa[boksinNumero];
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 2);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 3);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 4);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 5);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 6);
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 7);
        }

    }
}
