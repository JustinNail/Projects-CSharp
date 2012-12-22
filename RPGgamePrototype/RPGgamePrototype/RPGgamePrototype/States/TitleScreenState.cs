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

using RPGgamePrototype.Controls;

namespace RPGgamePrototype.States
{
	public class TitleScreenState : BaseGameState
	{
		#region Field region
		Texture2D backgroundImage;

		LinkLabel startLabel;

		Button startButton;
		Button loadButton;
		Button exitButton;
		#endregion

		#region Constructor region
		public TitleScreenState(Game game, GameStateManager manager)
			: base(game, manager)
		{
		}
		#endregion

		#region XNA Method region
		protected override void LoadContent()
		{
			ContentManager Content = GameRef.Content;//Game1's ContentManager
			backgroundImage = Content.Load<Texture2D>(@"Backgrounds\Dark_Heresy_Title");
			base.LoadContent();

			startLabel=new LinkLabel("CLICK to continue");
			startLabel.Position = new Vector2(600, 400);
			startLabel.Color = Color.Red;
			startLabel.SelectedColor = Color.Black;
			startLabel.Selected+=new EventHandler(startLabel_Selected);
			ControlManager.Add(startLabel);

			startButton = new Button("Start");
			startButton.Position = new Vector2(600, 400);
			startButton.Size = new Vector2(200, 50);
			startButton.Color = Color.Black;
			startButton.Image = Content.Load<Texture2D>(@"Control Images\Button_Gold_Up");
			startButton.SelectedImage = Content.Load<Texture2D>(@"Control Images\Button_Gold_Down");
			startButton.Selectable = true;
			startButton.HasFocus = false;
			startButton.Selected += new EventHandler(startButton_Selected);
			startButton.Enabled = false;
			startButton.Visible = false;
			ControlManager.Add(startButton);

			loadButton = new Button("Load");
			loadButton.Position = new Vector2(600, 450);
			loadButton.Size = new Vector2(200, 50);
			loadButton.Color = Color.Black;
			loadButton.Image = Content.Load<Texture2D>(@"Control Images\Button_Gold_Up");
			loadButton.SelectedImage = Content.Load<Texture2D>(@"Control Images\Button_Gold_Down");
			loadButton.Selectable = true;
			loadButton.HasFocus = false;
			loadButton.Selected += new EventHandler(loadButton_Selected);
			loadButton.Enabled = false;
			loadButton.Visible = false;
			ControlManager.Add(loadButton);

			exitButton = new Button("Exit");
			exitButton.Position = new Vector2(600, 500);
			exitButton.Size = new Vector2(200, 50);
			exitButton.Color = Color.Black;
			exitButton.Image = Content.Load<Texture2D>(@"Control Images\Button_Gold_Up");
			exitButton.SelectedImage = Content.Load<Texture2D>(@"Control Images\Button_Gold_Down");
			exitButton.Selectable = true;
			exitButton.HasFocus = false;
			exitButton.Selected += new EventHandler(exitButton_Selected);
			exitButton.Enabled = false;
			exitButton.Visible = false;
			ControlManager.Add(exitButton);
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

			startButton.Enabled = true;
			startButton.Visible = true;

			loadButton.Enabled = true;
			loadButton.Visible = true;

			exitButton.Enabled = true;
			exitButton.Visible = true;

		}
		private void startButton_Selected(object sender, EventArgs e)
		{
			//StateManager.PushState(GameRef.StartMenuScreen);
		}
		private void loadButton_Selected(object sender, EventArgs e)
		{
			//StateManager.PushState(GameRef.StartMenuScreen);
		}
		private void exitButton_Selected(object sender, EventArgs e)
		{
			GameRef.Exit();
		}
		#endregion
	}
}
