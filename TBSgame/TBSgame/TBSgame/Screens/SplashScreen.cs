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
	public class SplashScreen:BaseGameScreen
	{
		#region Field region
		Texture2D backgroundImage;
		ScreenWideDetector detector;
		TimeSpan timer;
		TimeSpan waitTime = TimeSpan.FromSeconds(3.0);
		#endregion

		#region Constructor region
		public SplashScreen(Game game, GameScreenManager manager)
			: base(game, manager)
		{
		}
		#endregion

		#region XNA Method region
		protected override void LoadContent()
		{
			ContentManager Content = GameRef.Content;//Game1's ContentManager
			backgroundImage = Content.Load<Texture2D>(@"Backgrounds\SplashScreen");
			base.LoadContent();

			detector = new ScreenWideDetector("");
			detector.Position = new Vector2(0, 0);
			detector.Color = Color.White;
			detector.Selectable = true;
			detector.HasFocus = true;
			detector.Selected += new EventHandler(detector_Selected);
			ControlManager.Add(detector);
		}

		public override void Update(GameTime gameTime)
		{
			ControlManager.Update(gameTime, PlayerIndex.One);
			timer += gameTime.ElapsedGameTime;
			if (timer > waitTime)
			{
				StateManager.PushState(GameRef.TitleScreen);
			}
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
		private void detector_Selected(object sender, EventArgs e)
		{
			StateManager.PushState(GameRef.TitleScreen);
		}
		#endregion
	}
}
