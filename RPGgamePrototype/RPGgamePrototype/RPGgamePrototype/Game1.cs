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

using RPGgamePrototype.States;

namespace RPGgamePrototype
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		#region XNA Field Region

		GraphicsDeviceManager graphics;
		public SpriteBatch SpriteBatch;

		#endregion

		#region Game State Region

		GameStateManager stateManager;
		public TitleScreenState TitleScreen;
		public SplashScreenState SplashScreen;
		
		#endregion

		#region Screen Field Region

		const int screenWidth = 1024;
		const int screenHeight = 768;

		public readonly Rectangle ScreenRectangle;

		#endregion

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = screenWidth;
			graphics.PreferredBackBufferHeight = screenHeight;

			ScreenRectangle = new Rectangle(
				0,
				0,
				screenWidth,
				screenHeight);

			Content.RootDirectory = "Content";

			Components.Add(new InputHandler(this));

			stateManager = new GameStateManager(this);
			Components.Add(stateManager);

			TitleScreen = new TitleScreenState(this, stateManager);
			SplashScreen = new SplashScreenState(this, stateManager);

			stateManager.ChangeState(SplashScreen);
		}

		
		protected override void Initialize()
		{
			base.Initialize();
			this.IsMouseVisible = true;
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			base.Draw(gameTime);
		}
	}
}
