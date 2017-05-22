using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace DimensionalBeats.Scenes
{
    class TestScene : Scene
    {
        public override void initialize()
        {
            base.initialize();
            clearColor = Color.CornflowerBlue;
            addRenderer(new DefaultRenderer());

            Entity tiledEntity = createEntity("tiled-map");
            TiledMap map = content.Load<TiledMap>("Temp");
            TiledMapComponent mapComponent = new TiledMapComponent(map);
            tiledEntity.addComponent<TiledMapComponent>(mapComponent);
        }
    }
}
