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
        private List<PictureBox> partyPics = new List<PictureBox>();
        private List<PictureBox> boxPics = new List<PictureBox>();
        private List<PictureBox> boxGridPics = new List<PictureBox>();
        private List<Label> boxNameLabels = new List<Label>();
        string title = "";
        private string savefile = "";
        PKMDS.Save sav = new PKMDS.Save();
        frmPKMViewer pkmviewer = new frmPKMViewer();
        bool uiset = false;
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
                    uiset = false;
                    savefile = fileOpen.FileName;
                    sav = PKMDS.ReadSaveFile(savefile);
                    SetSaveFile();
                    uiset = true;
                }
            }
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            PKMDS.CloseDB();
            PKMDS.CloseImgDB();
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
        private PKMDS.Pokemon ViewPokemon(PKMDS.Pokemon pkm)
        {
            pkmviewer.SetPokemon(pkm);
            pkmviewer.ShowDialog();
            return pkmviewer.SharedPokemon;
        }
        private PKMDS.PartyPokemon ViewPokemon(PKMDS.PartyPokemon ppkm)
        {
            pkmviewer.SetPokemon(ppkm.PokemonData.Clone());
            pkmviewer.ShowDialog();
            PKMDS.PartyPokemon newppkm = new PKMDS.PartyPokemon();
            newppkm.PokemonData = pkmviewer.SharedPokemon.Clone();
            return newppkm;
        }
        private void SetSaveFile()
        {
            this.Text = title + " - " + sav.TrainerName + " (" + sav.TID.ToString("00000") + ")";
            btnPreviousBox.Enabled = (sav.CurrentBox != 0);
            btnNextBox.Enabled = (sav.CurrentBox != 23);
            txtBoxName.Enabled = true;
            splitMain.Panel2.Enabled = true;
            UpdateParty();
            UpdateBox();
            UpdateBoxWallpaper();
            UpdateBoxName();
            UpdateBoxNameLabels();
            UpdateBoxGrids();
        }
        private void UpdateParty()
        {

            for (int partySlot = 0; partySlot < 6; partySlot++)
            {
                PKMDS.Pokemon pokemon = new PKMDS.Pokemon();
                pokemon = sav.GetPartyPokemon(partySlot).PokemonData;
                if ((pokemon.SpeciesID != 0) && (partySlot < sav.PartySize))
                {
                    partyPics[partySlot].Image = pokemon.Icon;
                }
                else
                {
                    partyPics[partySlot].Image = null;
                }
            }
        }
        private void UpdateBox()
        {
            PKMDS.Pokemon pokemon = new PKMDS.Pokemon();
            for (int boxSlot = 0; boxSlot < 30; boxSlot++)
            {
                pokemon = sav.GetStoredPokemon(sav.CurrentBox, boxSlot);
                if (pokemon.SpeciesID != 0)
                {
                    boxPics[boxSlot].Image = pokemon.Icon;
                }
                else
                {
                    boxPics[boxSlot].Image = null;
                }
            }
        }
        private void UpdateBoxWallpaper()
        {
            pnlBox.BackgroundImage = sav.GetBoxWallpaper(sav.CurrentBox);
        }
        private void UpdateBoxName()
        {
            txtBoxName.Text = sav.GetBoxName(sav.CurrentBox);
        }
        private void UpdateBoxNameLabel(int box)
        {
            boxNameLabels[box].Text = sav.GetBoxName(box);
        }
        private void UpdateBoxNameLabels()
        {
            for (int box = 0; box < 24; box++)
            {
                UpdateBoxNameLabel(box);
            }
        }
        private void UpdateBoxGrid(int box)
        {
            boxGridPics[box].Image = sav.GetBoxGrid(box);
        }
        private void UpdateBoxGrids()
        {
            for (int box = 0; box < 24; box++)
            {
                UpdateBoxGrid(box);
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            partyPics.Add(pbPartySlot01);
            partyPics.Add(pbPartySlot02);
            partyPics.Add(pbPartySlot03);
            partyPics.Add(pbPartySlot04);
            partyPics.Add(pbPartySlot05);
            partyPics.Add(pbPartySlot06);
            boxPics.Add(pbBoxSlot01);
            boxPics.Add(pbBoxSlot02);
            boxPics.Add(pbBoxSlot03);
            boxPics.Add(pbBoxSlot04);
            boxPics.Add(pbBoxSlot05);
            boxPics.Add(pbBoxSlot06);
            boxPics.Add(pbBoxSlot07);
            boxPics.Add(pbBoxSlot08);
            boxPics.Add(pbBoxSlot09);
            boxPics.Add(pbBoxSlot10);
            boxPics.Add(pbBoxSlot11);
            boxPics.Add(pbBoxSlot12);
            boxPics.Add(pbBoxSlot13);
            boxPics.Add(pbBoxSlot14);
            boxPics.Add(pbBoxSlot15);
            boxPics.Add(pbBoxSlot16);
            boxPics.Add(pbBoxSlot17);
            boxPics.Add(pbBoxSlot18);
            boxPics.Add(pbBoxSlot19);
            boxPics.Add(pbBoxSlot20);
            boxPics.Add(pbBoxSlot21);
            boxPics.Add(pbBoxSlot22);
            boxPics.Add(pbBoxSlot23);
            boxPics.Add(pbBoxSlot24);
            boxPics.Add(pbBoxSlot25);
            boxPics.Add(pbBoxSlot26);
            boxPics.Add(pbBoxSlot27);
            boxPics.Add(pbBoxSlot28);
            boxPics.Add(pbBoxSlot29);
            boxPics.Add(pbBoxSlot30);
            boxGridPics.Add(pbBoxGrid01);
            boxGridPics.Add(pbBoxGrid02);
            boxGridPics.Add(pbBoxGrid03);
            boxGridPics.Add(pbBoxGrid04);
            boxGridPics.Add(pbBoxGrid05);
            boxGridPics.Add(pbBoxGrid06);
            boxGridPics.Add(pbBoxGrid07);
            boxGridPics.Add(pbBoxGrid08);
            boxGridPics.Add(pbBoxGrid09);
            boxGridPics.Add(pbBoxGrid10);
            boxGridPics.Add(pbBoxGrid11);
            boxGridPics.Add(pbBoxGrid12);
            boxGridPics.Add(pbBoxGrid13);
            boxGridPics.Add(pbBoxGrid14);
            boxGridPics.Add(pbBoxGrid15);
            boxGridPics.Add(pbBoxGrid16);
            boxGridPics.Add(pbBoxGrid17);
            boxGridPics.Add(pbBoxGrid18);
            boxGridPics.Add(pbBoxGrid19);
            boxGridPics.Add(pbBoxGrid20);
            boxGridPics.Add(pbBoxGrid21);
            boxGridPics.Add(pbBoxGrid22);
            boxGridPics.Add(pbBoxGrid23);
            boxGridPics.Add(pbBoxGrid24);
            boxNameLabels.Add(lblBoxGrid01);
            boxNameLabels.Add(lblBoxGrid02);
            boxNameLabels.Add(lblBoxGrid03);
            boxNameLabels.Add(lblBoxGrid04);
            boxNameLabels.Add(lblBoxGrid05);
            boxNameLabels.Add(lblBoxGrid06);
            boxNameLabels.Add(lblBoxGrid07);
            boxNameLabels.Add(lblBoxGrid08);
            boxNameLabels.Add(lblBoxGrid09);
            boxNameLabels.Add(lblBoxGrid10);
            boxNameLabels.Add(lblBoxGrid11);
            boxNameLabels.Add(lblBoxGrid12);
            boxNameLabels.Add(lblBoxGrid13);
            boxNameLabels.Add(lblBoxGrid14);
            boxNameLabels.Add(lblBoxGrid15);
            boxNameLabels.Add(lblBoxGrid16);
            boxNameLabels.Add(lblBoxGrid17);
            boxNameLabels.Add(lblBoxGrid18);
            boxNameLabels.Add(lblBoxGrid19);
            boxNameLabels.Add(lblBoxGrid20);
            boxNameLabels.Add(lblBoxGrid21);
            boxNameLabels.Add(lblBoxGrid22);
            boxNameLabels.Add(lblBoxGrid23);
            boxNameLabels.Add(lblBoxGrid24);
        }
        private void txtBoxName_TextChanged(object sender, EventArgs e)
        {
            if (uiset)
            {
                sav.SetBoxName(sav.CurrentBox, txtBoxName.Text);
                UpdateBoxNameLabel(sav.CurrentBox);
            }
        }
        private void btnPreviousBox_Click(object sender, EventArgs e)
        {
            sav.CurrentBox--;
            UpdateBox();
            UpdateBoxWallpaper();
            UpdateBoxName();
            btnPreviousBox.Enabled = (sav.CurrentBox != 0);
            btnNextBox.Enabled = (sav.CurrentBox != 23);
        }
        private void btnNextBox_Click(object sender, EventArgs e)
        {
            sav.CurrentBox++;
            UpdateBox();
            UpdateBoxWallpaper();
            UpdateBoxName();
            btnPreviousBox.Enabled = (sav.CurrentBox != 0);
            btnNextBox.Enabled = (sav.CurrentBox != 23);
        }
        private void pbSlot_DoubleClick(object sender, EventArgs e)
        {
            int slot = 0;
            PictureBox pb = (PictureBox)(sender);
            if (pb.Name.Contains("Party"))
            {
                int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                slot--;
                sav.SetPartyPokemon(ViewPokemon(sav.GetPartyPokemon(slot)), slot);
                UpdateParty();
            }
            if (pb.Name.Contains("Box"))
            {
                int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                slot--;
                sav.SetStoredPokemon(ViewPokemon(sav.GetStoredPokemon(sav.CurrentBox, slot)), sav.CurrentBox, slot);
                UpdateBox();
                UpdateBoxGrid(sav.CurrentBox);
            }
        }
        private void pbBoxGrid_Click(object sender, EventArgs e)
        {
            int box = 0;
            PictureBox pb = (PictureBox)(sender);
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out box);
            box--;
            sav.CurrentBox = box;
            UpdateBox();
            UpdateBoxWallpaper();
            UpdateBoxName();
            btnPreviousBox.Enabled = (sav.CurrentBox != 0);
            btnNextBox.Enabled = (sav.CurrentBox != 23);
        }
        private void lblBoxGrid_Click(object sender, EventArgs e)
        {
            int box = 0;
            Label pb = (Label)(sender);
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out box);
            box--;
            sav.CurrentBox = box;
            UpdateBox();
            UpdateBoxWallpaper();
            UpdateBoxName();
            btnPreviousBox.Enabled = (sav.CurrentBox != 0);
            btnNextBox.Enabled = (sav.CurrentBox != 23);
        }
    }
}
