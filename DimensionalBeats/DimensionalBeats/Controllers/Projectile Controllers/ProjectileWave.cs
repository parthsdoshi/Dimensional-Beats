using Nez;
using DimensionalBeats.Helper;
using System;

namespace DimensionalBeats.Controllers.Projectile_Controllers {
    class ProjectileWave : ProjectileController, IUpdatable{

        public ProjectileWave() : base(){
            
        }

        public void update() {
            CollisionResult res;
            _mover.move(new Microsoft.Xna.Framework.Vector2(2 * Time.deltaTime, 0), out res);
            Debug.log("Testing");
            entity.destroy();
        }
    }
}
