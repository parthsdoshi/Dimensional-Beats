using Nez;
using DimensionalBeats.Helper;
using DimensionalBeats.Controllers;
using Nez.Sprites;
using DimensionalBeats.Controllers.Projectile_Controllers;
using Microsoft.Xna.Framework;

namespace DimensionalBeats.Entities {
    class ProjectileEntity : Entity{
        private ProjectileType _type;

        public ProjectileEntity(Controller projectileController, ProjectileType type) : base(){
            addComponent<Controller>(projectileController);
            //transform.scale = new Vector2(.5f, .5f);
        }
    }
}
