
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace DimensionalBeats.Helper {
    class PhysicsHandler {
        private const float _GRAVITY = 4.9f;

        private float _mass;
        private float _friction;

        public float jumpVelocity { get; set; }
        public float jumpDelay { get; set; }
        public float jumpTimer { get; set; }
        public float maxVelocity { get; set; }
        public float moveSpeed { get; set; }
        private bool _isMovingLeft;
        private bool _isMovingRight;

        private float _deltaX;
        private float _deltaY;
        private float _velocityX;
        private float _velocityY;

        private TiledMapMover.CollisionState _collisionState;

        public PhysicsHandler(TiledMapMover.CollisionState collisionState, float mass, float friction = .1f) {
            this._collisionState = collisionState;
            this._mass = mass;
            this._friction = friction;

            //Default values (units in per Tile)
            jumpVelocity = 2.5f;
            jumpTimer = 0;
            jumpDelay = .2f;
            maxVelocity = 2f;
            moveSpeed = 4f;
            _isMovingLeft = false;
            _isMovingRight = false;

            _deltaX = 0;
            _deltaY = 0;
            _velocityX = 0;
            _velocityY = 0;
        }

        public Vector2 calculateMovement(int dir) {
            //Apply gravity
            if (!_collisionState.below)
                _velocityY += _GRAVITY * Time.deltaTime;

            //Apply initial force
            switch (dir) {
                case 0:
                    if (_collisionState.below && jumpTimer >= jumpDelay) {
                        _velocityY = -jumpVelocity;
                        jumpTimer = 0;
                    }
                    break;
                case 1:
                    _velocityX += moveSpeed * Time.deltaTime;
                    if (_collisionState.below && jumpTimer >= jumpDelay) {
                        _velocityY = -jumpVelocity;
                        jumpTimer = 0;
                    }
                    break;
                case 2:
                    _velocityX += moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    _velocityX += moveSpeed * Time.deltaTime;
                    break;
                case 4:
                    break;
                case 5:
                    _velocityX -= moveSpeed * Time.deltaTime;
                    break;
                case 6:
                    _velocityX -= moveSpeed * Time.deltaTime;
                    break;
                case 7:
                    _velocityX -= moveSpeed * Time.deltaTime;
                    if (_collisionState.below && jumpTimer >= jumpDelay) {
                        _velocityY = -jumpVelocity;
                        jumpTimer = 0;
                    }
                    break;
                case -1:
                    break;
            }


            _isMovingLeft = (_velocityX < 0) ? true : false;
            _isMovingRight = (_velocityX > 0) ? true : false;


            Debug.log("Before friction - vX: " + _velocityX + " vY: " + _velocityY);

            /*Calculate friction
             * V = V0 - at
             */
            if (!_isMovingRight && _velocityX != 0) _velocityX = Mathf.clamp(_velocityX + ((_friction * _GRAVITY) * Time.deltaTime), -maxVelocity, 0);
            else if(!_isMovingLeft && _velocityX != 0) _velocityX = Mathf.clamp(_velocityX - ((_friction * _GRAVITY) * Time.deltaTime), 0, maxVelocity);

            if (Mathf.approximately(0, _velocityX)) _velocityX = 0;

            //Check statements
            if (_collisionState.left || _collisionState.right) _velocityX = 0;
            if (_collisionState.below) {
                jumpTimer = Mathf.clamp(jumpTimer + Time.deltaTime, 0, jumpDelay);
                if(_velocityY >= 0) _velocityY = 0;
            }
            if (_collisionState.above) _velocityY = 0;

            Debug.log("vX: " + _velocityX + " vY: " + _velocityY);

            return new Vector2(_velocityX, _velocityY);
        }
    }
}
