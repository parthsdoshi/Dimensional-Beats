using DimensionalBeats.Helper;
using DimensionalBeats.Entities;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;
using DimensionalBeats.Controllers.Projectile_Controllers;
using Microsoft.Xna.Framework.Graphics;
using Nez.Sprites;
using DimensionalBeats.Scenes;
using System;
using Nez.TextureAtlases;
using Nez.Textures;
using System.Collections.Generic;

namespace DimensionalBeats.Controllers {

    class PlayerController : Controller, IUpdatable {
        enum Animations {
            IDLE, RUN, ATTACK_IDLE, ATTACK_RUN
        }

        private InputHandler _inputHandler;
        private PhysicsHandler _physicsHandler;

        private BoxCollider _boxCollider;
        private TiledMapMover _mover;
        public TiledMapMover.CollisionState collisionState { get; }

        private short activeAbility;
        private Sprite<Animations> _animations;
        private Texture2D musicAttack_1;

        public PlayerController() : base() {
            activeAbility = 0;
            collisionState = new TiledMapMover.CollisionState();
        }
        
        public override void onAddedToEntity() {
            base.onAddedToEntity();
            loadContent();

            _boxCollider = entity.getComponent<BoxCollider>();
            _mover = entity.getComponent<TiledMapMover>();
            _physicsHandler = entity.getComponent<PhysicsHandler>();

            _inputHandler = new InputHandler();
        }
    
        public void loadContent() {
            //Implement later
            musicAttack_1 = entity.scene.content.Load<Texture2D>("Temp/MusicSprite1");

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

            //Attack Idle animation
            _animations.addAnimation(Animations.ATTACK_IDLE, new SpriteAnimation(new List<Subtexture>() {
                subtextures[16],
                subtextures[17],
                subtextures[18]
            }));

            //Attack Run animation
            _animations.addAnimation(Animations.ATTACK_RUN, new SpriteAnimation(new List<Subtexture>() {
                subtextures[24],
                subtextures[25],
                subtextures[26],
                subtextures[27]
            }));
            /*
            _animations.addAnimation(Animations.IDLE, atlas.getSpriteAnimation("Idle"));
            _animations.addAnimation(Animations.RUN, atlas.getSpriteAnimation("Run"));
            _animations.addAnimation(Animations.ATTACK_IDLE, atlas.getSpriteAnimation("Attack_Idle"));
            _animations.addAnimation(Animations.ATTACK_RUN, atlas.getSpriteAnimation("Attack_Run"));
            */
            Debug.log("Content loaded");
        }

        //Handle controls and movement
        public void update() {
            if (entity == null) return;

            int eventHandler = _inputHandler.getEvent();

            //Get event from keyboard
            switch (eventHandler) {
                case 0: //Jump
                    //Tweak physicsHandler later to incorporate jump here
                    break;
                case 1: //Ability 1
                    activeAbility = 0;
                    break;
                case 2: //Ability 2
                    activeAbility = 1;
                    break;
                case 10: //Fire ability
                    useAbility(activeAbility);
                    break;
            }

            //Use physics handler & TiledMapMover to calculate movement
            Vector2 dir;
            switch (_inputHandler.getMovement()) {
                case 0: //Up
                    dir = new Vector2(0, -1);
                    break;
                case 1: //Up Right
                    dir = new Vector2(1, -1);
                    if (_animations.flipX)
                        _animations.flipX = false;
                    if (!_animations.isAnimationPlaying(Animations.RUN))
                        _animations.play(Animations.RUN);
                    break;
                case 2: //Right
                    dir = new Vector2(1, 0);
                    if (_animations.flipX)
                        _animations.flipX = false;
                    if (!_animations.isAnimationPlaying(Animations.RUN))
                        _animations.play(Animations.RUN);
                    break;
                case 3: //Down Right
                    dir = new Vector2(1, 1);
                    if (_animations.flipX)
                        _animations.flipX = false;
                    if (!_animations.isAnimationPlaying(Animations.RUN))
                        _animations.play(Animations.RUN);
                    break;
                case 4: //Down
                    dir = new Vector2(0, 1);
                    break;
                case 5: //Down Left
                    dir = new Vector2(-1, 1);
                    if (!_animations.flipX)
                        _animations.flipX = true;
                    if (!_animations.isAnimationPlaying(Animations.RUN))
                        _animations.play(Animations.RUN);
                    break;
                case 6: //Left
                    dir = new Vector2(-1, 0);
                    if (!_animations.flipX)
                        _animations.flipX = true;
                    if (!_animations.isAnimationPlaying(Animations.RUN))
                        _animations.play(Animations.RUN);
                    break;
                case 7: //Up Left
                    dir = new Vector2(-1, -1);
                    if (!_animations.flipX)
                        _animations.flipX = true;
                    if (!_animations.isAnimationPlaying(Animations.RUN))
                        _animations.play(Animations.RUN);
                    break;
                case -1: //Non-movement command
                    dir = new Vector2(0, 0);
                    break;
                default:
                    dir = new Vector2(0, 0);
                    break;
            }

            if (dir.X == 0 && dir.Y == 0 && collisionState.below && !_animations.isAnimationPlaying(Animations.IDLE)) {
                _animations.play(Animations.IDLE);
            }

            _mover.move(_physicsHandler.calculateMovement(dir, eventHandler), _boxCollider, collisionState);
        }

        public void useAbility(short type) {
            TestScene scene = entity.scene as TestScene;
            Vector2 pos = new Vector2(entity.position.X, entity.position.Y);
            Sprite sprite;
            //Get relative direction of mouse
            float theta = _inputHandler.getMouseDirectionInRad(scene.camera.worldToScreenPoint(pos));

            switch (type) {
                case 0:
                    sprite = new Sprite(musicAttack_1);
                    sprite.setRenderLayer(1);
                    ProjectileWave projectileWave = new ProjectileWave(theta, 8f, 5f);

                    //_animations.play(Animations.ATTACK_IDLE);
                    createProjectile("Wave_Projectile", projectileWave, pos, ref sprite);
                    break;
                case 1:
                    sprite = new Sprite(musicAttack_1);
                    sprite.setRenderLayer(1);
                    ProjectileLinear projectileLinear = new ProjectileLinear(theta, 8f, 5f);

                    //_animations.play(Animations.ATTACK_IDLE);
                    createProjectile("Linear_Projectile", projectileLinear, pos, ref sprite);
                    break;

            }
        }

        public Entity createProjectile(string name, ProjectileController controller, Vector2 pos, ref Sprite sprite) {
            // Entity entity = createEntity("Entity");
            ProjectileEntity waveProjectile = new ProjectileEntity(name, controller, ProjectileType.WAVE);

            //Hardcoding position adjustments
            //pos.X += 16;
            waveProjectile.position = pos;

            //Attach Sprite
            sprite.setRenderLayer(1);
            waveProjectile.addComponent<Sprite>(sprite);

            //Attack physics
            PhysicsHandler physicsHandler = new PhysicsHandler(controller.collisionResult);
            physicsHandler.isProjectile = true;
            physicsHandler.applyGravity = false;
            waveProjectile.addComponent<PhysicsHandler>(physicsHandler);

            //Attach hit detection
            CircleCollider circleCollider = new CircleCollider(sprite.width);
            waveProjectile.addComponent<CircleCollider>(circleCollider);
            Flags.setFlagExclusive(ref circleCollider.collidesWithLayers, 0); //Prevent collisions on layer 0
            Flags.setFlagExclusive(ref circleCollider.physicsLayer, 2); //

            //Attack mover
            Mover mover = new Mover();
            waveProjectile.addComponent<Mover>(mover);

            entity.scene.addEntity<ProjectileEntity>(waveProjectile);

            return waveProjectile;
        }
    }
}

