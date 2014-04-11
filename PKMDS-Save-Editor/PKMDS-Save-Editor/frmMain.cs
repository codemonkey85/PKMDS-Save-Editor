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
        PKMDS.Save sav = new PKMDS.Save();
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
                    this.Text = title + " - " + sav.TrainerName + " (" + sav.TID.ToString("00000") + ")";
                    try
                    {
                        for (int box = 0; box < 24; box++)
                        {
                            cbBoxes.Items.Add(sav.GetBoxName(box));
                        }
                        cbBoxes.SelectedIndex = /*sav.Data[*/0/*]*/;
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
                    PKMDS.Pokemon pkm = new PKMDS.Pokemon();
                    lstPokemon.Clear();
                    for (int slot = 0; slot < 30; slot++)
                    {
                        PKMDS.GetPKMData(ref pkm, sav, cbBoxes.SelectedIndex, slot);
                        lstPokemon.Items.Add(pkm.SpeciesName);
                    }
                }
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            PKMDS.OpenDB(Properties.Settings.Default.veekunpokedex);
            PKMDS.OpenImgDB(Properties.Settings.Default.imagedb);
            //Pokemon pkm = new Pokemon();
            //MessageBox.Show(System.Runtime.InteropServices.Marshal.SizeOf(pkm).ToString(""));
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
                    PKMDS.Pokemon pkm = new PKMDS.Pokemon();
                    PKMDS.GetPKMData(ref pkm, sav, cbBoxes.SelectedIndex, lstPokemon.SelectedItems[0].Index);
                    pbSprite.Image = pkm.Sprite;
                }
            }
        }
        private void savesavToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //PKMDS.Pokemon pkm = new PKMDS.Pokemon();
            //PKMDS.GetPKMData(ref pkm, sav, 0, 0);
            //pkm.SpeciesID = 129;
            //PKMDS.SetPKMData(pkm, sav, 0, 0);
            //MessageBox.Show(sav.TrainerName);
            sav.TrainerName = "Stark";
            //MessageBox.Show(sav.TrainerName);
            if (fileSave.ShowDialog() != DialogResult.Cancel)
            {
                if (fileSave.FileName != "")
                {
                    //sav.WriteToFile(fileSave.FileName);
                    PKMDS.WriteSaveFile(sav, fileSave.FileName);
                }
            }
        }
    }
}