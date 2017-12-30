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
    public partial class Pisteikkuna : Form
    {
        public Pisteikkuna()
        {
            InitializeComponent();
            // asetetaan formille assburger- ikoni
            this.Icon = Niilo22Muistipeli.Properties.Resources.assburger;
        }
        public void AsetaPisteet(List<Pisterivi> pisteet)
        {
            dgvPisteet.DataSource = pisteet;
        }
    }
}
