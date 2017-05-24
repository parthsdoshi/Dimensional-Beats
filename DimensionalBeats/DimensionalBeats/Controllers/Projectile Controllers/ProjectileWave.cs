using DimensionalBeats.Entities;
using Microsoft.Xna.Framework;
using Nez;

namespace DimensionalBeats.Controllers.Projectile_Controllers {
    class ProjectileWave : ProjectileController, IUpdatable{

        public ProjectileWave() : base(){
            System.Random rand = new System.Random();
            int x = rand.Next(1, 10);
            Debug.log(x);
        }

        public void update() {
            ProjectileEntity projectile = entity as ProjectileEntity;
            CollisionResult res;
            
            
            float deltaX = projectile.velocity * Mathf.cos(projectile.theta);
            
            float deltaY = projectile.velocity * Mathf.sin(projectile.theta);
            _mover.move(new Vector2(deltaX, deltaY) * Time.deltaTime, out res);
        }

    }
}
