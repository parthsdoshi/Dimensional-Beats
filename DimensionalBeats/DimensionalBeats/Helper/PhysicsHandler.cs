
using Microsoft.Xna.Framework;
using DimensionalBeats.Entities;
using Nez;
using Nez.Tiled;

namespace DimensionalBeats.Helper {
    class PhysicsHandler : Component{
        private const float _GRAVITY = 9.8f;

        private CookieCutterEntity entity;

        private float _mass;
        private float _friction;

        public float jumpHeight { get; set; }
        //public float jumpDelay { get; set; }
        //public float jumpTimer { get; set; }
        public float maxVelocity { get; set; }
        public float moveSpeed { get; set; }

        private float _deltaX;
        private float _deltaY;
        private float _velocityX;
        private float _velocityY;

        private TiledMapMover.CollisionState _collisionState;

        public PhysicsHandler(CookieCutterEntity entity, TiledMapMover.CollisionState collisionState, float mass, float friction = .3f) {
            this.entity = entity;
            this._collisionState = collisionState;
            this._mass = mass;
            this._friction = friction;

            //Default values (units in per Tile)
            jumpHeight = 1.2f;
            //jumpTimer = 0;
            //jumpDelay = .2f;
            maxVelocity = 2f;
            moveSpeed = 16f;

            _deltaX = 0;
            _deltaY = 0;
            _velocityX = 0;
            _velocityY = 0;
        }

        public Vector2 calculateMovement(int dir, int trigger = -1) {
            //Apply gravity
            if (!_collisionState.below)
                _velocityY += _GRAVITY * Time.deltaTime;

            //Apply initial force
            switch (dir) {
                case 1:
                    _velocityX += moveSpeed * Time.deltaTime;
                    break;
                case 2:
                    _velocityX += moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    _velocityX += moveSpeed * Time.deltaTime;
                    break;
                case 5:
                    _velocityX -= moveSpeed * Time.deltaTime;
                    break;
                case 6:
                    _velocityX -= moveSpeed * Time.deltaTime;
                    break;
                case 7:
                    _velocityX -= moveSpeed * Time.deltaTime;
                    break;
                case -1:
                    break;
                default:
                    break;
            }

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

            entity.isMovingLeft = (_velocityX < 0) ? true : false;
            entity.isMovingRight = (_velocityX > 0) ? true : false;

            /*Calculate friction
             * V = V0 - at
             */
            if (!entity.isMovingRight && _velocityX != 0) _velocityX = Mathf.clamp(_velocityX + ((_friction * _GRAVITY) * Time.deltaTime), -maxVelocity, 0);
            else if(!entity.isMovingLeft && _velocityX != 0) _velocityX = Mathf.clamp(_velocityX - ((_friction * _GRAVITY) * Time.deltaTime), 0, maxVelocity);

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

            return new Vector2(_velocityX, _velocityY);
        }

        public void applyForce(Vector2 force) {
            _velocityX += force.X;
            _velocityY += force.Y;
        }

    }
}
