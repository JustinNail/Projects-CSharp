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

using RPGgamePrototype.Controls;

namespace RPGgamePrototype
{
	public class InputHandler : Microsoft.Xna.Framework.GameComponent
	{
		#region Keyboard Field Region
		static KeyboardState keyboardState;
		static KeyboardState lastKeyboardState;
		#endregion

		#region Game Pad Field Region
		static GamePadState[] gamePadStates;
		static GamePadState[] lastGamePadStates;
		#endregion

		#region Mouse Field Region
		static MouseState mouseState;
		static MouseState lastMouseState;
		#endregion

		#region Keyboard Property Region
		public static KeyboardState KeyboardState{ get { return keyboardState; } }
		public static KeyboardState LastKeyboardState{	get { return lastKeyboardState; } }
		#endregion

		#region Game Pad Property Region
		public static GamePadState[] GamePadStates{	get { return gamePadStates; } }
		public static GamePadState[] LastGamePadStates{	get { return lastGamePadStates; } }
		#endregion

		#region Mouse Property Region
		public static MouseState MouseState { get { return mouseState; } }
		public static MouseState LastMouseState { get { return lastMouseState; } }
		#endregion

		#region Constructor Region
		public InputHandler(Game game)
			: base(game)
		{
			keyboardState = Keyboard.GetState();
			gamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];
			mouseState = Mouse.GetState();

			foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
				gamePadStates[(int)index] = GamePad.GetState(index);
		}
		#endregion

		#region XNA methods
		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			lastKeyboardState = keyboardState;
			keyboardState = Keyboard.GetState();

			lastGamePadStates = (GamePadState[])gamePadStates.Clone();
			foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
			{
				gamePadStates[(int)index] = GamePad.GetState(index);
			}

			lastMouseState = mouseState;
			mouseState = Mouse.GetState();

			base.Update(gameTime);
		}
		#endregion

		#region General Method Region
		public static void Flush()
		{
			lastKeyboardState = keyboardState;
			lastMouseState = mouseState;
		}
		#endregion

		#region Keyboard Region
		public static bool KeyReleased(Keys key)
		{
			return keyboardState.IsKeyUp(key) &&
				lastKeyboardState.IsKeyDown(key);
		}

		public static bool KeyPressed(Keys key)
		{
			return keyboardState.IsKeyDown(key) &&
				lastKeyboardState.IsKeyUp(key);
		}

		public static bool KeyDown(Keys key)
		{
			return keyboardState.IsKeyDown(key);
		}
		#endregion

		#region Game Pad Region
		public static bool ButtonReleased(Buttons button, PlayerIndex index)
		{
			return gamePadStates[(int)index].IsButtonUp(button) &&
				lastGamePadStates[(int)index].IsButtonDown(button);
		}

		public static bool ButtonPressed(Buttons button, PlayerIndex index)
		{
			return gamePadStates[(int)index].IsButtonDown(button) &&
				lastGamePadStates[(int)index].IsButtonUp(button);
		}

		public static bool ButtonDown(Buttons button, PlayerIndex index)
		{
			return gamePadStates[(int)index].IsButtonDown(button);
		}
		#endregion

		#region Mouse Region

		public static bool MouseInArea(Rectangle Area)
		{
			if (
				InputHandler.MouseState.X < (Area.Right) &&
				InputHandler.MouseState.Y < (Area.Bottom) &&
				InputHandler.MouseState.X > (Area.Left) &&
				InputHandler.MouseState.Y > (Area.Top)
				)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		public static bool LeftMouseButtonReleased()
		{
			return lastMouseState.LeftButton == ButtonState.Pressed &&
				mouseState.LeftButton == ButtonState.Released;
		}
		public static bool RightMouseButtonReleased()
		{
			return lastMouseState.RightButton == ButtonState.Pressed &&
				mouseState.RightButton == ButtonState.Released;
		}
		public static bool MiddleMouseButtonReleased()
		{
			return lastMouseState.MiddleButton == ButtonState.Pressed &&
				mouseState.MiddleButton == ButtonState.Released;
		}

		public static bool LeftMouseButtonPressed()
		{
			return lastMouseState.LeftButton == ButtonState.Released &&
				mouseState.LeftButton == ButtonState.Pressed;
		}
		public static bool RightMouseButtonPressed()
		{
			return lastMouseState.RightButton == ButtonState.Released &&
				mouseState.RightButton == ButtonState.Pressed;
		}
		public static bool MiddleMouseButtonPressed()
		{
			return lastMouseState.MiddleButton == ButtonState.Released &&
				mouseState.MiddleButton == ButtonState.Pressed;
		}

		public static bool LeftMouseButtonDown()
		{
			return mouseState.LeftButton == ButtonState.Pressed;
		}
		public static bool RightMouseButtonDown()
		{
			return mouseState.RightButton == ButtonState.Pressed;
		}
		public static bool MiddleMouseButtonDown()
		{
			return mouseState.MiddleButton == ButtonState.Pressed;
		}
		#endregion
	}
}
