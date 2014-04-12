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
    public partial class frmPKMViewer : Form
    {
        public PKMDS.Pokemon SharedPokemon = new PKMDS.Pokemon();
        private PKMDS.Pokemon TempPokemon = new PKMDS.Pokemon();
        public PKMDS.PartyPokemon SharedPartyPokemon = new PKMDS.PartyPokemon();
        private bool UIReady = false;
        private bool IsParty = false;
        public frmPKMViewer()
        {
            PKMDS.OpenDB(Properties.Settings.Default.veekunpokedex);
            PKMDS.OpenImgDB(Properties.Settings.Default.imagedb);
            InitializeComponent();
            SetUI();
        }
        public void SetPokemon(PKMDS.Pokemon pkm)
        {
            this.SharedPokemon = pkm;
            this.TempPokemon = this.SharedPokemon;
            this.IsParty = false;
        }
        public void SetPokemon(PKMDS.PartyPokemon ppkm)
        {
            this.SharedPartyPokemon = ppkm;
            this.SharedPokemon = ppkm.PokemonData;
            this.TempPokemon = this.SharedPokemon;
            this.IsParty = true;
        }
        private void frmPKMViewer_Load(object sender, EventArgs e)
        {
            ClearForm();
            DisplayPokemon(TempPokemon);
        }
        private void DisplayPokemon(PKMDS.Pokemon pkm)
        {
            UpdateSprite();
            UpdateGenderPic();
            UpdateMarkings();
            UIReady = true;
        }
        private void ClearForm()
        {
            btnApply.Enabled = false;
            cbHeldItem.SelectedIndex = -1;
            pbHeldItem.Image = null;
            pbGender.Image = null;
        }
        private void UpdateSprite()
        {
            pbSprite.Image = TempPokemon.Sprite;
        }
        private void UpdateGenderPic()
        {
            pbGender.Image = TempPokemon.GenderIcon;
        }
        private void UpdateMarkings()
        {
            pbCircle.Image = TempPokemon.GetMarkingImage(0);
            pbTriangle.Image = TempPokemon.GetMarkingImage(1);
            pbSquare.Image = TempPokemon.GetMarkingImage(2);
            pbHeart.Image = TempPokemon.GetMarkingImage(3);
            pbStar.Image = TempPokemon.GetMarkingImage(4);
            pbDiamond.Image = TempPokemon.GetMarkingImage(5);
        }
        private void SetUI()
        {
            SetItems();
        }
        private void SetItems()
        {
            cbHeldItem.Items.Add(new PKMDS.Item(0));
            PKMDS.Item item = new PKMDS.Item();
            for (int itemindex = 0; itemindex <= 0x027E; itemindex++)
            {
                item.ItemID = itemindex;
                if ((item.ItemName != "") & (item.ItemName != null))
                {
                    cbHeldItem.Items.Add(new PKMDS.Item(itemindex));
                }
            }
            cbHeldItem.DisplayMember = "ItemName";
            cbHeldItem.ValueMember = "ItemID";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            TempPokemon.FixChecksum();
            this.SharedPokemon = this.TempPokemon;
            SharedPartyPokemon.PokemonData = SharedPokemon;
            this.Close();
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            TempPokemon.FixChecksum();
            this.SharedPokemon = this.TempPokemon;
            SharedPartyPokemon.PokemonData = SharedPokemon;
            CheckApplyButton();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            fileSave.FileName = "";
            if (fileSave.ShowDialog() != DialogResult.Cancel)
            {
                if (fileSave.FileName != "")
                {
                    TempPokemon.WriteToFile(fileSave.FileName, System.IO.Path.GetExtension(fileSave.FileName).ToLower() == ".ek5");
                }
            }
        }
        private void CheckApplyButton()
        {
            btnApply.Enabled = TempPokemon.IsModified;
        }
        private void pbCircle_Click(object sender, EventArgs e)
        {
            TempPokemon.SetMarking(0, !(TempPokemon.GetMarking(0)));
            pbCircle.Image = TempPokemon.GetMarkingImage(0);
            CheckApplyButton();
        }
        private void pbTriangle_Click(object sender, EventArgs e)
        {
            TempPokemon.SetMarking(1, !(TempPokemon.GetMarking(1)));
            pbTriangle.Image = TempPokemon.GetMarkingImage(1);
            CheckApplyButton();
        }
        private void pbSquare_Click(object sender, EventArgs e)
        {
            TempPokemon.SetMarking(2, !(TempPokemon.GetMarking(2)));
            pbSquare.Image = TempPokemon.GetMarkingImage(2);
            CheckApplyButton();
        }
        private void pbHeart_Click(object sender, EventArgs e)
        {
            TempPokemon.SetMarking(3, !(TempPokemon.GetMarking(3)));
            pbHeart.Image = TempPokemon.GetMarkingImage(3);
            CheckApplyButton();
        }
        private void pbStar_Click(object sender, EventArgs e)
        {
            TempPokemon.SetMarking(4, !(TempPokemon.GetMarking(4)));
            pbStar.Image = TempPokemon.GetMarkingImage(4);
            CheckApplyButton();
        }
        private void pbDiamond_Click(object sender, EventArgs e)
        {
            TempPokemon.SetMarking(5, !(TempPokemon.GetMarking(5)));
            pbDiamond.Image = TempPokemon.GetMarkingImage(5);
            CheckApplyButton();
        }
        private void frmPKMViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (TempPokemon.IsModified)
            {
                if (MessageBox.Show("Cancel changes?", "Close", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
        private void cbHeldItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UIReady)
            {
                if (cbHeldItem.SelectedIndex != -1)
                {
                    TempPokemon.ItemID = ((PKMDS.Item)(cbHeldItem.SelectedItem)).ItemID;
                    pbHeldItem.Image = ((PKMDS.Item)(cbHeldItem.SelectedItem)).ItemImage;
                    CheckApplyButton();
                }
            }
        }
    }
}
