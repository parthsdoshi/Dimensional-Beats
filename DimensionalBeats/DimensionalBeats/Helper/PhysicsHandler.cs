
using Microsoft.Xna.Framework;
using DimensionalBeats.Entities;
using Nez;
using Nez.Tiled;

namespace DimensionalBeats.Helper {
    class PhysicsHandler : Component{
        private const float _GRAVITY = 9.8f;

        private CookieCutterEntity cookieCutterEntity;

        private float _mass;
        private float _friction;

        public float jumpHeight { get; set; }
        //public float jumpDelay { get; set; }
        //public float jumpTimer { get; set; }
        public float maxVelocity { get; set; }
        public float moveSpeed { get; set; }
        public bool applyGravity { get; set; }

        private float _deltaX;
        private float _deltaY;
        private float _velocityX;
        private float _velocityY;

        private TiledMapMover.CollisionState _collisionState;

        public PhysicsHandler(CookieCutterEntity entity, TiledMapMover.CollisionState collisionState, float mass, float friction = .3f) {
            this.cookieCutterEntity = entity;
            this._collisionState = collisionState;
            this._mass = mass;
            this._friction = friction;

            //Default values (units in per Tile)
            jumpHeight = 1.2f;
            //jumpTimer = 0;
            //jumpDelay = .2f;
            maxVelocity = 2f;
            moveSpeed = 16f;
            applyGravity = true;

            _deltaX = 0;
            _deltaY = 0;
            _velocityX = 0;
            _velocityY = 0;
        }

        public Vector2 calculateMovement(Vector2 dir, int trigger = -1) {
            float theta = Mathf.atan2(dir.Y, dir.X);
            //Apply gravity
            if (!_collisionState.below && applyGravity)
                _velocityY += _GRAVITY * Time.deltaTime;

            //Apply initial force (apply force in direction)
            if(dir.X != 0 && dir.Y != 0)
                applyForce(new Vector2(moveSpeed / Mathf.sin(theta), 0));
            

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

            cookieCutterEntity.isMovingLeft = (_velocityX < 0) ? true : false;
            cookieCutterEntity.isMovingRight = (_velocityX > 0) ? true : false;

            /*Calculate friction
             * V = V0 - at
             */
            if (cookieCutterEntity.isMovingLeft) _velocityX = Mathf.clamp(_velocityX + ((_friction * _GRAVITY) * Time.deltaTime), -maxVelocity, 0);
            else if(cookieCutterEntity.isMovingRight) _velocityX = Mathf.clamp(_velocityX - ((_friction * _GRAVITY) * Time.deltaTime), 0, maxVelocity);

            // epsilon
            if (Mathf.approximately(0, _velocityX)) _velocityX = 0;

            //Check statements
            /*
            if (_collisionState.left || _collisionState.right) {
                _velocityX = 0;
                Debug.log("Clamp vX");
            }
            */
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
