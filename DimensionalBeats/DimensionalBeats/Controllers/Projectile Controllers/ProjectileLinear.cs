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
        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();
            
        }

        public void update() {
            CollisionResult res;

            float deltaX = _velocity * Game1.TILE_SIZE * Mathf.cos(_theta) * Time.deltaTime;
            float deltaY = _velocity * Game1.TILE_SIZE * Mathf.sin(_theta) * Time.deltaTime;

            if (_mover.move(new Vector2(deltaX, deltaY), out res))
                checkForCollision(res);

            _lifespan -= Time.deltaTime;
            if (_lifespan <= 0) entity.destroy();
        }
    }
}
