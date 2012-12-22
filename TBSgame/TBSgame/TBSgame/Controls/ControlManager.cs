using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TBSgame.Controls
{
	public class ControlManager : List<Control>
	{
		#region Fields and Properties

		int selectedControl = 0;
		bool acceptInput = true;

		static SpriteFont spriteFont;

		public static SpriteFont SpriteFont
		{
			get { return spriteFont; }
		}

		public bool AcceptInput
		{
			get { return acceptInput; }
			set { acceptInput = value; }
		}

		#endregion

		#region Event Region
		public event EventHandler FocusChanged;
		#endregion

		#region Constructors
		public ControlManager(SpriteFont spriteFont)
			: base()
		{
			ControlManager.spriteFont = spriteFont;
		}

		public ControlManager(SpriteFont spriteFont, int capacity)
			: base(capacity)
		{
			ControlManager.spriteFont = spriteFont;
		}

		public ControlManager(SpriteFont spriteFont, IEnumerable<Control> collection) :
			base(collection)
		{
			ControlManager.spriteFont = spriteFont;
		}
		#endregion

		#region Methods

		public void Update(GameTime gameTime, PlayerIndex playerIndex)
		{
			if (Count == 0)
				return;

			foreach (Control c in this)
			{
				if (c.Enabled)
				{
					c.Update(gameTime);
				}
			}

			foreach (Control c in this)
			{
				if (c.HasFocus)
				{
					c.HandleInput(playerIndex);
					break;
				}
			}
			foreach (Control c in this)
			{
				if (InputHandler.LeftMouseButtonDown())
				{
					if (InputHandler.MouseInArea(c.Area))
					{
							c.HasFocus = true;
							break;
					}
					
				}
			}
			if (!AcceptInput)
			{
				return;
			}

			if (InputHandler.ButtonPressed(Buttons.LeftThumbstickUp, playerIndex) ||
				InputHandler.ButtonPressed(Buttons.DPadUp, playerIndex) ||
				InputHandler.KeyPressed(Keys.Up))
			{
				PreviousControl();
			}

			if (InputHandler.ButtonPressed(Buttons.LeftThumbstickDown, playerIndex) ||
				InputHandler.ButtonPressed(Buttons.DPadDown, playerIndex) ||
				InputHandler.KeyPressed(Keys.Down))
			{
				NextControl();
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (Control c in this)
			{
				if (c.Visible)
					c.Draw(spriteBatch);
			}
		}

		public void NextControl()
		{
			//no controls
			if (Count == 0)
			{
				return;
			}

			int currentControl = selectedControl;
			this[selectedControl].HasFocus = false;

			//cycle through until you find a valid control, or go back to begining
			do
			{
				selectedControl++;
				if (selectedControl == Count)
				{
					selectedControl = 0;
				}

				if (this[selectedControl].Selectable && this[selectedControl].Enabled)
				{
					if (FocusChanged != null)
					{
						FocusChanged(this[selectedControl], null);
					}
					break;
				}

			} while (currentControl != selectedControl);

			this[selectedControl].HasFocus = true;
		}

		public void PreviousControl()
		{
			//no controls
			if (Count == 0)
			{
				return;
			}

			int currentControl = selectedControl;
			this[selectedControl].HasFocus = false;

			//cycle through until valid control, or back at begining
			do
			{
				selectedControl--;

				if (selectedControl < 0)
				{
					selectedControl = Count - 1;
				}

				if (this[selectedControl].Selectable && this[selectedControl].Enabled)
				{
					if (FocusChanged != null)
						FocusChanged(this[selectedControl], null);

					break;
				}
			} while (currentControl != selectedControl);

			this[selectedControl].HasFocus = true;
		}

		#endregion
	}
}
