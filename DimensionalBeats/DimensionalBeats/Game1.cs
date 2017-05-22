﻿using Microsoft.Xna.Framework;
using Nez;

namespace DimensionalBeats
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {
        public const short DEFAULT_ART_WIDTH = 1280;
        public const short DEFAULT_ART_HEIGHT = 720;

        private bool changeScenes = false;
        
        public Game1() : base( DEFAULT_ART_WIDTH, DEFAULT_ART_HEIGHT, false, true, "Dimensional Beats", "Content" )
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            // Window.IsBorderless = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            

            Scene.setDefaultDesignResolution(DEFAULT_ART_WIDTH, DEFAULT_ART_HEIGHT, Scene.SceneResolutionPolicy.ShowAll);
            scene = new Scenes.TestScene();
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);

            if(gameTime.TotalGameTime.Seconds > 10 && !changeScenes) {
                changeScene();
                changeScenes = true;
            }
            Debug.log(scene.entities.count);
        }

        private void changeScene() {
            scene = new Scenes.TestScene2();
            Debug.log("Changed Scene");
        }
    }
}
