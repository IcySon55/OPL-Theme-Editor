using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OPL_Theme_Editor
{
	class ResizeablePictureBox : SelectablePictureBox
	{
		private int _gripWidth = 4;
		private ResizeModes _edges = ResizeModes.None;
		private ResizeModes _allowedEdges = ResizeModes.All;
		private Rectangle _controlOrigin = new Rectangle(0, 0, 0, 0);
		private Point _mouseOrigin = new Point(0, 0);
		private bool _mouseDown = false;

		public event ResizeOccurredEventHandler ResizeOccurred;
		public delegate void ResizeOccurredEventHandler(ResizeablePictureBox sender);

		public event MouseMovedEventHandler MouseMoved;
		public delegate void MouseMovedEventHandler(int x, int y, int width, int height, int eX, int eY);

		public ResizeModes AllowedEdges
		{
			get
			{
				return _allowedEdges;
			}
			set
			{
				_allowedEdges = value;
			}
		}

		public ResizeablePictureBox(ResizeModes edges, int gripWidth = 4) : base()
		{
			_gripWidth = gripWidth;
			_allowedEdges = edges;
		}

		protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if (e.KeyCode == Keys.Escape && _mouseDown)
			{
				_mouseDown = false;
				SetBounds(_controlOrigin.X, _controlOrigin.Y, _controlOrigin.Width, _controlOrigin.Height);
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			Cursor = Cursors.Default;
			_edges = ResizeModes.None;
			Refresh();
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left)
			{
				_mouseDown = true;
				_mouseOrigin = new Point(e.X, e.Y);
				_controlOrigin = new Rectangle(Location.X, Location.Y, Width, Height);
			}
			if (MouseMoved != null)
				MouseMoved(Left, Top, Width, Height, e.X, e.Y);
		}

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_mouseDown = false;

			if (ResizeOccurred != null)
				ResizeOccurred(this);
		}

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (_mouseDown && _edges != ResizeModes.None && Focused)
			{
				int x = 0;
				int y = 0;
				int width = 0;
				int height = 0;

				SuspendLayout();
				switch (_edges)
				{
					case ResizeModes.Move:
						x = Math.Max(Math.Min(Left + e.X - _mouseOrigin.X, Parent.Width - Width), 0);
						y = Math.Max(Math.Min(Top + e.Y - _mouseOrigin.Y, Parent.Height - Height), 0);
						SetBounds(x, y, Width, Height);
						break;
					case ResizeModes.Top:
						y = Math.Max(Math.Min(Top + e.Y, Parent.Height - Height), 0);
						height = Math.Max(Math.Min(Height - e.Y, _controlOrigin.Height + _controlOrigin.Y), 0);
						SetBounds(Left, y, Width, height);
						break;
					case ResizeModes.Right:
						width = Math.Max(Math.Min(Width - (Width - e.X), Parent.Width - _controlOrigin.X), 0);
						SetBounds(Left, Top, width, Height);
						break;
					case ResizeModes.Bottom:
						height = Math.Max(Math.Min(Height - (Height - e.Y), Parent.Height - _controlOrigin.Y), 0);
						SetBounds(Left, Top, Width, height);
						break;
					case ResizeModes.Left:
						x = Math.Max(Math.Min(Left + e.X, Parent.Width - Width), 0);
						width = Math.Max(Math.Min(Width - e.X, _controlOrigin.Width + _controlOrigin.X), 0);
						SetBounds(x, Top, width, Height);
						break;
					case ResizeModes.TopLeft:
						x = Math.Max(Math.Min(Left + e.X, Parent.Width - Width), 0);
						y = Math.Max(Math.Min(Top + e.Y, Parent.Height - Height), 0);
						width = Math.Max(Math.Min(Width - e.X, _controlOrigin.Width + _controlOrigin.X), 0);
						height = Math.Max(Math.Min(Height - e.Y, _controlOrigin.Height + _controlOrigin.Y), 0);
						SetBounds(x, y, width, height);
						break;
					case ResizeModes.TopRight:
						y = Math.Max(Math.Min(Top + e.Y, Parent.Height - Height), 0);
						width = Math.Max(Math.Min(Width - (Width - e.X), Parent.Width - _controlOrigin.X), 0);
						height = Math.Max(Math.Min(Height - e.Y, _controlOrigin.Height + _controlOrigin.Y), 0);
						SetBounds(Left, y, width, height);
						break;
					case ResizeModes.BottomRight:
						width = Math.Max(Math.Min(Width - (Width - e.X), Parent.Width - _controlOrigin.X), 0);
						height = Math.Max(Math.Min(Height - (Height - e.Y), Parent.Height - _controlOrigin.Y), 0);
						SetBounds(Left, Top, width, height);
						break;
					case ResizeModes.BottomLeft:
						x = Math.Max(Math.Min(Left + e.X, Parent.Width - Width), 0);
						width = Math.Max(Math.Min(Width - e.X, _controlOrigin.Width + _controlOrigin.X), 0);
						height = Math.Max(Math.Min(Height - (Height - e.Y), Parent.Height - _controlOrigin.Y), 0);
						SetBounds(x, Top, width, height);
						break;
				}
				Invalidate();
				ResumeLayout();

				if (MouseMoved != null)
					MouseMoved(Left, Top, Width, Height, e.X, e.Y);
			}
			else
			{
				if (Focused)
				{
					if (_allowedEdges == ResizeModes.Move && e.X >= 0 && e.Y <= Width && e.Y >= 0 && e.Y <= Height)
					{
						// Move
						Cursor = Cursors.SizeAll;
						_edges = ResizeModes.Move;
					}
					else if (e.X >= _gripWidth && e.X <= Width - _gripWidth && e.Y >= _gripWidth && e.Y <= Height - _gripWidth)
					{
						// Move
						Cursor = Cursors.SizeAll;
						_edges = ResizeModes.Move;
					}
					else if (e.X <= _gripWidth && e.Y <= _gripWidth)
					{
						// TopLeft
						Cursor = Cursors.SizeNWSE;
						_edges = ResizeModes.TopLeft;
					}
					else if (e.X >= Width - _gripWidth && e.Y <= _gripWidth)
					{
						// TopRight
						Cursor = Cursors.SizeNESW;
						_edges = ResizeModes.TopRight;
					}
					else if (e.X >= Width - _gripWidth && e.Y >= Height - _gripWidth)
					{
						// BottomRight
						Cursor = Cursors.SizeNWSE;
						_edges = ResizeModes.BottomRight;
					}
					else if (e.X <= _gripWidth && e.Y >= Height - _gripWidth)
					{
						// BottomLeft
						Cursor = Cursors.SizeNESW;
						_edges = ResizeModes.BottomLeft;
					}
					else if (e.Y <= _gripWidth)
					{
						// Top
						Cursor = Cursors.SizeNS;
						_edges = ResizeModes.Top;
					}
					else if (e.X >= Width - _gripWidth)
					{
						// Right
						Cursor = Cursors.SizeWE;
						_edges = ResizeModes.Right;
					}
					else if (e.Y >= Height - _gripWidth)
					{
						// Bottom
						Cursor = Cursors.SizeNS;
						_edges = ResizeModes.Bottom;
					}
					else if (e.X <= _gripWidth)
					{
						// Left
						Cursor = Cursors.SizeWE;
						_edges = ResizeModes.Left;
					}
					else
					{
						Cursor = Cursors.Default;
						_edges = ResizeModes.None;
					}
				}

				_edges = _edges & _allowedEdges;
				if (_edges == ResizeModes.None)
					Cursor = Cursors.Default;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.Focused)
			{
				Pen pBlack = new Pen(Color.Black);
				SolidBrush sbWhite = new SolidBrush(Color.White);

				if ((_allowedEdges & ResizeModes.TopLeft) == ResizeModes.TopLeft)
				{
					e.Graphics.DrawRectangle(pBlack, new Rectangle(0, 0, _gripWidth + 1, _gripWidth + 1));
					e.Graphics.FillRectangle(sbWhite, new Rectangle(1, 1, _gripWidth, _gripWidth));
				}

				if ((_allowedEdges & ResizeModes.TopRight) == ResizeModes.TopRight)
				{
					e.Graphics.DrawRectangle(pBlack, new Rectangle(Width - _gripWidth - 2, 0, _gripWidth + 1, _gripWidth + 1));
					e.Graphics.FillRectangle(sbWhite, new Rectangle(Width - _gripWidth - 1, 1, _gripWidth, _gripWidth));
				}

				if ((_allowedEdges & ResizeModes.BottomRight) == ResizeModes.BottomRight)
				{
					e.Graphics.DrawRectangle(pBlack, new Rectangle(Width - _gripWidth - 2, Height - _gripWidth - 2, _gripWidth + 1, _gripWidth + 1));
					e.Graphics.FillRectangle(sbWhite, new Rectangle(Width - _gripWidth - 1, Height - _gripWidth - 1, _gripWidth, _gripWidth));
				}

				if ((_allowedEdges & ResizeModes.BottomLeft) == ResizeModes.BottomLeft)
				{
					e.Graphics.DrawRectangle(pBlack, new Rectangle(0, Height - _gripWidth - 2, _gripWidth + 1, _gripWidth + 1));
					e.Graphics.FillRectangle(sbWhite, new Rectangle(1, Height - _gripWidth - 1, _gripWidth, _gripWidth));
				}
			}
		}
	}
}