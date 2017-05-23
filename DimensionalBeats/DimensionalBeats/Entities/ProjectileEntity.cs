using Nez;
using DimensionalBeats.Helper;

namespace DimensionalBeats.Entities {
    class ProjectileEntity : Entity{
        private float velocity;
        private float direction;
        private ProjectileType type;

        public ProjectileEntity(PhysicsHandler physicsHandler, ProjectileType type) : base(){

        }

    }
}
