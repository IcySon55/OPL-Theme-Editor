using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPL_Theme_Editor
{
	public enum ItemType
	{
		Background,
		StaticImage,
		StaticText,
		MenuIcon,
		MenuText,
		ItemIcon,
		ItemCover,
		GameImage,
		ItemsList,
		ItemText,
		HintText,
		InfoHintText,
		LoadingIcon,
		AttributeImage,
		AttributeText
	}

	[Flags()]
	public enum ResizeModes
	{
		None = 0,
		Move = 1,
		Top = 2,
		Right = 4,
		Bottom = 8,
		Left = 16,
		TopLeft = 32,
		TopRight = 64,
		BottomRight = 128,
		BottomLeft = 256,
		All = Move | Top | Right | Bottom | Left | TopLeft | TopRight | BottomRight | BottomLeft
	}
}