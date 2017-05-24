using Nez;
using DimensionalBeats.Helper;
using DimensionalBeats.Controllers;
using Nez.Sprites;

namespace DimensionalBeats.Entities {
    class ProjectileEntity : Entity{
        private float velocity;
        private float direction;
        private ProjectileType type;

        public ProjectileEntity(Controller projectileController, ProjectileType type) : base(){
            addComponent<Controller>(projectileController);

        }


        public override void update() {
            base.update();
        }
    }
}
