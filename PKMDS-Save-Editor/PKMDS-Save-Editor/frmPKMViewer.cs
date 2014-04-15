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
        private bool UISet = false;
        private bool PokemonSet = false;
        //private bool IsParty = false;
        public frmPKMViewer()
        {
            PKMDS.OpenDB(Properties.Settings.Default.veekunpokedex);
            PKMDS.OpenImgDB(Properties.Settings.Default.imagedb);
            InitializeComponent();
            SetUI();
        }
        public void SetPokemon(PKMDS.Pokemon pkm)
        {
            PokemonSet = false;
            this.SharedPokemon = pkm.Clone();
            this.TempPokemon = pkm.Clone();
            //this.IsParty = false;
        }
        public void SetPokemon(PKMDS.PartyPokemon ppkm)
        {
            PokemonSet = false;
            //SetPokemon(ppkm.PokemonData);
            this.SharedPokemon = ppkm.PokemonData.Clone();
            this.TempPokemon = ppkm.PokemonData.Clone();
            //this.IsParty = true;
        }
        private void frmPKMViewer_Load(object sender, EventArgs e)
        {
            try
            {
                tcTabs.TabPages.Remove(tcTabs.TabPages["tpRibbon"]);
            }
            catch (Exception ex)
            {

            }
            ClearForm();
            DisplayPokemon(TempPokemon);
        }
        private void DisplayPokemon(PKMDS.Pokemon pkm)
        {
            UpdateCommonInfo();
            UpdateBasicInfo();
            UpdateStatsInfo();
            UpdateMovesInfo();
            UpdateOriginsInfo();
            UpdateRibbonsInfo();
            UpdateMiscInfo();
            PokemonSet = true;
        }
        private void ClearForm()
        {
            btnApply.Enabled = false;
            cbHeldItem.SelectedIndex = -1;
            //cbHeldItem.SelectedValue = 0;
            pbHeldItem.Image = null;
            pbGender.Image = null;
        }

        #region UpdateCommonInfo
        private void UpdateCommonInfo()
        {
            UpdateSprite();
            UpdateGenderPic();
            UpdateMarkings();
            UpdateHeldItem();
            UpdateShiny();
            UpdateBall();
            UpdatePokerus();
            UpdateSpecies();
            UpdateLevel();
            UpdateForm();
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
        private void UpdateHeldItem()
        {
            cbHeldItem.SelectedValue = TempPokemon.ItemID;
            pbHeldItem.Image = ((PKMDS.Item)(cbHeldItem.SelectedItem)).ItemImage;
        }
        private void UpdateShiny()
        {
            pbShiny.Image = TempPokemon.ShinyIcon;
        }
        private void UpdateBall() { }
        private void UpdatePokerus()
        {
            pbPokerus.Image = TempPokemon.PokerusIcon;
        }
        private void UpdateSpecies()
        {

        }
        private void UpdateLevel()
        {
            numLevel.Value = TempPokemon.Level;
        }
        private void UpdateForm()
        {

        }
        #endregion

        #region UpdateBasicInfo
        private void UpdateBasicInfo()
        {
            UpdateNickname();
            UpdateOTName();
            UpdateTIDSID();
            UpdateTypes();
            UpdateAbility();
            UpdateEXP();
        }
        private void UpdateNickname()
        {
            txtNickname.Text = TempPokemon.Nickname;
            chkNicknamed.Checked = TempPokemon.IsNicknamed;
        }
        private void UpdateOTName()
        {
            txtOTName.Text = TempPokemon.OTName;
            if (TempPokemon.OTGenderID == 0)
            {
                txtOTName.ForeColor = Color.Blue;
            }
            else
            {
                txtOTName.ForeColor = Color.Red;
            }
        }
        private void UpdateTIDSID()
        {
            numTID.Value = (Decimal)(TempPokemon.TID);
            numSID.Value = (Decimal)(TempPokemon.SID);
        }
        private void UpdateTypes()
        {
            pbType1.Image = TempPokemon.GetTypePic(1);
            pbType2.Image = TempPokemon.GetTypePic(2);
        }
        private void UpdateAbility()
        {
            // TODO: ability
        }
        private void UpdateEXP()
        {
            numEXP.Value = (Decimal)(TempPokemon.EXP);
            txtTNL.Text = TempPokemon.TNL.ToString();
            if (TempPokemon.TNL == 0)
            {
                pbTNL.Minimum = 0;
                pbTNL.Maximum = 1;
                pbTNL.Value = 0;
                txtTNL.Text = (0).ToString();
                txtTNLPercent.Visible = false;
            }
            else
            {
                pbTNL.Minimum = 0;
                pbTNL.Maximum = (int)((int)(TempPokemon.EXPAtGivenLevel(TempPokemon.Level + 1)) - TempPokemon.EXPAtCurLevel); // (int)(TempPokemon.EXPAtGivenLevel(TempPokemon.Level + 1));
                pbTNL.Value = (int)(TempPokemon.EXP - TempPokemon.EXPAtCurLevel);
                txtTNL.Text = TempPokemon.TNL.ToString();
                txtTNLPercent.Visible = true;
                int percent = (100 * pbTNL.Value) / pbTNL.Maximum;
                txtTNLPercent.Text = percent.ToString("0") + "%";
            }
        }
        #endregion

        #region UpdateStatsInfo
        private void UpdateStatsInfo()
        {
            UpdateIVs();
            UpdateEVs();
            UpdateTotalEVs();
        }
        private void UpdateIVs()
        {
            numHPIV.Value = (Decimal)(TempPokemon.GetIV(0));
            numAtkIV.Value = (Decimal)(TempPokemon.GetIV(1));
            numDefIV.Value = (Decimal)(TempPokemon.GetIV(2));
            numSpAtkIV.Value = (Decimal)(TempPokemon.GetIV(3));
            numSpDefIV.Value = (Decimal)(TempPokemon.GetIV(4));
            numSpeedIV.Value = (Decimal)(TempPokemon.GetIV(5));
        }
        private void UpdateEVs()
        {
            numHPEV.Value = (Decimal)(TempPokemon.GetEV(0));
            numAtkEV.Value = (Decimal)(TempPokemon.GetEV(1));
            numDefEV.Value = (Decimal)(TempPokemon.GetEV(2));
            numSpAtkEV.Value = (Decimal)(TempPokemon.GetEV(3));
            numSpDefEV.Value = (Decimal)(TempPokemon.GetEV(4));
            numSpeedEV.Value = (Decimal)(TempPokemon.GetEV(5));
        }
        private void UpdateTotalEVs()
        {
            txtTotalEVs.Text =
                (numHPEV.Value +
                numAtkEV.Value +
                numDefEV.Value +
                numSpAtkEV.Value +
                numSpDefEV.Value +
                numSpeedEV.Value).ToString();
        }

        #endregion

        #region UpdateMovesInfo
        private void UpdateMovesInfo()
        {

        }

        #endregion

        #region UpdateOriginsInfo
        private void UpdateOriginsInfo()
        {

        }

        #endregion

        #region UpdateRibbonsInfo
        private void UpdateRibbonsInfo()
        {

        }

        #endregion

        #region UpdateMiscInfo
        private void UpdateMiscInfo()
        {

        }

        #endregion

        private void SetUI()
        {
            SetItems();
            UISet = true;
        }
        private void SetItems()
        {
            List<PKMDS.Item> items = new List<PKMDS.Item>();
            PKMDS.Item item = new PKMDS.Item();
            items.Add(item);
            for (UInt16 itemindex = 0; itemindex <= 0x027E; itemindex++)
            {
                item = new PKMDS.Item(itemindex);
                if ((item.ItemName != "") & (item.ItemName != null))
                {
                    items.Add(item);
                }
            }
            cbHeldItem.DataSource = items;
            cbHeldItem.DisplayMember = "ItemName";
            cbHeldItem.ValueMember = "ItemID";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            TempPokemon.FixChecksum();
            this.SharedPokemon = this.TempPokemon.Clone();
            this.SharedPartyPokemon = new PKMDS.PartyPokemon();
            this.SharedPartyPokemon.PokemonData = this.TempPokemon.Clone();
            this.Close();
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            TempPokemon.FixChecksum();
            this.SharedPokemon = this.TempPokemon.Clone();
            this.SharedPartyPokemon = new PKMDS.PartyPokemon();
            this.SharedPartyPokemon.PokemonData = this.TempPokemon.Clone();
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
            if (UISet && PokemonSet)
            {
                if (cbHeldItem.SelectedIndex != -1)
                {
                    TempPokemon.ItemID = (UInt16)(cbHeldItem.SelectedValue);
                    pbHeldItem.Image = ((PKMDS.Item)(cbHeldItem.SelectedItem)).ItemImage;
                    CheckApplyButton();
                }
            }
        }
        private void chkNicknamed_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.IsNicknamed = chkNicknamed.Checked;
                CheckApplyButton();
            }
        }
        private void txtNickname_TextChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                if (txtNickname.Text.Length != 0)
                {
                    TempPokemon.Nickname = txtNickname.Text;
                    chkNicknamed.Checked = true;
                    CheckApplyButton();
                }
            }
        }

        private void txtOTName_TextChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                if (txtOTName.Text.Length != 0)
                {
                    TempPokemon.OTName = txtOTName.Text;
                    CheckApplyButton();
                }
            }
        }
        private void numTID_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.TID = (UInt16)(numTID.Value);
                CheckApplyButton();
            }
        }
        private void numSID_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SID = (UInt16)(numSID.Value);
                CheckApplyButton();
            }
        }
        private void rbOTMale_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                if (rbOTMale.Checked)
                {
                    TempPokemon.OTGenderID = 0;
                    txtOTName.ForeColor = Color.Blue;
                }
            }
        }
        private void rbOTFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                if (rbOTFemale.Checked)
                {
                    TempPokemon.OTGenderID = 1;
                    txtOTName.ForeColor = Color.Red;
                }
            }
        }
        private void cbAbility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: ability
            }
        }
        private void numEXP_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.EXP = (UInt32)(numEXP.Value);
            }
        }
        private void cbBall_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: ball
            }
        }
        private void cbSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: species
            }
        }
        private void numSpecies_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: species
            }
        }
        private void cbForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: form
            }
        }
        private void numLevel_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.Level = (int)(numLevel.Value);
            }
        }
        private void numHPIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(0, (int)(numHPIV.Value));
            }
        }
        private void numAtkIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numDefIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numSpAtkIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numSpDefIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numSpeedIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numHPEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numAtkEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numDefEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numSpAtkEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numSpDefEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numSpeedEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void cbNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: nature
            }
        }
        private void numTameness_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void cbMove1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: moves
            }
        }
        private void cbMove2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: moves
            }
        }
        private void cbMove3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: moves
            }
        }
        private void cbMove4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: moves
            }
        }
        private void numMove1PPUps_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMovePPUp(0, (int)(numMove1PPUps.Value));
            }
        }
        private void numMove1PP_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMovePP(0, (int)(numMove1PP.Value));
            }
        }
        private void numMove2PPUps_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numMove2PP_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numMove3PPUps_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numMove3PP_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numMove4PPUps_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void numMove4PP_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {

            }
        }
        private void cbMetLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: location
            }
        }
        private void dtMetDate_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.MetDate = dtMetDate.Value;
            }
        }
        private void cbEggLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: location
            }
        }
        private void dtEggDate_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.EggDate = dtEggDate.Value;
            }
        }
        private void cbMetAsEgg_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: met as egg
            }
        }
        private void numMetLevel_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.MetLevel = (int)(numMetLevel.Value);
            }
        }
        private void cbGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: game
            }
        }
        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: country
            }
        }
        private void cbIsEgg_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.IsEgg = cbIsEgg.Checked;
            }
        }
        private void cbNsPokemon_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.IsNsPokemon = cbNsPokemon.Checked;
            }
        }
        private void cbFateful_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.IsFateful = cbFateful.Checked;
            }
        }
        //private void btnClose_Click(object sender, EventArgs e)
        //{

        //}
    }
}
