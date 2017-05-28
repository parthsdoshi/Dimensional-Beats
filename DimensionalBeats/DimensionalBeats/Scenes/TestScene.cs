using Microsoft.Xna.Framework;
using DimensionalBeats.Entities;
using DimensionalBeats.Controllers;
using DimensionalBeats.Controllers.Projectile_Controllers;
using DimensionalBeats.Helper;
using Nez;
using Nez.Tiled;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using DimensionalBeats.Controllers.Enemy_Controller;

namespace DimensionalBeats.Scenes
{

    class TestScene : Scene
    {
        private int offsetX;
        private int offsetY;

        public TestScene() : base() {
        }

        public override void initialize()
        {
            Debug.log("Scene initialized");

            clearColor = Color.CornflowerBlue;
            addRenderer(new DefaultRenderer());


            Entity tiledEntity = createEntity("tiled-map");
            PlayerController playerController = new PlayerController();
            Sprite playerSprite = new Sprite(content.Load<Texture2D>("Temp/TestPlayerAnimationFiles/Idle/Idle_1"));
            playerSprite.setRenderLayer(1);

            //Load map here************************************************************
            TiledMap map = content.Load<TiledMap>("Temp/TestingWorld");
            TiledObject spawn = map.getObjectGroup("SpawnPoint").objectWithName("Spawn");
            TiledMapComponent mapComponent = new TiledMapComponent(map, "Ground");
            mapComponent.setRenderLayer(2);
            tiledEntity.addComponent(mapComponent);

            #region SpawnPlayer
            //Create player Entity
            CookieCutterEntity player = new CookieCutterEntity("Player", spawn.position, ref playerSprite, playerController);

            //Add camera to player
            FollowCamera camera = new FollowCamera(player);
            camera.mapLockEnabled = true;
            camera.mapSize = new Vector2(map.width * map.tileWidth, map.height * map.tileHeight);
            player.addComponent<FollowCamera>(camera);

            //Add collision layers here*******************************************************************
            BoxCollider collider = new BoxCollider(-playerSprite.width / 2, -playerSprite.height / 2, playerSprite.width, playerSprite.height);
            player.addComponent<TiledMapMover>(new TiledMapMover(map.getLayer<TiledTileLayer>("Ground")));
            player.addComponent<BoxCollider>(collider);
            player.addComponent<PhysicsHandler>(new PhysicsHandler(player, playerController.collisionState, 20f, .5f));
            Flags.setFlagExclusive(ref collider.collidesWithLayers, 0); //Prevent collisions with projectiles
            Flags.setFlagExclusive(ref collider.physicsLayer, 1);

            this.addEntity<Entity> (player);
            #endregion

            #region SpawnAI
            //Initialize EnemyController
            EnemyMeleeController enemy = new EnemyMeleeController(map.getLayer<TiledTileLayer>("Ground"));
            Sprite enemySprite = new Sprite(content.Load<Texture2D>("Temp/TestPlayerAnimationFiles/Idle/Idle_1"));
            enemySprite.setRenderLayer(1);
            TiledObject enemySpawn = map.getObjectGroup("SpawnPoint").objectWithName("EnemySpawn");
            CookieCutterEntity enemyEntity = new CookieCutterEntity("Enemy", enemySpawn.position, ref enemySprite, enemy);

            //Add collision layers
            BoxCollider enemyCollider = new BoxCollider(-enemySprite.width / 2, -enemySprite.height / 2, enemySprite.width, enemySprite.height);
            enemy.addComponent<TiledMapMover>(new TiledMapMover(map.getLayer<TiledTileLayer>("Ground")));
            enemy.addComponent<BoxCollider>(enemyCollider);
            enemy.addComponent<PhysicsHandler>(new PhysicsHandler(enemyEntity, enemy.collisionState, 20f, .5f));
            Flags.setFlagExclusive(ref enemyCollider.collidesWithLayers, 0); //Prevent collisions with projectiles
            Flags.setFlagExclusive(ref enemyCollider.physicsLayer, 1);

            //Add target
            enemy.target = player;

            this.addEntity<Entity>(enemyEntity);
            #endregion

        }

        
    }
}

