using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using TBSgame.Controls;

namespace TBSgame.States
{
	public class TitleScreen : BaseGameScreen
	{
		#region Field region
		Texture2D backgroundImage;

		LinkLabel startLabel;

		LinkLabel newLabel;
		LinkLabel loadLabel;
		LinkLabel exitLabel;

		
		#endregion

		#region Constructor region
		public TitleScreen(Game game, GameScreenManager manager)
			: base(game, manager)
		{
		}
		#endregion

		#region XNA Method region
		protected override void LoadContent()
		{
			ContentManager Content = GameRef.Content;//Game1's ContentManager
			backgroundImage = Content.Load<Texture2D>(@"Backgrounds\TitleScreen");
			base.LoadContent();

			startLabel=new LinkLabel("CLICK to continue");
			startLabel.Position = new Vector2(300, 500);
			startLabel.Color = Color.Black;
			startLabel.SelectedColor = Color.Red;
			startLabel.Selected+=new EventHandler(startLabel_Selected);
			ControlManager.Add(startLabel);

			newLabel = new LinkLabel("New Game");
			newLabel.Visible = false;
			newLabel.Enabled = false;
			newLabel.Position = new Vector2(150, 500);
			newLabel.Color = Color.Black;
			newLabel.SelectedColor = Color.Red;
			newLabel.Selected += new EventHandler(newLabel_Selected);
			ControlManager.Add(newLabel);

			loadLabel = new LinkLabel("Load Game");
			loadLabel.Visible = false;
			loadLabel.Enabled = false;
			loadLabel.Position = new Vector2(300, 500);
			loadLabel.Color = Color.Black;
			loadLabel.SelectedColor = Color.Red;
			loadLabel.Selected += new EventHandler(loadLabel_Selected);
			ControlManager.Add(loadLabel);

			exitLabel = new LinkLabel("Exit");
			exitLabel.Visible = false;
			exitLabel.Enabled = false;
			exitLabel.Position = new Vector2(450, 500);
			exitLabel.Color = Color.Black;
			exitLabel.SelectedColor = Color.Red;
			exitLabel.Selected += new EventHandler(exitLabel_Selected);
			ControlManager.Add(exitLabel);

		}

		public override void Update(GameTime gameTime)
		{
			ControlManager.Update(gameTime, PlayerIndex.One);
			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			GameRef.SpriteBatch.Begin();

			base.Draw(gameTime);

			GameRef.SpriteBatch.Draw(
				backgroundImage,
				GameRef.ScreenRectangle,
				Color.White);

			ControlManager.Draw(GameRef.SpriteBatch);

			GameRef.SpriteBatch.End();
		}
		#endregion

		#region Title Screen Methods
		private void startLabel_Selected(object sender, EventArgs e)
		{
			startLabel.Visible = false;
			startLabel.Enabled = false;
			ControlManager.Remove(startLabel);

			newLabel.Visible = true;
			newLabel.Enabled = true;

			loadLabel.Visible = true;
			loadLabel.Enabled = true;

			exitLabel.Visible = true;
			exitLabel.Enabled = true;

		}
		private void newLabel_Selected(object sender, EventArgs e)
		{
			//StateManager.PushState(GameRef.StartMenuScreen);
		}
		private void loadLabel_Selected(object sender, EventArgs e)
		{
			//StateManager.PushState(GameRef.StartMenuScreen);
		}
		private void exitLabel_Selected(object sender, EventArgs e)
		{
			GameRef.Exit();
		}
		#endregion
	}
}
