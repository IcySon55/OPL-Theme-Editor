using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace OPL_Theme_Editor
{
	class PathUtil
	{
		public static string CompactPathFromLeft(string longPathName, int wantedLength)
		{
			string result = longPathName;
			string pattern = @"(?<=:)(\\\.{3})?\\[^\\/:*?""<>|\r\n.]+?\\(?!$)";

			while (result.Length > wantedLength)
			{
				if (!Regex.IsMatch(result, pattern))
					break;
				result = Regex.Replace(result, pattern, @"\...\", RegexOptions.IgnoreCase);
			}

			return result;
		}
	}
}