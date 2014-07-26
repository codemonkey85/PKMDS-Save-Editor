namespace PKMDS_Save_Editor {
	partial class frmPKMList {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing ) {
			if ( disposing && ( components != null ) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeaderNickname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderSpecies = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderTrainer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderTrainerID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderSecretID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderAbility = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderHeldItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderIVTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderIVHP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderIVAtk = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderIVDef = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderIVSpA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderIVSpD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderIVSpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderEVTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderEVHP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderEVAtk = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderEVDef = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderEVSpA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderEVSpD = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderEVSpe = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderNature = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderTameness = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeaderPokedexNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderNickname,
            this.columnHeaderPokedexNumber,
            this.columnHeaderSpecies,
            this.columnHeaderTrainer,
            this.columnHeaderTrainerID,
            this.columnHeaderSecretID,
            this.columnHeaderAbility,
            this.columnHeaderLevel,
            this.columnHeaderHeldItem,
            this.columnHeaderIVTotal,
            this.columnHeaderIVHP,
            this.columnHeaderIVAtk,
            this.columnHeaderIVDef,
            this.columnHeaderIVSpA,
            this.columnHeaderIVSpD,
            this.columnHeaderIVSpe,
            this.columnHeaderEVTotal,
            this.columnHeaderEVHP,
            this.columnHeaderEVAtk,
            this.columnHeaderEVDef,
            this.columnHeaderEVSpA,
            this.columnHeaderEVSpD,
            this.columnHeaderEVSpe,
            this.columnHeaderNature,
            this.columnHeaderTameness});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(12, 13);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(1166, 403);
			this.listView1.TabIndex = 0;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
			this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
			// 
			// columnHeaderNickname
			// 
			this.columnHeaderNickname.Text = "Pokemon";
			this.columnHeaderNickname.Width = 70;
			// 
			// columnHeaderSpecies
			// 
			this.columnHeaderSpecies.Text = "Species";
			this.columnHeaderSpecies.Width = 72;
			// 
			// columnHeaderTrainer
			// 
			this.columnHeaderTrainer.Text = "Trainer";
			this.columnHeaderTrainer.Width = 67;
			// 
			// columnHeaderTrainerID
			// 
			this.columnHeaderTrainerID.Text = "ID";
			this.columnHeaderTrainerID.Width = 42;
			// 
			// columnHeaderSecretID
			// 
			this.columnHeaderSecretID.Text = "SID";
			this.columnHeaderSecretID.Width = 42;
			// 
			// columnHeaderAbility
			// 
			this.columnHeaderAbility.Text = "Ability";
			this.columnHeaderAbility.Width = 85;
			// 
			// columnHeaderLevel
			// 
			this.columnHeaderLevel.Text = "Lv";
			this.columnHeaderLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderLevel.Width = 30;
			// 
			// columnHeaderHeldItem
			// 
			this.columnHeaderHeldItem.Text = "Item";
			this.columnHeaderHeldItem.Width = 79;
			// 
			// columnHeaderIVTotal
			// 
			this.columnHeaderIVTotal.Text = "IV";
			this.columnHeaderIVTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderIVTotal.Width = 30;
			// 
			// columnHeaderIVHP
			// 
			this.columnHeaderIVHP.Text = "HP";
			this.columnHeaderIVHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderIVHP.Width = 30;
			// 
			// columnHeaderIVAtk
			// 
			this.columnHeaderIVAtk.Text = "Atk";
			this.columnHeaderIVAtk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderIVAtk.Width = 30;
			// 
			// columnHeaderIVDef
			// 
			this.columnHeaderIVDef.Text = "Def";
			this.columnHeaderIVDef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderIVDef.Width = 30;
			// 
			// columnHeaderIVSpA
			// 
			this.columnHeaderIVSpA.Text = "SA";
			this.columnHeaderIVSpA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderIVSpA.Width = 30;
			// 
			// columnHeaderIVSpD
			// 
			this.columnHeaderIVSpD.Text = "SD";
			this.columnHeaderIVSpD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderIVSpD.Width = 30;
			// 
			// columnHeaderIVSpe
			// 
			this.columnHeaderIVSpe.Text = "Sp";
			this.columnHeaderIVSpe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderIVSpe.Width = 30;
			// 
			// columnHeaderEVTotal
			// 
			this.columnHeaderEVTotal.Text = "EV";
			this.columnHeaderEVTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderEVTotal.Width = 30;
			// 
			// columnHeaderEVHP
			// 
			this.columnHeaderEVHP.Text = "HP";
			this.columnHeaderEVHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderEVHP.Width = 30;
			// 
			// columnHeaderEVAtk
			// 
			this.columnHeaderEVAtk.Text = "Atk";
			this.columnHeaderEVAtk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderEVAtk.Width = 30;
			// 
			// columnHeaderEVDef
			// 
			this.columnHeaderEVDef.Text = "Def";
			this.columnHeaderEVDef.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderEVDef.Width = 30;
			// 
			// columnHeaderEVSpA
			// 
			this.columnHeaderEVSpA.Text = "SA";
			this.columnHeaderEVSpA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderEVSpA.Width = 30;
			// 
			// columnHeaderEVSpD
			// 
			this.columnHeaderEVSpD.Text = "SD";
			this.columnHeaderEVSpD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderEVSpD.Width = 30;
			// 
			// columnHeaderEVSpe
			// 
			this.columnHeaderEVSpe.Text = "Sp";
			this.columnHeaderEVSpe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderEVSpe.Width = 30;
			// 
			// columnHeaderNature
			// 
			this.columnHeaderNature.Text = "Nature";
			this.columnHeaderNature.Width = 54;
			// 
			// columnHeaderTameness
			// 
			this.columnHeaderTameness.Text = "Tame";
			this.columnHeaderTameness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderTameness.Width = 30;
			// 
			// columnHeaderPokedexNumber
			// 
			this.columnHeaderPokedexNumber.Text = "No.";
			this.columnHeaderPokedexNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeaderPokedexNumber.Width = 30;
			// 
			// frmPKMList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1190, 428);
			this.Controls.Add(this.listView1);
			this.Name = "frmPKMList";
			this.Text = "frmPKMList";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeaderNickname;
		private System.Windows.Forms.ColumnHeader columnHeaderSpecies;
		private System.Windows.Forms.ColumnHeader columnHeaderTrainer;
		private System.Windows.Forms.ColumnHeader columnHeaderTrainerID;
		private System.Windows.Forms.ColumnHeader columnHeaderSecretID;
		private System.Windows.Forms.ColumnHeader columnHeaderAbility;
		private System.Windows.Forms.ColumnHeader columnHeaderLevel;
		private System.Windows.Forms.ColumnHeader columnHeaderHeldItem;
		private System.Windows.Forms.ColumnHeader columnHeaderIVTotal;
		private System.Windows.Forms.ColumnHeader columnHeaderIVHP;
		private System.Windows.Forms.ColumnHeader columnHeaderIVAtk;
		private System.Windows.Forms.ColumnHeader columnHeaderIVDef;
		private System.Windows.Forms.ColumnHeader columnHeaderIVSpA;
		private System.Windows.Forms.ColumnHeader columnHeaderIVSpD;
		private System.Windows.Forms.ColumnHeader columnHeaderIVSpe;
		private System.Windows.Forms.ColumnHeader columnHeaderEVTotal;
		private System.Windows.Forms.ColumnHeader columnHeaderEVHP;
		private System.Windows.Forms.ColumnHeader columnHeaderEVAtk;
		private System.Windows.Forms.ColumnHeader columnHeaderEVDef;
		private System.Windows.Forms.ColumnHeader columnHeaderEVSpA;
		private System.Windows.Forms.ColumnHeader columnHeaderEVSpD;
		private System.Windows.Forms.ColumnHeader columnHeaderEVSpe;
		private System.Windows.Forms.ColumnHeader columnHeaderNature;
		private System.Windows.Forms.ColumnHeader columnHeaderTameness;
		private System.Windows.Forms.ColumnHeader columnHeaderPokedexNumber;
	}
}