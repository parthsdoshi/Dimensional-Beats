using Nez;
using Nez.AI.BehaviorTrees;
using Nez.Tiled;
using System;


namespace DimensionalBeats.Controllers.Enemy_Controller {
    class EnemyMeleeController : EnemyController, IUpdatable {

        //Physics values
        public float velocity { get; set; }
        public float maxVelocity { get; set; }
        private float _jumpHeight { get; set; }

        //Mob values
        private float _health { get; }
        private float _damage { get; }

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
            BehaviorTreeBuilder<EnemyController> _behaviorTreeBuilder = BehaviorTreeBuilder<EnemyController>.begin(this);
            _behaviorTreeBuilder.untilSuccess();
            //Creates a method with input of EnemyController and output of TaskStatus
            Func<EnemyController, TaskStatus> walk = delegate(EnemyController context) {
                //_grid.search();
                return TaskStatus.Running;
            };


            _behaviorTreeBuilder.action(walk);
    }

        public void update() {
            throw new NotImplementedException();
        }
    }
}
