using Nez;
using DimensionalBeats.Helper;
using DimensionalBeats.Controllers;
using Nez.Sprites;
using DimensionalBeats.Controllers.Projectile_Controllers;
using Microsoft.Xna.Framework;

namespace DimensionalBeats.Entities {
    class ProjectileEntity : Entity{
        private ProjectileType _type;

        public ProjectileEntity(string name, Controller projectileController, ProjectileType type) : base(name){
            this._type = type;
            addComponent<Controller>(projectileController);
        }
    }
}
