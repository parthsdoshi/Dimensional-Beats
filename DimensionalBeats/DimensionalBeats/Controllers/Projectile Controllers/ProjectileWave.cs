using DimensionalBeats.Entities;
using Microsoft.Xna.Framework;
using Nez;

namespace DimensionalBeats.Controllers.Projectile_Controllers {
    class ProjectileWave : ProjectileController, IUpdatable{

        public float velocity { get; set; }
        public float theta { get; set; }

        public ProjectileWave() : base(){
        }

        public void update() {
            CollisionResult res;
            
            
            float deltaX = velocity * Mathf.cos(theta);
            float deltaY = velocity * Mathf.sin(theta);

            _mover.move(new Vector2(deltaX, deltaY) * Time.deltaTime, out res);
        }

    }
}
