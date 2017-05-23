using Microsoft.Xna.Framework;
using Nez;

namespace DimensionalBeats
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {
        public const short DEFAULT_ART_WIDTH = 1280/4;
        public const short DEFAULT_ART_HEIGHT = 720/4;
        public const short DEFAULT_SCALE = 4;
        public const short TILE_SIZE = 16;
        
        //Resolution of the window
        public Game1() : base( DEFAULT_ART_WIDTH * DEFAULT_SCALE, DEFAULT_ART_HEIGHT * DEFAULT_SCALE, false, true, "Dimensional Beats", "Content" )
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

            

            //Resolution of the game
            Scene.setDefaultDesignResolution(DEFAULT_ART_WIDTH, DEFAULT_ART_HEIGHT, Scene.SceneResolutionPolicy.ShowAll);
            scene = new Scenes.TestScene();
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);

        }

        private void changeScene() {
            scene = new Scenes.TestScene2();
            Debug.log("Changed Scene");
        }
    }
}
