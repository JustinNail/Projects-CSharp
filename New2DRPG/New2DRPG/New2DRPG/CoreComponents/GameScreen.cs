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

namespace New2DRPG.CoreComponents
{
	//inherits from DrawableGameComponent so it can draw itself
	public class GameScreen : Microsoft.Xna.Framework.DrawableGameComponent
	{
		//will contain menus, etc
		private List<GameComponent> childComponents;
		//property returning list of child components
		public List<GameComponent> Components
		{
			get { return childComponents; }
		}

		//constructor just take Game object
		//initializes childComponents and tells it not to draw yet
		public GameScreen(Game game)
			: base(game)
		{
			childComponents = new List<GameComponent>();
			Visible = false;
			Enabled = false;
		}
		
		public override void Initialize()
		{
			base.Initialize();
		}

		//update each child if it's enabled
		public override void Update(GameTime gameTime)
		{
			foreach (GameComponent child in childComponents)
			{
				if (child.Enabled)
				{
					child.Update(gameTime);
				}
			}
			base.Update(gameTime);
		}

		//if a child can and should be drawn, draw it
		public override void Draw(GameTime gameTime)
		{
			foreach (GameComponent child in childComponents)
			{
				if ((child is DrawableGameComponent) &&
					((DrawableGameComponent)child).Visible)
				{
					((DrawableGameComponent)child).Draw(gameTime);
				}
			}
			base.Draw(gameTime);
		}

		//should draw
		public virtual void Show()
		{
			Visible = true;
			Enabled = true;
		}
		//shouldn't
		public virtual void Hide()
		{
			Visible = false;
			Enabled = false;
		}
	}
}