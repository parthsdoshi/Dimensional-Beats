using Microsoft.Xna.Framework;
using DimensionalBeats.Entities;
using Nez;
using Nez.Tiled;

namespace DimensionalBeats.Helper {
    class PhysicsHandler : Component{

        #region VARIABLES
        private const float _GRAVITY = 9.8f;
        private CookieCutterEntity _cookieCutterEntity;

        private float _mass;
        private float _friction;

        public float jumpHeight { get; set; }
        //public float jumpDelay { get; set; }
        //public float jumpTimer { get; set; }
        public float maxVelocity { get; set; }
        public float moveSpeed { get; set; }
        public bool applyGravity { get; set; }
        public bool isProjectile { get; set; }

        private float _deltaX;
        private float _deltaY;
        private float _velocityX;
        private float _velocityY;

        private TiledMapMover.CollisionState _collisionState;
        private CollisionResult _collisionResult;
        #endregion

        //Deal with entities
        public PhysicsHandler(CookieCutterEntity entity, TiledMapMover.CollisionState collisionState, float mass, float friction = .3f) : base() {
            this._cookieCutterEntity = entity;
            this._collisionState = collisionState;
            this._mass = mass;
            this._friction = friction;

            //Default values (units in per Tile)
            jumpHeight = .8f;
            maxVelocity = 2.5f;
            moveSpeed = 16f;
            applyGravity = true;
            _deltaX = moveSpeed;
            _deltaY = 0;
            _velocityX = 0;
            _velocityY = 0;
        }

        //Deal with projectiles
        public PhysicsHandler(CollisionResult collisionResult, float mass = 10f) : base() {
            this._collisionResult = collisionResult;
            this._mass = mass;

            _friction = 0f;

            jumpHeight = 0f;
            maxVelocity = 4f;
            moveSpeed = 16f;
            applyGravity = true;
            _deltaX = moveSpeed;
            _deltaY = moveSpeed;
            _velocityX = 0;
            _velocityY = 0;

        }

        public Vector2 calculateMovement(Vector2 dir, int trigger = -1) {
            float theta = Mathf.atan2(dir.Y, dir.X);

            //Apply gravity
            if (!_collisionState.below && applyGravity)
                _velocityY += _GRAVITY * Time.deltaTime;

            //Apply initial force (apply force in direction)
            if (dir.X != 0 || dir.Y != 0) {
                _deltaX = Mathf.cos(theta) * moveSpeed * Time.deltaTime;
                if(isProjectile)
                    _deltaY = Mathf.sin(theta) * moveSpeed * Time.deltaTime;

                //Apply forces
                applyForce(new Vector2(_deltaX, _deltaY));
            }

            //Deal with triggers here
            switch (trigger) {
                //Jump trigger
                case 0:
                    if (_collisionState.below /*&& jumpTimer >= jumpDelay*/) {
                        applyForce(new Vector2(0, -Mathf.sqrt(2 * jumpHeight * _GRAVITY)));
                        //jumpTimer = 0;
                    }
                    break;
                case -1:
                    break;
                default:
                    break;
            }

            //Set entity movement direction
            _cookieCutterEntity.isMovingLeft = (_velocityX < 0) ? true : false;
            _cookieCutterEntity.isMovingRight = (_velocityX > 0) ? true : false;

            /*Calculate friction
             * V = V0 - at
             */

            if (_cookieCutterEntity.isMovingLeft) _velocityX = Mathf.clamp(_velocityX + ((_friction * _GRAVITY) * Time.deltaTime), -maxVelocity, 0);
            else if(_cookieCutterEntity.isMovingRight) _velocityX = Mathf.clamp(_velocityX - ((_friction * _GRAVITY) * Time.deltaTime), 0, maxVelocity);
            

            if (Mathf.approximately(0, _velocityX)) _velocityX = 0;
            //Check statements
            
            if (_collisionState.left || _collisionState.right) {
                _velocityX = 0;
                Debug.log("Clamp vX");
            }

            if (_collisionState.below) {
                //jumpTimer = Mathf.clamp(jumpTimer + Time.deltaTime, 0, jumpDelay);
                if(_velocityY >= 0) _velocityY = 0;
            }
            if (_collisionState.above) _velocityY = 0;
            return getImpulse();
        }
        
        public Vector2 getImpulse() { return new Vector2(_velocityX, _velocityY); }

        public void applyForce(Vector2 force) {
            _velocityX += force.X;
            _velocityY += force.Y;
        }
    }
}



