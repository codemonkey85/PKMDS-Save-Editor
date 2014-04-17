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
            numTID.Value = TempPokemon.TID;
            numSID.Value = TempPokemon.SID;
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
            numEXP.Value = TempPokemon.EXP;
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
            UpdateNature();
            UpdateCalculatedStats();
            UpdateTameness();
            UpdateCharacteristic();
        }
        private void UpdateIVs()
        {
            numHPIV.Value = TempPokemon.GetIV(0);
            numAtkIV.Value = TempPokemon.GetIV(1);
            numDefIV.Value = TempPokemon.GetIV(2);
            numSpAtkIV.Value = TempPokemon.GetIV(3);
            numSpDefIV.Value = TempPokemon.GetIV(4);
            numSpeedIV.Value = TempPokemon.GetIV(5);
        }
        private void UpdateEVs()
        {
            numHPEV.Value = TempPokemon.GetEV(0);
            numAtkEV.Value = TempPokemon.GetEV(1);
            numDefEV.Value = TempPokemon.GetEV(2);
            numSpAtkEV.Value = TempPokemon.GetEV(3);
            numSpDefEV.Value = TempPokemon.GetEV(4);
            numSpeedEV.Value = TempPokemon.GetEV(5);
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

        /*
         void pkmviewer::updatestatcolors()
{
    if(redisplayok)
    {
        QLabel * statlbls[6] =
        {
            ui->lblHPIV,
            ui->lblAtkIV,
            ui->lblDefIV,
            ui->lblSpAtkIV,
            ui->lblSpDefIV,
            ui->lblSpeedIV
        };
        QColor statcolor;
        int incr = 0;
        int decr = 0;
        for(int i = 0; i < 6; i++)
        {
            statcolor = Qt::black;
            incr = getnatureincrease(temppkm)-1;
            decr = getnaturedecrease(temppkm)-1;

            if(incr == i)
            {
                if(decr == i)
                {
                    statcolor = Qt::black;
                }
                else
                {
                    statcolor = Qt::red;
                }
            }
            if(decr == i)
            {
                if(incr == i)
                {
                    statcolor = Qt::black;
                }
                else
                {
                    statcolor = Qt::blue;
                }
            }
            QPalette statpalette = statlbls[i]->palette();
            statpalette.setColor(statlbls[i]->foregroundRole(), statcolor);
            statlbls[i]->setPalette(statpalette);
        }
    }
}
         */

        private void UpdateNature()
        {
            // TODO: nature
            int inc = TempPokemon.NatureIncrease;
            int dec = TempPokemon.NatureDecrease;
            if (inc == 2) { lblAtkStats.ForeColor = Color.Red; } else { lblAtkStats.ForeColor = Color.Black; }
            if (inc == 3) { lblDefStats.ForeColor = Color.Red; } else { lblAtkStats.ForeColor = Color.Black; }
            if (inc == 4) { lblSpAtkStats.ForeColor = Color.Red; } else { lblAtkStats.ForeColor = Color.Black; }
            if (inc == 5) { lblSpDefStats.ForeColor = Color.Red; } else { lblAtkStats.ForeColor = Color.Black; }
            if (inc == 6) { lblSpeedStats.ForeColor = Color.Red; } else { lblAtkStats.ForeColor = Color.Black; }
            if (dec == 2) { lblAtkStats.ForeColor = Color.Blue; } else { lblAtkStats.ForeColor = Color.Black; }
            if (dec == 3) { lblDefStats.ForeColor = Color.Blue; } else { lblAtkStats.ForeColor = Color.Black; }
            if (dec == 4) { lblSpAtkStats.ForeColor = Color.Blue; } else { lblAtkStats.ForeColor = Color.Black; }
            if (dec == 5) { lblSpDefStats.ForeColor = Color.Blue; } else { lblAtkStats.ForeColor = Color.Black; }
            if (dec == 6) { lblSpeedStats.ForeColor = Color.Blue; } else { lblAtkStats.ForeColor = Color.Black; }
        }
        private void UpdateCalculatedStats()
        {
            int[] calcstats = TempPokemon.GetStats;
            txtCalcHP.Text = calcstats[0].ToString();
            txtCalcAtk.Text = calcstats[1].ToString();
            txtCalcDef.Text = calcstats[2].ToString();
            txtCalcSpAtk.Text = calcstats[3].ToString();
            txtCalcSpDef.Text = calcstats[4].ToString();
            txtCalcSpeed.Text = calcstats[5].ToString();
        }
        private void UpdateTameness()
        {
            numTameness.Value = TempPokemon.Tameness;
        }
        private void UpdateCharacteristic()
        {
            lblCharacteristic.Text = TempPokemon.Characteristic;
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

        private void SetControlFont(ref NumericUpDown control, bool bold = false)
        {
            if (numTameness.Value == 255M)
            {
                control.Font = new Font(control.Font, FontStyle.Bold);
            }
            else
            {
                control.Font = new Font(control.Font, FontStyle.Regular);
            }
        }
        private void SetControlFont(ref TextBox control, bool bold = false)
        {
            if (numTameness.Value == 255M)
            {
                control.Font = new Font(control.Font, FontStyle.Bold);
            }
            else
            {
                control.Font = new Font(control.Font, FontStyle.Regular);
            }
        }

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
                CheckApplyButton();
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
                CheckApplyButton();
            }
        }
        private void cbAbility_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: ability
                CheckApplyButton();
            }
        }
        private void numEXP_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.EXP = (UInt32)(numEXP.Value);
                CheckApplyButton();
            }
        }
        private void cbBall_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: ball
                CheckApplyButton();
            }
        }
        private void cbSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: species
                CheckApplyButton();
            }
        }
        private void numSpecies_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: species
                CheckApplyButton();
            }
        }
        private void cbForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: form
                CheckApplyButton();
            }
        }
        private void numLevel_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.Level = (int)(numLevel.Value);
                CheckApplyButton();
            }
        }
        private void numHPIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(0, (int)(numHPIV.Value));
                CheckApplyButton();
            }
        }
        private void numAtkIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(1, (int)(numAtkIV.Value));
                CheckApplyButton();
            }
        }
        private void numDefIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(2, (int)(numDefIV.Value));
                CheckApplyButton();
            }
        }
        private void numSpAtkIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(3, (int)(numSpAtkIV.Value));
                CheckApplyButton();
            }
        }
        private void numSpDefIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(4, (int)(numSpDefIV.Value));
                CheckApplyButton();
            }
        }
        private void numSpeedIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(5, (int)(numSpeedIV.Value));
                CheckApplyButton();
            }
        }
        private void numHPEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(0, (int)(numHPEV.Value));
                SetControlFont(ref numHPEV, numHPEV.Value >= 252M);
                CheckApplyButton();
            }
        }
        private void numAtkEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(1, (int)(numAtkEV.Value));
                SetControlFont(ref numAtkEV, numAtkEV.Value >= 252M);
                CheckApplyButton();
            }
        }
        private void numDefEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(2, (int)(numDefEV.Value));
                SetControlFont(ref numDefEV, numDefEV.Value >= 252M);
                CheckApplyButton();
            }
        }
        private void numSpAtkEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(3, (int)(numSpAtkEV.Value));
                SetControlFont(ref numSpAtkEV, numSpAtkEV.Value >= 252M);
                CheckApplyButton();
            }
        }
        private void numSpDefEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(4, (int)(numSpDefEV.Value));
                SetControlFont(ref numSpDefEV, numSpDefEV.Value >= 252M);
                CheckApplyButton();
            }
        }
        private void numSpeedEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(5, (int)(numSpeedEV.Value));
                SetControlFont(ref numSpeedEV, numSpeedEV.Value >= 252M);
                CheckApplyButton();
            }
        }
        private void cbNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: nature
                int inc = TempPokemon.NatureIncrease;
                int dec = TempPokemon.NatureDecrease;
                if (inc == 2) { lblAtkStats.ForeColor = Color.Red; } else { lblAtkStats.ForeColor = Color.Black; }
                if (inc == 3) { lblDefStats.ForeColor = Color.Red; } else { lblAtkStats.ForeColor = Color.Black; }
                if (inc == 4) { lblSpAtkStats.ForeColor = Color.Red; } else { lblAtkStats.ForeColor = Color.Black; }
                if (inc == 5) { lblSpDefStats.ForeColor = Color.Red; } else { lblAtkStats.ForeColor = Color.Black; }
                if (inc == 6) { lblSpeedStats.ForeColor = Color.Red; } else { lblAtkStats.ForeColor = Color.Black; }
                if (dec == 2) { lblAtkStats.ForeColor = Color.Blue; } else { lblAtkStats.ForeColor = Color.Black; }
                if (dec == 3) { lblDefStats.ForeColor = Color.Blue; } else { lblAtkStats.ForeColor = Color.Black; }
                if (dec == 4) { lblSpAtkStats.ForeColor = Color.Blue; } else { lblAtkStats.ForeColor = Color.Black; }
                if (dec == 5) { lblSpDefStats.ForeColor = Color.Blue; } else { lblAtkStats.ForeColor = Color.Black; }
                if (dec == 6) { lblSpeedStats.ForeColor = Color.Blue; } else { lblAtkStats.ForeColor = Color.Black; }
                CheckApplyButton();
            }
        }
        private void numTameness_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.Tameness = (int)(numTameness.Value);
                SetControlFont(ref numTameness, numTameness.Value == 255M);
                CheckApplyButton();
            }
        }
        private void cbMove1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: moves
                CheckApplyButton();
            }
        }
        private void cbMove2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: moves
                CheckApplyButton();
            }
        }
        private void cbMove3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: moves
                CheckApplyButton();
            }
        }
        private void cbMove4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: moves
                CheckApplyButton();
            }
        }
        private void numMove1PPUps_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMovePPUp(0, (int)(numMove1PPUps.Value));
                CheckApplyButton();
            }
        }
        private void numMove1PP_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMovePP(0, (int)(numMove1PP.Value));
                CheckApplyButton();
            }
        }
        private void numMove2PPUps_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMovePPUp(1, (int)(numMove2PPUps.Value));
                CheckApplyButton();
            }
        }
        private void numMove2PP_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMovePP(1, (int)(numMove2PP.Value));
                CheckApplyButton();
            }
        }
        private void numMove3PPUps_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMovePPUp(2, (int)(numMove3PPUps.Value));
                CheckApplyButton();
            }
        }
        private void numMove3PP_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMovePP(2, (int)(numMove3PP.Value));
                CheckApplyButton();
            }
        }
        private void numMove4PPUps_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMovePPUp(3, (int)(numMove4PPUps.Value));
                CheckApplyButton();
            }
        }
        private void numMove4PP_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMovePP(3, (int)(numMove4PP.Value));
                CheckApplyButton();
            }
        }
        private void cbMetLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: location
                CheckApplyButton();
            }
        }
        private void dtMetDate_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.MetDate = dtMetDate.Value;
                CheckApplyButton();
            }
        }
        private void cbEggLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: location
                CheckApplyButton();
            }
        }
        private void dtEggDate_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.EggDate = dtEggDate.Value;
                CheckApplyButton();
            }
        }
        private void cbMetAsEgg_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: met as egg
                CheckApplyButton();
            }
        }
        private void numMetLevel_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.MetLevel = (int)(numMetLevel.Value);
                CheckApplyButton();
            }
        }
        private void cbGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: game
                CheckApplyButton();
            }
        }
        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                // TODO: country
                CheckApplyButton();
            }
        }
        private void cbIsEgg_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.IsEgg = cbIsEgg.Checked;
                CheckApplyButton();
            }
        }
        private void cbNsPokemon_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.IsNsPokemon = cbNsPokemon.Checked;
                CheckApplyButton();
            }
        }
        private void cbFateful_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.IsFateful = cbFateful.Checked;
                CheckApplyButton();
            }
        }
        //private void btnClose_Click(object sender, EventArgs e)
        //{

        //}
    }
}
