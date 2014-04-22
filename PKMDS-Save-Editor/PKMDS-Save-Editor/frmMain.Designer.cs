namespace PKMDS_Save_Editor
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savesavToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileOpen = new System.Windows.Forms.OpenFileDialog();
            this.cbBoxes = new System.Windows.Forms.ComboBox();
            this.fileSave = new System.Windows.Forms.SaveFileDialog();
            this.lstPokemon = new System.Windows.Forms.ListView();
            this.pbSprite = new System.Windows.Forms.PictureBox();
            this.pbGender = new System.Windows.Forms.PictureBox();
            this.lstParty = new System.Windows.Forms.ListView();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSprite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGender)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(631, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSaveToolStripMenuItem,
            this.savesavToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadSaveToolStripMenuItem
            // 
            this.loadSaveToolStripMenuItem.Name = "loadSaveToolStripMenuItem";
            this.loadSaveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.loadSaveToolStripMenuItem.Text = "Load .sav";
            this.loadSaveToolStripMenuItem.Click += new System.EventHandler(this.loadSaveToolStripMenuItem_Click);
            // 
            // savesavToolStripMenuItem
            // 
            this.savesavToolStripMenuItem.Name = "savesavToolStripMenuItem";
            this.savesavToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.savesavToolStripMenuItem.Text = "Save .sav";
            this.savesavToolStripMenuItem.Click += new System.EventHandler(this.savesavToolStripMenuItem_Click);
            // 
            // fileOpen
            // 
            this.fileOpen.Filter = ".sav files|*.sav|All files|*.*";
            // 
            // cbBoxes
            // 
            this.cbBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBoxes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxes.FormattingEnabled = true;
            this.cbBoxes.Location = new System.Drawing.Point(12, 535);
            this.cbBoxes.Name = "cbBoxes";
            this.cbBoxes.Size = new System.Drawing.Size(607, 21);
            this.cbBoxes.TabIndex = 2;
            this.cbBoxes.SelectedIndexChanged += new System.EventHandler(this.cbBoxes_SelectedIndexChanged);
            // 
            // fileSave
            // 
            this.fileSave.Filter = ".sav files|*.sav";
            // 
            // lstPokemon
            // 
            this.lstPokemon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPokemon.Location = new System.Drawing.Point(12, 99);
            this.lstPokemon.MultiSelect = false;
            this.lstPokemon.Name = "lstPokemon";
            this.lstPokemon.Size = new System.Drawing.Size(607, 251);
            this.lstPokemon.TabIndex = 1;
            this.lstPokemon.UseCompatibleStateImageBehavior = false;
            this.lstPokemon.View = System.Windows.Forms.View.List;
            this.lstPokemon.SelectedIndexChanged += new System.EventHandler(this.lstPokemon_SelectedIndexChanged);
            this.lstPokemon.DoubleClick += new System.EventHandler(this.lstPokemon_DoubleClick);
            // 
            // pbSprite
            // 
            this.pbSprite.Location = new System.Drawing.Point(12, 356);
            this.pbSprite.Name = "pbSprite";
            this.pbSprite.Size = new System.Drawing.Size(100, 100);
            this.pbSprite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbSprite.TabIndex = 3;
            this.pbSprite.TabStop = false;
            // 
            // pbGender
            // 
            this.pbGender.Location = new System.Drawing.Point(118, 356);
            this.pbGender.Name = "pbGender";
            this.pbGender.Size = new System.Drawing.Size(235, 173);
            this.pbGender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbGender.TabIndex = 4;
            this.pbGender.TabStop = false;
            // 
            // lstParty
            // 
            this.lstParty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstParty.Location = new System.Drawing.Point(12, 27);
            this.lstParty.MultiSelect = false;
            this.lstParty.Name = "lstParty";
            this.lstParty.Size = new System.Drawing.Size(607, 66);
            this.lstParty.TabIndex = 5;
            this.lstParty.UseCompatibleStateImageBehavior = false;
            this.lstParty.View = System.Windows.Forms.View.List;
            this.lstParty.SelectedIndexChanged += new System.EventHandler(this.lstParty_SelectedIndexChanged);
            this.lstParty.DoubleClick += new System.EventHandler(this.lstParty_DoubleClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 568);
            this.Controls.Add(this.lstParty);
            this.Controls.Add(this.pbGender);
            this.Controls.Add(this.pbSprite);
            this.Controls.Add(this.lstPokemon);
            this.Controls.Add(this.cbBoxes);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PKMDS Save Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSprite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGender)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSaveToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog fileOpen;
        private System.Windows.Forms.ComboBox cbBoxes;
        private System.Windows.Forms.SaveFileDialog fileSave;
        private System.Windows.Forms.ListView lstPokemon;
        private System.Windows.Forms.PictureBox pbSprite;
        private System.Windows.Forms.PictureBox pbGender;
        private System.Windows.Forms.ToolStripMenuItem savesavToolStripMenuItem;
        private System.Windows.Forms.ListView lstParty;
    }
}

