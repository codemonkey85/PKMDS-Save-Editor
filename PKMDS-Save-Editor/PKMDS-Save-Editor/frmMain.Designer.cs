﻿namespace PKMDS_Save_Editor
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
            this.fileOpen = new System.Windows.Forms.OpenFileDialog();
            this.cbBoxes = new System.Windows.Forms.ComboBox();
            this.fileSave = new System.Windows.Forms.SaveFileDialog();
            this.lstPokemon = new System.Windows.Forms.ListView();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(428, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSaveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadSaveToolStripMenuItem
            // 
            this.loadSaveToolStripMenuItem.Name = "loadSaveToolStripMenuItem";
            this.loadSaveToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.loadSaveToolStripMenuItem.Text = "Load Save...";
            this.loadSaveToolStripMenuItem.Click += new System.EventHandler(this.loadSaveToolStripMenuItem_Click);
            // 
            // fileOpen
            // 
            this.fileOpen.Filter = "All files|*.*";
            // 
            // cbBoxes
            // 
            this.cbBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbBoxes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxes.FormattingEnabled = true;
            this.cbBoxes.Location = new System.Drawing.Point(12, 331);
            this.cbBoxes.Name = "cbBoxes";
            this.cbBoxes.Size = new System.Drawing.Size(404, 21);
            this.cbBoxes.TabIndex = 1;
            this.cbBoxes.SelectedIndexChanged += new System.EventHandler(this.cbBoxes_SelectedIndexChanged);
            // 
            // lstPokemon
            // 
            this.lstPokemon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPokemon.Location = new System.Drawing.Point(12, 27);
            this.lstPokemon.Name = "lstPokemon";
            this.lstPokemon.Size = new System.Drawing.Size(404, 298);
            this.lstPokemon.TabIndex = 2;
            this.lstPokemon.UseCompatibleStateImageBehavior = false;
            this.lstPokemon.View = System.Windows.Forms.View.List;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 364);
            this.Controls.Add(this.lstPokemon);
            this.Controls.Add(this.cbBoxes);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "frmMain";
            this.Text = "PKMDS Save Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
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
    }
}

