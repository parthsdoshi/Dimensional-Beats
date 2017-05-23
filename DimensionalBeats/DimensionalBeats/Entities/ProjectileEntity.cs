using Nez;
using DimensionalBeats.Helper;
using DimensionalBeats.Controllers;
using Nez.Sprites;

namespace DimensionalBeats.Entities {
    class ProjectileEntity : Entity{
        private float velocity;
        private float direction;
        private ProjectileType type;

        public ProjectileEntity(Controller projectileController, ProjectileType type, Sprite sprite) : base(){
            addComponent<Controller>(projectileController);
            addComponent<Sprite>(sprite);

        }


        public override void update() {
            base.update();
        }
    }
}
