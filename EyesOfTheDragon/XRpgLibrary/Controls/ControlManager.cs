using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace XRpgLibrary.Controls
{
	//ControlManager is a List of controls with added functionalities
	public class ControlManager : List<Control>
	{
		#region Fields and Properties
		int selectedControl = 0;//index
		static SpriteFont spriteFont;//default SpriteFont
		public static SpriteFont SpriteFont
		{
			get { return spriteFont; }
		}
		#endregion

		#region Event Region
		public event EventHandler FocusChanged;
		#endregion

		#region Constructors
		//all take a default SpriteFont for all controls
		//one for each List<T> constructor

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
			//update each Control and handle input on focused Control
			foreach (Control c in this)
			{
				if (c.Enabled)
					c.Update(gameTime);
				if (c.HasFocus)
					c.HandleInput(playerIndex);
			}

			//cycle through controls
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
			//draw each Control
			foreach (Control c in this)
			{
				if (c.Visible)
					c.Draw(spriteBatch);
			}
		}
		public void NextControl()
		{
			if (Count == 0)
				return;
			int currentControl = selectedControl;
			this[selectedControl].HasFocus = false;
			do
			{
				selectedControl++;
				if (selectedControl == Count)
					selectedControl = 0;
				if (this[selectedControl].TabStop && this[selectedControl].Enabled)
				{
					if (FocusChanged != null)
						FocusChanged(this[selectedControl], null);
					break;
				}
			} while (currentControl != selectedControl);
			this[selectedControl].HasFocus = true;
		}
		public void PreviousControl()
		{
			if (Count == 0)
				return;
			int currentControl = selectedControl;
			this[selectedControl].HasFocus = false;
			do
			{
				selectedControl--;
				if (selectedControl < 0)
					selectedControl = Count - 1;
				if (this[selectedControl].TabStop && this[selectedControl].Enabled)
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