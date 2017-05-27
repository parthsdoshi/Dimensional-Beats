using Microsoft.Xna.Framework;
using Nez;
using Nez.AI.Pathfinding;
using Nez.Tiled;
using System.Collections.Generic;

namespace DimensionalBeats.Controllers.Projectile_Controllers {
    class MagicProjectile : ProjectileController, IUpdatable{
        private AstarGridGraph _grid;
        private List<Point> _waypoints;
        private Point _goal;
        private int _count;

        public MagicProjectile(float theta, float velocity, float lifespan, TiledTileLayer collisionLayer) : base(theta, velocity, lifespan) {
            _grid = new AstarGridGraph(collisionLayer);
            _count = 0;
        }

        public override void initialize() {
            base.initialize();
        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();
            _waypoints = null;
        }

        public void update() {
            Point currentNode = new Point((int)entity.position.X / 16, (int)entity.position.Y / 16);
            Vector2 mousePosition = entity.scene.camera.mouseToWorldPoint();
            Point mouseNode = new Point((int)mousePosition.X / 16, (int)mousePosition.Y / 16);
            Point nextNode = currentNode;

            if (currentNode == mouseNode)
                entity.destroy();

            if(mouseNode != _goal) {
                if (!_grid.walls.Contains(mouseNode)) {
                    _goal = mouseNode;
                    _waypoints = _grid.search(currentNode, mouseNode);
                    _count = 1;
                }
            }

            if(_waypoints != null && _waypoints.Count > _count) {
                nextNode = _waypoints[_count];
                if (currentNode == _waypoints[_count]) {
                    if (!_grid.walls.Contains(mouseNode))
                        _waypoints = _grid.search(currentNode, mouseNode);
                    else _count++;
                }
            }
            _mover.move(new Vector2((nextNode.X - currentNode.X) * 16 * _velocity * Time.deltaTime, (nextNode.Y - currentNode.Y) * 16 * _velocity * Time.deltaTime), out CollisionResult res);
        }
    }
}
