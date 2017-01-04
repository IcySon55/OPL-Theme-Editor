namespace OPL_Theme_Editor
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
			this.mnuMain = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmNew = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmRecent = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmImport = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmSave = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.ofdOpen = new System.Windows.Forms.OpenFileDialog();
			this.tlsToolbar = new System.Windows.Forms.ToolStrip();
			this.tsbNew = new System.Windows.Forms.ToolStripButton();
			this.tsbOpen = new System.Windows.Forms.ToolStripSplitButton();
			this.tsbImport = new System.Windows.Forms.ToolStripButton();
			this.tsbSave = new System.Windows.Forms.ToolStripButton();
			this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
			this.tsbProperties = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbCopy = new System.Windows.Forms.ToolStripButton();
			this.tsbPaste = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbAdd = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmBackground = new System.Windows.Forms.ToolStripMenuItem();
			this.tsbDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbHelp = new System.Windows.Forms.ToolStripButton();
			this.pnlAnchor = new System.Windows.Forms.Panel();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.sfdSave = new System.Windows.Forms.SaveFileDialog();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblDimensions = new System.Windows.Forms.Label();
			this.lstItems = new System.Windows.Forms.ListBox();
			this.mnuMain.SuspendLayout();
			this.tlsToolbar.SuspendLayout();
			this.pnlAnchor.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnuMain
			// 
			this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.tsmHelp});
			this.mnuMain.Location = new System.Drawing.Point(0, 0);
			this.mnuMain.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
			this.mnuMain.Name = "mnuMain";
			this.mnuMain.Size = new System.Drawing.Size(899, 24);
			this.mnuMain.TabIndex = 1;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmNew,
            this.tsmOpen,
            this.tsmRecent,
            this.tsmImport,
            this.tsmSave,
            this.tsmSaveAs,
            this.toolStripSeparator4,
            this.tsmExit});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// tsmNew
			// 
			this.tsmNew.Image = global::OPL_Theme_Editor.Properties.Resources.menu_new;
			this.tsmNew.Name = "tsmNew";
			this.tsmNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.tsmNew.Size = new System.Drawing.Size(181, 22);
			this.tsmNew.Text = "&New Theme";
			this.tsmNew.Click += new System.EventHandler(this.tsmNew_Click);
			// 
			// tsmOpen
			// 
			this.tsmOpen.Image = global::OPL_Theme_Editor.Properties.Resources.menu_open;
			this.tsmOpen.Name = "tsmOpen";
			this.tsmOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.tsmOpen.Size = new System.Drawing.Size(181, 22);
			this.tsmOpen.Text = "&Open...";
			this.tsmOpen.Click += new System.EventHandler(this.tsmOpen_Click);
			// 
			// tsmRecent
			// 
			this.tsmRecent.Image = global::OPL_Theme_Editor.Properties.Resources.menu_recent;
			this.tsmRecent.Name = "tsmRecent";
			this.tsmRecent.Size = new System.Drawing.Size(181, 22);
			this.tsmRecent.Text = "Recent Files";
			// 
			// tsmImport
			// 
			this.tsmImport.Image = global::OPL_Theme_Editor.Properties.Resources.menu_import;
			this.tsmImport.Name = "tsmImport";
			this.tsmImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.tsmImport.Size = new System.Drawing.Size(181, 22);
			this.tsmImport.Text = "&Import";
			this.tsmImport.Click += new System.EventHandler(this.tsmImport_Click);
			// 
			// tsmSave
			// 
			this.tsmSave.Enabled = false;
			this.tsmSave.Image = global::OPL_Theme_Editor.Properties.Resources.menu_save;
			this.tsmSave.Name = "tsmSave";
			this.tsmSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.tsmSave.Size = new System.Drawing.Size(181, 22);
			this.tsmSave.Text = "&Save";
			this.tsmSave.Click += new System.EventHandler(this.tsmSave_Click);
			// 
			// tsmSaveAs
			// 
			this.tsmSaveAs.Enabled = false;
			this.tsmSaveAs.Image = global::OPL_Theme_Editor.Properties.Resources.menu_save_as;
			this.tsmSaveAs.Name = "tsmSaveAs";
			this.tsmSaveAs.ShortcutKeys = System.Windows.Forms.Keys.F12;
			this.tsmSaveAs.Size = new System.Drawing.Size(181, 22);
			this.tsmSaveAs.Text = "Save &As...";
			this.tsmSaveAs.Click += new System.EventHandler(this.tsmSaveAs_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(178, 6);
			// 
			// tsmExit
			// 
			this.tsmExit.Image = global::OPL_Theme_Editor.Properties.Resources.menu_exit;
			this.tsmExit.Name = "tsmExit";
			this.tsmExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.tsmExit.Size = new System.Drawing.Size(181, 22);
			this.tsmExit.Text = "E&xit";
			this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCopy,
            this.tsmPaste});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// tsmCopy
			// 
			this.tsmCopy.Enabled = false;
			this.tsmCopy.Image = global::OPL_Theme_Editor.Properties.Resources.menu_copy;
			this.tsmCopy.Name = "tsmCopy";
			this.tsmCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.tsmCopy.Size = new System.Drawing.Size(144, 22);
			this.tsmCopy.Text = "&Copy";
			// 
			// tsmPaste
			// 
			this.tsmPaste.Enabled = false;
			this.tsmPaste.Image = global::OPL_Theme_Editor.Properties.Resources.menu_paste;
			this.tsmPaste.Name = "tsmPaste";
			this.tsmPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.tsmPaste.Size = new System.Drawing.Size(144, 22);
			this.tsmPaste.Text = "&Paste";
			// 
			// tsmHelp
			// 
			this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmAbout});
			this.tsmHelp.Name = "tsmHelp";
			this.tsmHelp.Size = new System.Drawing.Size(44, 20);
			this.tsmHelp.Text = "&Help";
			// 
			// tsmAbout
			// 
			this.tsmAbout.Image = global::OPL_Theme_Editor.Properties.Resources.menu_about;
			this.tsmAbout.Name = "tsmAbout";
			this.tsmAbout.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.tsmAbout.Size = new System.Drawing.Size(126, 22);
			this.tsmAbout.Text = "&About";
			this.tsmAbout.Click += new System.EventHandler(this.tsmAbout_Click);
			// 
			// ofdOpen
			// 
			this.ofdOpen.Filter = "XML Themes|*.thm|All Files|*.*";
			// 
			// tlsToolbar
			// 
			this.tlsToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tlsToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbOpen,
            this.tsbImport,
            this.tsbSave,
            this.tsbSaveAs,
            this.tsbProperties,
            this.toolStripSeparator3,
            this.tsbCopy,
            this.tsbPaste,
            this.toolStripSeparator2,
            this.tsbAdd,
            this.tsbDelete,
            this.toolStripSeparator1,
            this.tsbHelp});
			this.tlsToolbar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.tlsToolbar.Location = new System.Drawing.Point(0, 24);
			this.tlsToolbar.Name = "tlsToolbar";
			this.tlsToolbar.Padding = new System.Windows.Forms.Padding(6, 0, 3, 0);
			this.tlsToolbar.Size = new System.Drawing.Size(899, 25);
			this.tlsToolbar.TabIndex = 9;
			// 
			// tsbNew
			// 
			this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbNew.Image = global::OPL_Theme_Editor.Properties.Resources.menu_new;
			this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbNew.Name = "tsbNew";
			this.tsbNew.Size = new System.Drawing.Size(23, 22);
			this.tsbNew.Text = "&New Theme";
			this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
			// 
			// tsbOpen
			// 
			this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbOpen.Image = global::OPL_Theme_Editor.Properties.Resources.menu_open;
			this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbOpen.Name = "tsbOpen";
			this.tsbOpen.Size = new System.Drawing.Size(32, 22);
			this.tsbOpen.Text = "&Open";
			this.tsbOpen.ButtonClick += new System.EventHandler(this.tsbOpen_Click);
			// 
			// tsbImport
			// 
			this.tsbImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbImport.Image = global::OPL_Theme_Editor.Properties.Resources.menu_import;
			this.tsbImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbImport.Name = "tsbImport";
			this.tsbImport.Size = new System.Drawing.Size(23, 22);
			this.tsbImport.Text = "toolStripButton1";
			this.tsbImport.Click += new System.EventHandler(this.tsbImport_Click);
			// 
			// tsbSave
			// 
			this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSave.Enabled = false;
			this.tsbSave.Image = global::OPL_Theme_Editor.Properties.Resources.menu_save;
			this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSave.Name = "tsbSave";
			this.tsbSave.Size = new System.Drawing.Size(23, 22);
			this.tsbSave.Text = "&Save";
			this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
			// 
			// tsbSaveAs
			// 
			this.tsbSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbSaveAs.Enabled = false;
			this.tsbSaveAs.Image = global::OPL_Theme_Editor.Properties.Resources.menu_save_as;
			this.tsbSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbSaveAs.Name = "tsbSaveAs";
			this.tsbSaveAs.Size = new System.Drawing.Size(23, 22);
			this.tsbSaveAs.Text = "Save &As...";
			this.tsbSaveAs.Click += new System.EventHandler(this.tsbSaveAs_Click);
			// 
			// tsbProperties
			// 
			this.tsbProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbProperties.Enabled = false;
			this.tsbProperties.Image = global::OPL_Theme_Editor.Properties.Resources.menu_properties;
			this.tsbProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbProperties.Name = "tsbProperties";
			this.tsbProperties.Size = new System.Drawing.Size(23, 22);
			this.tsbProperties.Text = "&Properties";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbCopy
			// 
			this.tsbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbCopy.Enabled = false;
			this.tsbCopy.Image = global::OPL_Theme_Editor.Properties.Resources.menu_copy;
			this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbCopy.Name = "tsbCopy";
			this.tsbCopy.Size = new System.Drawing.Size(23, 22);
			this.tsbCopy.Text = "&Copy";
			// 
			// tsbPaste
			// 
			this.tsbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbPaste.Enabled = false;
			this.tsbPaste.Image = global::OPL_Theme_Editor.Properties.Resources.menu_paste;
			this.tsbPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbPaste.Name = "tsbPaste";
			this.tsbPaste.Size = new System.Drawing.Size(23, 22);
			this.tsbPaste.Text = "&Paste";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbAdd
			// 
			this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmBackground});
			this.tsbAdd.Enabled = false;
			this.tsbAdd.Image = global::OPL_Theme_Editor.Properties.Resources.menu_add;
			this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbAdd.Name = "tsbAdd";
			this.tsbAdd.Size = new System.Drawing.Size(29, 22);
			this.tsbAdd.Text = "&Add";
			this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
			// 
			// tsmBackground
			// 
			this.tsmBackground.Name = "tsmBackground";
			this.tsmBackground.Size = new System.Drawing.Size(138, 22);
			this.tsmBackground.Text = "&Background";
			// 
			// tsbDelete
			// 
			this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbDelete.Enabled = false;
			this.tsbDelete.Image = global::OPL_Theme_Editor.Properties.Resources.menu_delete;
			this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbDelete.Name = "tsbDelete";
			this.tsbDelete.Size = new System.Drawing.Size(23, 22);
			this.tsbDelete.Text = "&Delete";
			this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbHelp
			// 
			this.tsbHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbHelp.Image = global::OPL_Theme_Editor.Properties.Resources.menu_about;
			this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbHelp.Name = "tsbHelp";
			this.tsbHelp.Size = new System.Drawing.Size(23, 22);
			this.tsbHelp.Text = "He&lp";
			this.tsbHelp.Click += new System.EventHandler(this.tsbAbout_Click);
			// 
			// pnlAnchor
			// 
			this.pnlAnchor.Controls.Add(this.pnlMain);
			this.pnlAnchor.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlAnchor.Location = new System.Drawing.Point(0, 49);
			this.pnlAnchor.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAnchor.Name = "pnlAnchor";
			this.pnlAnchor.Size = new System.Drawing.Size(650, 490);
			this.pnlAnchor.TabIndex = 10;
			// 
			// pnlMain
			// 
			this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMain.BackgroundImage = global::OPL_Theme_Editor.Properties.Resources.grid;
			this.pnlMain.Location = new System.Drawing.Point(5, 5);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(640, 480);
			this.pnlMain.TabIndex = 0;
			// 
			// sfdSave
			// 
			this.sfdSave.Filter = "XML Themes|*.thm";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblDimensions);
			this.panel1.Controls.Add(this.lstItems);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(649, 49);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(250, 490);
			this.panel1.TabIndex = 12;
			// 
			// lblDimensions
			// 
			this.lblDimensions.Location = new System.Drawing.Point(4, 232);
			this.lblDimensions.Name = "lblDimensions";
			this.lblDimensions.Size = new System.Drawing.Size(241, 213);
			this.lblDimensions.TabIndex = 1;
			// 
			// lstItems
			// 
			this.lstItems.FormattingEnabled = true;
			this.lstItems.IntegralHeight = false;
			this.lstItems.Location = new System.Drawing.Point(5, 5);
			this.lstItems.Name = "lstItems";
			this.lstItems.Size = new System.Drawing.Size(240, 224);
			this.lstItems.TabIndex = 0;
			this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(899, 539);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.pnlAnchor);
			this.Controls.Add(this.tlsToolbar);
			this.Controls.Add(this.mnuMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.mnuMain;
			this.MinimumSize = new System.Drawing.Size(656, 567);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.mnuMain.ResumeLayout(false);
			this.mnuMain.PerformLayout();
			this.tlsToolbar.ResumeLayout(false);
			this.tlsToolbar.PerformLayout();
			this.pnlAnchor.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mnuMain;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmNew;
		private System.Windows.Forms.ToolStripMenuItem tsmOpen;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmExit;
		private System.Windows.Forms.OpenFileDialog ofdOpen;
		private System.Windows.Forms.ToolStrip tlsToolbar;
		private System.Windows.Forms.ToolStripButton tsbNew;
		private System.Windows.Forms.ToolStripButton tsbSave;
		private System.Windows.Forms.ToolStripButton tsbCopy;
		private System.Windows.Forms.ToolStripButton tsbPaste;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbHelp;
		private System.Windows.Forms.Panel pnlAnchor;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbDelete;
		private System.Windows.Forms.ToolStripMenuItem tsmSave;
		private System.Windows.Forms.SaveFileDialog sfdSave;
		private System.Windows.Forms.ToolStripMenuItem tsmCopy;
		private System.Windows.Forms.ToolStripMenuItem tsmPaste;
		private System.Windows.Forms.ToolStripMenuItem tsmHelp;
		private System.Windows.Forms.ToolStripMenuItem tsmAbout;
		private System.Windows.Forms.ToolStripMenuItem tsmSaveAs;
		private System.Windows.Forms.ToolStripButton tsbSaveAs;
		private System.Windows.Forms.ToolStripButton tsbProperties;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSplitButton tsbOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem tsmRecent;
		private System.Windows.Forms.ToolStripDropDownButton tsbAdd;
		private System.Windows.Forms.ToolStripMenuItem tsmBackground;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripMenuItem tsmImport;
		private System.Windows.Forms.ToolStripButton tsbImport;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.ListBox lstItems;
		private System.Windows.Forms.Label lblDimensions;
	}
}

