using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace OPL_Theme_Editor
{
	class SelectablePictureBox : PictureBox
	{
		private float _opacity = 1.0f;

		public float Opacity
		{
			get
			{
				return _opacity;
			}
			set
			{
				_opacity = value;
			}
		}

		public SelectablePictureBox()
		{
			this.SetStyle(ControlStyles.Selectable, true);
			this.TabStop = true;
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			this.Parent.Refresh();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.Focus();
			base.OnMouseDown(e);
		}

		protected override void OnEnter(EventArgs e)
		{
			this.Invalidate();
			base.OnEnter(e);
		}

		protected override void OnLeave(EventArgs e)
		{
			this.Invalidate();
			base.OnLeave(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.Focused)
			{
				ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle, Color.White, Color.Black);
			}
		}

		protected override void OnPaintBackground(PaintEventArgs e)
		// Paint background with underlying graphics from other controls
		{
			base.OnPaintBackground(e);
			Graphics g = e.Graphics;

			if (Parent != null)
			{
				// Take each control in turn
				int index = Parent.Controls.GetChildIndex(this);
				for (int i = Parent.Controls.Count - 1; i > index; i--)
				{
					Control c = Parent.Controls[i];

					// Check it's visible and overlaps this control
					if (c.Bounds.IntersectsWith(Bounds) && c.Visible)
					{
						// Load appearance of underlying control and redraw it on this background
						Bitmap bmp = new Bitmap(c.Width, c.Height, g);
						c.DrawToBitmap(bmp, c.ClientRectangle);
						g.TranslateTransform(c.Left - Left, c.Top - Top);
						g.DrawImageUnscaled(bmp, Point.Empty);
						g.TranslateTransform(Left - c.Left, Top - c.Top);
						bmp.Dispose();
					}
				}
			}

			// Paint my own background
			if (BackgroundImage != null)
			{
				if (BackgroundImageLayout == ImageLayout.Tile)
				{
					ColorMatrix matrix = new ColorMatrix();
					ImageAttributes attributes = new ImageAttributes();
					matrix.Matrix33 = _opacity;
					attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

					Image newTex = new Bitmap(BackgroundImage);
					Graphics g2 = Graphics.FromImage(newTex);
					g2.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
					g2.DrawImage(BackgroundImage, Rectangle.FromLTRB(0, 0, BackgroundImage.Width, BackgroundImage.Height), 0, 0, BackgroundImage.Width, BackgroundImage.Height, GraphicsUnit.Pixel, attributes);

					g.FillRectangle(new TextureBrush(newTex), this.ClientRectangle);
				}
				else if (BackgroundImageLayout == ImageLayout.Stretch)
					g.DrawImage(this.BackgroundImage, this.ClientRectangle);
			}
		}
	}
}