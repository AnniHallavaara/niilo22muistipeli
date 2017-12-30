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
    // tietue pelaajatietoja varten
    public struct Pelaaja
    {
        public TextBox nimiboksi;
        public Label pelaajaLabel;
        public int oikeinArvatut;
        public int vaarinArvatut;
    }
    // tietue pisteitä varten
    public struct Pisterivi
    {
        public int voitot;
        public int haviot;
        public string nimi;

        public string Nimi
        {
            get
            {
                return nimi;
            }
        }
        public int Voitot
        {
            get
            {
                return voitot;
            }
        }
        public int Haviot
        {
            get
            {
                return haviot;
            }
        }
        
    }

    public partial class Pelialusta : Form
    {
        bool onkoYksinpeli = false;

        Pelaaja pelaaja1, pelaaja2;

        Pelaaja? vuorossaOlevaPelaaja;

        int aiemminKlikatunKortinNumero = -1;

        int korttienMaara = 16;

        PictureBox[] pictureboksit;

        bool[] nakyvissa;

        public List<Pisterivi> pisteet = new List<Pisterivi>();

        Bitmap[] kuvat;

        Bitmap[] niilot = new Bitmap[]
        {
            Niilo22Muistipeli.Properties.Resources.Niilo1_pieni,
            Niilo22Muistipeli.Properties.Resources.Niilo2_pieni,
            Niilo22Muistipeli.Properties.Resources.Niilo3_pieni,
            Niilo22Muistipeli.Properties.Resources.Niilo4_pieni,
        };

        private void NaytaKysymysmerkki(int kortinnumero, PictureBox pbox)
        {
            nakyvissa[kortinnumero] = false;
            pbox.Image = Niilo22Muistipeli.Properties.Resources.KysymysmerkkiEmoji;
        }
        private void VaihdaVuorossaOlevaPelaaja(bool pakotaPelaaja1)
        {
            // poistetaan edellisen vuorossa olleen pelaajan visualisointi
            // null- tarkistus tehdään siksi, että alussa ei ole vielä vuorossa olevaa pelaajaa
            if (vuorossaOlevaPelaaja != null)
            {
                vuorossaOlevaPelaaja.Value.pelaajaLabel.ForeColor = Color.Black;
            }
            // vaihdetaan vuorossa oleva pelaaja
            if (pakotaPelaaja1 || pelaaja2.Equals(vuorossaOlevaPelaaja.Value))
            {
                vuorossaOlevaPelaaja = pelaaja1;
            }
            else
            {
                vuorossaOlevaPelaaja = pelaaja2;
            }
            // visualisoidaan vuorossa oleva pelaaja
            vuorossaOlevaPelaaja.Value.pelaajaLabel.ForeColor = Color.Red;
            
        }
        private void AlustaPelilauta()
        {
            VaihdaVuorossaOlevaPelaaja(true);

            // nollataan pelaajien oikeat ja väärät arvaukset
            pelaaja1.oikeinArvatut = 0;
            pelaaja1.vaarinArvatut = 0;
            pelaaja2.oikeinArvatut = 0;
            pelaaja2.vaarinArvatut = 0;

            // Alustetaan korttien näkyvyys ja kuvat- taulukot uusiksi
            nakyvissa = new bool[korttienMaara];
            kuvat = new Bitmap[korttienMaara];
            // unohdetaan aiemmin valittu kortti
            aiemminKlikatunKortinNumero = -1;

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
            for (i = 0; i < korttienMaara; i++)
            {
                NaytaKysymysmerkki(i, pictureboksit[i]);
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

        public Pelialusta()
        {
            InitializeComponent();
            // asetetaan formille assburger- ikoni
            this.Icon = Niilo22Muistipeli.Properties.Resources.assburger;
            // Säilytetään pictureboxeja aputaulukossa jotta voidaan käsitellä niitä helposti loopeissa.
            pictureboksit = new PictureBox[]
            {
                pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5,
                pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10,
                pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15, pictureBox16
            };
            
            pelaaja1.pelaajaLabel = lblPelaaja1;
            pelaaja1.nimiboksi = tbPelaaja1;
            pelaaja2.pelaajaLabel = lblPelaaja2;
            pelaaja2.nimiboksi = tbPelaaja2;
            AlustaPelilauta();
        }

        private bool EnsimmaistaKaantamassa()
        {
            if (aiemminKlikatunKortinNumero == -1)
                return true;
            else
                return false;
        }

        private bool LoppuikoPeli()
        {
            if (nakyvissa.Contains(false))
                return false;
            else
                return true;
        }

        private void PictureBox_Click(object sender, EventArgs e, int juuriKlikatunKortinNumero)
        {


            // -tarkistetaan oliko kortti jo näkyvissä

            if (nakyvissa[juuriKlikatunKortinNumero])
            {
                //    - jos oli, ei tehdä mitään.
                return;
            }
            // kortti käännetään näkyviin
            PictureBox juuriKlikattuPictureBox = (PictureBox)sender;
            juuriKlikattuPictureBox.Image = kuvat[juuriKlikatunKortinNumero];
            // ja tallennetaan että se on näkyvissä
            nakyvissa[juuriKlikatunKortinNumero] = true;


            if (EnsimmaistaKaantamassa())
            {
                // Kun ollaan klikkaamassa vuoron ensimmäistä korttia
                // tallennetaan valitun kortin numero
                aiemminKlikatunKortinNumero = juuriKlikatunKortinNumero;

            }
            else
            {

                //      -  vertaillaan juuri käännetyn kortin imagea aiemmin käännetyn kortin imageen
                PictureBox aiemminKlikattuPictureBox = pictureboksit[aiemminKlikatunKortinNumero];
                if (juuriKlikattuPictureBox.Image == aiemminKlikattuPictureBox.Image)
                {

                    if (LoppuikoPeli())
                    {
                        // Kun peli loppuu, soitetaan äänimerkki
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(Niilo22Muistipeli.Properties.Resources.Niilo22PelinLoppu);
                        player.Play();

                        // otetaan voittajan ja häviäjän nimet talteen
                        string voittajanNimi = vuorossaOlevaPelaaja.Value.nimiboksi.Text;
                        string haviajanNimi;
                        // häviäjä on ei vuorossa oleva pelaaja
                        if (pelaaja1.Equals(vuorossaOlevaPelaaja.Value))
                        {
                            haviajanNimi = pelaaja2.nimiboksi.Text;
                        }
                        else
                        {
                            haviajanNimi = pelaaja1.nimiboksi.Text;
                        }

                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBox.Show("Peli loppui! Voittaja on: " + voittajanNimi, "Game over!", buttons);
                        // TODO: korosta voittajan nimi

                        // päivitetään pisteet
                        PaivitaPisteLista(voittajanNimi, haviajanNimi);
                        // näytetään pisteet
                        NaytaPisteikkuna();
                    }
                    else
                    {
                        // kun saadaan pari, soitetaan äänimerkki
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(Niilo22Muistipeli.Properties.Resources.Niilo22MulstipeliON);
                        player.Play();
                    }

                }
                else
                {
                    // - jos kortti oli eri kuin ensimmäinen
                    // - ilmoitetaan että valinta meni väärin ja soitetaan äänimerkki
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Niilo22Muistipeli.Properties.Resources.Niilo22EipaOllu);
                    player.Play();
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show("Väärin meni!", "Hups!", buttons);
                    //   molemmat kortit käännetään takaisin nurinpäin.
                    NaytaKysymysmerkki(aiemminKlikatunKortinNumero, aiemminKlikattuPictureBox);
                    NaytaKysymysmerkki(juuriKlikatunKortinNumero, juuriKlikattuPictureBox);
                    // pelaajan vuoro päättyy
                    VaihdaVuorossaOlevaPelaaja(onkoYksinpeli);
                }
                // unohdetaan aiemmin klikattu kortti
                aiemminKlikatunKortinNumero = -1;

            }
        }

        private void PaivitaPisteLista(string voittajanNimi, string haviajanNimi)
        {
            bool voittajaLoytyi = false;
            bool haviajaLoytyi = false;
            int i; 
            for (i = 0; i < pisteet.Count; i++)
            {
                Pisterivi rivi = pisteet[i];
                if (rivi.nimi == voittajanNimi)
                {
                    // lisätään voitto
                    rivi.voitot = rivi.voitot + 1;
                    pisteet[i] = rivi;
                    voittajaLoytyi = true;
                }
                if (rivi.nimi == haviajanNimi && !onkoYksinpeli)
                {
                    // lisätään häviö paitsi yksinpelissä
                    rivi.haviot = rivi.haviot + 1;
                    pisteet[i] = rivi;
                    haviajaLoytyi = true;
                }
            }
            if (voittajaLoytyi == false)
            {
                // jos voittajaa ei ole löytynyt, se lisätään
                Pisterivi voittajarivi;
                voittajarivi.nimi = voittajanNimi;
                voittajarivi.voitot = 1;
                voittajarivi.haviot = 0;
                pisteet.Add(voittajarivi);
            }
            if (haviajaLoytyi == false && !onkoYksinpeli)
            {
                // jos häviäjää ei ole löytynyt, se lisätään paitsi yksinpelissä
                Pisterivi haviajarivi;
                haviajarivi.nimi = haviajanNimi;
                haviajarivi.voitot = 0;
                haviajarivi.haviot = 1;
                pisteet.Add(haviajarivi);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 2);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 3);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 4);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 5);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 6);
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 7);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void laudanKoko8_Click(object sender, EventArgs e)
        {
            korttienMaara = 8;
            AlustaPelilauta();
        }

        private void laudanKoko12_Click(object sender, EventArgs e)
        {
            korttienMaara = 12;
            AlustaPelilauta();
        }

        private void laudanKoko16_Click(object sender, EventArgs e)
        {
            korttienMaara = 16;
            AlustaPelilauta();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 8);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 9);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 10);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 11);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 12);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 13);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 14);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            PictureBox_Click(sender, e, 15);
        }

        private void tbPelaaja1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTilastot_Click(object sender, EventArgs e)
        {
            NaytaPisteikkuna();
        }

        private void NaytaPisteikkuna()
        {

            Pisteikkuna akkuna = new Pisteikkuna();
            akkuna.AsetaPisteet(pisteet);
            akkuna.ShowDialog(this);
        }


        private void yksinpeliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onkoYksinpeli = true;
            AlustaPelilauta();
            lblPelaaja2.Visible = false;
            tbPelaaja2.Visible = false;
        }

        private void kaksinpeliToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onkoYksinpeli = false;
            AlustaPelilauta();
            lblPelaaja2.Visible = true;
            tbPelaaja2.Visible = true;
        }

        private void lblPelaaja2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
