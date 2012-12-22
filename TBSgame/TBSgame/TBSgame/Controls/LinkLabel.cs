using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TBSgame.Controls
{
	public class LinkLabel : Control
	{
		#region Fields and Properties
		Color selectedColor = Color.Red;

		public Color SelectedColor{	get { return selectedColor; }	set { selectedColor = value; }	}
		#endregion

		#region Constructor Region
		public LinkLabel(string text)
			:base(text)
		{
			Size = SpriteFont.MeasureString(Text);
			Selectable = true;
			HasFocus = false;
			Position = Vector2.Zero;
		}
		#endregion

		#region Abstract Methods
		public override void Update(GameTime gameTime)
		{
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (HasFocus)
			{
				
				spriteBatch.DrawString(SpriteFont, Text, Position, selectedColor);
			}
			else
			{
				spriteBatch.DrawString(SpriteFont, Text, Position, Color);
			}
		}

		public override void HandleInput(PlayerIndex playerIndex)
		{
			if (!HasFocus)
				return;

			if (
				InputHandler.KeyReleased(Keys.Enter) ||
				InputHandler.ButtonReleased(Buttons.A, playerIndex)
				)
			{
				base.OnSelected(null);
			}

			if (InputHandler.LeftMouseButtonReleased())
			{
				HasFocus = false;

				if (InputHandler.MouseInArea(Area))
				{
					base.OnSelected(null);
				}
			}
		}
		#endregion
	}
}
