using DimensionalBeats.Entities;
using DimensionalBeats.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.AI.BehaviorTrees;
using Nez.AI.Pathfinding;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;
using System.Collections.Generic;

namespace DimensionalBeats.Controllers.Enemy_Controller {
    class EnemyController : Controller {
        enum Animations {
            RUN, IDLE
        }

        protected PhysicsHandler _physicsHandler;

        private BoxCollider _boxCollider;
        protected TiledMapMover _mover;
        public TiledMapMover.CollisionState collisionState { get; }

        private Sprite<Animations> _animations;

        //Physics values
        public float velocity { get; set; }
        public float maxVelocity { get; set; }
        protected float _jumpHeight { get; set; }

        //Mob values
        protected float _health { get; }
        protected float _damage { get; }

        //AI Stuff
        protected WeightedGridGraph _grid;
        protected BehaviorTree<EnemyController> _behaviorTree;
        protected List<Point> _waypoints;
        protected Point _origin;
        protected int _currentWaypointIndex;
        public Vector2 _dir { get; set; }
        public CookieCutterEntity target;

        public EnemyController(TiledTileLayer collisionLayer) : base() {
            _grid = new WeightedGridGraph(collisionLayer);
            this.collisionState = new TiledMapMover.CollisionState();
            _dir = new Vector2();
            maxVelocity = 1f;
        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();
            loadContent();

            _boxCollider = entity.getComponent<BoxCollider>();
            _mover = entity.getComponent<TiledMapMover>();
            _physicsHandler = entity.getComponent<PhysicsHandler>();
            _physicsHandler.maxVelocity = this.maxVelocity;
        }

        public void loadContent() {
            //Splits spritesheet into subtextures
            Texture2D texture = entity.scene.content.Load<Texture2D>("Temp/TestPlayerAnimationSheet");
            List<Subtexture> subtextures = Subtexture.subtexturesFromAtlas(texture, 16, 32);
            _animations = entity.addComponent(new Sprite<Animations>(subtextures[0]));

            //Load animations
            //Idle animation
            _animations.addAnimation(Animations.IDLE, new SpriteAnimation(new List<Subtexture>() {
                subtextures[0],
                subtextures[1]
            }));

            //Run animation
            _animations.addAnimation(Animations.RUN, new SpriteAnimation(new List<Subtexture>() {
                subtextures[8],
                subtextures[9],
                subtextures[10],
                subtextures[11]
            }));
        }

        public void move(Vector2 motion) {
            if (motion != new Vector2() && !_animations.isAnimationPlaying(Animations.RUN)) _animations.play(Animations.RUN);
            else if (motion == new Vector2() && !_animations.isAnimationPlaying(Animations.IDLE)) _animations.play(Animations.IDLE);
            _mover.move(_physicsHandler.calculateMovement(motion), _boxCollider, collisionState);
        }

        public void flipSprite(bool flip) {
            _animations.flipX = flip;
        }
    }
}
