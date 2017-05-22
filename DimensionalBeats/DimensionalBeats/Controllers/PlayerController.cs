using Nez;
using DimensionalBeats.Helper;
using Nez.Tiled;
using Microsoft.Xna.Framework;

namespace DimensionalBeats.Controllers {
    class PlayerController : Controller, IUpdatable {

        private InputHandler _inputHandler;
        private PhysicsHandler physicsHandler;

        private BoxCollider _boxCollider;
        private TiledMapMover _mover;
        private TiledMapMover.CollisionState _collisionState;

        public PlayerController() : base() {
            _inputHandler = new InputHandler();
            _collisionState = new TiledMapMover.CollisionState();
            physicsHandler = new PhysicsHandler(_collisionState, 10f, .3f);
        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();
            _boxCollider = entity.getComponent<BoxCollider>();
            _mover = entity.getComponent<TiledMapMover>();
        }

        public void update() {
            //throw new NotImplementedException();
            

            //Use physics handler & TiledMapMover to calculate movement
            _mover.move(physicsHandler.calculateMovement(_inputHandler.getMovement()), _boxCollider, _collisionState);
        }
    }
}
