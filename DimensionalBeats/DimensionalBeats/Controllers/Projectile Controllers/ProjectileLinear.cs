using Microsoft.Xna.Framework;
using Nez;

namespace DimensionalBeats.Controllers.Projectile_Controllers {
    class ProjectileLinear : ProjectileController, IUpdatable{

        private float _theta;
        private float _velocity;
        private float _lifespan;

        public ProjectileLinear(float theta, float velocity, float lifespan) : base(){
            this._theta = theta;
            this._velocity = velocity;
            this._lifespan = lifespan;

            Debug.log("Creating linear projectile at theta: " + -Mathf.rad2Deg * theta);
        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();
            
        }

        public void update() {
            Debug.log("LinearProjectile Y: " + entity.position.Y);

            CollisionResult res;

            float deltaX = _velocity * Game1.TILE_SIZE * Mathf.cos(_theta) * Time.deltaTime;
            float deltaY = _velocity * Game1.TILE_SIZE * Mathf.sin(_theta) * Time.deltaTime;

            if(_mover.move(new Vector2(deltaX, deltaY), out res))
                entity.destroy();

            _lifespan -= Time.deltaTime;
            if (_lifespan <= 0) entity.destroy();
        }
    }
}
