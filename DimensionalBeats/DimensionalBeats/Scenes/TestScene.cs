using Microsoft.Xna.Framework;
using DimensionalBeats.Entities;
using DimensionalBeats.Controllers;
using DimensionalBeats.Controllers.Projectile_Controllers;
using DimensionalBeats.Helper;
using Nez;
using Nez.Tiled;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace DimensionalBeats.Scenes
{

    class TestScene : Scene
    {
        private int offsetX;
        private int offsetY;
        FollowCamera camera;

        public TestScene() : base() {
            initialize();
        }

        public override void initialize()
        {
            clearColor = Color.CornflowerBlue;
            addRenderer(new DefaultRenderer());


            Entity tiledEntity = createEntity("tiled-map");
            PlayerController playerController = new PlayerController();
            Sprite playerSprite = new Sprite(content.Load<Texture2D>("Temp/TestingSprite"));

            //Load map here************************************************************
            TiledMap map = content.Load<TiledMap>("Temp/TestingWorld");
            TiledObject spawn = map.getObjectGroup("SpawnPoint").objectWithName("Spawn");
            TiledMapComponent mapComponent = new TiledMapComponent(map, "Ground");
            tiledEntity.addComponent(mapComponent);

            //Create player Entity
            CookieCutterEntity player = new CookieCutterEntity("Player", spawn.position, playerSprite, playerController);

            //Add camera to player
            camera = new FollowCamera(player);
            camera.mapLockEnabled = true;
            camera.mapSize = new Vector2(map.width * map.tileWidth, map.height * map.tileHeight);
            player.addComponent<FollowCamera>(camera);

            //Add collision layers here*******************************************************************
            player.addComponent<TiledMapMover>(new TiledMapMover(map.getLayer<TiledTileLayer>("Ground")));
            player.addComponent<BoxCollider>(new BoxCollider(-playerSprite.width/2, -playerSprite.height/2, playerSprite.width, playerSprite.height));
            player.addComponent<PhysicsHandler>(new PhysicsHandler(player, playerController.collisionState, 20f, .5f));
            this.addEntity<Entity> (player);

        }

        public Entity createProjectile(Vector2 pos, Vector2 dir, Sprite sprite) {
            Entity entity = createEntity("Entity");
            ProjectileWave waveProjectileController = new ProjectileWave();
            ProjectileEntity waveProjectile = new ProjectileEntity(waveProjectileController, ProjectileType.WAVE, sprite);
            entity.position = pos;

            //Attach Sprite
            sprite.setRenderLayer(1);
            entity.addComponent<Sprite>(sprite);

            //Attack physics
            PhysicsHandler physicsHandler = new PhysicsHandler(waveProjectile, waveProjectileController.collisionResult);
            physicsHandler.moveSpeed = 4f;
            physicsHandler.isProjectile = true;
            physicsHandler.applyGravity = false;
            entity.addComponent<PhysicsHandler>(physicsHandler);

            //Attach hit detection
            CircleCollider circleCollider = new CircleCollider(sprite.width);
            entity.addComponent<CircleCollider>(circleCollider);

            //Attack mover
            Mover mover = new Mover();
            entity.addComponent<Mover>(mover);

            Debug.log("Projectile created at X: " + pos.X + " Y: " + pos.Y);
            return entity;
        }
    }
}

