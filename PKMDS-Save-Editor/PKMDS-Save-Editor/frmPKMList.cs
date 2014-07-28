using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PKMDS_CS;
using System.Collections;

namespace PKMDS_Save_Editor
{
    enum ColumnType
    {
        SpeciesID, SpeciesName, Nickname, Gender, Area, Type1, Type2,
        Level, StatHP, StatAtk, StatDef, StatSpAtk, StatSpDef, StatSpeed,
        IVTotal, IVHP, IVAtk, IVDef, IVSpAtk, IVSpDef, IVSpeed,
        EVTotal, EVHP, EVAtk, EVDef, EVSpAtk, EVSpDef, EVSpeed,
        Move1, Move2, Move3, Move4,
        Ability, Nature, Markings, Ribbons,
        TrainerName, TrainerID, TrainerSecretID,
        Tameness, HeldItem, Shiny, Pokerus,
        ColumnTypeCount
    }

    public partial class frmPKMList : Form
    {
        public frmPKMList()
        {
            InitializeComponent();

            columnHeaderPokedexNumber.Tag = ColumnType.SpeciesID;
            columnHeaderSpeciesName.Tag = ColumnType.SpeciesName;
            columnHeaderNickname.Tag = ColumnType.Nickname;
            columnHeaderGender.Tag = ColumnType.Gender;
            columnHeaderArea.Tag = ColumnType.Area;
            columnHeaderType1.Tag = ColumnType.Type1;
            columnHeaderType2.Tag = ColumnType.Type2;
            columnHeaderLevel.Tag = ColumnType.Level;
            columnHeaderStatsHP.Tag = ColumnType.StatHP;
            columnHeaderStatsAtk.Tag = ColumnType.StatAtk;
            columnHeaderStatsDef.Tag = ColumnType.StatDef;
            columnHeaderStatsSpA.Tag = ColumnType.StatSpAtk;
            columnHeaderStatsSpD.Tag = ColumnType.StatSpDef;
            columnHeaderStatsSpe.Tag = ColumnType.StatSpeed;
            columnHeaderIVTotal.Tag = ColumnType.IVTotal;
            columnHeaderIVHP.Tag = ColumnType.IVHP;
            columnHeaderIVAtk.Tag = ColumnType.IVAtk;
            columnHeaderIVDef.Tag = ColumnType.IVDef;
            columnHeaderIVSpA.Tag = ColumnType.IVSpAtk;
            columnHeaderIVSpD.Tag = ColumnType.IVSpDef;
            columnHeaderIVSpe.Tag = ColumnType.IVSpeed;
            columnHeaderEVTotal.Tag = ColumnType.EVTotal;
            columnHeaderEVHP.Tag = ColumnType.EVHP;
            columnHeaderEVAtk.Tag = ColumnType.EVAtk;
            columnHeaderEVDef.Tag = ColumnType.EVDef;
            columnHeaderEVSpA.Tag = ColumnType.EVSpAtk;
            columnHeaderEVSpD.Tag = ColumnType.EVSpDef;
            columnHeaderEVSpe.Tag = ColumnType.EVSpeed;
            columnHeaderMove1.Tag = ColumnType.Move1;
            columnHeaderMove2.Tag = ColumnType.Move2;
            columnHeaderMove3.Tag = ColumnType.Move3;
            columnHeaderMove4.Tag = ColumnType.Move4;
            columnHeaderAbility.Tag = ColumnType.Ability;
            columnHeaderNature.Tag = ColumnType.Nature;
            columnHeaderTameness.Tag = ColumnType.Tameness;
            columnHeaderTrainerName.Tag = ColumnType.TrainerName;
            columnHeaderTrainerID.Tag = ColumnType.TrainerID;
            columnHeaderSecretID.Tag = ColumnType.TrainerSecretID;
            columnHeaderHeldItem.Tag = ColumnType.HeldItem;
            columnHeaderMark.Tag = ColumnType.Markings;
            columnHeaderRibbons.Tag = ColumnType.Ribbons;
            columnHeaderShiny.Tag = ColumnType.Shiny;
            columnHeaderPokeRus.Tag = ColumnType.Pokerus;
        }

        public void loadFromSave(PKMDS.Save sav)
        {
            for (int i = 0; i < sav.Party.Count; ++i)
            {
                var pkm = sav.Party[i].PokemonData;
                if (pkm.SpeciesID != 0)
                {
                    AddPokemon(pkm, "Party", -1, i);
                }
            }
            for (int i = 0; i < sav.PCStorage.Count; ++i)
            {
                var box = sav.PCStorage[i];
                for (int j = 0; j < box.Count; ++j)
                {
                    var pkm = box[j];
                    if (pkm.SpeciesID != 0)
                    {
                        AddPokemon(pkm, sav.BoxNames[i].Name, i, j);
                    }
                }
            }
        }

        private Color GetBackgroundColor(bool even, int column, int group)
        {
            if (even)
            {
                if (group % 2 == 0)
                {
                    if (column % 2 == 0)
                    {
                        return Color.White;
                    }
                    else
                    {
                        return Color.FromArgb(0xFF, 0xF7, 0xF7, 0xF7);
                    }
                }
                else
                {
                    if (column % 2 == 0)
                    {
                        return Color.FromArgb(0xFF, 0xDF, 0xDF, 0xDF);
                    }
                    else
                    {
                        return Color.FromArgb(0xFF, 0xD7, 0xD7, 0xD7);
                    }
                }
            }
            else
            {
                if (group % 2 == 0)
                {
                    if (column % 2 == 0)
                    {
                        return Color.FromArgb(0xFF, 0xEF, 0xEF, 0xEF);
                    }
                    else
                    {
                        return Color.FromArgb(0xFF, 0xE7, 0xE7, 0xE7);
                    }
                }
                else
                {
                    if (column % 2 == 0)
                    {
                        return Color.FromArgb(0xFF, 0xCF, 0xCF, 0xCF);
                    }
                    else
                    {
                        return Color.FromArgb(0xFF, 0xC7, 0xC7, 0xC7);
                    }
                }
            }
        }

        struct PokemonWithLocation
        {
            public PKMDS.Pokemon Pokemon;
            public short Box;
            public short Slot;
            public PokemonWithLocation(PKMDS.Pokemon pkm, short box, short slot)
            {
                this.Pokemon = pkm;
                this.Box = box;
                this.Slot = slot;
            }
        }
        // box should be -1 for party
        public void AddPokemon(PKMDS.Pokemon pkm, string areaString, int box, int slot)
        {
            bool even = listView1.Items.Count % 2 == 0;
            int columnCount = 0;
            int group = 0;

            ListViewItem lvm = new ListViewItem();
            lvm.UseItemStyleForSubItems = false;
            lvm.Tag = new PokemonWithLocation(pkm, (short)box, (short)slot);

            ListViewItem.ListViewSubItem item = new ListViewItem.ListViewSubItem(lvm, pkm.SpeciesID.ToString("000"));
            item.Tag = ColumnType.SpeciesID;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems[0] = item;

            item = new ListViewItem.ListViewSubItem(lvm, pkm.SpeciesName);
            item.Tag = ColumnType.SpeciesName;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.Nickname);
            item.Tag = ColumnType.Nickname;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.GenderID == 0 ? "♂" : pkm.GenderID == 1 ? "♀" : "");
            item.Tag = ColumnType.Gender;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            item.ForeColor = pkm.GenderID == 0 ? Color.Blue : pkm.GenderID == 1 ? Color.Red : Color.Black;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, areaString);
            item.Tag = ColumnType.Area;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            item.ForeColor = box == -1 ? Color.Green : Color.DarkCyan;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, PKMDS.GetTypeName(pkm.GetType(1)));
            item.Tag = ColumnType.Type1;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, PKMDS.GetTypeName(pkm.GetType(2)));
            item.Tag = ColumnType.Type2;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            group++;

            item = new ListViewItem.ListViewSubItem(lvm, pkm.Level.ToString());
            item.Tag = ColumnType.Level;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            int[] stats = pkm.GetStats;
            for (int i = 0; i < stats.Length; ++i)
            {
                item = new ListViewItem.ListViewSubItem(lvm, stats[i].ToString());
                item.Tag = (ColumnType)(ColumnType.StatHP + i);
                item.BackColor = GetBackgroundColor(even, columnCount++, group);
                lvm.SubItems.Add(item);
            }

            group++;

            int summedIV = pkm.HPIV + pkm.AttackIV + pkm.DefenseIV + pkm.SpecialAttackIV + pkm.SpecialDefenseIV + pkm.SpeedIV;
            item = new ListViewItem.ListViewSubItem(lvm, summedIV.ToString());
            item.Tag = ColumnType.IVTotal;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            for (int i = 0; i < stats.Length; ++i)
            {
                item = new ListViewItem.ListViewSubItem(lvm, pkm.GetIV(i).ToString());
                item.Tag = (ColumnType)(ColumnType.IVHP + i);
                item.BackColor = GetBackgroundColor(even, columnCount++, group);
                lvm.SubItems.Add(item);
            }

            group++;

            int summedEV = pkm.HPEV + pkm.AttackEV + pkm.DefenseEV + pkm.SpecialAttackEV + pkm.SpecialDefenseEV + pkm.SpeedEV;
            item = new ListViewItem.ListViewSubItem(lvm, summedEV.ToString());
            item.Tag = ColumnType.EVTotal;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            for (int i = 0; i < stats.Length; ++i)
            {
                item = new ListViewItem.ListViewSubItem(lvm, pkm.GetEV(i).ToString());
                item.Tag = (ColumnType)(ColumnType.EVHP + i);
                item.BackColor = GetBackgroundColor(even, columnCount++, group);
                lvm.SubItems.Add(item);
            }

            group++;

            ushort[] moveIds = pkm.GetMoveIDs;
            for (int i = 0; i < moveIds.Length; ++i)
            {
                item = new ListViewItem.ListViewSubItem(lvm, PKMDS.GetMoveName(moveIds[i]));
                item.Tag = (ColumnType)(ColumnType.Move1 + i);
                item.BackColor = GetBackgroundColor(even, columnCount++, group);
                lvm.SubItems.Add(item);
            }

            group++;

            item = new ListViewItem.ListViewSubItem(lvm, PKMDS.GetAbilityName(pkm.AbilityID));
            item.Tag = ColumnType.Ability;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.NatureName);
            item.Tag = ColumnType.Nature;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            group++;

            string mark =
                (pkm.Circle ? "●" : "") + (pkm.Triangle ? "▲" : "") + (pkm.Square ? "■" : "") +
                (pkm.Square ? "♥" : "") + (pkm.Star ? "★" : "") + (pkm.Diamond ? "♦" : "");
            item = new ListViewItem.ListViewSubItem(lvm, mark);
            item.Tag = ColumnType.Markings;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, "");
            item.Tag = ColumnType.Ribbons;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            group++;

            item = new ListViewItem.ListViewSubItem(lvm, pkm.OTName);
            item.Tag = ColumnType.TrainerName;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            item.ForeColor = pkm.OTIsMale ? Color.Blue : pkm.OTIsFemale ? Color.Red : Color.Black;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.TID.ToString("00000"));
            item.Tag = ColumnType.TrainerID;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.SID.ToString("00000"));
            item.Tag = ColumnType.TrainerSecretID;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.Tameness.ToString());
            item.Tag = ColumnType.Tameness;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            group++;

            item = new ListViewItem.ListViewSubItem(lvm, pkm.ItemName);
            item.Tag = ColumnType.HeldItem;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.IsShiny ? "★" : "");
            item.Tag = ColumnType.Shiny;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.PokerusStrain > 0 ? pkm.PokerusDays > 0 ? "●" : "○" : "");
            item.Tag = ColumnType.Pokerus;
            item.BackColor = GetBackgroundColor(even, columnCount++, group);
            lvm.SubItems.Add(item);


            listView1.Items.Add(lvm);
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {

        }

        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }
        class ListViewItemComparerDesc : IComparer
        {
            private int col;
            public ListViewItemComparerDesc()
            {
                col = 0;
            }
            public ListViewItemComparerDesc(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                return -String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }
        private int sortColumn = -1;
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
                listView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.Descending;
                else
                    listView1.Sorting = SortOrder.Ascending;
            }

            switch (listView1.Sorting)
            {
                case SortOrder.Ascending:
                    listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
                    break;
                case SortOrder.Descending:
                    listView1.ListViewItemSorter = new ListViewItemComparerDesc(e.Column);
                    break;
            }

            listView1.Sort();
        }
    }
}
