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
    public partial class frmPKMList : Form
    {
        public frmPKMList()
        {
            InitializeComponent();
        }

        public void loadFromSave(PKMDS.Save sav)
        {
            foreach (var pkm in sav.Party)
            {
                if (pkm.PokemonData.SpeciesID != 0)
                {
                    AddPokemon(pkm.PokemonData);
                }
            }
            foreach (var box in sav.PCStorage)
            {
                foreach (var pkm in box)
                {
                    if (pkm.SpeciesID != 0)
                    {
                        AddPokemon(pkm);
                    }
                }
            }
        }

        public void AddPokemon(PKMDS.Pokemon pkm)
        {
            ListViewItem lvm = new ListViewItem();

            lvm.SubItems[0] = new ListViewItem.ListViewSubItem(lvm, pkm.Nickname);
            lvm.SubItems.Add(pkm.SpeciesID.ToString("000"));
            lvm.SubItems.Add(pkm.SpeciesName);
            lvm.SubItems.Add(pkm.OTName);
            lvm.SubItems.Add(pkm.TID.ToString("00000"));
            lvm.SubItems.Add(pkm.SID.ToString("00000"));
            lvm.SubItems.Add(PKMDS.GetAbilityName(pkm.AbilityID));
            lvm.SubItems.Add(pkm.Level.ToString());
            lvm.SubItems.Add(pkm.ItemName);

            int summedIV = pkm.HPIV + pkm.AttackIV + pkm.DefenseIV + pkm.SpecialAttackIV + pkm.SpecialDefenseIV + pkm.SpeedIV;
            lvm.SubItems.Add(summedIV.ToString());
            lvm.SubItems.Add(pkm.HPIV.ToString());
            lvm.SubItems.Add(pkm.AttackIV.ToString());
            lvm.SubItems.Add(pkm.DefenseIV.ToString());
            lvm.SubItems.Add(pkm.SpecialAttackIV.ToString());
            lvm.SubItems.Add(pkm.SpecialDefenseIV.ToString());
            lvm.SubItems.Add(pkm.SpeedIV.ToString());

            int summedEV = pkm.HPEV + pkm.AttackEV + pkm.DefenseEV + pkm.SpecialAttackEV + pkm.SpecialDefenseEV + pkm.SpeedEV;
            lvm.SubItems.Add(summedEV.ToString());
            lvm.SubItems.Add(pkm.HPEV.ToString());
            lvm.SubItems.Add(pkm.AttackEV.ToString());
            lvm.SubItems.Add(pkm.DefenseEV.ToString());
            lvm.SubItems.Add(pkm.SpecialAttackEV.ToString());
            lvm.SubItems.Add(pkm.SpecialDefenseEV.ToString());
            lvm.SubItems.Add(pkm.SpeedEV.ToString());

            lvm.SubItems.Add(pkm.NatureName);
            lvm.SubItems.Add(pkm.Tameness.ToString());


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
