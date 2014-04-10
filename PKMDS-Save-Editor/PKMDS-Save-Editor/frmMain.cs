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
        string title = "";
        private string savefile = "";
        Save sav = new Save();
        public frmMain()
        {
            InitializeComponent();
            title = this.Text;
        }
        private void loadSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileOpen.ShowDialog() != DialogResult.Cancel)
            {
                if (fileOpen.FileName != "")
                {
                    cbBoxes.SelectedIndex = -1;
                    cbBoxes.Items.Clear();
                    savefile = fileOpen.FileName;
                    PKMDS.GetSAVData(ref sav, savefile);
                    this.Text = title + " - " + PKMDS.GetTrainerName_FromSav(sav) + " (" + PKMDS.GetTrainerSID_FromSav(sav).ToString("00000") + ")";
                    try
                    {
                        for (int box = 0; box < 24; box++)
                        {
                            cbBoxes.Items.Add(PKMDS.GetBoxName(sav, box));
                        }
                        cbBoxes.SelectedIndex = sav.Data[0];
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private unsafe void cbBoxes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBoxes.Items.Count > 0)
            {
                if (cbBoxes.SelectedIndex >= 0)
                {
                    Pokemon pkm = new Pokemon();
                    lstPokemon.Clear();
                    for (int slot = 0; slot < 30; slot++)
                    {
                        PKMDS.GetPKMData(ref pkm, sav, cbBoxes.SelectedIndex, slot);
                        lstPokemon.Items.Add(PKMDS.GetPKMName_FromObj(pkm))/*Sav(sav, 0, 0))*/;
                    }
                }
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            PKMDS.OpenDB(Properties.Settings.Default.veekunpokedex);
            PKMDS.OpenImgDB(Properties.Settings.Default.imagedb);
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            PKMDS.CloseDB();
            PKMDS.CloseImgDB();
        }

        private void lstPokemon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBoxes.SelectedIndex != -1)
            {
                if (lstPokemon.SelectedItems.Count > 0)
                {
                    Pokemon pkm = new Pokemon();
                    PKMDS.GetPKMData(ref pkm, sav, cbBoxes.SelectedIndex, lstPokemon.SelectedItems[0].Index);
                    pbSprite.Image = PKMDS.GetPKMSprite(pkm);
                }
            }
        }
    }
}
