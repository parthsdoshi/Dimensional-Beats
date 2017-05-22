using System;
using Nez;
using DimensionalBeats.Helpers;
using Nez.Tiled;

namespace DimensionalBeats.Controllers {
    class PlayerController : Controller, IUpdatable {
        private InputHandler _inputHandler;
        private BoxCollider _boxCollider;
        private TiledMapMover _mover;

        public PlayerController() : base() {
            _inputHandler = new InputHandler();
        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();
            _boxCollider = entity.getComponent<BoxCollider>();
            _mover = entity.getComponent<TiledMapMover>();
        }

        public void update() {
            //throw new NotImplementedException();
        }
    }
}
