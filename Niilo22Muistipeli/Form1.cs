﻿using System;
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
        int valittuKortti = -1;
        int korttienMaara = 16;
        PictureBox[] pictureboksit;
        bool[] nakyvissa;
        Bitmap[] kuvat;
        Bitmap[] niilot = new Bitmap[]
        {
            Niilo22Muistipeli.Properties.Resources.Niilo1_pieni,
            Niilo22Muistipeli.Properties.Resources.Niilo2_pieni,
            Niilo22Muistipeli.Properties.Resources.Niilo3_pieni,
            Niilo22Muistipeli.Properties.Resources.Niilo4_pieni,
        };

        private void naytaKysymysmerkki(int kortinnumero, PictureBox pbox)
        {
            nakyvissa[kortinnumero] = false;
            pbox.Image = Niilo22Muistipeli.Properties.Resources.NiilonKysymysmerkki;
        }
        private void alustaPelilauta()
        {
            // Alustetaan korttien näkyvyys ja kuvat- taulukot uusiksi
            nakyvissa = new bool[korttienMaara];
            kuvat = new Bitmap[korttienMaara];
           
            // Näytetään valittu määrä kortteja
            panel12.Visible = false;
            panel16.Visible = false;
            if (korttienMaara >= 12)
            { panel12.Visible = true; }
            if (korttienMaara >= 16)
            {
                panel16.Visible = true;
            }

            // Käännetään näkyvissä olevat kortit nurinpäin
            int i;
            for (i = 0; i <korttienMaara; i++)
            {
                naytaKysymysmerkki(i, pictureboksit[i]);
            }

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

            // Järjestetään kuvat- taulukon kuvat satunnaiseen järjestykseen
            Random r = new Random();
            kuvat = kuvat.OrderBy(x => r.Next()).ToArray();
        }

        public Form1()
        {
            InitializeComponent();
            // Säilytetään pictureboxeja aputaulukossa jotta voidaan käsitellä niitä helposti loopeissa.
            pictureboksit = new PictureBox[]
            {
                pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16
            };
            alustaPelilauta();
        }

        private void pictureBox_Click(object sender, EventArgs e, int boksinNumero)
        {
            // Kun ollaan klikkaamassa vuoron ensimmäistä korttia,
            // -tarkistetaan oliko kortti jo näkyvissä
            //    - jos oli, niin tulee virheilmoitus "kortti on jo arvattu"  ja ei tehdä mitään.
            //    - jos ei ollut, kortti käännetään näkyviin ja tallennetaan että se on näkyvissä ja tallennetaan valitun kortin numero.
            // Kun ollaan klikkaamassa vuoron toista korttia,
            // -tarkistetaan oliko kortti jo näkyvissä
            //    - jos oli, niin tulee virheilmoitus "kortti on jo arvattu"  ja ei tehdä mitään.
            //    - jos ei ollut, kortti käännetään näkyviin ja tallennetaan että se on näkyvissä
            //      -  vertaillaan ensimmäiseen käännettyyn korttiin, 
            //          - jos kortti oli sama, pelaajan vuoro jatkuu (mahdollisesti annetaan visuaalinen vinkki vuoron jatkumisesta/ lisätään pisteitä)
            //            - lisäksi jos kaikki kortit on käännetty, peli päättyy
            //          - jos kortti oli eri kuin ensimmäinen, pelaajan vuoro päättyy ja molemmat kortit käännetään takaisin nurinpäin.
            // 
            PictureBox pbox = (PictureBox)sender;
            if (nakyvissa[boksinNumero])
            { pbox.Image = Niilo22Muistipeli.Properties.Resources.NiilonKysymysmerkki; }
            else
            { pbox.Image = kuvat[boksinNumero]; }
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void laudanKoko8_Click(object sender, EventArgs e)
        {
            korttienMaara = 8;
            alustaPelilauta();
        }

        private void laudanKoko12_Click(object sender, EventArgs e)
        {
            korttienMaara = 12;
            alustaPelilauta();
        }

        private void laudanKoko16_Click(object sender, EventArgs e)
        {
            korttienMaara = 16;
            alustaPelilauta();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 8);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 9);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 10);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 11);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 12);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 13);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 14);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            pictureBox_Click(sender, e, 15);
        }
    }
}
