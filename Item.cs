using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OPL_Theme_Editor.Properties;

namespace OPL_Theme_Editor
{
	public partial class frmItem : Form
	{
		private Theme _entity = null;
		private Item _item = null;
		private bool _isNew = false;

		public frmItem()
		{
			InitializeComponent();
		}

		public frmItem(Theme theme, Item item, bool isNew = false)
		{
			InitializeComponent();
			_entity = theme;
			_item = item;
			_isNew = isNew;
		}

		private void frmItem_Load(object sender, EventArgs e)
		{
			Text = Settings.Default.ApplicationName + " - Item Properties";
			Icon = Resources.ps2;

			txtType.Text = _item.Type.ToString();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}