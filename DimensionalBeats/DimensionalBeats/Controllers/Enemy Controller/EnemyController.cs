using DimensionalBeats.Helper;
using Nez;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionalBeats.Controllers.Enemy_Controller {
    class EnemyController : Controller {
        private PhysicsHandler _physicsHandler;

        private BoxCollider _boxCollider;
        private TiledMapMover _mover;
        public TiledMapMover.CollisionState collisionState { get; }

        public EnemyController() : base() {

        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();
            loadContent();

            _boxCollider = entity.getComponent<BoxCollider>();
            _mover = entity.getComponent<TiledMapMover>();
            _physicsHandler = entity.getComponent<PhysicsHandler>();
        }

        public void loadContent() {

        }
    }
}
