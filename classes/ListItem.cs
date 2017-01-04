using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPL_Theme_Editor
{
	class ListItem
	{
		public string Text { get; set; }
		public object Value { get; set; }
		public bool Enabled { get; set; }

		public ListItem(string text, object value, bool enabled = true)
		{
			Text = text;
			Value = value;
			Enabled = enabled;
		}

		public override string ToString()
		{
			return Text;
		}
	}
}