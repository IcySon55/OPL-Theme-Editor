using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace OPL_Theme_Editor
{
	[XmlRoot("theme")]
	public class Theme
	{
		#region Properties

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("author")]
		public string Author { get; set; }

		[XmlAttribute("version")]
		public string Version { get; set; }

		[XmlAttribute("site")]
		public string Site { get; set; }

		[XmlElement("Notes")]
		public string Notes { get; set; }

		[XmlAttribute("bgColor")]
		public string BackgroundColor { get; set; }

		[XmlAttribute("selectedTextColor")]
		public string SelectedTextColor { get; set; }

		[XmlAttribute("settingsTextColor")]
		public string SettingsTextColor { get; set; }

		[XmlAttribute("textColor")]
		public string TextColor { get; set; }

		[XmlAttribute("uiTextColor")]
		public string UITextColor { get; set; }

		[XmlAttribute("font")]
		public string Font { get; set; }

		// Local
		[XmlAttribute("size")]
		public string Size { get; set; }

		[XmlArray("main")]
		[XmlArrayItem("item")]
		public List<Item> MainItems { get; set; }

		[XmlArray("info")]
		[XmlArrayItem("item")]
		public List<Item> InfoItems { get; set; }

		#endregion

		public Theme()
		{
			Name = string.Empty;
			Author = string.Empty;
			Version = string.Empty;
			Site = string.Empty;
			Notes = string.Empty;
			BackgroundColor = string.Empty;
			SelectedTextColor = string.Empty;
			SettingsTextColor = string.Empty;
			TextColor = string.Empty;
			UITextColor = string.Empty;
			Font = string.Empty;
			Size = "480";
			MainItems = new List<Item>();
			InfoItems = new List<Item>();
		}

		public static Theme Load(string filename)
		{
			try
			{
				using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					Theme theme = (Theme)new XmlSerializer(typeof(Theme)).Deserialize(fs);
					return theme;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public bool Save(string filename)
		{
			try
			{
				// XML
				XmlWriterSettings xmlSettings = new XmlWriterSettings();
				xmlSettings.Encoding = Encoding.UTF8;
				xmlSettings.Indent = true;
				xmlSettings.NewLineOnAttributes = false;
				xmlSettings.IndentChars = "	";

				using (StreamWriter xmlIO = new StreamWriter(filename, false, xmlSettings.Encoding))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(Theme));
					XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
					namespaces.Add(string.Empty, string.Empty);
					serializer.Serialize(XmlWriter.Create(xmlIO, xmlSettings), this, namespaces);
				}

				// conf_theme.cfg
				FileInfo fi = new FileInfo(filename);
				using (StreamWriter sw = new StreamWriter(new FileStream(fi.DirectoryName + @"\conf_theme.cfg", FileMode.Create, FileAccess.Write, FileShare.None), Encoding.UTF8))
				{
					sw.WriteLine("# Name: " + Name);
					sw.WriteLine("# Author: " + Author);
					sw.WriteLine("# Version: " + Version);
					sw.WriteLine("# Site: " + Site);

					// Notes
					foreach (string line in Regex.Split(Notes, "\r\n"))
					{
						if (line.Trim().Length > 0)
							sw.WriteLine("# " + line.Trim());
					}

					sw.WriteLine(string.Empty);

					// General Settings
					sw.WriteLine("bg_color=" + BackgroundColor);
					sw.WriteLine("sel_text_color=" + SelectedTextColor);
					sw.WriteLine("settings_text_color=" + SettingsTextColor);
					sw.WriteLine("text_color=" + TextColor);
					sw.WriteLine("ui_text_color=" + UITextColor);
					sw.WriteLine("default_font=" + Font);

					sw.WriteLine(string.Empty);

					// Main Items
					int counter = 0;
					foreach (Item item in MainItems)
					{
						// Label
						sw.WriteLine("main" + counter + ":");

						// Comment
						if (item.Comment.Length > 0)
							sw.WriteLine("#\t" + item.Comment);

						// Type
						sw.WriteLine("\ttype=" + item.Type.ToString());

						// X/Y
						if (item.Type != ItemType.Background && item.Type != ItemType.StaticImage)
						{
							sw.WriteLine("\tx=" + item.X);
							sw.WriteLine("\ty=" + item.Y);
						}

						// Width/Height
						if (item.Type != ItemType.Background && item.Type == ItemType.StaticImage)
						{
							sw.WriteLine("\twidth=" + item.Width);
							sw.WriteLine("\theight=" + item.Height);
						}

						// Default
						if (item.DefaultImage.Length > 0)
							sw.WriteLine("\tdefault=" + item.DefaultImage);

						counter++;
					}

					sw.WriteLine(string.Empty);
				}
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		public void AddItem(Item item, bool main = true)
		{
			if (main)
				MainItems.Add(item);
			else
				InfoItems.Add(item);
		}
	}

	public class Item
	{
		[XmlAttribute("comment")]
		public string Comment { get; set; }

		[XmlAttribute("type")]
		public ItemType Type { get; set; }

		[XmlAttribute("x")]
		public string X { get; set; }

		[XmlAttribute("y")]
		public string Y { get; set; }

		[XmlAttribute("width")]
		public string Width { get; set; }

		[XmlAttribute("height")]
		public string Height { get; set; }

		[XmlAttribute("attribute")]
		public string Attribute { get; set; }

		[XmlAttribute("pattern")]
		public string Pattern { get; set; }

		[XmlAttribute("default")]
		public string DefaultImage { get; set; }

		[XmlAttribute("color")]
		public string Color { get; set; }

		[XmlAttribute("aligned")]
		public int Aligned { get; set; }

		[XmlAttribute("display")]
		public int Display { get; set; }

		[XmlElement("overlay")]
		public Overlay Overlay { get; set; }

		[XmlIgnore()]
		public Control Tag { get; set; }

		public Item()
		{
			Comment = string.Empty;
			Type = ItemType.Background;
			X = string.Empty;
			Y = string.Empty;
			Width = string.Empty;
			Height = string.Empty;
			Attribute = string.Empty;
			Pattern = string.Empty;
			DefaultImage = string.Empty;
			Color = string.Empty;
			Aligned = 0;
			Display = 0;
			Overlay = new Overlay();
		}

		public override string ToString()
		{
			return Type + " (" + X + ", " + Y + ", " + Width + ", " + Height + ")";
		}
	}

	public class Overlay
	{
		[XmlAttribute("image")]
		public string Image { get; set; }

		[XmlAttribute("ulx")]
		public int UpperLeftX { get; set; }

		[XmlAttribute("uly")]
		public int UpperLeftY { get; set; }

		[XmlAttribute("urx")]
		public int UpperRightX { get; set; }

		[XmlAttribute("ury")]
		public int UpperRightY { get; set; }

		[XmlAttribute("llx")]
		public int LowerLeftX { get; set; }

		[XmlAttribute("lly")]
		public int LowerLeftY { get; set; }

		[XmlAttribute("lrx")]
		public int LowerRightX { get; set; }

		[XmlAttribute("lry")]
		public int LowerRightY { get; set; }
	}
}