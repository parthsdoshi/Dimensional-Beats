using DimensionalBeats.Helper;
using DimensionalBeats.Entities;
using Nez;
using Nez.Tiled;
using System;

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
            //throw new NotImplementedException();
            if (entity == null) return;

            //Use physics handler & TiledMapMover to calculate movement
            _mover.move(_physicsHandler.calculateMovement((_inputHandler.getMovement()), _inputHandler.getEvent()), _boxCollider, collisionState);
        }

    }
}
