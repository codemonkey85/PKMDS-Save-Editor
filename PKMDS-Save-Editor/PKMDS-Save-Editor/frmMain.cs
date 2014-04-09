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
        string title = "";
        private string savefile = "";
        public frmMain()
        {
            InitializeComponent();
            title = this.Text;
        }
        private void loadSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Pokemon pkm = new Pokemon();
            //UnencryptedData unenc = new UnencryptedData();
            //BlockA blocka = new BlockA();
            //BlockB blockb = new BlockB();
            //BlockC blockc = new BlockC();
            //BlockD blockd = new BlockD();
            //int thesize = 0;
            //thesize = System.Runtime.InteropServices.Marshal.SizeOf(pkm);
            //MessageBox.Show(thesize.ToString("0"));
            //thesize = System.Runtime.InteropServices.Marshal.SizeOf(unenc);
            //MessageBox.Show(thesize.ToString("0"));
            //thesize = System.Runtime.InteropServices.Marshal.SizeOf(blocka);
            //MessageBox.Show(thesize.ToString("0"));
            //thesize = System.Runtime.InteropServices.Marshal.SizeOf(blockb);
            //MessageBox.Show(thesize.ToString("0"));
            //thesize = System.Runtime.InteropServices.Marshal.SizeOf(blockc);
            //MessageBox.Show(thesize.ToString("0"));
            //thesize = System.Runtime.InteropServices.Marshal.SizeOf(blockd);
            //MessageBox.Show(thesize.ToString("0"));
            if (fileOpen.ShowDialog() != DialogResult.Cancel)
            {
                if (fileOpen.FileName != "")
                {
                    cbBoxes.SelectedIndex = -1;
                    cbBoxes.Items.Clear();
                    savefile = fileOpen.FileName;
                    PKMDS.OpenDB(Properties.Settings.Default.veekunpokedex);
                    this.Text = title + " - " + PKMDS.GetTrainerName_FromSav(savefile) + " (" + PKMDS.GetTrainerSID_FromSav(savefile).ToString("00000") + ")";
                    try
                    {
                        for (int box = 0; box < 24; box++)
                        {
                            cbBoxes.Items.Add(PKMDS.GetBoxName(savefile, box));
                        }
                        cbBoxes.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Pokemon pkm = new Pokemon();
                    //PKMDS.GetPKMData(pkm, savefile, 0, 0);
                    PKMDS.GetPKMData(ref pkm, savefile, 0, 0);
                    MessageBox.Show(((int)(pkm.Species)).ToString("000"));

                    PKMDS.CloseDB();
                }
            }
        }
    }
}
