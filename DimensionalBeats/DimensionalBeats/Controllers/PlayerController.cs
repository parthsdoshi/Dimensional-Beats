using DimensionalBeats.Helper;
using DimensionalBeats.Entities;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;
using DimensionalBeats.Controllers.Projectile_Controllers;
using Microsoft.Xna.Framework.Graphics;
using Nez.Sprites;
using DimensionalBeats.Scenes;

namespace DimensionalBeats.Controllers {

    class PlayerController : Controller, IUpdatable {
        private InputHandler _inputHandler;
        private PhysicsHandler _physicsHandler;

        private BoxCollider _boxCollider;
        private TiledMapMover _mover;
        public TiledMapMover.CollisionState collisionState { get; }

        private Texture2D musicAttack_1;

        public PlayerController() : base() {
            collisionState = new TiledMapMover.CollisionState();
        }
        
        public override void onAddedToEntity() {
            base.onAddedToEntity();
            _boxCollider = entity.getComponent<BoxCollider>();
            _mover = entity.getComponent<TiledMapMover>();
            _physicsHandler = entity.getComponent<PhysicsHandler>();

            _inputHandler = new InputHandler();
            loadContent();
        }
    
        public void loadContent() {
            //Implement later
            musicAttack_1 = entity.scene.content.Load<Texture2D>("Temp/MusicSprite1");
            Debug.log("Content loaded");
        }

        public void update() {
            if (entity == null) return;

            int eventHandler = _inputHandler.getEvent();

            //Get event from keyboard
            switch (eventHandler) {
                case 0: //Jump
                    //Tweak physicsHandler later to incorporate jump here
                    break;
                case 1: //Ability 1
                    useAbility(0);
                    break;
            }

            //Use physics handler & TiledMapMover to calculate movement
            Vector2 dir;
            switch (_inputHandler.getMovement()) {
                case 0: //Up
                    dir = new Vector2(0, -1);
                    break;
                case 1: //Up Right
                    dir = new Vector2(1, -1);
                    break;
                case 2: //Right
                    dir = new Vector2(1, 0);
                    break;
                case 3: //Down Right
                    dir = new Vector2(1, 1);
                    break;
                case 4: //Down
                    dir = new Vector2(0, 1);
                    break;
                case 5: //Down Left
                    dir = new Vector2(-1, 1);
                    break;
                case 6: //Left
                    dir = new Vector2(-1, 0);
                    break;
                case 7: //Up Left
                    dir = new Vector2(-1, -1);
                    break;
                case -1: //Non-movement command
                    dir = new Vector2(0, 0);
                    break;
                default:
                    dir = new Vector2(0, 0);
                    break;
            }

            _mover.move(_physicsHandler.calculateMovement(dir, eventHandler), _boxCollider, collisionState);
        }

        public void useAbility(short type) {
            switch (type) {
                case 0:
                    System.Random rand = new System.Random();
                    TestScene scene = entity.scene as TestScene;
                    Vector2 pos = new Vector2(entity.position.X, entity.position.Y);
                    //scene.createProjectile(pos, new Vector2(4, 0), new PrototypeSprite(16, 16));
                    Sprite sprite = new Sprite(musicAttack_1);
                    createProjectile(pos, 0f, 4f, sprite);
                    break;
            }
        }

        public Entity createProjectile(Vector2 pos, float theta, float velocity, Sprite sprite) {
            // Entity entity = createEntity("Entity");
            ProjectileWave waveProjectileController = new ProjectileWave();
            ProjectileEntity waveProjectile = new ProjectileEntity(waveProjectileController, ProjectileType.WAVE);

            //Hardcoding position adjustments
            //pos.X += 16;
            waveProjectile.position = pos;
            
            waveProjectileController.velocity = velocity * Game1.TILE_SIZE;
            waveProjectileController.theta = theta;

            //Attach Sprite
            sprite.setRenderLayer(1);
            waveProjectile.addComponent<Sprite>(sprite);

            //Attack physics
            PhysicsHandler physicsHandler = new PhysicsHandler(waveProjectileController.collisionResult);
            physicsHandler.isProjectile = true;
            physicsHandler.applyGravity = false;
            waveProjectile.addComponent<PhysicsHandler>(physicsHandler);

            //Attach hit detection
            CircleCollider circleCollider = new CircleCollider(sprite.width);
            waveProjectile.addComponent<CircleCollider>(circleCollider);

            //Attack mover
            Mover mover = new Mover();
            waveProjectile.addComponent<Mover>(mover);

            entity.scene.addEntity<ProjectileEntity>(waveProjectile);

            return waveProjectile;
        }
    }
}

