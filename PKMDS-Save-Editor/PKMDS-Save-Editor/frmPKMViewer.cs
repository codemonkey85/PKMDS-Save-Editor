namespace PKMDS_Save_Editor;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public partial class frmPKMViewer : Form
{
    public Pokemon SharedPokemon = new();
    private Pokemon TempPokemon = new();
    public PartyPokemon SharedPartyPokemon = new();
    private bool UISet = false;
    private bool PokemonSet = false;
    //private bool IsParty = false;

    public frmPKMViewer()
    {
        SQL.OpenDB(Properties.Settings.Default.veekunpokedex);
        InitializeComponent();
        SetUI();
    }

    public void SetPokemon(Pokemon pkm)
    {
        PokemonSet = false;
        SharedPokemon = pkm.Clone();
        TempPokemon = pkm.Clone();
        //this.IsParty = false;
    }

    public void SetPokemon(PartyPokemon ppkm)
    {
        PokemonSet = false;
        //SetPokemon(ppkm.PokemonData);
        SharedPokemon = ppkm.PokemonData.Clone();
        TempPokemon = ppkm.PokemonData.Clone();
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
            Debug.WriteLine(ex.Message);
        }
        ClearForm();
        DisplayPokemon(TempPokemon);
    }

    private void DisplayPokemon(Pokemon pkm)
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
        cbForm.Text = string.Empty;
    }

    private void UpdateFormIcon()
    {
        var icon = (Bitmap)TempPokemon.Icon;
        Icon = Icon.FromHandle(icon.GetHicon());
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

    private void UpdateSprite() => pbSprite.Image = TempPokemon.Sprite;

    private void UpdateGenderPic() => pbGender.Image = TempPokemon.GenderIcon;

    private void UpdateMarkings()
    {
        pbCircle.Image = GetMarkingImage(Markings.Circle, TempPokemon.Circle);
        pbTriangle.Image = GetMarkingImage(Markings.Triangle, TempPokemon.Triangle);
        pbSquare.Image = GetMarkingImage(Markings.Square, TempPokemon.Square);
        pbHeart.Image = GetMarkingImage(Markings.Heart, TempPokemon.Heart);
        pbStar.Image = GetMarkingImage(Markings.Star, TempPokemon.Star);
        pbDiamond.Image = GetMarkingImage(Markings.Diamond, TempPokemon.Diamond);
    }

    private void UpdateHeldItem()
    {
        cbHeldItem.SelectedValue = TempPokemon.ItemID;
        pbHeldItem.Image = ((Item)cbHeldItem.SelectedItem).ItemImage;
        lblHeldItemFlavor.Text = ((Item)cbHeldItem.SelectedItem).ItemFlavor;
    }

    private void UpdateShiny() => pbShiny.Image = TempPokemon.ShinyIcon;

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
        numSpecies.Value = TempPokemon.SpeciesID;
    }

    private void UpdateLevel() => numLevel.Value = TempPokemon.Level;

    private void UpdateForm()
    {
        SetForms();
        if (!cbForm.Enabled)
        {
            return;
        }
        if (cbForm.Items.Count < TempPokemon.FormID)
        {
            TempPokemon.FormID = 0;
            cbForm.SelectedIndex = -1;
            cbForm.Text = string.Empty;
        }
        else
        {
            cbForm.SelectedIndex = TempPokemon.FormID;
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
        txtOTName.ForeColor = TempPokemon.OTGenderID == 0 ? Color.Blue : Color.Red;
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
        lblAbilityFlavor.Text = ((Ability)cbAbility.SelectedItem).AbilityFlavor;
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
            txtTNL.Text = 0.ToString();
            txtTNLPercent.Visible = false;
        }
        else
        {
            pbTNL.Minimum = 0;
            pbTNL.Maximum = (int)((int)TempPokemon.EXPAtGivenLevel(TempPokemon.Level + 1) - TempPokemon.EXPAtCurLevel); // (int)(TempPokemon.EXPAtGivenLevel(TempPokemon.Level + 1));
            pbTNL.Value = (int)(TempPokemon.EXP - TempPokemon.EXPAtCurLevel);
            txtTNL.Text = TempPokemon.TNL.ToString();
            txtTNLPercent.Visible = true;
            var percent = 100 * pbTNL.Value / pbTNL.Maximum;
            txtTNLPercent.Text = $"{percent:0}%";
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
        var totalEVs =
            (int)numHPEV.Value +
            (int)numAtkEV.Value +
            (int)numDefEV.Value +
            (int)numSpAtkEV.Value +
            (int)numSpDefEV.Value +
            (int)numSpeedEV.Value;
        txtTotalEVs.Text = totalEVs.ToString();
        SetControlFont(ref txtTotalEVs, totalEVs >= 510);
    }

    private void UpdateNature()
    {
        cbNature.SelectedValue = TempPokemon.NatureID;
        var inc = TempPokemon.NatureIncrease - 1;
        var dec = TempPokemon.NatureDecrease - 1;
        Color statcolor;
        Label[] statlbls =
        [
            lblHPStats,
            lblAtkStats,
            lblDefStats,
            lblSpAtkStats,
            lblSpDefStats,
            lblSpeedStats
        ];
        for (var i = 0; i < 6; i++)
        {
            statcolor = Color.Black;
            if (inc == i)
            {
                statcolor = dec == i ? Color.Black : Color.Red;
            }
            if (dec == i)
            {
                statcolor = inc == i ? Color.Black : Color.Blue;
            }
            statlbls[i].ForeColor = statcolor;
        }
    }

    private void UpdateCalculatedStats()
    {
        var calcstats = TempPokemon.GetStats;
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

    private void UpdateCharacteristic() => lblCharacteristic.Text = TempPokemon.Characteristic;
    #endregion

    #region UpdateMovesInfo
    private void UpdateMovesInfo()
    {
        UpdateMove1();
        UpdateMove2();
        UpdateMove3();
        UpdateMove4();
    }

    private static void AutosizeFont(ref Label ctrl)
    {
        //       while ((Decimal)(ctrl.Width) > (Decimal)(System.Windows.Forms.TextRenderer.MeasureText(ctrl.Text,
        //new Font(ctrl.Font.FontFamily, ctrl.Font.Size, ctrl.Font.Style)).Width) / 1.00M)
        //       {
        //           ctrl.Font = new Font(ctrl.Font.FontFamily, ctrl.Font.Size + 0.5f, ctrl.Font.Style);
        //       }
        while (ctrl.Width < TextRenderer.MeasureText(ctrl.Text,
            new Font(ctrl.Font.FontFamily, ctrl.Font.Size, ctrl.Font.Style)).Width / 1.90M)
        {
            ctrl.Font = new Font(ctrl.Font.FontFamily, ctrl.Font.Size - 0.5f, ctrl.Font.Style);
        }
    }

    private void UpdateMove1()
    {
        var moveid = TempPokemon.GetMoveIDs[0];
        cbMove1.SelectedValue = moveid;
        var move = (Move)cbMove1.SelectedItem;
        lblMove1Flavor.Text = move.MoveFlavor;
        lblMove1Accuracy.Text = move.MoveAccuracy is not 0 and not 1 ? $"{move.MoveAccuracy:0}%" : "-";
        lblMove1Power.Text = move.MovePower is not 0 and not 1 ? move.MovePower.ToString() : "-";
        numMove1PP.Value = TempPokemon.GetMovePP(0);
        numMove1PPUps.Value = TempPokemon.GetMovePPUp(0);
        txtMove1MaxPP.Text = (move.MoveBasePP + numMove1PPUps.Value * (move.MoveBasePP / 5)).ToString("0");
        pbMove1Type.Image = move.MoveTypeImage;
        pbMove1Category.Image = move.MoveCategoryImage;
        AutosizeFont(ref lblMove1Flavor);
    }

    private void UpdateMove2()
    {
        var moveid = TempPokemon.GetMoveIDs[1];
        if (moveid == 0)
        {
            cbMove2.SelectedIndex = -1;
            numMove2PPUps.Enabled = false;
            numMove2PP.Enabled = false;
            lblMove2Accuracy.Text = string.Empty;
            lblMove2Flavor.Text = string.Empty;
            lblMove2Power.Text = string.Empty;
            numMove2PP.Value = 0M;
            numMove2PPUps.Value = 0M;
            txtMove2MaxPP.Text = string.Empty;
            pbMove2Type.Image = null;
            pbMove2Category.Image = null;
        }
        else
        {
            cbMove2.SelectedValue = moveid;
            numMove2PPUps.Enabled = true;
            numMove2PP.Enabled = true;
            var move = (Move)cbMove2.SelectedItem;
            lblMove2Flavor.Text = move.MoveFlavor;
            lblMove2Accuracy.Text = move.MoveAccuracy is not 0 and not 1 ? $"{move.MoveAccuracy:0}%" : "-";
            lblMove2Power.Text = move.MovePower is not 0 and not 1 ? move.MovePower.ToString() : "-";
            numMove2PP.Value = TempPokemon.GetMovePP(1);
            numMove2PPUps.Value = TempPokemon.GetMovePPUp(1);
            txtMove2MaxPP.Text = (move.MoveBasePP + numMove2PPUps.Value * (move.MoveBasePP / 5)).ToString("0");
            pbMove2Type.Image = move.MoveTypeImage;
            pbMove2Category.Image = move.MoveCategoryImage;
            AutosizeFont(ref lblMove2Flavor);
        }
    }

    private void UpdateMove3()
    {
        var moveid = TempPokemon.GetMoveIDs[2];
        if (moveid == 0)
        {
            cbMove3.SelectedIndex = -1;
            numMove3PPUps.Enabled = false;
            numMove3PP.Enabled = false;
            lblMove3Accuracy.Text = string.Empty;
            lblMove3Flavor.Text = string.Empty;
            lblMove3Power.Text = string.Empty;
            numMove3PP.Value = 0M;
            numMove3PPUps.Value = 0M;
            txtMove3MaxPP.Text = string.Empty;
            pbMove3Type.Image = null;
            pbMove3Category.Image = null;
        }
        else
        {
            cbMove3.SelectedValue = moveid;
            numMove3PPUps.Enabled = true;
            numMove3PP.Enabled = true;
            var move = (Move)cbMove3.SelectedItem;
            lblMove3Flavor.Text = move.MoveFlavor;
            lblMove3Accuracy.Text = move.MoveAccuracy is not 0 and not 1 ? $"{move.MoveAccuracy:0}%" : "-";
            lblMove3Power.Text = move.MovePower is not 0 and not 1 ? move.MovePower.ToString() : "-";
            numMove3PP.Value = TempPokemon.GetMovePP(2);
            numMove3PPUps.Value = TempPokemon.GetMovePPUp(2);
            txtMove3MaxPP.Text = (move.MoveBasePP + numMove3PPUps.Value * (move.MoveBasePP / 5)).ToString("0");
            pbMove3Type.Image = move.MoveTypeImage;
            pbMove3Category.Image = move.MoveCategoryImage;
            AutosizeFont(ref lblMove3Flavor);
        }
    }

    private void UpdateMove4()
    {
        var moveid = TempPokemon.GetMoveIDs[3];
        if (moveid == 0)
        {
            cbMove4.SelectedIndex = -1;
            numMove4PPUps.Enabled = false;
            numMove4PP.Enabled = false;
            lblMove4Accuracy.Text = string.Empty;
            lblMove4Flavor.Text = string.Empty;
            lblMove4Power.Text = string.Empty;
            numMove4PP.Value = 0M;
            numMove4PPUps.Value = 0M;
            txtMove4MaxPP.Text = string.Empty;
            pbMove4Type.Image = null;
            pbMove4Category.Image = null;
        }
        else
        {
            cbMove4.SelectedValue = moveid;
            numMove4PPUps.Enabled = true;
            numMove4PP.Enabled = true;
            var move = (Move)cbMove4.SelectedItem;
            lblMove4Flavor.Text = move.MoveFlavor;
            lblMove4Accuracy.Text = move.MoveAccuracy is not 0 and not 1 ? $"{move.MoveAccuracy:0}%" : "-";
            lblMove4Power.Text = move.MovePower is not 0 and not 1 ? move.MovePower.ToString() : "-";
            numMove4PP.Value = TempPokemon.GetMovePP(3);
            numMove4PPUps.Value = TempPokemon.GetMovePPUp(3);
            txtMove4MaxPP.Text = (move.MoveBasePP + numMove4PPUps.Value * (move.MoveBasePP / 5)).ToString("0");
            pbMove4Type.Image = move.MoveTypeImage;
            pbMove4Category.Image = move.MoveCategoryImage;
            AutosizeFont(ref lblMove4Flavor);
        }
    }

    #endregion

    #region UpdateOriginsInfo
    private void UpdateOriginsInfo()
    {
        numMetLevel.Value = TempPokemon.MetLevel;
        cbIsEgg.Checked = TempPokemon.IsEgg;
        UpdateHatchSteps();
        cbNsPokemon.Checked = TempPokemon.IsNsPokemon;
        cbFateful.Checked = TempPokemon.IsFateful;
        cbGame.SelectedValue = TempPokemon.HometownID;
        cbCountry.SelectedValue = TempPokemon.LanguageID;
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
    private static void UpdateRibbonsInfo()
    {
        // TODO: Ribbons
    }

    #endregion

    #region UpdateMiscInfo
    private static void UpdateMiscInfo()
    {
        // TODO: Misc
    }

    #endregion

    private static void SetControlFont(ref NumericUpDown control, bool bold = false) =>
        control.Font = bold ? new Font(control.Font, FontStyle.Bold) : new Font(control.Font, FontStyle.Regular);

    private static void SetControlFont(ref TextBox control, bool bold = false) =>
        control.Font = bold ? new Font(control.Font, FontStyle.Bold) : new Font(control.Font, FontStyle.Regular);

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
        if (TempPokemon.SpeciesID == 0)
        {
            return;
        }
        var formnames = GetPKMFormNames(TempPokemon.SpeciesID);
        if (formnames.Length != 0)
        {
            if (!(formnames.Length == 1 && formnames[0] == string.Empty))
            {
                cbForm.Items.AddRange(GetPKMFormNames(TempPokemon.SpeciesID));
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

    private void SetItems()
    {
        var items = new List<Item>();
        var item = new Item();
        items.Add(item);
        for (ushort itemindex = 0; itemindex <= 0x027E; itemindex++)
        {
            item = new Item(itemindex);
            if (item.ItemName is { Length: > 0 })
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
        var abilities = new List<Ability>();
        for (ushort abilityindex = 0; abilityindex <= 164; abilityindex++)
        {
            var ability = new Ability(abilityindex);
            if (ability is { AbilityName.Length: > 0, AbilityID: not 0 })
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
        var species_l = new List<Species>();
        for (ushort speciesindex = 0; speciesindex <= 649; speciesindex++)
        {
            var species = new Species(speciesindex);
            if (species is { SpeciesName.Length: > 0, SpeciesID: not 0 })
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
        var balls = new List<Ball>();
        for (byte ballindex = 0; ballindex <= 25; ballindex++)
        {
            var ball = new Ball(ballindex);
            if (ball.BallName is { Length: > 0 })
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
        var natures = new List<Nature>();
        for (byte natureindex = 0; natureindex <= 24; natureindex++)
        {
            var nature = new Nature(natureindex);
            if (nature.NatureName is { Length: > 0 })
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
        var metlocations = new List<Location>();
        var egglocations = new List<Location>();
        Location location;
        for (ushort locationindex = 0; locationindex <= 153; locationindex++)
        {
            location = new Location(locationindex);
            if (location is { LocationName.Length: > 0, LocationID: not 0 })
            {
                metlocations.Add(location);
                egglocations.Add(location);
            }
        }
        location = new Location(2000);
        metlocations.Add(location);
        egglocations.Add(location);
        location = new Location(30001);
        metlocations.Add(location);
        egglocations.Add(location);
        location = new Location(30012);
        metlocations.Add(location);
        egglocations.Add(location);
        location = new Location(30013);
        metlocations.Add(location);
        egglocations.Add(location);
        location = new Location(30015);
        metlocations.Add(location);
        egglocations.Add(location);
        location = new Location(40001);
        metlocations.Add(location);
        egglocations.Add(location);
        location = new Location(40069);
        metlocations.Add(location);
        egglocations.Add(location);
        location = new Location(60002);
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
        var moves1 = new List<Move>();
        var moves2 = new List<Move>();
        var moves3 = new List<Move>();
        var moves4 = new List<Move>();
        for (ushort moveindex = 0; moveindex <= 559; moveindex++)
        {
            var move = new Move(moveindex);
            if (move is { MoveName.Length: > 0, MoveID: not 0 })
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
        var hometowns = new List<Hometown>();
        for (byte hometownindex = 0; hometownindex <= 25; hometownindex++)
        {
            var hometown = new Hometown(hometownindex);
            if (hometown is { HometownName.Length: > 0, HometownID: not 0 })
            {
                hometowns.Add(hometown);
            }
        }
        cbGame.DataSource = hometowns;
        cbGame.DisplayMember = "HometownName";
        cbGame.ValueMember = "HometownID";
    }

    private void SetCountries()
    {
        var countries = new List<Country>();
        for (byte countryindex = 0; countryindex <= 8; countryindex++)
        {
            if (countryindex != 6)
            {
                var country = new Country(countryindex);
                if (country is { CountryName.Length: > 0, CountryID: not 0 })
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
        SharedPokemon = TempPokemon.Clone();
        SharedPartyPokemon = new PartyPokemon
        {
            PokemonData = TempPokemon.Clone()
        };
        Close();
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
        TempPokemon.FixChecksum();
        SharedPokemon = TempPokemon.Clone();
        SharedPartyPokemon = new PartyPokemon
        {
            PokemonData = TempPokemon.Clone()
        };
        CheckApplyButton();
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        fileSave.FileName = string.Empty;
        if (fileSave.ShowDialog() != DialogResult.Cancel && fileSave.FileName != string.Empty)
        {
            TempPokemon.WriteToFile(fileSave.FileName, Path.GetExtension(fileSave.FileName).ToLower() == ".ek5");
        }
    }

    private void CheckApplyButton() => btnApply.Enabled = TempPokemon.IsModified;

    private void pbCircle_Click(object sender, EventArgs e)
    {
        TempPokemon.Circle = !TempPokemon.Circle;
        pbCircle.Image = GetMarkingImage(Markings.Circle, TempPokemon.Circle);
        CheckApplyButton();
    }

    private void pbTriangle_Click(object sender, EventArgs e)
    {
        TempPokemon.Triangle = !TempPokemon.Triangle;
        pbTriangle.Image = GetMarkingImage(Markings.Triangle, TempPokemon.Triangle);
        CheckApplyButton();
    }

    private void pbSquare_Click(object sender, EventArgs e)
    {
        TempPokemon.Square = !TempPokemon.Square;
        pbSquare.Image = GetMarkingImage(Markings.Square, TempPokemon.Square);
        CheckApplyButton();
    }

    private void pbHeart_Click(object sender, EventArgs e)
    {
        TempPokemon.Heart = !TempPokemon.Heart;
        pbHeart.Image = GetMarkingImage(Markings.Heart, TempPokemon.Heart);
        CheckApplyButton();
    }

    private void pbStar_Click(object sender, EventArgs e)
    {
        TempPokemon.Star = !TempPokemon.Star;
        pbStar.Image = GetMarkingImage(Markings.Star, TempPokemon.Star);
        CheckApplyButton();
    }

    private void pbDiamond_Click(object sender, EventArgs e)
    {
        TempPokemon.Diamond = !TempPokemon.Diamond;
        pbDiamond.Image = GetMarkingImage(Markings.Diamond, TempPokemon.Diamond);
        CheckApplyButton();
    }

    private void frmPKMViewer_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (TempPokemon.IsModified && MessageBox.Show("Cancel changes?", "Close", MessageBoxButtons.YesNo) == DialogResult.No)
        {
            e.Cancel = true;
        }
    }

    private void cbHeldItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet || cbHeldItem.SelectedIndex == -1)
        {
            return;
        }
        TempPokemon.ItemID = (ushort)cbHeldItem.SelectedValue;
        pbHeldItem.Image = ((Item)cbHeldItem.SelectedItem).ItemImage;
        lblHeldItemFlavor.Text = ((Item)cbHeldItem.SelectedItem).ItemFlavor;
        CheckApplyButton();
    }

    private void chkNicknamed_CheckedChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.IsNicknamed = chkNicknamed.Checked;
        CheckApplyButton();
    }

    private void txtNickname_TextChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet || txtNickname.Text.Length == 0)
        {
            return;
        }
        TempPokemon.Nickname = txtNickname.Text;
        chkNicknamed.Checked = true;
        CheckApplyButton();
    }

    private void txtOTName_TextChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet || txtOTName.Text.Length == 0)
        {
            return;
        }
        TempPokemon.OTName = txtOTName.Text;
        CheckApplyButton();
    }

    private void numTID_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.TID = (ushort)numTID.Value;
        UpdateSprite();
        UpdateShiny();
        CheckApplyButton();
    }

    private void numSID_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SID = (ushort)numSID.Value;
        UpdateSprite();
        UpdateShiny();
        CheckApplyButton();
    }

    private void rbOTMale_CheckedChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        if (rbOTMale.Checked)
        {
            TempPokemon.OTGenderID = 0;
            txtOTName.ForeColor = Color.Blue;
        }
        CheckApplyButton();
    }

    private void rbOTFemale_CheckedChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        if (rbOTFemale.Checked)
        {
            TempPokemon.OTGenderID = 1;
            txtOTName.ForeColor = Color.Red;
        }
        CheckApplyButton();
    }

    private void cbAbility_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet || cbAbility.SelectedIndex == -1)
        {
            return;
        }
        TempPokemon.AbilityID = (ushort)cbAbility.SelectedValue;
        lblAbilityFlavor.Text = ((Ability)cbAbility.SelectedItem).AbilityFlavor;
        CheckApplyButton();
    }

    private void numEXP_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.EXP = (uint)numEXP.Value;
        UISet = false;
        UpdateLevel();
        UpdateEXP();
        UISet = true;
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void cbBall_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet || cbBall.SelectedIndex == -1)
        {
            return;
        }
        TempPokemon.BallID = (byte)cbBall.SelectedValue;
        UpdateBall();
        CheckApplyButton();
    }

    private void cbSpecies_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet || cbSpecies.SelectedIndex == -1)
        {
            return;
        }
        TempPokemon.SpeciesID = (ushort)cbSpecies.SelectedValue;
        numSpecies.Value = TempPokemon.SpeciesID;
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

    private void numSpecies_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet || (int)(numSpecies.Value - 1) == cbSpecies.SelectedIndex)
        {
            return;
        }
        cbSpecies.SelectedIndex = (int)(numSpecies.Value - 1);
    }

    private void cbForm_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.FormID = (byte)cbForm.SelectedIndex;
        UpdateSprite();
        UpdateFormIcon();
        UpdateTypes();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numLevel_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.Level = (int)numLevel.Value;
        UISet = false;
        UpdateEXP();
        UISet = true;
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numHPIV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetIV(0, (int)numHPIV.Value);
        SetControlFont(ref numHPIV, numHPIV.Value == numHPIV.Maximum);
        UpdateCharacteristic();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numAtkIV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetIV(1, (int)numAtkIV.Value);
        SetControlFont(ref numAtkIV, numAtkIV.Value == numAtkIV.Maximum);
        UpdateCharacteristic();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numDefIV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetIV(2, (int)numDefIV.Value);
        SetControlFont(ref numDefIV, numDefIV.Value == numDefIV.Maximum);
        UpdateCharacteristic();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numSpAtkIV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetIV(3, (int)numSpAtkIV.Value);
        SetControlFont(ref numSpAtkIV, numSpAtkIV.Value == numSpAtkIV.Maximum);
        UpdateCharacteristic();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numSpDefIV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetIV(4, (int)numSpDefIV.Value);
        SetControlFont(ref numSpDefIV, numSpDefIV.Value == numSpDefIV.Maximum);
        UpdateCharacteristic();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numSpeedIV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetIV(5, (int)numSpeedIV.Value);
        SetControlFont(ref numSpeedIV, numSpeedIV.Value == numSpeedIV.Maximum);
        UpdateCharacteristic();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numHPEV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetEV(0, (int)numHPEV.Value);
        SetControlFont(ref numHPEV, numHPEV.Value >= 252M);
        UpdateTotalEVs();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numAtkEV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetEV(1, (int)numAtkEV.Value);
        SetControlFont(ref numAtkEV, numAtkEV.Value >= 252M);
        UpdateTotalEVs();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numDefEV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetEV(2, (int)numDefEV.Value);
        SetControlFont(ref numDefEV, numDefEV.Value >= 252M);
        UpdateTotalEVs();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numSpAtkEV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetEV(3, (int)numSpAtkEV.Value);
        SetControlFont(ref numSpAtkEV, numSpAtkEV.Value >= 252M);
        UpdateTotalEVs();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numSpDefEV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetEV(4, (int)numSpDefEV.Value);
        SetControlFont(ref numSpDefEV, numSpDefEV.Value >= 252M);
        UpdateTotalEVs();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numSpeedEV_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetEV(5, (int)numSpeedEV.Value);
        SetControlFont(ref numSpeedEV, numSpeedEV.Value >= 252M);
        UpdateTotalEVs();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void cbNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.NatureID = (byte)cbNature.SelectedValue;
        UpdateNature();
        UpdateCalculatedStats();
        CheckApplyButton();
    }

    private void numTameness_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.Tameness = (int)numTameness.Value;
        SetControlFont(ref numTameness, numTameness.Value == numTameness.Maximum);
        UpdateHatchSteps();
        CheckApplyButton();
    }

    private void cbMove1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetMoveID(0, (ushort)cbMove1.SelectedValue);
        numMove1PP.Value = ((Move)cbMove1.SelectedItem).MoveBasePP;
        UpdateMove1();
        CheckApplyButton();
    }

    private void cbMove2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        if (cbMove2.SelectedValue is null)
        {
            TempPokemon.SetMoveID(1, 0);
            numMove2PPUps.Enabled = false;
            numMove2PP.Enabled = false;
        }
        else
        {
            TempPokemon.SetMoveID(1, (ushort)cbMove2.SelectedValue);
            numMove2PPUps.Enabled = true;
            numMove2PP.Enabled = true;
            numMove2PP.Value = ((Move)cbMove2.SelectedItem).MoveBasePP;
        }
        UpdateMove2();
        CheckApplyButton();
    }

    private void cbMove3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        if (cbMove3.SelectedValue is null)
        {
            TempPokemon.SetMoveID(2, 0);
            numMove3PPUps.Enabled = false;
            numMove3PP.Enabled = false;
        }
        else
        {
            TempPokemon.SetMoveID(2, (ushort)cbMove3.SelectedValue);
            numMove3PPUps.Enabled = true;
            numMove3PP.Enabled = true;
            numMove3PP.Value = ((Move)cbMove3.SelectedItem).MoveBasePP;
        }
        UpdateMove3();
        CheckApplyButton();
    }

    private void cbMove4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        if (cbMove4.SelectedValue is null)
        {
            TempPokemon.SetMoveID(3, 0);
            numMove4PPUps.Enabled = false;
            numMove4PP.Enabled = false;
        }
        else
        {
            TempPokemon.SetMoveID(3, (ushort)cbMove4.SelectedValue);
            numMove4PPUps.Enabled = true;
            numMove4PP.Enabled = true;
            numMove4PP.Value = ((Move)cbMove4.SelectedItem).MoveBasePP;
        }
        UpdateMove4();
        CheckApplyButton();
    }

    private void numMove1PPUps_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetMovePPUp(0, (int)numMove1PPUps.Value);
        var move = (Move)cbMove1.SelectedItem;
        if (move is not null)
        {
            txtMove1MaxPP.Text = (move.MoveBasePP + numMove1PPUps.Value * (move.MoveBasePP / 5)).ToString("0");
        }
        CheckApplyButton();
    }

    private void numMove1PP_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetMovePP(0, (int)numMove1PP.Value);
        CheckApplyButton();
    }

    private void numMove2PPUps_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetMovePPUp(1, (int)numMove2PPUps.Value);
        var move = (Move)cbMove2.SelectedItem;
        if (move is not null)
        {
            txtMove2MaxPP.Text = (move.MoveBasePP + numMove2PPUps.Value * (move.MoveBasePP / 5)).ToString("0");
        }
        CheckApplyButton();
    }

    private void numMove2PP_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetMovePP(1, (int)numMove2PP.Value);
        CheckApplyButton();
    }

    private void numMove3PPUps_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetMovePPUp(2, (int)numMove3PPUps.Value);
        var move = (Move)cbMove3.SelectedItem;
        if (move is not null)
        {
            txtMove3MaxPP.Text = (move.MoveBasePP + numMove3PPUps.Value * (move.MoveBasePP / 5)).ToString("0");
        }
        CheckApplyButton();
    }

    private void numMove3PP_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetMovePP(2, (int)numMove3PP.Value);
        CheckApplyButton();
    }

    private void numMove4PPUps_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetMovePPUp(3, (int)numMove4PPUps.Value);
        var move = (Move)cbMove4.SelectedItem;
        if (move is not null)
        {
            txtMove4MaxPP.Text = (move.MoveBasePP + numMove4PPUps.Value * (move.MoveBasePP / 5)).ToString("0");
        }
        CheckApplyButton();
    }

    private void numMove4PP_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.SetMovePP(3, (int)numMove4PP.Value);
        CheckApplyButton();
    }

    private void cbMetLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.MetLocationID = (ushort)cbMetLocation.SelectedValue;
        CheckApplyButton();
    }

    private void dtMetDate_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.MetDate = dtMetDate.Value;
        CheckApplyButton();
    }

    private void cbEggLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.EggLocationID = (ushort)cbEggLocation.SelectedValue;
        CheckApplyButton();
    }

    private void dtEggDate_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.EggDate = dtEggDate.Value;
        CheckApplyButton();
    }

    private void cbMetAsEgg_CheckedChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
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

    private void numMetLevel_ValueChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.MetLevel = (byte)numMetLevel.Value;
        CheckApplyButton();
    }

    private void cbGame_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.HometownID = (byte)cbGame.SelectedValue;
        CheckApplyButton();
    }

    private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.LanguageID = (byte)cbCountry.SelectedValue;
        CheckApplyButton();
    }

    private void cbIsEgg_CheckedChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.IsEgg = cbIsEgg.Checked;
        UpdateHatchSteps();
        cbMetAsEgg.Checked = true;
        cbEggLocation.SelectedIndex = 0;
        dtEggDate.Value = DateTime.Today;
        UpdateSprite();
        UpdateFormIcon();
        CheckApplyButton();
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
            txtMinHatchSteps.Text = string.Empty;
        }
    }

    private void cbNsPokemon_CheckedChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.IsNsPokemon = cbNsPokemon.Checked;
        CheckApplyButton();
    }

    private void cbFateful_CheckedChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.IsFateful = cbFateful.Checked;
        CheckApplyButton();
    }

    private void cbPKRSStrain_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.PokerusStrain = cbPKRSStrain.SelectedIndex;
        UpdatePokerus();
        CheckApplyButton();
    }

    private void cbPKRSDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!UISet || !PokemonSet)
        {
            return;
        }
        TempPokemon.PokerusDays = cbPKRSDays.SelectedIndex;
        UpdatePokerus();
        CheckApplyButton();
    }
}
