using DimensionalBeats.Helper;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace DimensionalBeats.Controllers {

    class PlayerController : Controller, IUpdatable {
        private InputHandler _inputHandler;
        private PhysicsHandler _physicsHandler;

        private BoxCollider _boxCollider;
        private TiledMapMover _mover;
        public TiledMapMover.CollisionState collisionState { get; }

        public PlayerController() : base() {
            _inputHandler = new InputHandler();
            collisionState = new TiledMapMover.CollisionState();
        }
        
        public override void onAddedToEntity() {
            base.onAddedToEntity();
            _boxCollider = entity.getComponent<BoxCollider>();
            _mover = entity.getComponent<TiledMapMover>();
            _physicsHandler = entity.getComponent<PhysicsHandler>();
        }
    

        public void update() {
            if (entity == null) return;

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

            _mover.move(_physicsHandler.calculateMovement(dir, _inputHandler.getEvent()), _boxCollider, collisionState);
        }
    }
}

