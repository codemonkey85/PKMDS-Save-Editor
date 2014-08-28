using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PKMDS_Save_Editor
{
    public partial class frmFilterSelection : Form
    {
        public bool Accepted = false;
        public int Selection = -1;

        public frmFilterSelection()
        {
            InitializeComponent();
        }

        public void FillWithMovesFromList(ListView.ListViewItemCollection list)
        {
            HashSet<int> moveIdSet = new HashSet<int>();
            foreach (ListViewItem item in list)
            {
                var pkm = ((frmPKMList.PokemonWithLocation)item.Tag).Pokemon;
                moveIdSet.Add(pkm.Move1ID);
                moveIdSet.Add(pkm.Move2ID);
                moveIdSet.Add(pkm.Move3ID);
                moveIdSet.Add(pkm.Move4ID);
            }

            List<Tuple<int, string>> moveList = new List<Tuple<int, string>>(moveIdSet.Count);
            foreach (int moveId in moveIdSet)
            {
                if (moveId == 0) { continue; }
                moveList.Add(new Tuple<int, string>(moveId, PKMDS_CS.PKMDS.GetMoveName(moveId)));
            }

            moveList.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            foreach (var tuple in moveList)
            {
                ListViewItem lvm = new ListViewItem(tuple.Item2);
                lvm.Tag = tuple.Item1;
                listView1.Items.Add(lvm);
            }
        }
        public void FillWithAbilitiesFromList(ListView.ListViewItemCollection list)
        {
            HashSet<int> abilityIdSet = new HashSet<int>();
            foreach (ListViewItem item in list)
            {
                var pkm = ((frmPKMList.PokemonWithLocation)item.Tag).Pokemon;
                abilityIdSet.Add(pkm.AbilityID);
            }

            List<Tuple<int, string>> abilitySet = new List<Tuple<int, string>>(abilityIdSet.Count);
            foreach (int abilityId in abilityIdSet)
            {
                abilitySet.Add(new Tuple<int, string>(abilityId, PKMDS_CS.PKMDS.GetAbilityName(abilityId)));
            }

            abilitySet.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            foreach (var tuple in abilitySet)
            {
                ListViewItem lvm = new ListViewItem(tuple.Item2);
                lvm.Tag = tuple.Item1;
                listView1.Items.Add(lvm);
            }
        }
        public void FillWithItemsFromList(ListView.ListViewItemCollection list)
        {
            HashSet<int> itemIdSet = new HashSet<int>();
            foreach (ListViewItem item in list)
            {
                var pkm = ((frmPKMList.PokemonWithLocation)item.Tag).Pokemon;
                itemIdSet.Add(pkm.ItemID);
            }

            List<Tuple<int, string>> itemList = new List<Tuple<int, string>>(itemIdSet.Count);
            foreach (int itemId in itemIdSet)
            {
                if (itemId == 0)
                {
                    itemList.Add(new Tuple<int, string>(0, " None "));
                }
                else
                {
                    itemList.Add(new Tuple<int, string>(itemId, PKMDS_CS.PKMDS.GetItemName(itemId)));
                }
            }

            itemList.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            foreach (var tuple in itemList)
            {
                ListViewItem lvm = new ListViewItem(tuple.Item2.Trim());
                lvm.Tag = tuple.Item1;
                listView1.Items.Add(lvm);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Accepted = false;
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SetSelectionFromListView();
            Accepted = listView1.SelectedItems.Count > 0;
            Close();
        }
        private void SetSelectionFromListView()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Selection = (int)(listView1.SelectedItems[0].Tag);
            }
        }
    }
}
