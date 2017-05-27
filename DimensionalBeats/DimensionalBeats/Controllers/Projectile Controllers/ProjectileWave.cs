using DimensionalBeats.Entities;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace DimensionalBeats.Controllers.Projectile_Controllers {
    class ProjectileWave : ProjectileController, IUpdatable{
        private float _deltaTheta;

        public ProjectileWave(float theta, float velocity, float lifespan) : base(theta, velocity, lifespan){
            _deltaTheta = 0;
        }

        public void update() {
            CollisionResult res;

            float deltaX = _velocity * Game1.TILE_SIZE * Mathf.cos(_theta);
            float deltaY = _velocity * Game1.TILE_SIZE * Mathf.sin(_theta);
            Vector2 lineVector = new Vector2(deltaX, deltaY);
            Vector2 pulseVector = new Vector2(-deltaY, deltaX) * Mathf.sin(_deltaTheta);

            //Move and check collision
            if (_mover.move((lineVector + pulseVector) * Time.deltaTime, out res)) {
                checkForCollision(res);
            }

            if (_theta < MathHelper.Pi/2 && _theta > -MathHelper.Pi / 2) {
                _deltaTheta -= Mathf.deg2Rad * 15;
                if (_deltaTheta <= 0) _deltaTheta = MathHelper.TwoPi;
            } else {
                _deltaTheta += Mathf.deg2Rad * 15;
                if (_deltaTheta >= MathHelper.TwoPi) _deltaTheta = 0;
            }

            //Destroy after lifespan
            _lifespan -= Time.deltaTime;
            if (_lifespan <= 0) entity.destroy();
        }

    }
}
