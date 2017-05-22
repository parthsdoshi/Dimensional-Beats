using Nez;
using DimensionalBeats.Helpers;
using Nez.Tiled;
using Microsoft.Xna.Framework;

namespace DimensionalBeats.Controllers {
    class PlayerController : Controller, IUpdatable {
        private InputHandler _inputHandler;
        private BoxCollider _boxCollider;
        private TiledMapMover _mover;
        private TiledMapMover.CollisionState _collisionState;

        public PlayerController() : base() {
            _inputHandler = new InputHandler();
            _collisionState = new TiledMapMover.CollisionState();
        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();
            _boxCollider = entity.getComponent<BoxCollider>();
            _mover = entity.getComponent<TiledMapMover>();
        }

        public void update() {
            //throw new NotImplementedException();

            _mover.move(new Vector2(20, 0) * Time.deltaTime, _boxCollider, _collisionState);


        }
    }
}
