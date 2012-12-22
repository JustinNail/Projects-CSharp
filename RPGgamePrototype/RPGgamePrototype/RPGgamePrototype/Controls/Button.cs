using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGgamePrototype.Controls
{
	public class Button : Control
	{
		#region Fields
		Texture2D image;
		Texture2D selectedImage;
		#endregion

		#region Properties
		public Texture2D SelectedImage { get { return selectedImage; } set { selectedImage = value; } }
		public Texture2D Image { get { return image; } set { image = value; } }
		#endregion

		#region Constructor Region
		public Button(string text)
			:base(text)
		{
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
				spriteBatch.Draw(SelectedImage, Position, Color.White);
			}
			else
			{
				spriteBatch.Draw(Image, Position, Color.White);
			}
			float x = Position.X + (Size.X / 3);
			float y = Position.Y + (Size.Y / 3);
			
			Vector2 textPos = new Vector2(x,y);
			spriteBatch.DrawString(SpriteFont, Text, textPos, Color);
		}

		public override void HandleInput(PlayerIndex playerIndex)
		{
			if (!HasFocus)
			{
				return;
			}

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

				if (
					InputHandler.MouseState.X < (Area.Right) &&
					InputHandler.MouseState.Y < (Area.Bottom) &&

					InputHandler.MouseState.X > (Area.Left) &&
					InputHandler.MouseState.Y > (Area.Top)
					)
				{
					base.OnSelected(null);
				}
				
			}
		}
		#endregion
	}
}
