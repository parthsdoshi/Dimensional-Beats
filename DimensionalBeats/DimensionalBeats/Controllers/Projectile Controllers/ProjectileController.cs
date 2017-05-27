using DimensionalBeats.Helper;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace DimensionalBeats.Controllers.Projectile_Controllers {

    class ProjectileController : Controller {
        protected PhysicsHandler _physicsHandler;

        protected CircleCollider _circleCollider;
        public CollisionResult collisionResult { get; set; }
        protected Mover _mover;
        protected TiledMapComponent _mapComponent;

        //Projectile values
        protected float _theta;
        protected float _velocity;
        protected float _lifespan;

        public ProjectileController(float theta, float velocity, float lifespan) : base() {
            this._theta = theta;
            this._velocity = velocity;
            this._lifespan = lifespan;
        }

        public override void onAddedToEntity() {
            _circleCollider = entity.getComponent<CircleCollider>();
            _mover = entity.getComponent<Mover>();
            _physicsHandler = entity.getComponent<PhysicsHandler>();
            _mapComponent = entity.scene.findEntity("tiled-map").getComponent<TiledMapComponent>();
        }

        protected void checkForCollision(CollisionResult res) {
            //Find tile collision detects
            TiledTile tile = _mapComponent.getTileAtWorldPosition(entity.position + new Vector2(-16) * res.normal);

            if (tile != null && tile.tilesetTile != null && tile.tilesetTile.isDestructable) { //Destroy tile if isDestructable
                _mapComponent.collisionLayer.removeTile(tile.x, tile.y);
                _mapComponent.removeColliders();
                _mapComponent.addColliders();
            } else entity.destroy();
        }
    }
}
