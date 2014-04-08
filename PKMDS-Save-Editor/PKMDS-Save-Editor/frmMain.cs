using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PKMDS_CS;

namespace PKMDS_Save_Editor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void loadSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lbTest.Items.Clear();
            // Test to make sure the dependencies are working correctly
            //PKMDS.OpenDB("F:\\Dropbox\\PKMDS Databases\\veekun-pokedex.sqlite");
            PKMDS.OpenDB("C:\\Users\\michaelbond\\Downloads\\PKMDS Databases\\veekun-pokedex.sqlite");
            for (int i = 1; i <= 649; i++)
            {
                lbTest.Items.Add(PKMDS.GetPKMName(i));
            }
            PKMDS.CloseDB();
        }
    }
}
