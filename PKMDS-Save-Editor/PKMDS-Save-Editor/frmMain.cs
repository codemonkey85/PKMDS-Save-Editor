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
                    savefile = fileOpen.FileName;
                    sav = PKMDS.ReadSaveFile(savefile);
                    SetSaveFile();
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
            PKMDS.Pokemon pokemon = new PKMDS.Pokemon();
            this.Text = title + " - " + sav.TrainerName + " (" + sav.TID.ToString("00000") + ")";
            for (int partySlot = 0; partySlot < 6; partySlot++)
            {
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
            UpdateBox();
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
        private void frmMain_Load(object sender, EventArgs e)
        {
            partyPics.Add(pbPartySlot1);
            partyPics.Add(pbPartySlot2);
            partyPics.Add(pbPartySlot3);
            partyPics.Add(pbPartySlot4);
            partyPics.Add(pbPartySlot5);
            partyPics.Add(pbPartySlot6);
            boxPics.Add(pbBoxSlot1);
            boxPics.Add(pbBoxSlot2);
            boxPics.Add(pbBoxSlot3);
            boxPics.Add(pbBoxSlot4);
            boxPics.Add(pbBoxSlot5);
            boxPics.Add(pbBoxSlot6);
            boxPics.Add(pbBoxSlot7);
            boxPics.Add(pbBoxSlot8);
            boxPics.Add(pbBoxSlot9);
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
        }
    }
}
