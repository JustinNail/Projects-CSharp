using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Collections.Specialized;


namespace New2DRPG.CoreComponents
{
	//can draw itself
	public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
	{
		//sprite variables
		SpriteBatch spriteBatch = null;
		SpriteFont spriteFont;

		//colors
		Color normalColor = Color.Yellow;
		Color hiliteColor = Color.Red;


		KeyboardState oldState;

		Vector2 position = new Vector2();//menu position
		int selectedIndex = 0;//menu selection
		private StringCollection menuItems = new StringCollection();//string for menu names
		int width, height;//menu dimensions

		//takes the game object and what font to write menu with
		public MenuComponent(Game game, SpriteFont spriteFont)
			: base(game)
		{
			this.spriteFont = spriteFont;
			//use GetService to get the SpriteBatch because...
			spriteBatch =
				(SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
		}

		//properties for manipulating fields
		public int Width
		{
			get { return width; }
		}
		public int Height
		{
			get { return height; }
		}
		public int SelectedIndex
		{
			get { return selectedIndex; }
			set
			{
				//Clamp makes sure it's valid
				selectedIndex = (int)MathHelper.Clamp(
						value,
						0,
						menuItems.Count - 1);
			}
		}
		public Color NormalColor
		{
			get { return normalColor; }
			set { normalColor = value; }
		}
		public Color HiliteColor
		{
			get { return hiliteColor; }
			set { hiliteColor = value; }
		}
		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		//sets up the menu with the passed item names
		public void SetMenuItems(string[] items)
		{
			menuItems.Clear();
			menuItems.AddRange(items);
			CalculateBounds();
		}

		//makes the item widths uniform, based on longest string
		private void CalculateBounds()
		{
			width = 0;
			height = 0;
			foreach (string item in menuItems)
			{
				Vector2 size = spriteFont.MeasureString(item);
				if (size.X > width)
					width = (int)size.X;
				height += spriteFont.LineSpacing;
			}
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		//up and down keys cycles through list
		//updates keyboard state
		public override void Update(GameTime gameTime)
		{
			KeyboardState newState = Keyboard.GetState();
			if (CheckKey(Keys.Down))
			{
				selectedIndex++;
				if (selectedIndex == menuItems.Count)
					selectedIndex = 0;
			}
			if (CheckKey(Keys.Up))
			{
				selectedIndex--;
				if (selectedIndex == -1)
				{
					selectedIndex = menuItems.Count - 1;
				}
			}
			oldState = newState;
			base.Update(gameTime);
		}

		//checks if the passed key is pressed
		public bool CheckKey(Keys theKey)
		{
			KeyboardState newState = Keyboard.GetState();
			return oldState.IsKeyDown(theKey) && newState.IsKeyUp(theKey);
		}


		public override void Draw(GameTime gameTime)
		{
			Vector2 menuPosition = Position;
			Color myColor;
			for (int i = 0; i < menuItems.Count; i++)
			{
				if (i == SelectedIndex)
					myColor = HiliteColor;
				else
					myColor = NormalColor;
				spriteBatch.DrawString(
					spriteFont,
					menuItems[i],
					menuPosition + Vector2.One,
					Color.Black);
				spriteBatch.DrawString(spriteFont,
					menuItems[i],
					menuPosition,
					myColor);
				menuPosition.Y += spriteFont.LineSpacing;
			} base.Draw(gameTime);
		}
	}
}