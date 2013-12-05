using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tools.Markers;
using Traffic.Actions;
using Traffic.Cars;

namespace Traffic
{
    public class Manager : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private readonly Director director;

        //------------------------------------------------------------------
        public Road Road { get; private set; }

        //------------------------------------------------------------------
        public Manager (Game game) : base (game)
        {
            Road = new Road (Game);
            director = new Director (this);
        }

        //------------------------------------------------------------------
        public override void Initialize ()
        {
            spriteBatch = new SpriteBatch (Game.GraphicsDevice);
            
            Road.Setup ();
            director.Setup ();
        }

        //------------------------------------------------------------------
        public override void Update (GameTime gameTime)
        {
            float elapsed = (float) gameTime.ElapsedGameTime.TotalSeconds;

            elapsed *= ControlCenter.TimeScale;

            Road.Update (elapsed);
            director.Update (elapsed);
        }

        //------------------------------------------------------------------
        public override void Draw (GameTime gameTime)
        {
            spriteBatch.Begin (SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Road.Draw (spriteBatch);

            spriteBatch.End ();

        }

    }
}
