using DimensionalBeats.Entities;
using Microsoft.Xna.Framework;
using Nez;

namespace DimensionalBeats.Controllers.Projectile_Controllers {
    class ProjectileWave : ProjectileController, IUpdatable{

        private float _velocity;
        private float _theta;
        private float _deltaTheta;

        private float _lifespan;

        public ProjectileWave(float theta, float velocity, float lifespan) : base(){
            this._lifespan = lifespan;
            this._theta = theta;
            this._velocity = velocity;
        }

        public void update() {
            CollisionResult res;

            float deltaX = _velocity * Game1.TILE_SIZE * Mathf.cos(_theta);
            float deltaY = _velocity * Game1.TILE_SIZE * Mathf.sin(_deltaTheta);

            _mover.move(new Vector2(deltaX, deltaY) * Time.deltaTime, out res);

            _deltaTheta += Mathf.deg2Rad * 15;
            if (_deltaTheta >= MathHelper.TwoPi) _deltaTheta = 0;

            _lifespan -= Time.deltaTime;
            if (_lifespan <= 0) entity.destroy();
        }

    }
}
