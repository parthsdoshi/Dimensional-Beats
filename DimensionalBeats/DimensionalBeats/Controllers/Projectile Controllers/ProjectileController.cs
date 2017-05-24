using DimensionalBeats.Helper;
using Microsoft.Xna.Framework;
using Nez;

namespace DimensionalBeats.Controllers.Projectile_Controllers {

    class ProjectileController : Controller {
        protected PhysicsHandler _physicsHandler;

        protected CircleCollider _circleCollider;
        public CollisionResult collisionResult { get; set; }
        protected Mover _mover;

        public ProjectileController() : base() {

        }

        public override void onAddedToEntity() {
            _circleCollider = entity.getComponent<CircleCollider>();
            _mover = entity.getComponent<Mover>();
            _physicsHandler = entity.getComponent<PhysicsHandler>();
        }

        public void move(float theta, float speed) {
            CollisionResult res;
            float deltaX = speed * Mathf.cos(theta);
            float deltaY = speed * Mathf.sin(theta);
            _mover.move(new Vector2(deltaX, deltaY) * Time.deltaTime, out res);
        }
    }
}
