using Microsoft.Xna.Framework;
using Nez;
using Nez.AI.BehaviorTrees;
using Nez.Tiled;
using System;
using System.Collections.Generic;

namespace DimensionalBeats.Controllers.Enemy_Controller {
    class EnemyMeleeController : EnemyController, IUpdatable {

        public EnemyMeleeController(TiledTileLayer collisionLayer) : base(collisionLayer) {

        }

        public override void initialize() {
            base.initialize();

        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();
            buildBehaviorTree();
            
        }

        private void buildBehaviorTree() {
            //Initialize pathfinding
            _origin = (entity.position / Game1.TILE_SIZE).ToPoint();
            _waypoints = _grid.search(new Point(_origin.X, _origin.Y), new Point(_origin.X + 5, _origin.Y));

            BehaviorTreeBuilder<EnemyController> _behaviorTreeBuilder = BehaviorTreeBuilder<EnemyController>.begin(this);
            _behaviorTreeBuilder.untilSuccess();
            //Creates a method with input of EnemyController and output of TaskStatus
            Func<EnemyController, TaskStatus> chasePlayer = delegate(EnemyController context) {
                if (target == null) return TaskStatus.Failure;

                Point currentNode = (entity.position / Game1.TILE_SIZE).ToPoint();
                Point targetNode = (target.position / Game1.TILE_SIZE).ToPoint();

                _waypoints = _grid.search(currentNode, targetNode);
                _currentWaypointIndex = 0;

                //Check to see if path exists
                if (_waypoints == null) return TaskStatus.Failure;

                return TaskStatus.Running;
            };


            _behaviorTreeBuilder.action(chasePlayer);

            _behaviorTree = _behaviorTreeBuilder.build();
    }

        public void update() {
            //TODO
            _behaviorTree.tick();

            //Move AI
            if (_waypoints == null) return;

            Point currentNode = (entity.position / Game1.TILE_SIZE).ToPoint();
            if (currentNode.X == _waypoints[_currentWaypointIndex].X)                                //AI reached waypoint; change waypoint
                _currentWaypointIndex = Mathf.clamp(_currentWaypointIndex + 1, 0, _waypoints.Count-1); //Increment waypoint

            //Deal with changing waypoints
            float _deltaX = _waypoints[_currentWaypointIndex].X - currentNode.X;
            if (_deltaX >= 0) flipSprite(false);
            else flipSprite(true);
            _dir = new Vector2(_deltaX, 0);
            move(_dir);
        }
    }
}
