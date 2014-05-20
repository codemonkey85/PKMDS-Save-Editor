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
        private enum Mode
        {
            Single,
            Group,
            Item
        }
        private Mode mode;
        private List<PictureBox> partyPics = new List<PictureBox>();
        private List<PictureBox> boxPics = new List<PictureBox>();
        private List<PictureBox> boxGridPics = new List<PictureBox>();
        private List<Label> boxNameLabels = new List<Label>();
        private List<Label> boxcountLabels = new List<Label>();
        private List<Panel> boxPanels = new List<Panel>();
        string title = "";
        private string savefile = "";
        PKMDS.Save sav = new PKMDS.Save();
        PKMDS.Save tempsav = new PKMDS.Save();
        PKMDS.Pokemon pkm_from = new PKMDS.Pokemon();
        PKMDS.Pokemon pkm_to = new PKMDS.Pokemon();
        frmPKMViewer pkmviewer = new frmPKMViewer();
        bool dragfromparty = false;
        bool dragtoparty = false;
        //bool dragtobox = false;
        int frombox = -1;
        int fromslot = -1;
        int tobox = -1;
        int toslot = -1;
        bool uiset = false;
        string argfilename = "";
        public frmMain(string filename)
        {
            InitializeComponent();
            title = this.Text;
            argfilename = filename;
        }
        private void loadSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileOpen.FileName = "";
            if (SaveFileOpen.ShowDialog() != DialogResult.Cancel)
            {
                if (SaveFileOpen.FileName != "")
                {
                    try
                    {
                        savefile = SaveFileOpen.FileName;
                        tempsav = PKMDS.ReadSaveFile(savefile);
                        string message = "";
                        if (!tempsav.Validate(out message))
                        {
                            throw new Exception(message);
                        }
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
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            PKMDS.SQL.CloseDB();
        }
        private void savesavToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileSave.FileName = "";
            if (SaveFileSave.ShowDialog() != DialogResult.Cancel)
            {
                if (SaveFileSave.FileName != "")
                {
                    sav.WriteToFile(SaveFileSave.FileName);
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
            this.splitMain.Enabled = true;
            this.Text = title + " - " + sav.TrainerName + " (" + sav.TID.ToString("00000") + ")";
            btnPreviousBox.Enabled = (sav.CurrentBox != 0);
            btnNextBox.Enabled = (sav.CurrentBox != 23);
            txtBoxName.Enabled = true;
            splitMain.Panel2.Enabled = true;
            gbMode.Enabled = true;
            UpdateParty();
            UpdateBox();
            UpdateBoxWallpaper();
            UpdateBoxName();
            UpdateBoxNameLabels();
            UpdateBoxCountLabels();
            UpdateBoxGrids();
            splitMain.Panel2.VerticalScroll.Value = (70 * sav.CurrentBox);
            splitMain.Panel2.PerformLayout();
            foreach (Panel pan in boxPanels)
            {
                pan.BorderStyle = BorderStyle.None;
            }
            boxPanels[sav.CurrentBox].BorderStyle = BorderStyle.FixedSingle;
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
            lblPartySize.Text = "Party - " + sav.PartySize.ToString() + " / 6";
            sav.RecalculateParty();
        }
        private void UpdateBox()
        {
            switch (mode)
            {
                case Mode.Single:
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
                    break;
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
        private void UpdateBoxCountLabel(int box)
        {
            boxcountLabels[box].Text = sav.BoxCount(box).ToString() + "/30";
        }
        private void UpdateBoxCountLabels()
        {
            for (int box = 0; box < 24; box++)
            {
                UpdateBoxCountLabel(box);
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
            foreach (PictureBox pb in partyPics)
            {
                ((Control)pb).AllowDrop = true;
            }
            foreach (PictureBox pb in boxPics)
            {
                ((Control)pb).AllowDrop = true;
            }
            foreach (PictureBox pb in boxGridPics)
            {
                ((Control)pb).AllowDrop = true;
            }
            if (argfilename != "")
            {
                try
                {
                    savefile = argfilename;
                    tempsav = PKMDS.ReadSaveFile(savefile);
                    string message = "";
                    if (!tempsav.Validate(out message))
                    {
                        throw new Exception(message);
                    }
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
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
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
            splitMain.Panel2.VerticalScroll.Value = (70 * sav.CurrentBox);
            splitMain.Panel2.PerformLayout();
            foreach (Panel pan in boxPanels)
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
            btnPreviousBox.Enabled = (sav.CurrentBox != 0);
            btnNextBox.Enabled = (sav.CurrentBox != 23);
            splitMain.Panel2.VerticalScroll.Value = (70 * sav.CurrentBox);
            splitMain.Panel2.PerformLayout();
            foreach (Panel pan in boxPanels)
            {
                pan.BorderStyle = BorderStyle.None;
            }
            boxPanels[sav.CurrentBox].BorderStyle = BorderStyle.FixedSingle;
        }
        private void pbSlot_DoubleClick(object sender, EventArgs e)
        {
            int slot = 0;
            PictureBox pb = (PictureBox)(sender);
            if (pb.Name.Contains("Party"))
            {
                int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                slot--;
                PKMDS.Pokemon pkm = sav.GetPartyPokemon(slot).PokemonData;
                if ((pkm != null) && (pkm.SpeciesID != 0))
                {
                    sav.SetPartyPokemon(ViewPokemon(sav.GetPartyPokemon(slot)), slot);
                    UpdateParty();
                }
            }
            if (pb.Name.Contains("Box"))
            {
                int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                slot--;
                PKMDS.Pokemon pkm = sav.GetStoredPokemon(sav.CurrentBox, slot);
                if ((pkm != null) && (pkm.SpeciesID != 0))
                {
                    sav.SetStoredPokemon(ViewPokemon(sav.GetStoredPokemon(sav.CurrentBox, slot)), sav.CurrentBox, slot);
                }
                UpdateBox();
                UpdateBoxGrid(sav.CurrentBox);
            }
        }
        private void pbPartyBoxSlot_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                int slot = 0;
                PictureBox pb = (PictureBox)(sender);
                int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                slot--;
                if (pb.Name.Contains("Party"))
                {
                    if (sav.GetPartyPokemon(slot).PokemonData.SpeciesID != 0)
                    {
                        if (sav.PartySize > 1)
                        {
                            pkm_from = sav.GetPartyPokemon(slot).PokemonData;
                            dragfromparty = true;
                            frombox = -1;
                            fromslot = slot;
                            partyPics[slot].DoDragDrop(pkm_from, DragDropEffects.Move);
                        }
                    }
                }
                if (pb.Name.Contains("Box"))
                {
                    if (sav.GetStoredPokemon(sav.CurrentBox, slot).SpeciesID != 0)
                    {
                        pkm_from = sav.GetStoredPokemon(sav.CurrentBox, slot);
                        dragfromparty = false;
                        frombox = sav.CurrentBox;
                        fromslot = slot;
                        boxPics[slot].DoDragDrop(pkm_from, DragDropEffects.Move);
                    }
                }
            }
        }
        private void pbPartyBoxSlot_DragDrop(object sender, DragEventArgs e)
        {
            int slot = 0;
            PictureBox pb = (PictureBox)(sender);
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
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
                            sav.WithdrawPokemon(sav.GetPartyPokemon(fromslot).PokemonData);
                            sav.RemovePartyPokemon(fromslot);
                        }
                        else
                        {
                            PKMDS.SwapPartyParty(sav, fromslot, toslot);
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
                            sav.WithdrawPokemon(sav.GetStoredPokemon(frombox, fromslot));
                            sav.RemoveStoredPokemon(frombox, fromslot);
                        }
                        else
                        {
                            PKMDS.SwapBoxParty(sav, frombox, fromslot, toslot);
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
                        PKMDS.SwapPartyBox(sav, fromslot, tobox, toslot);
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
                        PKMDS.SwapBoxBox(sav, frombox, fromslot, tobox, toslot);
                        UpdateParty();
                        UpdateBox();
                        UpdateBoxGrid(sav.CurrentBox);
                        UpdateBoxCountLabel(sav.CurrentBox);
                    }
                }
            }
        }
        private void pbPartyBoxSlot_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                PKMDS.Pokemon dragpkm = (PKMDS.Pokemon)(e.Data.GetData("PKMDS_CS.PKMDS+Pokemon"));
                if (dragpkm.SpeciesID != 0)
                {
                    int slot = 0;
                    PictureBox pb = (PictureBox)(sender);
                    int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                    slot--;
                    if (pb.Name.Contains("Party"))
                    {
                        dragtoparty = true;
                        //dragtobox = false;
                        pkm_to = sav.GetPartyPokemon(slot).PokemonData;
                    }
                    if (pb.Name.Contains("Box"))
                    {
                        dragtoparty = false;
                        //dragtobox = false;
                        pkm_to = sav.GetStoredPokemon(sav.CurrentBox, slot);
                    }
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }
        private void pbBoxGrid_DragEnter(object sender, DragEventArgs e)
        {
            int box = 0;
            PictureBox pb = (PictureBox)(sender);
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out box);
            box--;
            if (e.Data != null)
            {
                PKMDS.Pokemon dragpkm = (PKMDS.Pokemon)(e.Data.GetData("PKMDS_CS.PKMDS+Pokemon"));
                if ((dragpkm.SpeciesID != 0) && (sav.BoxCount(box) < 30))
                {
                    pkm_to = dragpkm;
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }
        private void pbBoxGrid_DragDrop(object sender, DragEventArgs e)
        {
            int box = 0;
            PictureBox pb = (PictureBox)(sender);
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out box);
            box--;
            dragtoparty = false;
            //dragtobox = true;
            tobox = box;
            toslot = -1;
            if (fromslot != -1)
            {
                if (dragfromparty)
                {
                    sav.DepositPokemon(sav.GetPartyPokemon(fromslot).PokemonData, tobox);
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
                        sav.DepositPokemon(sav.GetStoredPokemon(frombox, fromslot), tobox);
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
        }
        private void pbBoxGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                int box = 0;
                PictureBox pb = (PictureBox)(sender);
                int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out box);
                box--;
                dragtoparty = false;
                //dragtobox = true;
                tobox = box;
                toslot = -1;
            }
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
            foreach (Panel pan in boxPanels)
            {
                pan.BorderStyle = BorderStyle.None;
            }
            boxPanels[box].BorderStyle = BorderStyle.FixedSingle;
        }
        private void lblBoxGrid_MouseEnter(object sender, EventArgs e)
        {
            int box = 0;
            Label pb = (Label)(sender);
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out box);
            box--;
            boxPanels[box].BackColor = Color.Orange;
            this.splitMain.Panel2.Focus();
        }
        private void lblBoxGrid_MouseLeave(object sender, EventArgs e)
        {
            int box = 0;
            Label pb = (Label)(sender);
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out box);
            box--;
            boxPanels[box].BackColor = Color.Transparent;
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
            foreach (Panel pan in boxPanels)
            {
                pan.BorderStyle = BorderStyle.None;
            }
            boxPanels[box].BorderStyle = BorderStyle.FixedSingle;
        }
        private void pbBoxGrid_MouseEnter(object sender, EventArgs e)
        {
            int box = 0;
            PictureBox pb = (PictureBox)(sender);
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out box);
            box--;
            dragtoparty = false;
            //dragtobox = true;
            tobox = box;
            toslot = -1;
            boxPanels[box].BackColor = Color.Orange;
            this.splitMain.Panel2.Focus();
        }
        private void pbBoxGrid_MouseLeave(object sender, EventArgs e)
        {
            int box = 0;
            PictureBox pb = (PictureBox)(sender);
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out box);
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
            int slot = 0;
            PictureBox pb = (PictureBox)(sender);
            int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
            slot--;
            PKMDS.Pokemon pkm = new PKMDS.Pokemon();
            if (pb.Name.Contains("Party"))
            {
                if (sav.GetPartyPokemon(slot).PokemonData.SpeciesID != 0)
                {
                    if (sav.PartySize >= 1)
                    {
                        pkm = sav.GetPartyPokemon(slot).PokemonData;
                    }
                }
            }
            if (pb.Name.Contains("Box"))
            {
                if (sav.GetStoredPokemon(sav.CurrentBox, slot).SpeciesID != 0)
                {
                    pkm = sav.GetStoredPokemon(sav.CurrentBox, slot);
                }
            }
            pb.BackColor = Color.Orange;
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
            PictureBox pb = (PictureBox)(sender);
            pb.BackColor = Color.Transparent;
            ClearPreview();
        }
        private void PreviewPokemon(PKMDS.Pokemon pkm)
        {
            pbSprite.Image = pkm.Sprite;
            pbGender.Image = pkm.GenderIcon;
            pbHeldItem.Image = pkm.ItemPic;
            pbBall.Image = pkm.BallPic;
            pbShiny.Image = pkm.ShinyIcon;
            lblHeldItem.Text = PKMDS.GetItemName(pkm.ItemID);
            lblNickname.Text = pkm.Nickname;
            lblLevel.Text = "Level " + pkm.Level.ToString("");
        }
        private void ClearPreview()
        {
            pbSprite.Image = null;
            pbGender.Image = null;
            pbHeldItem.Image = null;
            pbBall.Image = null;
            pbShiny.Image = null;
            lblNickname.Text = "";
            lblLevel.Text = "";
            lblHeldItem.Text = "";
        }
        private void splitMain_Panel2_MouseEnter(object sender, EventArgs e)
        {
            this.splitMain.Panel2.Focus();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    int slot = 0;
                    PictureBox pb = (PictureBox)(owner.SourceControl);
                    if (pb.Name.Contains("Party"))
                    {
                        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                        slot--;
                        PKMDS.Pokemon pkm = sav.GetPartyPokemon(slot).PokemonData;
                        if ((pkm != null) && (pkm.SpeciesID != 0))
                        {
                            sav.SetPartyPokemon(ViewPokemon(sav.GetPartyPokemon(slot)), slot);
                            UpdateParty();
                        }
                    }
                    if (pb.Name.Contains("Box"))
                    {
                        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                        slot--;
                        PKMDS.Pokemon pkm = sav.GetStoredPokemon(sav.CurrentBox, slot);
                        if ((pkm != null) && (pkm.SpeciesID != 0))
                        {
                            sav.SetStoredPokemon(ViewPokemon(sav.GetStoredPokemon(sav.CurrentBox, slot)), sav.CurrentBox, slot);
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
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    PKMDS.Pokemon pkm = new PKMDS.Pokemon();
                    int slot = 0;
                    PictureBox pb = (PictureBox)(owner.SourceControl);
                    if (pb.Name.Contains("Party"))
                    {
                        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                        slot--;
                        pkmFileOpen.FileName = "";
                        if (pkmFileOpen.ShowDialog() != DialogResult.Cancel)
                        {
                            if (pkmFileOpen.FileName != "")
                            {
                                if (System.IO.File.Exists(pkmFileOpen.FileName))
                                {
                                    System.IO.FileInfo file = new System.IO.FileInfo(pkmFileOpen.FileName);
                                    pkm = PKMDS.ReadPokemonFile(pkmFileOpen.FileName, file.Extension.ToLower() == "ek6");
                                    if (pkm.SpeciesID != 0)
                                    {
                                        PKMDS.PartyPokemon ppkm = new PKMDS.PartyPokemon();
                                        ppkm.PokemonData = pkm;
                                        if (sav.GetPartyPokemon(slot).PokemonData.SpeciesID == 0)
                                        {
                                            sav.WithdrawPokemon(ppkm.PokemonData);
                                        }
                                        else
                                        {
                                            sav.SetPartyPokemon(ppkm, slot);
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
                        pkmFileOpen.FileName = "";
                        if (pkmFileOpen.ShowDialog() != DialogResult.Cancel)
                        {
                            if (pkmFileOpen.FileName != "")
                            {
                                if (System.IO.File.Exists(pkmFileOpen.FileName))
                                {
                                    System.IO.FileInfo file = new System.IO.FileInfo(pkmFileOpen.FileName);
                                    pkm = PKMDS.ReadPokemonFile(pkmFileOpen.FileName, file.Extension.ToLower() == "ek6");
                                    if (pkm.SpeciesID != 0)
                                    {
                                        sav.SetStoredPokemon(pkm, sav.CurrentBox, slot);
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
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    PKMDS.Pokemon pkm = new PKMDS.Pokemon();
                    int slot = 0;
                    PictureBox pb = (PictureBox)(owner.SourceControl);
                    if (pb.Name.Contains("Party"))
                    {
                        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                        slot--;
                        pkm = sav.GetPartyPokemon(slot).PokemonData;
                    }
                    if (pb.Name.Contains("Box"))
                    {
                        int.TryParse(pb.Name.Substring(pb.Name.Length - 2, 2), out slot);
                        slot--;
                        pkm = sav.GetStoredPokemon(sav.CurrentBox, slot);
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
                            if (pkmFileSave.FileName != "")
                            {
                                System.IO.FileInfo file = new System.IO.FileInfo(pkmFileSave.FileName);
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
            }
        }
        private void rbGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (uiset)
            {
                mode = Mode.Group;
                UpdateBox();
                // TODO: group Pokemon sorting

            }
        }
        private void rbItems_CheckedChanged(object sender, EventArgs e)
        {
            if (uiset)
            {
                mode = Mode.Item;
                UpdateBox();
            }
        }
    }
}
