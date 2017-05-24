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

        public void update() {
            CollisionResult res;

            float deltaX = _velocity * Game1.TILE_SIZE * Mathf.cos(_theta);
            float deltaY = _velocity * Game1.TILE_SIZE * Mathf.sin(_theta);
            if(_mover.move(new Vector2(deltaX, deltaY) * Time.deltaTime, out res))
                entity.destroy();

            _lifespan -= Time.deltaTime;
            if (_lifespan <= 0) entity.destroy();
        }
    }
}
