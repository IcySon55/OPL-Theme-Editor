using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OPL_Theme_Editor.Properties;

namespace OPL_Theme_Editor
{
	public partial class frmMain : Form
	{
		private Theme _theme = null;
		private bool _fileOpen = false;
		private bool _hasChanges = false;
		private FileInfo _openFile = null;
		private bool _imported = false;
		private Control _selected = null;

		private enum Tabs
		{
			General,
			Database,
			Class,
			List,
			Form,
			Validation
		}

		public frmMain()
		{
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			// Load Settings
			Text = Settings.Default.ApplicationName;
			Icon = Resources.ps2;
			WindowState = Settings.Default.Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
			if (Settings.Default.WindowLocation != null)
				Location = Settings.Default.WindowLocation;

			PopulateRecentlyOpenedFiles();
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Save Settings
			Settings.Default.Maximized = WindowState == FormWindowState.Maximized;
			Settings.Default.WindowLocation = Location;
			Settings.Default.Save();
		}

		private void DoubleBuffer(DataGridView dgv, bool doubleBuffered)
		{
			dgv.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgv, doubleBuffered, null);
		}

		private void PopulateRecentlyOpenedFiles()
		{
			if (Settings.Default.RecentlyOpenedFiles == null)
				Settings.Default.RecentlyOpenedFiles = new System.Collections.Specialized.StringCollection();

			while (Settings.Default.RecentlyOpenedFiles.Count > 10)
				Settings.Default.RecentlyOpenedFiles.RemoveAt(Settings.Default.RecentlyOpenedFiles.Count - 1);

			tsbOpen.DropDownItems.Clear();
			tsmRecent.DropDownItems.Clear();
			for (int i = 0; i < Settings.Default.RecentlyOpenedFiles.Count; i++)
			{
				string rof = Settings.Default.RecentlyOpenedFiles[i];
				string index = (i + 1).ToString();
				string shortcut = "D" + index.Substring(index.Length - 1, 1);

				ToolStripMenuItem itemMenuBar = new ToolStripMenuItem();
				itemMenuBar.Name = "recentlyOpenedFileMenu" + i;
				itemMenuBar.Text = index + " " + PathUtil.CompactPathFromLeft(rof, 48);
				itemMenuBar.Click += new EventHandler(tsmRecentFile_Click);
				itemMenuBar.Tag = rof;
				itemMenuBar.ShortcutKeys = Keys.Alt ^ (Keys)Enum.Parse(typeof(Keys), shortcut);
				tsmRecent.DropDownItems.Add(itemMenuBar);

				ToolStripMenuItem itemToolStrip = new ToolStripMenuItem();
				itemToolStrip.Name = "recentlyOpenedFileButton" + i;
				itemToolStrip.Text = index + " " + PathUtil.CompactPathFromLeft(rof, 48);
				itemToolStrip.Click += new EventHandler(tsmRecentFile_Click);
				itemToolStrip.Tag = rof;
				itemToolStrip.ShortcutKeys = Keys.Alt ^ (Keys)Enum.Parse(typeof(Keys), shortcut);
				tsbOpen.DropDownItems.Add(itemToolStrip);
			}
			if (Settings.Default.RecentlyOpenedFiles.Count == 0)
			{
				ToolStripMenuItem itemMenuBar = new ToolStripMenuItem();
				itemMenuBar.Name = "recentlyOpenedFileMenuNone";
				itemMenuBar.Text = "No recently opened files...";
				itemMenuBar.Click += new EventHandler(tsmRecentFile_Click);
				itemMenuBar.Enabled = false;
				tsmRecent.DropDownItems.Add(itemMenuBar);

				ToolStripMenuItem itemToolStrip = new ToolStripMenuItem();
				itemToolStrip.Name = "recentlyOpenedFileButtonNone";
				itemToolStrip.Text = "No recently opened files...";
				itemToolStrip.Click += new EventHandler(tsmRecentFile_Click);
				itemToolStrip.Enabled = false;
				tsbOpen.DropDownItems.Add(itemToolStrip);
			}

			Settings.Default.Save();
		}

		#region Menu and Toolbar

		// New
		private void tsmNew_Click(object sender, EventArgs e)
		{
			ConfirmNew();
		}
		private void tsbNew_Click(object sender, EventArgs e)
		{
			tsmNew_Click(sender, e);
		}

		// Open
		private void tsmOpen_Click(object sender, EventArgs e)
		{
			ConfirmOpen();
		}
		private void tsbOpen_Click(object sender, EventArgs e)
		{
			tsmOpen_Click(sender, e);
		}
		private void tsmRecentFile_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = (ToolStripMenuItem)sender;
			OpenTheme((string)item.Tag);
		}

		// Import
		private void tsmImport_Click(object sender, EventArgs e)
		{
			ConfirmImport();
		}
		private void tsbImport_Click(object sender, EventArgs e)
		{
			tsmImport_Click(sender, e);
		}

		// Save
		private void tsmSave_Click(object sender, EventArgs e)
		{
			SaveTheme();
		}
		private void tsbSave_Click(object sender, EventArgs e)
		{
			tsmSave_Click(sender, e);
		}

		// Save As
		private void tsmSaveAs_Click(object sender, EventArgs e)
		{
			SaveTheme(true);
		}
		private void tsbSaveAs_Click(object sender, EventArgs e)
		{
			tsmSaveAs_Click(sender, e);
		}

		// Add
		private void tsmAdd_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
		private void tsbAdd_Click(object sender, EventArgs e)
		{
			tsmAdd_Click(sender, e);
		}

		// Delete
		private void tsmDelete_Click(object sender, EventArgs e)
		{
			UpdateForm();
		}
		private void tsbDelete_Click(object sender, EventArgs e)
		{
			tsmDelete_Click(sender, e);
		}

		// Properties

		// About
		private void tsmAbout_Click(object sender, EventArgs e)
		{
			frmAbout about = new frmAbout();
			about.ShowDialog();
		}
		private void tsbAbout_Click(object sender, EventArgs e)
		{
			tsmAbout_Click(sender, e);
		}

		// Exit
		private void tsmExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		#endregion

		private void ConfirmNew()
		{
			DialogResult dr = DialogResult.No;

			if (_fileOpen && _hasChanges)
			{
				dr = MessageBox.Show("You have unsaved changes in " + FileName() + ". Save changes before creating a new theme?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
			}

			switch (dr)
			{
				case DialogResult.Yes:
					dr = SaveTheme();
					if (dr == DialogResult.OK) NewTheme();
					break;
				case DialogResult.No:
					NewTheme();
					break;
				case DialogResult.Cancel:
					break;
			}
		}

		private void ConfirmOpen()
		{
			DialogResult dr = DialogResult.No;

			if (_fileOpen && _hasChanges)
			{
				dr = MessageBox.Show("You have unsaved changes in " + FileName() + ". Save changes before opening another theme?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
			}

			switch (dr)
			{
				case DialogResult.Yes:
					dr = SaveTheme();
					if (dr == DialogResult.OK) OpenTheme();
					break;
				case DialogResult.No:
					OpenTheme();
					break;
				case DialogResult.Cancel:
					break;
			}
		}

		private void ConfirmImport()
		{
			DialogResult dr = DialogResult.No;

			if (_fileOpen && _hasChanges)
			{
				dr = MessageBox.Show("You have unsaved changes in " + FileName() + ". Save changes before opening another theme?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
			}

			switch (dr)
			{
				case DialogResult.Yes:
					dr = SaveTheme();
					if (dr == DialogResult.OK) ImportTheme();
					break;
				case DialogResult.No:
					ImportTheme();
					break;
				case DialogResult.Cancel:
					break;
			}
		}

		private void NewTheme()
		{
			_openFile = null;
			_theme = new Theme();
			_fileOpen = true;
			_hasChanges = false;
			LoadTheme();
			UpdateForm();
		}

		private void OpenTheme(string filename = "")
		{
			ofdOpen.InitialDirectory = Settings.Default.InitialDirectory;
			ofdOpen.Filter = "XML Themes|*.thm|All Files|*.*";
			string path = filename;
			DialogResult dr = DialogResult.OK;

			if (filename == string.Empty)
			{
				dr = ofdOpen.ShowDialog();
				path = ofdOpen.FileName;
			}

			if (dr == DialogResult.OK)
			{
				try
				{
					if (!File.Exists(path))
						throw new FileNotFoundException(path + " was not found.", path);

					_openFile = new FileInfo(path);
					_theme = Theme.Load(_openFile.FullName);
					_fileOpen = true;
					_hasChanges = false;

					// Recently Opened Files
					if (!Settings.Default.RecentlyOpenedFiles.Contains(_openFile.FullName))
					{
						Settings.Default.RecentlyOpenedFiles.Insert(0, _openFile.FullName);
					}
					else
					{
						Settings.Default.RecentlyOpenedFiles.Remove(_openFile.FullName);
						Settings.Default.RecentlyOpenedFiles.Insert(0, _openFile.FullName);
					}

					Settings.Default.InitialDirectory = new DirectoryInfo(_openFile.FullName).FullName;
					PopulateRecentlyOpenedFiles();
					LoadTheme();
				}
				catch (FileNotFoundException)
				{
					if (Settings.Default.RecentlyOpenedFiles.Contains(path))
					{
						Settings.Default.RecentlyOpenedFiles.Remove(path);
						PopulateRecentlyOpenedFiles();
						MessageBox.Show(path + " no longer exists and has been removed from the Recently Opened Files list.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				catch (Exception)
				{
					_fileOpen = false;
					_hasChanges = false;
				}
				finally
				{
					UpdateForm();
				}
			}
		}

		private DialogResult ImportTheme(string filename = "")
		{
			ofdOpen.InitialDirectory = Settings.Default.InitialDirectory;
			ofdOpen.Filter = "CFG Themes|*.cfg|All Files|*.*";
			string path = filename;
			DialogResult dr = DialogResult.OK;

			if (filename == string.Empty)
			{
				dr = ofdOpen.ShowDialog();
				path = ofdOpen.FileName;
			}

			if (dr == DialogResult.OK)
			{
				try
				{
					if (!File.Exists(path))
						throw new FileNotFoundException(path + " was not found.", path);

					using (StreamReader sr = new StreamReader(File.OpenRead(path), Encoding.UTF8))
					{
						Theme theme = new Theme();
						string colonValue = "(?<=.*:).*";
						string equalValue = "(?<=.*=).*";
						bool inItem = false;
						Item item = new Item();

						while (!sr.EndOfStream)
						{
							string line = sr.ReadLine();

							if (line.StartsWith("#"))
							{
								if (!inItem)
								{
									if (line.StartsWith("# Theme Name:") || line.StartsWith("# Name:"))
										theme.Name = Regex.Match(line, colonValue).Value.Trim();
									else if (line.StartsWith("# Author:"))
										theme.Author = Regex.Match(line, colonValue).Value.Trim();
									else if (line.StartsWith("# Version:"))
										theme.Version = Regex.Match(line, colonValue).Value.Trim();
									else if (line.StartsWith("# Site:"))
										theme.Site = Regex.Match(line, colonValue).Value.Trim();
									else
										theme.Notes += line.TrimStart('#').Trim() + "\r\n";
								}
								else
								{
									// Item Comments
									item.Comment += line.TrimStart('#').Trim() + "\r\n";
								}
							}
							// Theme Settings
							else if (line.StartsWith("bg_color="))
								theme.BackgroundColor = Regex.Match(line, equalValue).Value.Trim();
							else if (line.StartsWith("sel_text_color="))
								theme.BackgroundColor = Regex.Match(line, equalValue).Value.Trim();
							else if (line.StartsWith("settings_text_color="))
								theme.BackgroundColor = Regex.Match(line, equalValue).Value.Trim();
							else if (line.StartsWith("text_color="))
								theme.BackgroundColor = Regex.Match(line, equalValue).Value.Trim();
							else if (line.StartsWith("ui_text_color="))
								theme.BackgroundColor = Regex.Match(line, equalValue).Value.Trim();
							else if (line.StartsWith("default_font="))
								theme.BackgroundColor = Regex.Match(line, equalValue).Value.Trim();
							// Page
							else if (line.StartsWith("main"))
							{
								item = new Item();
								theme.MainItems.Add(item);
								inItem = true;
							}
							else if (line.StartsWith("info"))
							{
								item = new Item();
								theme.InfoItems.Add(item);
								inItem = true;
							}
							else if (line.Trim().StartsWith("type"))
							{
								ItemType type = ItemType.Background;
								Enum.TryParse<ItemType>(Regex.Match(line, equalValue).Value.Trim(), out type);
								item.Type = type;
							}
							else if (line.Trim().StartsWith("x"))
								item.X = Regex.Match(line, equalValue).Value.Trim();
							else if (line.Trim().StartsWith("y"))
								item.Y = Regex.Match(line, equalValue).Value.Trim();
							else if (line.Trim().StartsWith("width"))
								item.Height = Regex.Match(line, equalValue).Value.Trim();
							else if (line.Trim().StartsWith("height"))
								item.Width = Regex.Match(line, equalValue).Value.Trim();
							else if (line.Trim().StartsWith("attribute"))
								item.Attribute = Regex.Match(line, equalValue).Value.Trim();
							else if (line.Trim().StartsWith("pattern"))
								item.Pattern = Regex.Match(line, equalValue).Value.Trim();
							else if (line.Trim().StartsWith("default"))
								item.DefaultImage = Regex.Match(line, equalValue).Value.Trim();
							else if (line.Trim().StartsWith("color"))
								item.Color = Regex.Match(line, equalValue).Value.Trim();
							else if (line.Trim().StartsWith("aligned"))
								item.Aligned = Int32.Parse(Regex.Match(line, equalValue).Value.Trim());
							else if (line.Trim().StartsWith("display"))
								item.Display = Int32.Parse(Regex.Match(line, equalValue).Value.Trim());
							else if (line.Trim().StartsWith("overlay="))
								item.Overlay.Image = Regex.Match(line, equalValue).Value.Trim();
							else if (line.Trim().StartsWith("overlay_ulx"))
								item.Overlay.UpperLeftX = Int32.Parse(Regex.Match(line, equalValue).Value.Trim());
							else if (line.Trim().StartsWith("overlay_uly"))
								item.Overlay.UpperLeftY = Int32.Parse(Regex.Match(line, equalValue).Value.Trim());
							else if (line.Trim().StartsWith("overlay_urx"))
								item.Overlay.UpperRightX = Int32.Parse(Regex.Match(line, equalValue).Value.Trim());
							else if (line.Trim().StartsWith("overlay_ury"))
								item.Overlay.UpperRightY = Int32.Parse(Regex.Match(line, equalValue).Value.Trim());
							else if (line.Trim().StartsWith("overlay_llx"))
								item.Overlay.LowerLeftX = Int32.Parse(Regex.Match(line, equalValue).Value.Trim());
							else if (line.Trim().StartsWith("overlay_lly"))
								item.Overlay.LowerLeftY = Int32.Parse(Regex.Match(line, equalValue).Value.Trim());
							else if (line.Trim().StartsWith("overlay_lrx"))
								item.Overlay.LowerRightX = Int32.Parse(Regex.Match(line, equalValue).Value.Trim());
							else if (line.Trim().StartsWith("overlay_lry"))
								item.Overlay.LowerRightY = Int32.Parse(Regex.Match(line, equalValue).Value.Trim());
						}

						_openFile = new FileInfo(path);
						_imported = true;
						_theme = theme;
						_fileOpen = true;
						_hasChanges = false;

						// Clean up negative values
						foreach (Item itm in theme.MainItems)
						{
							int temp = 0;

							switch(itm.Type)
							{
								case ItemType.LoadingIcon:
									Image img = Image.FromFile(_openFile.DirectoryName + @"\load0.png");

									if (Int32.TryParse(itm.X, out temp))
										if (temp < 0)
											itm.X = (pnlMain.Width + temp - img.Width).ToString();
									if (Int32.TryParse(itm.Y, out temp))
										if (temp < 0)
											itm.Y = (pnlMain.Height + temp - img.Height).ToString();

									itm.Width = img.Width.ToString();
									itm.Height = img.Height.ToString();
									break;
								case ItemType.ItemIcon:
									itm.Width = "64";
									itm.Height = "64";
									break;
								case ItemType.ItemCover:
									//image = _openFile.DirectoryName + @"\" + itm.Overlay.Image + ".png";
									break;
								case ItemType.HintText:
									if (Int32.TryParse(itm.X, out temp))
										if (temp < 0)
											itm.X = (pnlMain.Width + temp).ToString();
									if (Int32.TryParse(itm.Y, out temp))
										if (temp < 0)
											itm.Y = (pnlMain.Height + temp).ToString();
									break;
							}
						}

						LoadTheme();
					}

					Settings.Default.InitialDirectory = new DirectoryInfo(_openFile.FullName).FullName;
				}
				catch (Exception)
				{
					_fileOpen = false;
					_hasChanges = false;
				}
				finally
				{
					UpdateForm();
				}
			}

			return dr;
		}

		private DialogResult SaveTheme(bool saveAs = false)
		{
			DialogResult dr = DialogResult.OK;

			if (_openFile == null || _imported || saveAs)
			{
				sfdSave.InitialDirectory = Settings.Default.InitialDirectory;
				dr = sfdSave.ShowDialog();
			}

			if ((_openFile == null || _imported || saveAs) && dr == DialogResult.OK)
			{
				_openFile = new FileInfo(sfdSave.FileName);
				Settings.Default.InitialDirectory = new DirectoryInfo(sfdSave.FileName).FullName;
				Settings.Default.Save();
			}

			if (dr == DialogResult.OK)
			{
				_theme.Save(_openFile.FullName);

				if (!Settings.Default.RecentlyOpenedFiles.Contains(_openFile.FullName))
				{
					Settings.Default.RecentlyOpenedFiles.Insert(0, _openFile.FullName);
				}
				else
				{
					Settings.Default.RecentlyOpenedFiles.Remove(_openFile.FullName);
					Settings.Default.RecentlyOpenedFiles.Insert(0, _openFile.FullName);
				}

				PopulateRecentlyOpenedFiles();

				_imported = false;
				_hasChanges = false;
				UpdateForm();
			}

			return dr;
		}

		// Form Population
		private void LoadTheme()
		{
			List<Control> items = new List<Control>();
			pnlMain.Controls.Clear();
			lstItems.Items.Clear();

			foreach (Item item in _theme.MainItems)
			{
				// Background
				if (item.Type == ItemType.Background)
				{
					string backgroundPath = _openFile.DirectoryName + @"\" + item.DefaultImage + ".png";

					SelectablePictureBox pb = new SelectablePictureBox();
					if (File.Exists(backgroundPath))
					{
						Image img = Image.FromFile(backgroundPath);
						pb.BackgroundImage = img;
						pb.BackgroundImageLayout = ImageLayout.Stretch;
					}
					pb.SetBounds(0, 0, 640, 480);
					pb.Tag = item;
					item.Tag = pb;
					items.Add(pb);

					lstItems.Items.Add(item);
					pb.GotFocus += new EventHandler(Item_GotFocus);
					pb.LostFocus += new EventHandler(Item_LostFocus);
				}

				// Item Cover
				else if (item.Type == ItemType.ItemCover)
				{
					string testGame = "SLUS_211.13";
					string itemCoverPath = _openFile.DirectoryName + @"\..\..\art\" + testGame + "_COV.jpg";

					ResizeablePictureBox pb = new ResizeablePictureBox(ResizeModes.Move);
					if (File.Exists(itemCoverPath))
					{
						Image img = Image.FromFile(itemCoverPath);
						pb.BackgroundImage = img;
						pb.BackgroundImageLayout = ImageLayout.Stretch;
						pb.SetBounds(Int(item.X), Int(item.Y), img.Width, img.Height);
					}
					pb.Tag = item;
					item.Tag = pb;
					items.Add(pb);

					lstItems.Items.Add(item);
					pb.GotFocus += new EventHandler(Item_GotFocus);
					pb.LostFocus += new EventHandler(Item_LostFocus);
					pb.ResizeOccurred += new ResizeablePictureBox.ResizeOccurredEventHandler(MoveResizeCompleted);
					pb.MouseMoved += new ResizeablePictureBox.MouseMovedEventHandler(HandleMouseMove);
				}

				// Loading Icon
				else if (item.Type == ItemType.LoadingIcon)
				{
					string backgroundPath = _openFile.DirectoryName + @"\load0.png";

					ResizeablePictureBox pb = new ResizeablePictureBox(ResizeModes.Move);
					if (File.Exists(backgroundPath))
					{
						Image img = Image.FromFile(backgroundPath);
						pb.BackgroundImage = img;
						pb.BackgroundImageLayout = ImageLayout.Stretch;
						pb.SetBounds(Int(item.X), Int(item.Y), Int(item.Width), Int(item.Height));
					}
					pb.Tag = item;
					item.Tag = pb;
					items.Add(pb);

					lstItems.Items.Add(item);
					pb.GotFocus += new EventHandler(Item_GotFocus);
					pb.LostFocus += new EventHandler(Item_LostFocus);
				}

				// Fallback
				else
				{
					SelectablePictureBox pb = new SelectablePictureBox();

					Image img = Properties.Resources.grid;
					pb.Opacity = 0.5f;
					pb.BackgroundImage = img;
					pb.BackgroundImageLayout = ImageLayout.Tile;

					pb.SetBounds(Int(item.X), Int(item.Y), Math.Max(Int(item.Width), 32), Math.Max(Int(item.Height), 32));
					pb.Tag = item;
					item.Tag = pb;
					items.Add(pb);

					lstItems.Items.Add(item);
					pb.GotFocus += new EventHandler(Item_GotFocus);
					pb.LostFocus += new EventHandler(Item_LostFocus);
				}
			}

			// Add controls to the form
			items.Reverse();
			foreach (Control pb in items)
			{
				pnlMain.Controls.Add(pb);
			}
		}

		private int Int(string value)
		{
			int temp = 0;
			Int32.TryParse(value, out temp);
			return temp;
		}

		protected void Item_GotFocus(object sender, EventArgs e)
		{
			Control ctrl = (Control)sender;
			lstItems.SelectedItem = ctrl.Tag;
		}

		protected void Item_LostFocus(object sender, EventArgs e)
		{
			//Control ctrl = (Control)sender;
			//lstItems.SelectedItem = ctrl.Tag;
		}



		protected void MoveResizeCompleted(Control control)
		{
			Item item = (Item)control.Tag;
			item.X = control.Location.X.ToString();
			item.Y = control.Location.Y.ToString();

			//lstItems.Items[lstItems.SelectedIndex] = lstItems.SelectedItem;
		}

		private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			((Item)lstItems.Items[lstItems.SelectedIndex]).Tag.Focus();
		}

		private Item NewItem()
		{
			Item item = new Item();
			item.Comment = "New Item";
			return item;
		}

		// Utilities
		private string FileName()
		{
			return _openFile == null ? "Untitled" : _openFile.Name;
		}

		private string ThemeName()
		{
			return _openFile == null ? " (Untitled)" : " (" + _theme.Name + ")";
		}

		private void UpdateForm()
		{
			bool _itemSelected = false;

			// Form Caption
			Text = Settings.Default.ApplicationName + " - " + FileName() + (_hasChanges ? "*" : string.Empty) + ThemeName();

			// Enable
			tsmSave.Enabled = _fileOpen;
			tsbSave.Enabled = _fileOpen;

			tsmSaveAs.Enabled = _fileOpen;
			tsbSaveAs.Enabled = _fileOpen;

			tsbAdd.Enabled = _fileOpen;

			tsbDelete.Enabled = _fileOpen && _itemSelected;

			tsbProperties.Enabled = _fileOpen && _itemSelected;
		}

		private void HandleMouseMove(int x, int y, int width, int height, int eX, int eY)
		{
			lblDimensions.Text = "Dimensions:\r\n";
			lblDimensions.Text += "X: " + x + "\r\n";
			lblDimensions.Text += "Y: " + y + "\r\n";
			lblDimensions.Text += "W: " + width + "\r\n";
			lblDimensions.Text += "H: " + height + "\r\n";

			lblDimensions.Text += "eX: " + eX + "\r\n";
			lblDimensions.Text += "eY: " + eY;
		}

		#region Change Detection



		#endregion
	}
}