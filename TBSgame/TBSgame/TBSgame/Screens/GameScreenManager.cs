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

namespace TBSgame.States
{
	public enum ChangeType { Change, Pop, Push }

	public class GameScreenManager : GameComponent
	{
		#region Event Region

		public event EventHandler OnStateChange;

		#endregion

		#region Fields and Properties Region

		Stack<GameScreen> gameStates = new Stack<GameScreen>();

		const int startDrawOrder = 5000;
		const int drawOrderInc = 100;
		int drawOrder;

		public GameScreen CurrentState
		{
			get { return gameStates.Peek(); }
		}

		#endregion

		#region Constructor Region

		public GameScreenManager(Game game)
			: base(game)
		{
			drawOrder = startDrawOrder;
		}

		#endregion

		#region XNA Method Region

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		#endregion

		#region Methods Region

		public void PopState()
		{
			if (gameStates.Count > 0)
			{
				RemoveState();
				drawOrder -= drawOrderInc;

				if (OnStateChange != null)
					OnStateChange(this, null);
			}
		}

		private void RemoveState()
		{
			GameScreen State = gameStates.Peek();

			OnStateChange -= State.StateChange;
			Game.Components.Remove(State);
			gameStates.Pop();
		}

		public void PushState(GameScreen newState)
		{
			drawOrder += drawOrderInc;
			newState.DrawOrder = drawOrder;

			AddState(newState);

			if (OnStateChange != null)
				OnStateChange(this, null);
		}

		private void AddState(GameScreen newState)
		{
			gameStates.Push(newState);

			Game.Components.Add(newState);

			OnStateChange += newState.StateChange;
		}

		public void ChangeState(GameScreen newState)
		{
			while (gameStates.Count > 0)
				RemoveState();

			newState.DrawOrder = startDrawOrder;
			drawOrder = startDrawOrder;

			AddState(newState);

			if (OnStateChange != null)
				OnStateChange(this, null);
		}

		#endregion
	}
}
