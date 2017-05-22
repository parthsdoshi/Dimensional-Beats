using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace DimensionalBeats.Scenes {
    class TestScene2 : Scene {

        public override void initialize() {
            clearColor = Color.PowderBlue;
            addRenderer(new DefaultRenderer());

            Entity tiledEntity = createEntity("tiled-map");

            TiledMap map = content.Load<TiledMap>("Temp/Temp");
            TiledObject spawn = map.getObjectGroup("Entrances").objectWithName("Spawn");

            TiledMapComponent mapComponent = new TiledMapComponent(map);
            tiledEntity.addComponent<TiledMapComponent>(mapComponent);
        }
    }
}
