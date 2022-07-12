namespace PKMDS_Save_Editor;

public partial class frmMain : Form
{
    private enum Mode
    {
        Single,
        Group,
        Item
    }
    private Mode mode;
    private readonly List<PictureBox> partyPics = new();
    private readonly List<PictureBox> boxPics = new();
    private readonly List<PictureBox> boxGridPics = new();
    private readonly List<Label> boxNameLabels = new();
    private readonly List<Label> boxcountLabels = new();
    private readonly List<Panel> boxPanels = new();
    private readonly string title = string.Empty;
    private string savefile = string.Empty;
    private Save sav;
    private Save tempsav;
    private Pokemon pkm_from = new();
    private Pokemon pkm_to = new();
    private readonly frmPKMViewer pkmviewer = new();
    private bool dragfromparty = false;
    private bool dragtoparty = false;
    private int frombox = -1;
    private int fromslot = -1;
    private int tobox = -1;
    private int toslot = -1;
    private bool uiset = false;
    private readonly string argfilename = string.Empty;
    private readonly Color SelectionColor = Color.FromArgb(100, Color.Orange.R, Color.Orange.G, Color.Orange.B);
    public frmMain(string filename)
    {
        InitializeComponent();
        title = Text;
        argfilename = filename;
    }
    private void loadSaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveFileOpen.FileName = string.Empty;
        if (SaveFileOpen.ShowDialog() != DialogResult.Cancel)
        {
            if (SaveFileOpen.FileName != string.Empty)
            {
                try
                {
                    savefile = SaveFileOpen.FileName;
                    tempsav = new Save(savefile);
                    //string message = string.Empty;
                    //if (!tempsav.Validate(out message))
                    //{
                    //    throw new Exception(message);
                    //}
                    savesavToolStripMenuItem.Enabled = false;
                    uiset = false;
                    sav = tempsav;
                    SetSaveFile();
                    uiset = true;
                    savesavToolStripMenuItem.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
    private void frmMain_FormClosing(object sender, FormClosingEventArgs e) => PKMDS.SQL.CloseDB();
    private void savesavToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveFileSave.FileName = string.Empty;
        if (SaveFileSave.ShowDialog() != DialogResult.Cancel)
        {
            if (SaveFileSave.FileName != string.Empty)
            {
                sav.WriteToFile(SaveFileSave.FileName);
            }
        }
    }
    private Pokemon ViewPokemon(Pokemon pkm)
    {
        pkmviewer.SetPokemon(pkm);
        pkmviewer.ShowDialog();
        return pkmviewer.SharedPokemon;
    }
    private PartyPokemon ViewPokemon(PartyPokemon ppkm)
    {
        pkmviewer.SetPokemon(ppkm.PokemonData.Clone());
        pkmviewer.ShowDialog();
        var newppkm = new PartyPokemon
        {
            PokemonData = pkmviewer.SharedPokemon.Clone()
        };
        return newppkm;
    }
    private void SetSaveFile()
    {
        splitMain.Enabled = true;
        Text = title + " - " + sav.TrainerName + " (" + sav.TID.ToString("00000") + ")";
        btnPreviousBox.Enabled = sav.CurrentBox != 0;
        btnNextBox.Enabled = sav.CurrentBox != 23;
        txtBoxName.Enabled = true;
        splitMain.Panel2.Enabled = true;
        //gbMode.Enabled = true;
        UpdateParty();
        UpdateBox();
        UpdateBoxWallpaper();
        UpdateBoxName();
        UpdateBoxNameLabels();
        UpdateBoxCountLabels();
        UpdateBoxGrids();
        splitMain.Panel2.VerticalScroll.Value = 70 * sav.CurrentBox;
        splitMain.Panel2.PerformLayout();
        foreach (var pan in boxPanels)
        {
            pan.BorderStyle = BorderStyle.None;
        }
        boxPanels[sav.CurrentBox].BorderStyle = BorderStyle.FixedSingle;
    }
    private void UpdateParty()
    {
        switch (mode)
        {
            case Mode.Single:
                for (var partySlot = 0; partySlot < 6; partySlot++)
                {
                    var pokemon = sav.Party[partySlot].PokemonData;
                    partyPics[partySlot].Image = pokemon.SpeciesID != 0 && partySlot < sav.PartySize ? pokemon.Icon : null;
                }
                break;
            case Mode.Group:

                break;
            case Mode.Item:
                for (var slot = 0; slot < 6; slot++)
                {
                    var pkm = sav.Party[slot].PokemonData;
                    partyPics[slot].Image = pkm.SpeciesID != 0 && pkm.ItemID != 0 && slot < sav.PartySize ? pkm.ItemPic : null;
                }
                break;
        }
        lblPartySize.Text = "Party - " + sav.PartySize.ToString() + " / 6";
        sav.RecalculateParty();
    }
    private void UpdateBox()
    {
        switch (mode)
        {
            case Mode.Single:
                Pokemon pokemon;
                for (var boxSlot = 0; boxSlot < 30; boxSlot++)
                {
                    pokemon = sav.PCStorage[sav.CurrentBox][boxSlot];
                    boxPics[boxSlot].Image = pokemon.SpeciesID != 0 ? pokemon.Icon : null;
                }
                break;
            case Mode.Group:

                break;
            case Mode.Item:
                for (var slot = 0; slot < 30; slot++)
                {
                    var pkm = sav.PCStorage[sav.CurrentBox][slot];
                    boxPics[slot].Image = pkm.SpeciesID != 0 && pkm.ItemID != 0 ? pkm.ItemPic : null;
                }
                break;
            default:
                break;
        }
    }
    private void UpdateBoxWallpaper() => pnlBox.BackgroundImage = sav.BoxWallpapers[sav.CurrentBox].Wallpaper;
    private void UpdateBoxName() => txtBoxName.Text = sav.BoxNames[sav.CurrentBox].Name;
    private void UpdateBoxNameLabel(int box) => boxNameLabels[box].Text = sav.BoxNames[box].Name;
    private void UpdateBoxNameLabels()
    {
        for (var box = 0; box < 24; box++)
        {
            UpdateBoxNameLabel(box);
        }
    }
    private void UpdateBoxCountLabel(int box) => boxcountLabels[box].Text = sav.BoxCount(box).ToString() + "/30";
    private void UpdateBoxCountLabels()
    {
        for (var box = 0; box < 24; box++)
        {
            UpdateBoxCountLabel(box);
        }
    }
    private void UpdateBoxGrid(int box) => boxGridPics[box].Image = sav.PCStorage[box].Grid;
    private void UpdateBoxGrids()
    {
        for (var box = 0; box < 24; box++)
        {
            UpdateBoxGrid(box);
        }
    }
    private void frmMain_Load(object sender, EventArgs e)
    {
        ClearPreview();
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
        boxcountLabels.Add(lblBoxCount01);
        boxcountLabels.Add(lblBoxCount02);
        boxcountLabels.Add(lblBoxCount03);
        boxcountLabels.Add(lblBoxCount04);
        boxcountLabels.Add(lblBoxCount05);
        boxcountLabels.Add(lblBoxCount06);
        boxcountLabels.Add(lblBoxCount07);
        boxcountLabels.Add(lblBoxCount08);
        boxcountLabels.Add(lblBoxCount09);
        boxcountLabels.Add(lblBoxCount10);
        boxcountLabels.Add(lblBoxCount11);
        boxcountLabels.Add(lblBoxCount12);
        boxcountLabels.Add(lblBoxCount13);
        boxcountLabels.Add(lblBoxCount14);
        boxcountLabels.Add(lblBoxCount15);
        boxcountLabels.Add(lblBoxCount16);
        boxcountLabels.Add(lblBoxCount17);
        boxcountLabels.Add(lblBoxCount18);
        boxcountLabels.Add(lblBoxCount19);
        boxcountLabels.Add(lblBoxCount20);
        boxcountLabels.Add(lblBoxCount21);
        boxcountLabels.Add(lblBoxCount22);
        boxcountLabels.Add(lblBoxCount23);
        boxcountLabels.Add(lblBoxCount24);
        boxPanels.Add(pnlBoxGrid01);
        boxPanels.Add(pnlBoxGrid02);
        boxPanels.Add(pnlBoxGrid03);
        boxPanels.Add(pnlBoxGrid04);
        boxPanels.Add(pnlBoxGrid05);
        boxPanels.Add(pnlBoxGrid06);
        boxPanels.Add(pnlBoxGrid07);
        boxPanels.Add(pnlBoxGrid08);
        boxPanels.Add(pnlBoxGrid09);
        boxPanels.Add(pnlBoxGrid10);
        boxPanels.Add(pnlBoxGrid11);
        boxPanels.Add(pnlBoxGrid12);
        boxPanels.Add(pnlBoxGrid13);
        boxPanels.Add(pnlBoxGrid14);
        boxPanels.Add(pnlBoxGrid15);
        boxPanels.Add(pnlBoxGrid16);
        boxPanels.Add(pnlBoxGrid17);
        boxPanels.Add(pnlBoxGrid18);
        boxPanels.Add(pnlBoxGrid19);
        boxPanels.Add(pnlBoxGrid20);
        boxPanels.Add(pnlBoxGrid21);
        boxPanels.Add(pnlBoxGrid22);
        boxPanels.Add(pnlBoxGrid23);
        boxPanels.Add(pnlBoxGrid24);
        foreach (var pb in partyPics)
        {
            pb.AllowDrop = true;
        }
        foreach (var pb in boxPics)
        {
            pb.AllowDrop = true;
        }
        foreach (var pb in boxGridPics)
        {
            pb.AllowDrop = true;
        }
        if (argfilename != string.Empty)
        {
            try
            {
                savefile = argfilename;
                tempsav = new Save(savefile);
                //string message = string.Empty;
                //if (!tempsav.Validate(out message))
                //{
                //    throw new Exception(message);
                //}
                savesavToolStripMenuItem.Enabled = false;
                uiset = false;
                sav = tempsav;
                SetSaveFile();
                uiset = true;
                savesavToolStripMenuItem.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.Message);
            }
        }
        lblPartySize.Text = string.Empty;
    }
    private void txtBoxName_TextChanged(object sender, EventArgs e)
    {
        if (uiset)
        {
            sav.BoxNames[sav.CurrentBox].Name = txtBoxName.Text;
            UpdateBoxNameLabel(sav.CurrentBox);
        }
    }
    private void btnPreviousBox_Click(object sender, EventArgs e)
    {
        sav.CurrentBox--;
        UpdateBox();
        UpdateBoxWallpaper();
        UpdateBoxName();
        btnPreviousBox.Enabled = sav.CurrentBox != 0;
        btnNextBox.Enabled = sav.CurrentBox != 23;
        splitMain.Panel2.VerticalScroll.Value = 70 * sav.CurrentBox;
        splitMain.Panel2.PerformLayout();
        foreach (var pan in boxPanels)
        {
            pan.BorderStyle = BorderStyle.None;
        }
        boxPanels[sav.CurrentBox].BorderStyle = BorderStyle.FixedSingle;
    }
    private void btnNextBox_Click(object sender, EventArgs e)
    {
        sav.CurrentBox++;
        UpdateBox();
        UpdateBoxWallpaper();
        UpdateBoxName();
        btnPreviousBox.Enabled = sav.CurrentBox != 0;
        btnNextBox.Enabled = sav.CurrentBox != 23;
        splitMain.Panel2.VerticalScroll.Value = 70 * sav.CurrentBox;
        splitMain.Panel2.PerformLayout();
        foreach (var pan in boxPanels)
        {
            pan.BorderStyle = BorderStyle.None;
        }
        boxPanels[sav.CurrentBox].BorderStyle = BorderStyle.FixedSingle;
    }
    private void pbSlot_DoubleClick(object sender, EventArgs e)
    {
        var pb = (PictureBox)sender;
        int slot;
        if (pb.Name.Contains("Party"))
        {
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
            slot--;
            var pkm = sav.Party[slot].PokemonData;
            if (pkm != null && pkm.SpeciesID != 0)
            {
                sav.Party[slot] = ViewPokemon(sav.Party[slot]);
                UpdateParty();
            }
        }
        if (pb.Name.Contains("Box"))
        {
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
            slot--;
            var pkm = sav.PCStorage[sav.CurrentBox][slot];
            if (pkm != null && pkm.SpeciesID != 0)
            {
                sav.PCStorage[sav.CurrentBox][slot] = ViewPokemon(sav.PCStorage[sav.CurrentBox][slot]);
            }
            UpdateBox();
            UpdateBoxGrid(sav.CurrentBox);
        }
    }
    private void pbPartyBoxSlot_MouseDown(object sender, MouseEventArgs e)
    {
        var pb = (PictureBox)sender;
        if (pb.Image != null)
        {
            //this.Cursor = CreateCursor(pb.Image, 3, 3);
            //pb.Image = null;
        }
        if (e.Button == MouseButtons.Left && e.Clicks == 1)
        {
            switch (mode)
            {
                case Mode.Single:
                    int slot;
                    int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                    slot--;
                    if (pb.Name.Contains("Party"))
                    {
                        if (sav.Party[slot].PokemonData.SpeciesID != 0)
                        {
                            if (sav.PartySize > 1)
                            {
                                pkm_from = sav.Party[slot].PokemonData;
                                dragfromparty = true;
                                frombox = -1;
                                fromslot = slot;
                                partyPics[slot].DoDragDrop(pkm_from, DragDropEffects.Move);
                            }
                        }
                    }
                    if (pb.Name.Contains("Box"))
                    {
                        if (sav.PCStorage[sav.CurrentBox][slot].SpeciesID != 0)
                        {
                            pkm_from = sav.PCStorage[sav.CurrentBox][slot];
                            dragfromparty = false;
                            frombox = sav.CurrentBox;
                            fromslot = slot;
                            boxPics[slot].DoDragDrop(pkm_from, DragDropEffects.Move);
                        }
                    }
                    break;
                case Mode.Group:

                    break;
                case Mode.Item:

                    break;
                default:
                    break;
            }
        }
    }
    private void pbPartyBoxSlot_DragDrop(object sender, DragEventArgs e)
    {
        var pb = (PictureBox)sender;
        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out var slot);
        slot--;
        if (pb.Name.Contains("Party"))
        {
            dragtoparty = true;
            //dragtobox = false;
            tobox = -1;
            toslot = slot;
            if (dragfromparty)
            {
                if (dragtoparty)
                {
                    if (pkm_to.SpeciesID == 0)
                    {
                        sav.WithdrawPokemon(sav.Party[fromslot].PokemonData);
                        sav.RemovePartyPokemon(fromslot);
                    }
                    else
                    {
                        SwapPartyParty(sav, fromslot, toslot);
                    }
                    UpdateParty();
                }
            }
            else
            {
                if (dragtoparty)
                {
                    if (pkm_to.SpeciesID == 0)
                    {
                        sav.WithdrawPokemon(sav.PCStorage[frombox][fromslot]);
                        sav.RemoveStoredPokemon(frombox, fromslot);
                    }
                    else
                    {
                        SwapBoxParty(sav, frombox, fromslot, toslot);
                    }
                    UpdateParty();
                    UpdateBox();
                    UpdateBoxGrid(sav.CurrentBox);
                    UpdateBoxCountLabel(sav.CurrentBox);
                }
            }
        }
        if (pb.Name.Contains("Box"))
        {
            dragtoparty = false;
            //dragtobox = false;
            tobox = sav.CurrentBox;
            toslot = slot;
            if (dragfromparty)
            {
                if (!dragtoparty)
                {
                    SwapPartyBox(sav, fromslot, tobox, toslot);
                    if (pkm_to.SpeciesID == 0)
                    {
                        sav.RemovePartyPokemon(fromslot);
                    }
                    UpdateParty();
                    UpdateBox();
                    UpdateBoxGrid(sav.CurrentBox);
                    UpdateBoxCountLabel(sav.CurrentBox);
                }
            }
            else
            {
                if (!dragtoparty)
                {
                    SwapBoxBox(sav, frombox, fromslot, tobox, toslot);
                    UpdateParty();
                    UpdateBox();
                    UpdateBoxGrid(sav.CurrentBox);
                    UpdateBoxCountLabel(sav.CurrentBox);
                }
            }
        }
        //this.Cursor = Cursors.Arrow;
    }
    private void pbPartyBoxSlot_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data != null)
        {
            var dragpkm = (Pokemon)e.Data.GetData("PKMDS_CS.PKMDS+Pokemon");
            if (dragpkm is null)
            {
                return;
            }
            if (dragpkm.SpeciesID != 0)
            {
                var pb = (PictureBox)sender;
                int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out var slot);
                slot--;
                if (pb.Name.Contains("Party"))
                {
                    dragtoparty = true;
                    //dragtobox = false;
                    pkm_to = sav.Party[slot].PokemonData;
                }
                if (pb.Name.Contains("Box"))
                {
                    dragtoparty = false;
                    //dragtobox = false;
                    pkm_to = sav.PCStorage[sav.CurrentBox][slot];
                }
                e.Effect = DragDropEffects.Move;
                //this.Cursor = CreateCursor(dragpkm.Icon, 3, 3);
                //this.Cursor = new Cursor(typeof(System.Drawing.Image), "poke_ball");
                //this.Cursor = CreateCursor(dragpkm.Icon, 3, 3);
            }
            else
            {
                e.Effect = DragDropEffects.None;
                //this.Cursor = Cursors.Arrow;
                //UpdateBox();
                //UpdateParty();
            }
        }
    }
    private void pbBoxGrid_DragEnter(object sender, DragEventArgs e)
    {
        var pb = (PictureBox)sender;
        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out var box);
        box--;
        if (e.Data != null)
        {
            var dragpkm = (Pokemon)e.Data.GetData("PKMDS_CS.PKMDS+Pokemon");
            if (dragpkm.SpeciesID != 0 && sav.BoxCount(box) < 30)
            {
                pkm_to = dragpkm;
                e.Effect = DragDropEffects.Move;
                //this.Cursor = CreateCursor(dragpkm.Icon, 3, 3);
                //this.Cursor = new Cursor(typeof(System.Drawing.Image), "poke_ball");
                //this.Cursor = CreateCursor(dragpkm.Icon, 3, 3);
            }
            else
            {
                e.Effect = DragDropEffects.None;
                //this.Cursor = Cursors.Arrow;
                //UpdateBox();
                //UpdateParty();
            }
        }
    }
    private void pbBoxGrid_DragDrop(object sender, DragEventArgs e)
    {
        var pb = (PictureBox)sender;
        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out var box);
        box--;
        dragtoparty = false;
        //dragtobox = true;
        tobox = box;
        toslot = -1;
        if (fromslot != -1)
        {
            if (dragfromparty)
            {
                sav.DepositPokemon(sav.Party[fromslot].PokemonData, tobox);
                sav.RemovePartyPokemon(fromslot);
                if (pkm_to.SpeciesID == 0)
                {
                    sav.RemovePartyPokemon(fromslot);
                }
                UpdateParty();
            }
            else
            {
                if (frombox != -1)
                {
                    sav.DepositPokemon(sav.PCStorage[frombox][fromslot], tobox);
                    sav.RemoveStoredPokemon(frombox, fromslot);
                    UpdateBox();
                    UpdateBoxGrid(frombox);
                    UpdateBoxCountLabel(frombox);
                }
            }
            UpdateBoxGrid(tobox);
            UpdateBoxCountLabel(tobox);
            UpdateBox();
        }
        //this.Cursor = Cursors.Arrow;
    }
    private void pbBoxGrid_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left && e.Clicks == 1)
        {
            switch (mode)
            {
                case Mode.Single:
                    var pb = (PictureBox)sender;
                    int box;
                    int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out box);
                    box--;
                    dragtoparty = false;
                    //dragtobox = true;
                    tobox = box;
                    toslot = -1;
                    break;
                case Mode.Group:

                    break;
                case Mode.Item:

                    break;
                default:
                    break;
            }

        }
    }
    private void lblBoxGrid_Click(object sender, EventArgs e)
    {
        var pb = (Label)sender;
        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out var box);
        box--;
        sav.CurrentBox = Convert.ToByte(box);
        UpdateBox();
        UpdateBoxWallpaper();
        UpdateBoxName();
        btnPreviousBox.Enabled = sav.CurrentBox != 0;
        btnNextBox.Enabled = sav.CurrentBox != 23;
        foreach (var pan in boxPanels)
        {
            pan.BorderStyle = BorderStyle.None;
        }
        boxPanels[box].BorderStyle = BorderStyle.FixedSingle;
    }
    private void lblBoxGrid_MouseEnter(object sender, EventArgs e)
    {
        var pb = (Label)sender;
        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out var box);
        box--;
        boxPanels[box].BackColor = SelectionColor;
        splitMain.Panel2.Focus();
    }
    private void lblBoxGrid_MouseLeave(object sender, EventArgs e)
    {
        var pb = (Label)sender;
        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out var box);
        box--;
        boxPanels[box].BackColor = Color.Transparent;
    }
    private void pbBoxGrid_Click(object sender, EventArgs e)
    {
        var pb = (PictureBox)sender;
        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out var box);
        box--;
        sav.CurrentBox = Convert.ToByte(box);
        UpdateBox();
        UpdateBoxWallpaper();
        UpdateBoxName();
        btnPreviousBox.Enabled = sav.CurrentBox != 0;
        btnNextBox.Enabled = sav.CurrentBox != 23;
        foreach (var pan in boxPanels)
        {
            pan.BorderStyle = BorderStyle.None;
        }
        boxPanels[box].BorderStyle = BorderStyle.FixedSingle;
    }
    private void pbBoxGrid_MouseEnter(object sender, EventArgs e)
    {
        var pb = (PictureBox)sender;
        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out var box);
        box--;
        dragtoparty = false;
        //dragtobox = true;
        tobox = box;
        toslot = -1;
        boxPanels[box].BackColor = SelectionColor;
        splitMain.Panel2.Focus();
    }
    private void pbBoxGrid_MouseLeave(object sender, EventArgs e)
    {
        var pb = (PictureBox)sender;
        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out var box);
        box--;
        boxPanels[box].BackColor = Color.Transparent;
    }
    private void pbPartyBoxSlot_Click(object sender, EventArgs e)
    {
        //PictureBox pb = (PictureBox)(sender);
        //foreach (PictureBox pb_ in partyPics)
        //{
        //    pb_.BorderStyle = BorderStyle.None;
        //}
        //foreach (PictureBox pb_ in boxPics)
        //{
        //    pb_.BorderStyle = BorderStyle.None;
        //}
        //pb.BorderStyle = BorderStyle.FixedSingle;
    }
    private void pbPartyBoxSlot_MouseEnter(object sender, EventArgs e)
    {
        var pb = (PictureBox)sender;
        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out var slot);
        slot--;
        var pkm = new Pokemon();
        if (pb.Name.Contains("Party"))
        {
            if (sav.Party[slot].PokemonData.SpeciesID != 0)
            {
                if (sav.PartySize >= 1)
                {
                    pkm = sav.Party[slot].PokemonData;
                }
            }
        }
        if (pb.Name.Contains("Box"))
        {
            if (sav.PCStorage[sav.CurrentBox][slot].SpeciesID != 0)
            {
                pkm = sav.PCStorage[sav.CurrentBox][slot];
            }
        }
        pb.BackColor = SelectionColor;
        if (pkm.SpeciesID != 0)
        {
            PreviewPokemon(pkm);
        }
        else
        {
            ClearPreview();
        }
    }
    private void pbPartyBoxSlot_MouseLeave(object sender, EventArgs e)
    {
        var pb = (PictureBox)sender;
        pb.BackColor = Color.Transparent;
        ClearPreview();
    }
    private void PreviewPokemon(Pokemon pkm)
    {
        pbSprite.Image = pkm.Sprite;
        pbGender.Image = pkm.GenderIcon;
        pbHeldItem.Image = pkm.ItemPic;
        pbBall.Image = pkm.BallPic;
        pbShiny.Image = pkm.ShinyIcon;
        lblHeldItem.Text = GetItemName(pkm.ItemID);
        lblNickname.Text = pkm.Nickname;
        lblLevel.Text = "Level " + pkm.Level.ToString();
    }
    private void ClearPreview()
    {
        pbSprite.Image = null;
        pbGender.Image = null;
        pbHeldItem.Image = null;
        pbBall.Image = null;
        pbShiny.Image = null;
        lblNickname.Text = string.Empty;
        lblLevel.Text = string.Empty;
        lblHeldItem.Text = string.Empty;
    }
    private void splitMain_Panel2_MouseEnter(object sender, EventArgs e) => splitMain.Panel2.Focus();
    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (sender is ToolStripItem menuItem)
        {
            if (menuItem.Owner is ContextMenuStrip owner)
            {
                var pb = (PictureBox)owner.SourceControl;
                int slot;
                if (pb.Name.Contains("Party"))
                {
                    int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                    slot--;
                    var pkm = sav.Party[slot].PokemonData;
                    if (pkm != null && pkm.SpeciesID != 0)
                    {
                        sav.Party[slot] = ViewPokemon(sav.Party[slot]);
                        UpdateParty();
                    }
                }
                if (pb.Name.Contains("Box"))
                {
                    int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                    slot--;
                    var pkm = sav.PCStorage[sav.CurrentBox][slot];
                    if (pkm != null && pkm.SpeciesID != 0)
                    {
                        sav.PCStorage[sav.CurrentBox][slot] = ViewPokemon(sav.PCStorage[sav.CurrentBox][slot]);
                    }
                    UpdateBox();
                    UpdateBoxGrid(sav.CurrentBox);
                    UpdateBoxCountLabel(sav.CurrentBox);
                }
            }
        }
    }
    private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (sender is ToolStripItem menuItem)
        {
            if (menuItem.Owner is ContextMenuStrip owner)
            {
                var pkm = new Pokemon();
                var pb = (PictureBox)owner.SourceControl;
                int slot;
                if (pb.Name.Contains("Party"))
                {
                    int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                    slot--;
                    pkmFileOpen.FileName = string.Empty;
                    if (pkmFileOpen.ShowDialog() != DialogResult.Cancel)
                    {
                        if (pkmFileOpen.FileName != string.Empty)
                        {
                            if (File.Exists(pkmFileOpen.FileName))
                            {
                                var file = new FileInfo(pkmFileOpen.FileName);
                                pkm = ReadPokemonFile(pkmFileOpen.FileName, file.Extension.ToLower() == "ek6");
                                if (pkm.SpeciesID != 0)
                                {
                                    var ppkm = new PartyPokemon
                                    {
                                        PokemonData = pkm
                                    };
                                    if (sav.Party[slot].PokemonData.SpeciesID == 0)
                                    {
                                        sav.WithdrawPokemon(ppkm.PokemonData);
                                    }
                                    else
                                    {
                                        sav.Party[slot] = ppkm;
                                    }
                                    UpdateParty();
                                }
                            }
                        }
                    }
                }
                if (pb.Name.Contains("Box"))
                {
                    int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                    slot--;
                    pkmFileOpen.FileName = string.Empty;
                    if (pkmFileOpen.ShowDialog() != DialogResult.Cancel)
                    {
                        if (pkmFileOpen.FileName != string.Empty)
                        {
                            if (File.Exists(pkmFileOpen.FileName))
                            {
                                var file = new FileInfo(pkmFileOpen.FileName);
                                pkm = ReadPokemonFile(pkmFileOpen.FileName, file.Extension.ToLower() == "ek6");
                                if (pkm.SpeciesID != 0)
                                {
                                    sav.PCStorage[sav.CurrentBox][slot] = pkm;
                                    UpdateBox();
                                    UpdateBoxGrid(sav.CurrentBox);
                                    UpdateBoxCountLabel(sav.CurrentBox);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void exportToFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (sender is ToolStripItem menuItem)
        {
            if (menuItem.Owner is ContextMenuStrip owner)
            {
                var pkm = new Pokemon();
                var pb = (PictureBox)owner.SourceControl;
                int slot;
                if (pb.Name.Contains("Party"))
                {
                    int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                    slot--;
                    pkm = sav.Party[slot].PokemonData;
                }
                if (pb.Name.Contains("Box"))
                {
                    int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                    slot--;
                    pkm = sav.PCStorage[sav.CurrentBox][slot];
                }
                if (pkm.SpeciesID == 0)
                {
                    MessageBox.Show("Cannot export a null Pokemon!");
                }
                else
                {
                    pkmFileSave.FileName = pkm.Nickname + "_" + pkm.PID.ToString("X8");
                    if (pkmFileSave.ShowDialog() != DialogResult.Cancel)
                    {
                        if (pkmFileSave.FileName != string.Empty)
                        {
                            var file = new FileInfo(pkmFileSave.FileName);
                            pkm.WriteToFile(pkmFileSave.FileName, file.Extension.ToLower() == "ek6");
                        }
                    }
                }
            }
        }
    }
    private void rbSingle_CheckedChanged(object sender, EventArgs e)
    {
        if (uiset)
        {
            mode = Mode.Single;
            UpdateBox();
            UpdateParty();
        }
    }
    private void rbGroup_CheckedChanged(object sender, EventArgs e)
    {
        if (uiset)
        {
            mode = Mode.Group;
            UpdateBox();
            UpdateParty();
            // TODO: group Pokemon sorting

        }
    }
    private void rbItems_CheckedChanged(object sender, EventArgs e)
    {
        if (uiset)
        {
            mode = Mode.Item;
            UpdateBox();
            UpdateParty();
        }
    }
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
    [DllImport("user32.dll")]
    public static extern IntPtr CreateIconIndirect(ref IconInfo icon);
    public static Cursor CreateCursor(Bitmap bmp, int xHotSpot, int yHotSpot)
    {
        bmp = ChangeImageOpacity(bmp, 0.65);
        var ptr = bmp.GetHicon();
        var tmp = new IconInfo();
        GetIconInfo(ptr, ref tmp);
        tmp.xHotspot = xHotSpot;
        tmp.yHotspot = yHotSpot;
        tmp.fIcon = false;
        ptr = CreateIconIndirect(ref tmp);
        return new Cursor(ptr);
    }
    public static Cursor CreateCursor(Image img, int xHotSpot, int yHotSpot) => CreateCursor((Bitmap)img, xHotSpot, yHotSpot);

    private const int bytesPerPixel = 4;

    public static Bitmap ChangeImageOpacity(Bitmap originalImage, double opacity)
    {
        if ((originalImage.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
        {
            // Cannot modify an image with indexed colors
            return originalImage;
        }

        var bmp = (Bitmap)originalImage.Clone();

        // Specify a pixel format.
        var pxf = PixelFormat.Format32bppArgb;

        // Lock the bitmap's bits.
        var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
        var bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

        // Get the address of the first line.
        var ptr = bmpData.Scan0;

        // Declare an array to hold the bytes of the bitmap.
        // This code is specific to a bitmap with 32 bits per pixels 
        // (32 bits = 4 bytes, 3 for RGB and 1 byte for alpha).
        var numBytes = bmp.Width * bmp.Height * bytesPerPixel;
        var argbValues = new byte[numBytes];

        // Copy the ARGB values into the array.
        Marshal.Copy(ptr, argbValues, 0, numBytes);

        // Manipulate the bitmap, such as changing the
        // RGB values for all pixels in the the bitmap.
        for (var counter = 0; counter < argbValues.Length; counter += bytesPerPixel)
        {
            // argbValues is in format BGRA (Blue, Green, Red, Alpha)

            // If 100% transparent, skip pixel
            if (argbValues[counter + bytesPerPixel - 1] == 0)
            {
                continue;
            }

            var pos = 0;
            pos++; // B value
            pos++; // G value
            pos++; // R value

            argbValues[counter + pos] = (byte)(argbValues[counter + pos] * opacity);
        }

        // Copy the ARGB values back to the bitmap
        Marshal.Copy(argbValues, 0, ptr, numBytes);

        // Unlock the bits.
        bmp.UnlockBits(bmpData);

        return bmp;
    }
    public static Bitmap ChangeImageOpacity(Image originalImage, double opacity)
    {
        var bmp = (Bitmap)originalImage.Clone();
        return ChangeImageOpacity(bmp, opacity);
    }
    private void pbPartyBoxSlotBoxGrid_MouseUp(object sender, MouseEventArgs e)
    {
        //this.Cursor = Cursors.Arrow;
        //UpdateBox();
        //UpdateParty();
    }
    private void frmMain_MouseUp(object sender, MouseEventArgs e)
    {
        //this.Cursor = Cursors.Arrow;
        //UpdateBox();
        //UpdateParty();
    }
}
