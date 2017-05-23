using DimensionalBeats.Helper;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionalBeats.Controllers.Projectile_Controllers {

    class ProjectileController : Controller{
        protected PhysicsHandler _physicsHandler;

        protected CircleCollider _circleCollider;
        public CollisionResult collisionResult { get; set; }
        protected Mover _mover;

        public ProjectileController() : base() {

        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();
            _circleCollider = entity.getComponent<CircleCollider>();
            _mover = entity.getComponent<Mover>();
            _physicsHandler = entity.getComponent<PhysicsHandler>();
        }
    }
}
