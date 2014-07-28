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
            ReColorizeListView();
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
            ListViewItem lvm = new ListViewItem();
            lvm.UseItemStyleForSubItems = false;
            lvm.Tag = new PokemonWithLocation(pkm, (short)box, (short)slot);

            ListViewItem.ListViewSubItem item = new ListViewItem.ListViewSubItem(lvm, pkm.SpeciesID.ToString("000"));
            item.Tag = ColumnType.SpeciesID;
            lvm.SubItems[0] = item;

            item = new ListViewItem.ListViewSubItem(lvm, pkm.SpeciesName);
            item.Tag = ColumnType.SpeciesName;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.Nickname);
            item.Tag = ColumnType.Nickname;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.GenderID == 0 ? "♂" : pkm.GenderID == 1 ? "♀" : "");
            item.Tag = ColumnType.Gender;
            item.ForeColor = pkm.GenderID == 0 ? Color.Blue : pkm.GenderID == 1 ? Color.Red : Color.Black;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, areaString);
            item.Tag = ColumnType.Area;
            item.ForeColor = box == -1 ? Color.Green : Color.DarkCyan;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, PKMDS.GetTypeName(pkm.GetType(1)));
            item.Tag = ColumnType.Type1;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, PKMDS.GetTypeName(pkm.GetType(2)));
            item.Tag = ColumnType.Type2;
            lvm.SubItems.Add(item);


            item = new ListViewItem.ListViewSubItem(lvm, pkm.Level.ToString());
            item.Tag = ColumnType.Level;
            lvm.SubItems.Add(item);

            int[] stats = pkm.GetStats;
            for (int i = 0; i < stats.Length; ++i)
            {
                item = new ListViewItem.ListViewSubItem(lvm, stats[i].ToString());
                item.Tag = (ColumnType)(ColumnType.StatHP + i);
                lvm.SubItems.Add(item);
            }


            int summedIV = pkm.HPIV + pkm.AttackIV + pkm.DefenseIV + pkm.SpecialAttackIV + pkm.SpecialDefenseIV + pkm.SpeedIV;
            item = new ListViewItem.ListViewSubItem(lvm, summedIV.ToString());
            item.Tag = ColumnType.IVTotal;
            lvm.SubItems.Add(item);

            for (int i = 0; i < stats.Length; ++i)
            {
                item = new ListViewItem.ListViewSubItem(lvm, pkm.GetIV(i).ToString());
                item.Tag = (ColumnType)(ColumnType.IVHP + i);
                lvm.SubItems.Add(item);
            }


            int summedEV = pkm.HPEV + pkm.AttackEV + pkm.DefenseEV + pkm.SpecialAttackEV + pkm.SpecialDefenseEV + pkm.SpeedEV;
            item = new ListViewItem.ListViewSubItem(lvm, summedEV.ToString());
            item.Tag = ColumnType.EVTotal;
            lvm.SubItems.Add(item);

            for (int i = 0; i < stats.Length; ++i)
            {
                item = new ListViewItem.ListViewSubItem(lvm, pkm.GetEV(i).ToString());
                item.Tag = (ColumnType)(ColumnType.EVHP + i);
                lvm.SubItems.Add(item);
            }


            ushort[] moveIds = pkm.GetMoveIDs;
            for (int i = 0; i < moveIds.Length; ++i)
            {
                item = new ListViewItem.ListViewSubItem(lvm, PKMDS.GetMoveName(moveIds[i]));
                item.Tag = (ColumnType)(ColumnType.Move1 + i);
                lvm.SubItems.Add(item);
            }


            item = new ListViewItem.ListViewSubItem(lvm, PKMDS.GetAbilityName(pkm.AbilityID));
            item.Tag = ColumnType.Ability;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.NatureName);
            item.Tag = ColumnType.Nature;
            lvm.SubItems.Add(item);


            string mark =
                (pkm.Circle ? "●" : "") + (pkm.Triangle ? "▲" : "") + (pkm.Square ? "■" : "") +
                (pkm.Square ? "♥" : "") + (pkm.Star ? "★" : "") + (pkm.Diamond ? "♦" : "");
            item = new ListViewItem.ListViewSubItem(lvm, mark);
            item.Tag = ColumnType.Markings;
            lvm.SubItems.Add(item);

            // TODO: Insert Ribbon count here.
            item = new ListViewItem.ListViewSubItem(lvm, "0");
            item.Tag = ColumnType.Ribbons;
            lvm.SubItems.Add(item);


            item = new ListViewItem.ListViewSubItem(lvm, pkm.OTName);
            item.Tag = ColumnType.TrainerName;
            item.ForeColor = pkm.OTIsMale ? Color.Blue : pkm.OTIsFemale ? Color.Red : Color.Black;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.TID.ToString("00000"));
            item.Tag = ColumnType.TrainerID;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.SID.ToString("00000"));
            item.Tag = ColumnType.TrainerSecretID;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.Tameness.ToString());
            item.Tag = ColumnType.Tameness;
            lvm.SubItems.Add(item);


            item = new ListViewItem.ListViewSubItem(lvm, pkm.ItemName);
            item.Tag = ColumnType.HeldItem;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.IsShiny ? "★" : "");
            item.Tag = ColumnType.Shiny;
            lvm.SubItems.Add(item);

            item = new ListViewItem.ListViewSubItem(lvm, pkm.PokerusStrain > 0 ? pkm.PokerusDays > 0 ? "●" : "○" : "");
            item.Tag = ColumnType.Pokerus;
            lvm.SubItems.Add(item);


            listView1.Items.Add(lvm);
        }

        public void ReColorizeListView()
        {
            int row = 0;
            foreach (ListViewItem lvm in listView1.Items)
            {
                ReColorizeListViewItem(lvm, row++);
            }
        }
        public void ReColorizeListViewItem(ListViewItem lvm, int row)
        {
            bool even = row % 2 == 0;
            int group = 0;

            for (int i = 0; i < lvm.SubItems.Count; ++i)
            {
                var item = lvm.SubItems[i];

                switch ((ColumnType)(item.Tag))
                {
                    case ColumnType.Level:
                    case ColumnType.IVTotal:
                    case ColumnType.EVTotal:
                    case ColumnType.Move1:
                    case ColumnType.Ability:
                    case ColumnType.Markings:
                    case ColumnType.TrainerName:
                    case ColumnType.HeldItem:
                        ++group;
                        break;
                }

                item.BackColor = GetBackgroundColor(even, i, group);
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {

        }

        class ListViewItemComparerAlphaAsc : IComparer
        {
            private int col;
            public ListViewItemComparerAlphaAsc()
            {
                col = 0;
            }
            public ListViewItemComparerAlphaAsc(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }
        class ListViewItemComparerAlphaDesc : IComparer
        {
            private int col;
            public ListViewItemComparerAlphaDesc()
            {
                col = 0;
            }
            public ListViewItemComparerAlphaDesc(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                return -String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }
        class ListViewItemComparerNumberAsc : IComparer
        {
            private int col;
            public ListViewItemComparerNumberAsc()
            {
                col = 0;
            }
            public ListViewItemComparerNumberAsc(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                int ix = Int32.Parse(((ListViewItem)x).SubItems[col].Text);
                int iy = Int32.Parse(((ListViewItem)y).SubItems[col].Text);
                return ix.CompareTo(iy);
            }
        }
        class ListViewItemComparerNumberDesc : IComparer
        {
            private int col;
            public ListViewItemComparerNumberDesc()
            {
                col = 0;
            }
            public ListViewItemComparerNumberDesc(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                int ix = Int32.Parse(((ListViewItem)x).SubItems[col].Text);
                int iy = Int32.Parse(((ListViewItem)y).SubItems[col].Text);
                return iy.CompareTo(ix);
            }
        }
        private int sortColumn = -1;
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ContextMenu menu = new System.Windows.Forms.ContextMenu();

            ColumnType type = (ColumnType)listView1.Columns[e.Column].Tag;
            switch (type)
            {
                case ColumnType.SpeciesID:
                case ColumnType.Level:
                case ColumnType.StatHP:
                case ColumnType.StatAtk:
                case ColumnType.StatDef:
                case ColumnType.StatSpAtk:
                case ColumnType.StatSpDef:
                case ColumnType.StatSpeed:
                case ColumnType.IVTotal:
                case ColumnType.IVHP:
                case ColumnType.IVAtk:
                case ColumnType.IVDef:
                case ColumnType.IVSpAtk:
                case ColumnType.IVSpDef:
                case ColumnType.IVSpeed:
                case ColumnType.EVTotal:
                case ColumnType.EVHP:
                case ColumnType.EVAtk:
                case ColumnType.EVDef:
                case ColumnType.EVSpAtk:
                case ColumnType.EVSpDef:
                case ColumnType.EVSpeed:
                case ColumnType.Ribbons:
                case ColumnType.TrainerID:
                case ColumnType.TrainerSecretID:
                case ColumnType.Tameness:
                    // sort numerically
                    SortData(e.Column, true);
                    ReColorizeListView();
                    return;
                case ColumnType.SpeciesName:
                case ColumnType.Nickname:
                case ColumnType.TrainerName:
                    // sort alpha
                    SortData(e.Column, false);
                    ReColorizeListView();
                    return;
                case ColumnType.Gender:
                    // show only specific gender
                    break;
                case ColumnType.Area:
                    // sort by area; maybe only show specific areas?
                    break;
                case ColumnType.Type1:
                case ColumnType.Type2:
                    // show only specific type(s)
                    break;
                case ColumnType.Move1:
                case ColumnType.Move2:
                case ColumnType.Move3:
                case ColumnType.Move4:
                    // show only specific move(s)
                    break;
                case ColumnType.Ability:
                    // show only specific ability
                    break;
                case ColumnType.Nature:
                    // show only specific nature
                    break;
                case ColumnType.Markings:
                    // show only specific marking(s)
                    break;
                case ColumnType.HeldItem:
                    // show only specific item
                    break;
                case ColumnType.Shiny:
                    // show only shiny
                    break;
                case ColumnType.Pokerus:
                    // show only infected/prev. infected
                    break;
                default:
                    throw new Exception("Unknown ColumnType in column tag. (" + type.ToString() + ")");
            }

            menu.Show(this, this.PointToClient(MousePosition));
            return;
        }
        private void SortData(int column, bool numerically)
        {
            if (column != sortColumn)
            {
                sortColumn = column;
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
                    if (numerically)
                    {
                        listView1.ListViewItemSorter = new ListViewItemComparerNumberAsc(column);
                    }
                    else
                    {
                        listView1.ListViewItemSorter = new ListViewItemComparerAlphaAsc(column);
                    }
                    break;
                case SortOrder.Descending:
                    if (numerically)
                    {
                        listView1.ListViewItemSorter = new ListViewItemComparerNumberDesc(column);
                    }
                    else
                    {
                        listView1.ListViewItemSorter = new ListViewItemComparerAlphaDesc(column);
                    }
                    break;
            }

            listView1.Sort();
        }
    }
}
