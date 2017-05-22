using Microsoft.Xna.Framework;
using DimensionalBeats.Entities;
using DimensionalBeats.Controllers;
using Nez;
using Nez.Tiled;
using Nez.Sprites;

namespace DimensionalBeats.Scenes
{
    class TestScene : Scene
    {
        public override void initialize()
        {
            clearColor = Color.CornflowerBlue;
            addRenderer(new DefaultRenderer());

            Entity tiledEntity = createEntity("tiled-map");
            Entity player = new CookieCutterEntity(new Vector2(300, 300));
            Controller playerController = new PlayerController();

            TiledMap map = content.Load<TiledMap>("Temp/Temp");
            TiledObject spawn = map.getObjectGroup("Entrances").objectWithName("Spawn");

            TiledMapComponent mapComponent = new TiledMapComponent(map);
            tiledEntity.addComponent<TiledMapComponent>(mapComponent);

            player.addComponent<Controller>(playerController);
            player.transform.setPosition(spawn.position);
            player.addComponent<TiledMapMover>(new TiledMapMover(map.getLayer<TiledTileLayer>("Ground")));
            player.addComponent<BoxCollider>(new BoxCollider(0, 0, player.getComponent<Sprite>().width, player.getComponent<Sprite>().height));

            this.addEntity<Entity>(player);
        }
    }
}
