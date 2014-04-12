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
        frmPKMViewer pkmviewer = new frmPKMViewer();
        public frmMain()
        {
            InitializeComponent();
            title = this.Text;
        }
        private void loadSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileOpen.FileName = "";
            if (fileOpen.ShowDialog() != DialogResult.Cancel)
            {
                if (fileOpen.FileName != "")
                {
                    cbBoxes.SelectedIndex = -1;
                    cbBoxes.Items.Clear();
                    lstPokemon.Clear();
                    lstParty.Clear();
                    savefile = fileOpen.FileName;
                    sav = PKMDS.ReadSaveFile(savefile);
                    this.Text = title + " - " + sav.TrainerName + " (" + sav.TID.ToString("00000") + ")";
                    try
                    {
                        PKMDS.PartyPokemon ppkm = new PKMDS.PartyPokemon();
                        for (int slot = 0; slot < 6; slot++)
                        {
                            ppkm = sav.GetPartyPokemon(slot);
                            System.Diagnostics.Debug.Write("");
                            lstParty.Items.Add(ppkm.PokemonData.SpeciesName);
                        }
                        for (int box = 0; box < 24; box++)
                        {
                            cbBoxes.Items.Add(sav.GetBoxName(box));
                        }
                        cbBoxes.SelectedIndex = sav.CurrentBox;
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
                    sav.CurrentBox = cbBoxes.SelectedIndex;
                    PKMDS.Pokemon pkm = new PKMDS.Pokemon();
                    lstPokemon.Clear();
                    for (int slot = 0; slot < 30; slot++)
                    {
                        pkm = sav.GetStoredPokemon(cbBoxes.SelectedIndex, slot);
                        lstPokemon.Items.Add(pkm.SpeciesName);
                    }
                    pbGender.Image = sav.GetBoxWallpaper(cbBoxes.SelectedIndex);
                }
            }
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
                    lstParty.SelectedItems.Clear();
                    PKMDS.Pokemon pkm = new PKMDS.Pokemon();
                    pkm = sav.GetStoredPokemon(cbBoxes.SelectedIndex, lstPokemon.SelectedItems[0].Index);
                    pbSprite.Image = pkm.Sprite;
                }
            }
        }
        private void savesavToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileSave.FileName = "";
            if (fileSave.ShowDialog() != DialogResult.Cancel)
            {
                if (fileSave.FileName != "")
                {
                    sav.WriteToFile(fileSave.FileName);
                }
            }
        }
        private void lstParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstParty.SelectedItems.Count > 0)
            {
                lstPokemon.SelectedItems.Clear();
                PKMDS.PartyPokemon ppkm = new PKMDS.PartyPokemon();
                ppkm = sav.GetPartyPokemon(lstParty.SelectedItems[0].Index);
                pbSprite.Image = ppkm.PokemonData.Sprite;
            }
        }
        private void lstPokemon_DoubleClick(object sender, EventArgs e)
        {
            if (lstPokemon.Items.Count > 0)
            {
                if (lstPokemon.SelectedItems.Count > 0)
                {
                    PKMDS.Pokemon pkm = new PKMDS.Pokemon();
                    pkm = sav.GetStoredPokemon(cbBoxes.SelectedIndex, lstPokemon.SelectedItems[0].Index);
                    sav.SetStoredPokemon(ViewPokemon(pkm), cbBoxes.SelectedIndex, lstPokemon.SelectedItems[0].Index);
                }
            }
        }
        private void lstParty_DoubleClick(object sender, EventArgs e)
        {
            if (lstParty.Items.Count > 0)
            {
                if (lstParty.SelectedItems.Count > 0)
                {
                    PKMDS.PartyPokemon ppkm = new PKMDS.PartyPokemon();
                    ppkm = sav.GetPartyPokemon(lstParty.SelectedItems[0].Index);
                    sav.SetPartyPokemon(ViewPokemon(ppkm), lstParty.SelectedItems[0].Index);
                }
            }
        }
        private PKMDS.Pokemon ViewPokemon(PKMDS.Pokemon pkm)
        {
            pkmviewer.SetPokemon(pkm);
            pkmviewer.ShowDialog();
            return pkmviewer.SharedPokemon;
        }
        private PKMDS.PartyPokemon ViewPokemon(PKMDS.PartyPokemon ppkm)
        {
            pkmviewer.SetPokemon(ppkm);
            pkmviewer.ShowDialog();
            return pkmviewer.SharedPartyPokemon;
        }
    }
}
