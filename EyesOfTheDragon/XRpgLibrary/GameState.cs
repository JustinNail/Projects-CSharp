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


namespace XRpgLibrary
{
	//needs to draw itself
    public abstract partial class GameState : DrawableGameComponent
    {
        #region Fields and Properties
		//Components owned by the state
        List<GameComponent> childComponents;
        public List<GameComponent> Components
        {
            get { return childComponents; }
        }

		//reference to this gamestate
        GameState tag;
        public GameState Tag
        {
            get { return tag; }
        }

        protected GameStateManager StateManager;
        #endregion

        #region Constructor Region
		//takes required game object and a reference to the GameStateManager
        public GameState(Game game, GameStateManager manager)
            : base(game)
        {
            StateManager = manager;
            childComponents = new List<GameComponent>();
            tag = this;
        }        
		#endregion

        #region XNA Drawable Game Component Methods
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
			//update every enabled child
            foreach (GameComponent component in childComponents)
            {
                if (component.Enabled)
                    component.Update(gameTime);
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
			//draw every visible drawable child
            DrawableGameComponent drawComponent;
            foreach (GameComponent component in childComponents)
            {
                if (component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;//cast
                    if (drawComponent.Visible)
					{
                        drawComponent.Draw(gameTime);
					}
                }
            }
            base.Draw(gameTime);
        }
        #endregion

        #region GameState Method Region
		//handles events that change states
		//All active screens will subscribe to an event in the game state manager class. 
		//All states that are subscribed to the event will receive a message that 
		//the active screen was changed
        internal protected virtual void StateChange(object sender, EventArgs e)
        {
            if (StateManager.CurrentState == Tag)
			{
                Show();
			}
            else
			{
                Hide();
			}
        }
		//show the state and its children
        protected virtual void Show()
        {
            Visible = true;
            Enabled = true;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
				{
                    ((DrawableGameComponent)component).Visible = true;
				}
            }
        }
		//hides the state and its children
        protected virtual void Hide()
        {
            Visible = false;
            Enabled = false;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
				{
					((DrawableGameComponent)component).Visible = false;
				}
            }
        }
        #endregion
    }
}