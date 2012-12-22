using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RPGgamePrototype.Controls
{
	class ScreenWideDetector:Control
	{
		#region Constructor Region
		public ScreenWideDetector(string text)
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
		}

		public override void HandleInput(PlayerIndex playerIndex)
		{
			if (!HasFocus)
			{
				return;
			}

			if (
				InputHandler.KeyReleased(Keys.Enter) ||
				InputHandler.ButtonReleased(Buttons.A, playerIndex) ||
				InputHandler.LeftMouseButtonReleased()
				)
			{
				base.OnSelected(null);
			}
		}
		#endregion

	}
}
