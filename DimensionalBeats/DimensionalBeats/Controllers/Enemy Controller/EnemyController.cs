using DimensionalBeats.Helper;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.AI.BehaviorTrees;
using Nez.AI.Pathfinding;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionalBeats.Controllers.Enemy_Controller {
    class EnemyController : Controller {
        enum Animations {
            RUN, IDLE
        }
        protected WeightedGridGraph _grid;
        protected BehaviorTree<EnemyController> _behaviorTree;

        private PhysicsHandler _physicsHandler;

        private BoxCollider _boxCollider;
        private TiledMapMover _mover;
        public TiledMapMover.CollisionState collisionState { get; }

        private Sprite<Animations> _animations;

        public EnemyController(TiledTileLayer collisionLayer) : base() {
            _grid = new WeightedGridGraph(collisionLayer);

        }

        public override void onAddedToEntity() {
            base.onAddedToEntity();
            loadContent();

            _boxCollider = entity.getComponent<BoxCollider>();
            _mover = entity.getComponent<TiledMapMover>();
            _physicsHandler = entity.getComponent<PhysicsHandler>();
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
    }
}
