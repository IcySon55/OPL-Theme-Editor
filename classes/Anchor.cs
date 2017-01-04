using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace OPL_Theme_Editor
{
	public class Anchor
	{
		private Control _controlWithEvents;
		private ResizeModes _edges = ResizeModes.None;
		private int _gripWidth = 4;
		private bool _mouseDown = false;
		private bool _outlineDrawn = false;
		private ResizeModes _allowedEdges = ResizeModes.All;
		private Rectangle _controlOrigin = new Rectangle(0, 0, 0, 0);
		private Point _mouseOrigin = new Point(0, 0);

		public event ResizeOccurredEventHandler ResizeOccurred;
		public delegate void ResizeOccurredEventHandler(ref Control control);

		#region Properties

		private Control _control
		{
			get
			{
				return _controlWithEvents;
			}
			set
			{
				if (_controlWithEvents != null)
				{
					_controlWithEvents.MouseDown -= _control_MouseDown;
					_controlWithEvents.MouseUp -= _control_MouseUp;
					_controlWithEvents.MouseMove -= _control_MouseMove;
					_controlWithEvents.MouseLeave -= _control_MouseLeave;
				}
				_controlWithEvents = value;
				if (_controlWithEvents != null)
				{
					_controlWithEvents.KeyUp += _control_KeyUp;
					_controlWithEvents.MouseDown += _control_MouseDown;
					_controlWithEvents.MouseUp += _control_MouseUp;
					_controlWithEvents.MouseMove += _control_MouseMove;
					_controlWithEvents.MouseLeave += _control_MouseLeave;
					_controlWithEvents.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(_controlWithEvents, true, null);
				}
			}
		}

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

		#endregion

		public Anchor(Control Control)
		{
			_control = Control;
		}

		public Anchor(Control control, ResizeModes edges)
		{
			_control = control;
			_allowedEdges = edges;
		}

		#region Event Handlers

		private void _control_KeyUp(object sender, KeyEventArgs e)
		{
			Control control = (Control)sender;
			if (e.KeyCode == Keys.Escape)
			{
				_mouseDown = false;
				control.SetBounds(_controlOrigin.X, _controlOrigin.Y, _controlOrigin.Width, _controlOrigin.Height);
			}
		}

		private void _control_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				_mouseDown = true;
				_mouseOrigin = new Point(e.X, e.Y);
				_controlOrigin = new Rectangle(_controlWithEvents.Location.X, _controlWithEvents.Location.Y, _controlWithEvents.Width, _controlWithEvents.Height);
			}
		}

		private void _control_MouseUp(object sender, MouseEventArgs e)
		{
			Control control = (Control)sender;
			_mouseDown = false;

			if (ResizeOccurred != null)
				ResizeOccurred(ref control);
		}

		private void _control_MouseMove(object sender, MouseEventArgs e)
		{
			Control control = (Control)sender;

			if (_mouseDown && _edges != ResizeModes.None && control.Focused)
			{
				control.SuspendLayout();
				switch (_edges)
				{
					case ResizeModes.Move:
						int x = Math.Max(Math.Min(control.Left + e.X - _mouseOrigin.X, control.Parent.Width - control.Width), 0);
						int y = Math.Max(Math.Min(control.Top + e.Y - _mouseOrigin.Y, control.Parent.Height - control.Height), 0);
						control.SetBounds(x, y, control.Width, control.Height);
						break;
					case ResizeModes.Top:

						// TODO: Limit all resizes by parent bounds

						control.SetBounds(control.Left, control.Top + e.Y, control.Width, control.Height - e.Y);
						break;
					case ResizeModes.Right:
						control.SetBounds(control.Left, control.Top, control.Width - (control.Width - e.X), control.Height);
						break;
					case ResizeModes.Bottom:
						control.SetBounds(control.Left, control.Top, control.Width, control.Height - (control.Height - e.Y));
						break;
					case ResizeModes.Left:
						control.SetBounds(control.Left + e.X, control.Top, control.Width - e.X, control.Height);
						break;
					case ResizeModes.TopLeft:
						control.SetBounds(control.Left + e.X, control.Top + e.Y, control.Width - e.X, control.Height - e.Y);
						break;
					case ResizeModes.TopRight:
						control.SetBounds(control.Left, control.Top + e.Y, control.Width - (control.Width - e.X), control.Height - e.Y);
						break;
					case ResizeModes.BottomRight:
						control.SetBounds(control.Left, control.Top, control.Width - (control.Width - e.X), control.Height - (control.Height - e.Y));
						break;
					case ResizeModes.BottomLeft:
						control.SetBounds(control.Left + e.X, control.Top, control.Width - e.X, control.Height - (control.Height - e.Y));
						break;
				}
				control.ResumeLayout();
			}
			else
			{
				if (control.Focused)
				{
					if (e.X >= _gripWidth && e.X <= control.Width - _gripWidth && e.Y >= _gripWidth && e.Y <= control.Height - _gripWidth)
					{
						// Move
						control.Cursor = Cursors.SizeAll;
						_edges = ResizeModes.Move;
					}
					else if (e.X <= _gripWidth && e.Y <= _gripWidth)
					{
						// TopLeft
						control.Cursor = Cursors.SizeNWSE;
						_edges = ResizeModes.TopLeft;
					}
					else if (e.X >= control.Width - _gripWidth && e.Y <= _gripWidth)
					{
						// TopRight
						control.Cursor = Cursors.SizeNESW;
						_edges = ResizeModes.TopRight;
					}
					else if (e.X >= control.Width - _gripWidth && e.Y >= control.Height - _gripWidth)
					{
						// BottomRight
						control.Cursor = Cursors.SizeNWSE;
						_edges = ResizeModes.BottomRight;
					}
					else if (e.X <= _gripWidth && e.Y >= control.Height - _gripWidth)
					{
						// BottomLeft
						control.Cursor = Cursors.SizeNESW;
						_edges = ResizeModes.BottomLeft;
					}
					else if (e.Y <= _gripWidth)
					{
						// Top
						control.Cursor = Cursors.SizeNS;
						_edges = ResizeModes.Top;
					}
					else if (e.X >= control.Width - _gripWidth)
					{
						// Right
						control.Cursor = Cursors.SizeWE;
						_edges = ResizeModes.Right;
					}
					else if (e.Y >= control.Height - _gripWidth)
					{
						// Bottom
						control.Cursor = Cursors.SizeNS;
						_edges = ResizeModes.Bottom;
					}
					else if (e.X <= _gripWidth)
					{
						// Left
						control.Cursor = Cursors.SizeWE;
						_edges = ResizeModes.Left;
					}
					else
					{
						control.Cursor = Cursors.Default;
						_edges = ResizeModes.None;
					}
				}

				_edges = _edges & _allowedEdges;
				if (_edges == ResizeModes.None)
					control.Cursor = Cursors.Default;
			}
		}

		private void _control_MouseLeave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			c.Cursor = Cursors.Default;
			_edges = ResizeModes.None;
			c.Refresh();
		}

		#endregion
	}
}