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
            PKMDS.SQL.OpenDB(Properties.Settings.Default.veekunpokedex);
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
                tcTabs.TabPages.Remove(tcTabs.TabPages["tpMisc"]);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            ClearForm();
            DisplayPokemon(TempPokemon);
        }
        private void DisplayPokemon(PKMDS.Pokemon pkm)
        {
            UpdateFormIcon();
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
            //pbType1.Image = null;
            //pbType2.Image = null;
            //pbShiny.Image = null;
            //pbPokerus.Image = null;
            cbForm.Items.Clear();
            cbForm.Text = "";
        }

        private void UpdateFormIcon()
        {
            System.Drawing.Bitmap icon = (System.Drawing.Bitmap)(TempPokemon.Icon);
            this.Icon = Icon.FromHandle(icon.GetHicon());
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
            lblHeldItemFlavor.Text = ((PKMDS.Item)(cbHeldItem.SelectedItem)).ItemFlavor;
        }
        private void UpdateShiny()
        {
            pbShiny.Image = TempPokemon.ShinyIcon;
        }
        private void UpdateBall()
        {
            pbBall.Image = TempPokemon.BallPic;
            cbBall.SelectedValue = TempPokemon.BallID;
        }
        private void UpdatePokerus()
        {
            pbPokerus.Image = TempPokemon.PokerusIcon;
            cbPKRSStrain.SelectedIndex = TempPokemon.PokerusStrain;
            cbPKRSDays.SelectedIndex = TempPokemon.PokerusDays;
        }
        private void UpdateSpecies()
        {
            cbSpecies.SelectedValue = TempPokemon.SpeciesID;
            numSpecies.Value = (Decimal)(TempPokemon.SpeciesID);
        }
        private void UpdateLevel()
        {
            numLevel.Value = TempPokemon.Level;
        }
        private void UpdateForm()
        {
            SetForms();
            if (cbForm.Enabled)
            {
                if (cbForm.Items.Count < TempPokemon.FormID)
                {
                    TempPokemon.FormID = 0;
                    cbForm.SelectedIndex = -1;
                    cbForm.Text = "";
                }
                else
                {
                    cbForm.SelectedIndex = TempPokemon.FormID;
                }
            }
        }
        #endregion

        #region UpdateBasicInfo
        private void UpdateBasicInfo()
        {
            UpdateNickname();
            UpdateOTGender();
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
        private void UpdateOTGender()
        {
            if (TempPokemon.OTGenderID == 1)
            {
                rbOTFemale.Checked = true;
            }
            else
            {
                rbOTMale.Checked = true;
            }
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
            cbAbility.SelectedValue = TempPokemon.AbilityID;
            lblAbilityFlavor.Text = ((PKMDS.Ability)(cbAbility.SelectedItem)).AbilityFlavor;
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
            SetControlFont(ref numHPIV, numHPIV.Value == numHPIV.Maximum);
            SetControlFont(ref numAtkIV, numAtkIV.Value == numAtkIV.Maximum);
            SetControlFont(ref numDefIV, numDefIV.Value == numDefIV.Maximum);
            SetControlFont(ref numSpAtkIV, numSpAtkIV.Value == numSpAtkIV.Maximum);
            SetControlFont(ref numSpDefIV, numSpDefIV.Value == numSpDefIV.Maximum);
            SetControlFont(ref numSpeedIV, numSpeedIV.Value == numSpeedIV.Maximum);
        }
        private void UpdateEVs()
        {
            numHPEV.Value = TempPokemon.GetEV(0);
            numAtkEV.Value = TempPokemon.GetEV(1);
            numDefEV.Value = TempPokemon.GetEV(2);
            numSpAtkEV.Value = TempPokemon.GetEV(3);
            numSpDefEV.Value = TempPokemon.GetEV(4);
            numSpeedEV.Value = TempPokemon.GetEV(5);
            SetControlFont(ref numHPEV, numHPEV.Value >= 252M);
            SetControlFont(ref numAtkEV, numAtkEV.Value >= 252M);
            SetControlFont(ref numDefEV, numDefEV.Value >= 252M);
            SetControlFont(ref numSpAtkEV, numSpAtkEV.Value >= 252M);
            SetControlFont(ref numSpDefEV, numSpDefEV.Value >= 252M);
            SetControlFont(ref numSpeedEV, numSpeedEV.Value >= 252M);
        }
        private void UpdateTotalEVs()
        {
            int totalEVs =
                ((int)(numHPEV.Value) +
                (int)(numAtkEV.Value) +
                (int)(numDefEV.Value) +
                (int)(numSpAtkEV.Value) +
                (int)(numSpDefEV.Value) +
                (int)(numSpeedEV.Value));
            txtTotalEVs.Text = totalEVs.ToString();
            SetControlFont(ref txtTotalEVs, totalEVs >= 510);
        }
        private void UpdateNature()
        {
            cbNature.SelectedValue = TempPokemon.NatureID;
            int inc = TempPokemon.NatureIncrease - 1;
            int dec = TempPokemon.NatureDecrease - 1;
            Color statcolor = new Color();
            Label[] statlbls =
        {
            lblHPStats,
            lblAtkStats,
            lblDefStats,
            lblSpAtkStats,
            lblSpDefStats,
            lblSpeedStats
        };
            for (int i = 0; i < 6; i++)
            {
                statcolor = Color.Black;
                if (inc == i)
                {
                    if (dec == i)
                    {
                        statcolor = Color.Black;
                    }
                    else
                    {
                        statcolor = Color.Red;
                    }
                }
                if (dec == i)
                {
                    if (inc == i)
                    {
                        statcolor = Color.Black;
                    }
                    else
                    {
                        statcolor = Color.Blue;
                    }
                }
                statlbls[i].ForeColor = statcolor;
            }
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
            SetControlFont(ref numTameness, numTameness.Value == numTameness.Maximum);
        }
        private void UpdateCharacteristic()
        {
            lblCharacteristic.Text = TempPokemon.Characteristic;
        }
        #endregion

        #region UpdateMovesInfo
        private void UpdateMovesInfo()
        {
            UpdateMove1();
            UpdateMove2();
            UpdateMove3();
            UpdateMove4();
        }

        private void AutosizeFont(ref System.Windows.Forms.Label ctrl)
        {
            //       while ((Decimal)(ctrl.Width) > (Decimal)(System.Windows.Forms.TextRenderer.MeasureText(ctrl.Text,
            //new Font(ctrl.Font.FontFamily, ctrl.Font.Size, ctrl.Font.Style)).Width) / 1.00M)
            //       {
            //           ctrl.Font = new Font(ctrl.Font.FontFamily, ctrl.Font.Size + 0.5f, ctrl.Font.Style);
            //       }
            while ((Decimal)(ctrl.Width) < (Decimal)(System.Windows.Forms.TextRenderer.MeasureText(ctrl.Text,
     new Font(ctrl.Font.FontFamily, ctrl.Font.Size, ctrl.Font.Style)).Width) / 1.90M)
            {
                ctrl.Font = new Font(ctrl.Font.FontFamily, ctrl.Font.Size - 0.5f, ctrl.Font.Style);
            }
        }

        private void UpdateMove1()
        {
            UInt16 moveid = TempPokemon.GetMoveIDs[0];
            cbMove1.SelectedValue = moveid;
            PKMDS.Move move = (PKMDS.Move)(cbMove1.SelectedItem);
            lblMove1Flavor.Text = move.MoveFlavor;
            if ((move.MoveAccuracy != 0) && (move.MoveAccuracy != 1))
            {
                lblMove1Accuracy.Text = move.MoveAccuracy.ToString("0") + "%";
            }
            else
            {
                lblMove1Accuracy.Text = "-";
            }
            if ((move.MovePower != 0) && (move.MovePower != 1))
            {
                lblMove1Power.Text = move.MovePower.ToString();
            }
            else
            {
                lblMove1Power.Text = "-";
            }
            numMove1PP.Value = TempPokemon.GetMovePP(0);
            numMove1PPUps.Value = TempPokemon.GetMovePPUp(0);
            txtMove1MaxPP.Text = (move.MoveBasePP + (numMove1PPUps.Value * (move.MoveBasePP / 5))).ToString("0");
            pbMove1Type.Image = move.MoveTypeImage;
            pbMove1Category.Image = move.MoveCategoryImage;
            AutosizeFont(ref lblMove1Flavor);
        }
        private void UpdateMove2()
        {
            UInt16 moveid = TempPokemon.GetMoveIDs[1];
            if (moveid == 0)
            {
                cbMove2.SelectedIndex = -1;
                numMove2PPUps.Enabled = false;
                numMove2PP.Enabled = false;
                lblMove2Accuracy.Text = "";
                lblMove2Flavor.Text = "";
                lblMove2Power.Text = "";
                numMove2PP.Value = 0M;
                numMove2PPUps.Value = 0M;
                txtMove2MaxPP.Text = "";
                pbMove2Type.Image = null;
                pbMove2Category.Image = null;
            }
            else
            {
                cbMove2.SelectedValue = moveid;
                numMove2PPUps.Enabled = true;
                numMove2PP.Enabled = true;
                PKMDS.Move move = (PKMDS.Move)(cbMove2.SelectedItem);
                lblMove2Flavor.Text = move.MoveFlavor;
                if ((move.MoveAccuracy != 0) && (move.MoveAccuracy != 1))
                {
                    lblMove2Accuracy.Text = move.MoveAccuracy.ToString("0") + "%";
                }
                else
                {
                    lblMove2Accuracy.Text = "-";
                }
                if ((move.MovePower != 0) && (move.MovePower != 1))
                {
                    lblMove2Power.Text = move.MovePower.ToString();
                }
                else
                {
                    lblMove2Power.Text = "-";
                }
                numMove2PP.Value = TempPokemon.GetMovePP(1);
                numMove2PPUps.Value = TempPokemon.GetMovePPUp(1);
                txtMove2MaxPP.Text = (move.MoveBasePP + (numMove2PPUps.Value * (move.MoveBasePP / 5))).ToString("0");
                pbMove2Type.Image = move.MoveTypeImage;
                pbMove2Category.Image = move.MoveCategoryImage;
                AutosizeFont(ref lblMove2Flavor);
            }
        }
        private void UpdateMove3()
        {
            UInt16 moveid = TempPokemon.GetMoveIDs[2];
            if (moveid == 0)
            {
                cbMove3.SelectedIndex = -1;
                numMove3PPUps.Enabled = false;
                numMove3PP.Enabled = false;
                lblMove3Accuracy.Text = "";
                lblMove3Flavor.Text = "";
                lblMove3Power.Text = "";
                numMove3PP.Value = 0M;
                numMove3PPUps.Value = 0M;
                txtMove3MaxPP.Text = "";
                pbMove3Type.Image = null;
                pbMove3Category.Image = null;
            }
            else
            {
                cbMove3.SelectedValue = moveid;
                numMove3PPUps.Enabled = true;
                numMove3PP.Enabled = true;
                PKMDS.Move move = (PKMDS.Move)(cbMove3.SelectedItem);
                lblMove3Flavor.Text = move.MoveFlavor;
                if ((move.MoveAccuracy != 0) && (move.MoveAccuracy != 1))
                {
                    lblMove3Accuracy.Text = move.MoveAccuracy.ToString("0") + "%";
                }
                else
                {
                    lblMove3Accuracy.Text = "-";
                }
                if ((move.MovePower != 0) && (move.MovePower != 1))
                {
                    lblMove3Power.Text = move.MovePower.ToString();
                }
                else
                {
                    lblMove3Power.Text = "-";
                }
                numMove3PP.Value = TempPokemon.GetMovePP(2);
                numMove3PPUps.Value = TempPokemon.GetMovePPUp(2);
                txtMove3MaxPP.Text = (move.MoveBasePP + (numMove3PPUps.Value * (move.MoveBasePP / 5))).ToString("0");
                pbMove3Type.Image = move.MoveTypeImage;
                pbMove3Category.Image = move.MoveCategoryImage;
                AutosizeFont(ref lblMove3Flavor);
            }
        }
        private void UpdateMove4()
        {
            UInt16 moveid = TempPokemon.GetMoveIDs[3];
            if (moveid == 0)
            {
                cbMove4.SelectedIndex = -1;
                numMove4PPUps.Enabled = false;
                numMove4PP.Enabled = false;
                lblMove4Accuracy.Text = "";
                lblMove4Flavor.Text = "";
                lblMove4Power.Text = "";
                numMove4PP.Value = 0M;
                numMove4PPUps.Value = 0M;
                txtMove4MaxPP.Text = "";
                pbMove4Type.Image = null;
                pbMove4Category.Image = null;
            }
            else
            {
                cbMove4.SelectedValue = moveid;
                numMove4PPUps.Enabled = true;
                numMove4PP.Enabled = true;
                PKMDS.Move move = (PKMDS.Move)(cbMove4.SelectedItem);
                lblMove4Flavor.Text = move.MoveFlavor;
                if ((move.MoveAccuracy != 0) && (move.MoveAccuracy != 1))
                {
                    lblMove4Accuracy.Text = move.MoveAccuracy.ToString("0") + "%";
                }
                else
                {
                    lblMove4Accuracy.Text = "-";
                }
                if ((move.MovePower != 0) && (move.MovePower != 1))
                {
                    lblMove4Power.Text = move.MovePower.ToString();
                }
                else
                {
                    lblMove4Power.Text = "-";
                }
                numMove4PP.Value = TempPokemon.GetMovePP(3);
                numMove4PPUps.Value = TempPokemon.GetMovePPUp(3);
                txtMove4MaxPP.Text = (move.MoveBasePP + (numMove4PPUps.Value * (move.MoveBasePP / 5))).ToString("0");
                pbMove4Type.Image = move.MoveTypeImage;
                pbMove4Category.Image = move.MoveCategoryImage;
                AutosizeFont(ref lblMove4Flavor);
            }
        }

        #endregion

        #region UpdateOriginsInfo
        private void UpdateOriginsInfo()
        {
            numMetLevel.Value = (Decimal)(TempPokemon.MetLevel);
            cbIsEgg.Checked = TempPokemon.IsEgg;
            UpdateHatchSteps();
            cbNsPokemon.Checked = TempPokemon.IsNsPokemon;
            cbFateful.Checked = TempPokemon.IsFateful;
            cbGame.SelectedValue = (Byte)(TempPokemon.HometownID);
            cbCountry.SelectedValue = (Byte)(TempPokemon.LanguageID);
            cbMetLocation.SelectedValue = TempPokemon.MetLocationID;
            dtMetDate.Value = TempPokemon.MetDate;
            cbMetAsEgg.Checked = TempPokemon.MetAsEgg;
            cbEggLocation.Enabled = false;
            dtEggDate.Enabled = false;
            if (TempPokemon.MetAsEgg)
            {
                cbEggLocation.Enabled = true;
                cbEggLocation.SelectedValue = TempPokemon.EggLocationID;
                dtEggDate.Enabled = true;
                dtEggDate.Value = TempPokemon.EggDate;
            }
        }

        #endregion

        #region UpdateRibbonsInfo
        private void UpdateRibbonsInfo()
        {
            // TODO: Ribbons
        }

        #endregion

        #region UpdateMiscInfo
        private void UpdateMiscInfo()
        {
            // TODO: Misc
        }

        #endregion

        private void SetControlFont(ref NumericUpDown control, bool bold = false)
        {
            if (bold)
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
            if (bold)
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
            SetAbilities();
            SetSpecies();
            SetBalls();
            SetNatures();
            SetLocations();
            SetMoves();
            SetHometowns();
            SetCountries();
            UISet = true;
        }
        private void SetForms()
        {
            cbForm.Items.Clear();
            if (TempPokemon.SpeciesID != 0)
            {
                string[] formnames = PKMDS.GetPKMFormNames(TempPokemon.SpeciesID);
                if (formnames.Length != 0)
                {
                    if (!((formnames.Length == 1) && (formnames[0] == "")))
                    {
                        cbForm.Items.AddRange(PKMDS.GetPKMFormNames(TempPokemon.SpeciesID));
                        cbForm.Enabled = true;
                    }
                    else
                    {
                        cbForm.Enabled = false;
                    }
                }
                else
                {
                    cbForm.Enabled = false;
                }
            }
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
        private void SetAbilities()
        {
            List<PKMDS.Ability> abilities = new List<PKMDS.Ability>();
            PKMDS.Ability ability = new PKMDS.Ability();
            for (UInt16 abilityindex = 0; abilityindex <= 164; abilityindex++)
            {
                ability = new PKMDS.Ability(abilityindex);
                if ((ability.AbilityName != "") & (ability.AbilityName != null) & (ability.AbilityID != 0))
                {
                    abilities.Add(ability);
                }
            }
            cbAbility.DataSource = abilities;
            cbAbility.DisplayMember = "AbilityName";
            cbAbility.ValueMember = "AbilityID";
        }
        private void SetSpecies()
        {
            List<PKMDS.Species> species_l = new List<PKMDS.Species>();
            PKMDS.Species species = new PKMDS.Species();
            for (UInt16 speciesindex = 0; speciesindex <= 649; speciesindex++)
            {
                species = new PKMDS.Species(speciesindex);
                if ((species.SpeciesName != "") & (species.SpeciesName != null) & (species.SpeciesID != 0))
                {
                    species_l.Add(species);
                }
            }
            cbSpecies.DataSource = species_l;
            cbSpecies.DisplayMember = "SpeciesName";
            cbSpecies.ValueMember = "SpeciesID";
        }
        private void SetBalls()
        {
            List<PKMDS.Ball> balls = new List<PKMDS.Ball>();
            PKMDS.Ball ball = new PKMDS.Ball();
            for (Byte ballindex = 0; ballindex <= 25; ballindex++)
            {
                ball = new PKMDS.Ball(ballindex);
                if ((ball.BallName != "") & (ball.BallName != null))
                {
                    balls.Add(ball);
                }
            }
            cbBall.DataSource = balls;
            cbBall.DisplayMember = "BallName";
            cbBall.ValueMember = "BallID";
        }
        private void SetNatures()
        {
            List<PKMDS.Nature> natures = new List<PKMDS.Nature>();
            PKMDS.Nature nature = new PKMDS.Nature();
            for (Byte natureindex = 0; natureindex <= 24; natureindex++)
            {
                nature = new PKMDS.Nature(natureindex);
                if ((nature.NatureName != "") & (nature.NatureName != null))
                {
                    natures.Add(nature);
                }
            }
            cbNature.DataSource = natures;
            cbNature.DisplayMember = "NatureName";
            cbNature.ValueMember = "NatureID";
        }
        private void SetLocations()
        {
            List<PKMDS.Location> metlocations = new List<PKMDS.Location>();
            List<PKMDS.Location> egglocations = new List<PKMDS.Location>();
            PKMDS.Location location = new PKMDS.Location();
            for (UInt16 locationindex = 0; locationindex <= 153; locationindex++)
            {
                location = new PKMDS.Location(locationindex);
                if ((location.LocationName != "") & (location.LocationName != null) & (location.LocationID != 0))
                {
                    metlocations.Add(location);
                    egglocations.Add(location);
                }
            }
            location = new PKMDS.Location(2000);
            metlocations.Add(location);
            egglocations.Add(location);
            location = new PKMDS.Location(30001);
            metlocations.Add(location);
            egglocations.Add(location);
            location = new PKMDS.Location(30012);
            metlocations.Add(location);
            egglocations.Add(location);
            location = new PKMDS.Location(30013);
            metlocations.Add(location);
            egglocations.Add(location);
            location = new PKMDS.Location(30015);
            metlocations.Add(location);
            egglocations.Add(location);
            location = new PKMDS.Location(40001);
            metlocations.Add(location);
            egglocations.Add(location);
            location = new PKMDS.Location(40069);
            metlocations.Add(location);
            egglocations.Add(location);
            location = new PKMDS.Location(60002);
            metlocations.Add(location);
            egglocations.Add(location);

            cbMetLocation.DataSource = metlocations;
            cbMetLocation.DisplayMember = "LocationName";
            cbMetLocation.ValueMember = "LocationID";
            cbEggLocation.DataSource = egglocations;
            cbEggLocation.DisplayMember = "LocationName";
            cbEggLocation.ValueMember = "LocationID";
        }
        private void SetMoves()
        {
            List<PKMDS.Move> moves1 = new List<PKMDS.Move>();
            List<PKMDS.Move> moves2 = new List<PKMDS.Move>();
            List<PKMDS.Move> moves3 = new List<PKMDS.Move>();
            List<PKMDS.Move> moves4 = new List<PKMDS.Move>();
            PKMDS.Move move = new PKMDS.Move();
            for (UInt16 moveindex = 0; moveindex <= 559; moveindex++)
            {
                move = new PKMDS.Move(moveindex);
                if ((move.MoveName != "") & (move.MoveName != null) & (move.MoveID != 0))
                {
                    moves1.Add(move);
                }
                moves2.Add(move);
                moves3.Add(move);
                moves4.Add(move);
            }
            cbMove1.DataSource = moves1;
            cbMove1.DisplayMember = "MoveName";
            cbMove1.ValueMember = "MoveID";
            cbMove2.DataSource = moves2;
            cbMove2.DisplayMember = "MoveName";
            cbMove2.ValueMember = "MoveID";
            cbMove3.DataSource = moves3;
            cbMove3.DisplayMember = "MoveName";
            cbMove3.ValueMember = "MoveID";
            cbMove4.DataSource = moves4;
            cbMove4.DisplayMember = "MoveName";
            cbMove4.ValueMember = "MoveID";
        }
        private void SetHometowns()
        {
            List<PKMDS.Hometown> hometowns = new List<PKMDS.Hometown>();
            PKMDS.Hometown hometown = new PKMDS.Hometown();
            for (Byte hometownindex = 0; hometownindex <= 25; hometownindex++)
            {
                if ((hometownindex != 6) ||
                    (hometownindex != 9) ||
                    (hometownindex != 13) ||
                    (hometownindex != 14) ||
                    (hometownindex != 16) ||
                    (hometownindex != 17) ||
                    (hometownindex != 18) ||
                    (hometownindex != 19))
                {
                    hometown = new PKMDS.Hometown(hometownindex);
                    if ((hometown.HometownName != "") & (hometown.HometownName != null) & (hometown.HometownID != 0))
                    {
                        hometowns.Add(hometown);
                    }
                }
            }
            cbGame.DataSource = hometowns;
            cbGame.DisplayMember = "HometownName";
            cbGame.ValueMember = "HometownID";
        }
        private void SetCountries()
        {
            List<PKMDS.Country> countries = new List<PKMDS.Country>();
            PKMDS.Country country = new PKMDS.Country();
            for (Byte countryindex = 0; countryindex <= 8; countryindex++)
            {
                if (countryindex != 6)
                {
                    country = new PKMDS.Country(countryindex);
                    if ((country.CountryName != "") & (country.CountryName != null) & (country.CountryID != 0))
                    {
                        countries.Add(country);
                    }
                }
            }
            cbCountry.DataSource = countries;
            cbCountry.DisplayMember = "CountryName";
            cbCountry.ValueMember = "CountryID";
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
                    lblHeldItemFlavor.Text = ((PKMDS.Item)(cbHeldItem.SelectedItem)).ItemFlavor;
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
                UpdateSprite();
                UpdateShiny();
                CheckApplyButton();
            }
        }
        private void numSID_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SID = (UInt16)(numSID.Value);
                UpdateSprite();
                UpdateShiny();
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
                if (cbAbility.SelectedIndex != -1)
                {
                    TempPokemon.AbilityID = (UInt16)(cbAbility.SelectedValue);
                    lblAbilityFlavor.Text = ((PKMDS.Ability)(cbAbility.SelectedItem)).AbilityFlavor;
                    CheckApplyButton();
                }
            }
        }
        private void numEXP_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.EXP = (UInt32)(numEXP.Value);
                UISet = false;
                UpdateLevel();
                UpdateEXP();
                UISet = true;
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void cbBall_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                if (cbBall.SelectedIndex != -1)
                {
                    TempPokemon.BallID = (Byte)(cbBall.SelectedValue);
                    UpdateBall();
                    CheckApplyButton();
                }
            }
        }
        private void cbSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                if (cbSpecies.SelectedIndex != -1)
                {
                    TempPokemon.SpeciesID = (UInt16)(cbSpecies.SelectedValue);
                    numSpecies.Value = (Decimal)(TempPokemon.SpeciesID);
                    UpdateForm();
                    if (!cbForm.Enabled)
                    {
                        TempPokemon.FormID = 0;
                    }
                    UpdateSprite();
                    UpdateFormIcon();
                    UpdateTypes();
                    UpdateGenderPic();
                    UISet = false;
                    UpdateLevel();
                    UISet = true;
                    UpdateCalculatedStats();
                    CheckApplyButton();
                }
            }
        }
        private void numSpecies_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet && ((int)(numSpecies.Value - 1)) != cbSpecies.SelectedIndex)
            {
                cbSpecies.SelectedIndex = (int)(numSpecies.Value - 1);
            }
        }
        private void cbForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.FormID = (Byte)(cbForm.SelectedIndex);
                UpdateSprite();
                UpdateFormIcon();
                UpdateTypes();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numLevel_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.Level = (int)(numLevel.Value);
                UISet = false;
                UpdateEXP();
                UISet = true;
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numHPIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(0, (int)(numHPIV.Value));
                SetControlFont(ref numHPIV, numHPIV.Value == numHPIV.Maximum);
                UpdateCharacteristic();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numAtkIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(1, (int)(numAtkIV.Value));
                SetControlFont(ref numAtkIV, numAtkIV.Value == numAtkIV.Maximum);
                UpdateCharacteristic();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numDefIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(2, (int)(numDefIV.Value));
                SetControlFont(ref numDefIV, numDefIV.Value == numDefIV.Maximum);
                UpdateCharacteristic();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numSpAtkIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(3, (int)(numSpAtkIV.Value));
                SetControlFont(ref numSpAtkIV, numSpAtkIV.Value == numSpAtkIV.Maximum);
                UpdateCharacteristic();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numSpDefIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(4, (int)(numSpDefIV.Value));
                SetControlFont(ref numSpDefIV, numSpDefIV.Value == numSpDefIV.Maximum);
                UpdateCharacteristic();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numSpeedIV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetIV(5, (int)(numSpeedIV.Value));
                SetControlFont(ref numSpeedIV, numSpeedIV.Value == numSpeedIV.Maximum);
                UpdateCharacteristic();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numHPEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(0, (int)(numHPEV.Value));
                SetControlFont(ref numHPEV, numHPEV.Value >= 252M);
                UpdateTotalEVs();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numAtkEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(1, (int)(numAtkEV.Value));
                SetControlFont(ref numAtkEV, numAtkEV.Value >= 252M);
                UpdateTotalEVs();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numDefEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(2, (int)(numDefEV.Value));
                SetControlFont(ref numDefEV, numDefEV.Value >= 252M);
                UpdateTotalEVs();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numSpAtkEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(3, (int)(numSpAtkEV.Value));
                SetControlFont(ref numSpAtkEV, numSpAtkEV.Value >= 252M);
                UpdateTotalEVs();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numSpDefEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(4, (int)(numSpDefEV.Value));
                SetControlFont(ref numSpDefEV, numSpDefEV.Value >= 252M);
                UpdateTotalEVs();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numSpeedEV_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetEV(5, (int)(numSpeedEV.Value));
                SetControlFont(ref numSpeedEV, numSpeedEV.Value >= 252M);
                UpdateTotalEVs();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void cbNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.NatureID = (Byte)(cbNature.SelectedValue);
                UpdateNature();
                UpdateCalculatedStats();
                CheckApplyButton();
            }
        }
        private void numTameness_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.Tameness = (int)(numTameness.Value);
                SetControlFont(ref numTameness, numTameness.Value == numTameness.Maximum);
                UpdateHatchSteps();
                CheckApplyButton();
            }
        }
        private void cbMove1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMoveID(0, (UInt16)(cbMove1.SelectedValue));
                numMove1PP.Value = (Decimal)(((PKMDS.Move)(cbMove1.SelectedItem)).MoveBasePP);
                UpdateMove1();
                CheckApplyButton();
            }
        }
        private void cbMove2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                if (cbMove2.SelectedValue == null)
                {
                    TempPokemon.SetMoveID(1, 0);
                    numMove2PPUps.Enabled = false;
                    numMove2PP.Enabled = false;
                }
                else
                {
                    TempPokemon.SetMoveID(1, (UInt16)(cbMove2.SelectedValue));
                    numMove2PPUps.Enabled = true;
                    numMove2PP.Enabled = true;
                    numMove2PP.Value = (Decimal)(((PKMDS.Move)(cbMove2.SelectedItem)).MoveBasePP);
                }
                UpdateMove2();
                CheckApplyButton();
            }
        }
        private void cbMove3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                if (cbMove3.SelectedValue == null)
                {
                    TempPokemon.SetMoveID(2, 0);
                    numMove3PPUps.Enabled = false;
                    numMove3PP.Enabled = false;
                }
                else
                {
                    TempPokemon.SetMoveID(2, (UInt16)(cbMove3.SelectedValue));
                    numMove3PPUps.Enabled = true;
                    numMove3PP.Enabled = true;
                    numMove3PP.Value = (Decimal)(((PKMDS.Move)(cbMove3.SelectedItem)).MoveBasePP);
                }
                UpdateMove3();
                CheckApplyButton();
            }
        }
        private void cbMove4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                if (cbMove4.SelectedValue == null)
                {
                    TempPokemon.SetMoveID(3, 0);
                    numMove4PPUps.Enabled = false;
                    numMove4PP.Enabled = false;
                }
                else
                {
                    TempPokemon.SetMoveID(3, (UInt16)(cbMove4.SelectedValue));
                    numMove4PPUps.Enabled = true;
                    numMove4PP.Enabled = true;
                    numMove4PP.Value = (Decimal)(((PKMDS.Move)(cbMove4.SelectedItem)).MoveBasePP);
                }
                UpdateMove4();
                CheckApplyButton();
            }
        }
        private void numMove1PPUps_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.SetMovePPUp(0, (int)(numMove1PPUps.Value));
                PKMDS.Move move = ((PKMDS.Move)(cbMove1.SelectedItem));
                if (move != null)
                {
                    txtMove1MaxPP.Text = (move.MoveBasePP + (numMove1PPUps.Value * (move.MoveBasePP / 5))).ToString("0");
                }
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
                PKMDS.Move move = ((PKMDS.Move)(cbMove2.SelectedItem));
                if (move != null)
                {
                    txtMove2MaxPP.Text = (move.MoveBasePP + (numMove2PPUps.Value * (move.MoveBasePP / 5))).ToString("0");
                }
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
                PKMDS.Move move = ((PKMDS.Move)(cbMove3.SelectedItem));
                if (move != null)
                {
                    txtMove3MaxPP.Text = (move.MoveBasePP + (numMove3PPUps.Value * (move.MoveBasePP / 5))).ToString("0");
                }
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
                PKMDS.Move move = ((PKMDS.Move)(cbMove4.SelectedItem));
                if (move != null)
                {
                    txtMove4MaxPP.Text = (move.MoveBasePP + (numMove4PPUps.Value * (move.MoveBasePP / 5))).ToString("0");
                }
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
                TempPokemon.MetLocationID = (UInt16)(cbMetLocation.SelectedValue);
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
                TempPokemon.EggLocationID = (UInt16)(cbEggLocation.SelectedValue);
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
                if (cbMetAsEgg.Checked)
                {
                    cbEggLocation.Enabled = true;
                    dtEggDate.Enabled = true;
                    TempPokemon.EggDate = dtEggDate.Value;
                }
                else
                {
                    cbEggLocation.Enabled = false;
                    dtEggDate.Enabled = false;
                    TempPokemon.SetNoEggDate();
                }
                CheckApplyButton();
            }
        }
        private void numMetLevel_ValueChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.MetLevel = (Byte)(numMetLevel.Value);
                CheckApplyButton();
            }
        }
        private void cbGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.HometownID = (Byte)(cbGame.SelectedValue);
                CheckApplyButton();
            }
        }
        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.LanguageID = (Byte)(cbCountry.SelectedValue);
                CheckApplyButton();
            }
        }
        private void cbIsEgg_CheckedChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.IsEgg = cbIsEgg.Checked;
                UpdateHatchSteps();
                cbMetAsEgg.Checked = true;
                cbEggLocation.SelectedIndex = 0;
                dtEggDate.Value = System.DateTime.Today;
                UpdateSprite();
                UpdateFormIcon();
                CheckApplyButton();
            }
        }
        private void UpdateHatchSteps()
        {
            if (TempPokemon.IsEgg)
            {
                txtMinHatchSteps.Enabled = true;
                txtMinHatchSteps.Text = (255 * TempPokemon.Tameness).ToString("N0");
            }
            else
            {
                txtMinHatchSteps.Enabled = false;
                txtMinHatchSteps.Text = "";
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
        private void cbPKRSStrain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.PokerusStrain = cbPKRSStrain.SelectedIndex;
                UpdatePokerus();
                CheckApplyButton();
            }
        }
        private void cbPKRSDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UISet && PokemonSet)
            {
                TempPokemon.PokerusDays = cbPKRSDays.SelectedIndex;
                UpdatePokerus();
                CheckApplyButton();
            }
        }
    }
}
