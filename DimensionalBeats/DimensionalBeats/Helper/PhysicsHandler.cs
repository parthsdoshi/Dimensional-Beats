
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace DimensionalBeats.Helper {
    class PhysicsHandler {
        private const float _gravity = 32;

        private float _mass;
        private float _friction;

        public float _jumpVelocity { get; set; }
        public float _maxVelocity { get; set; }
        public float _moveSpeed { get; set; }
        private bool _isMoving;

        private float _deltaX;
        private float _deltaY;
        private float _velocityX;
        private float _velocityY;

        private TiledMapMover.CollisionState _collisionState;

        public PhysicsHandler(TiledMapMover.CollisionState collisionState, float mass, float friction = .3f) {
            this._collisionState = collisionState;
            this._mass = mass;
            this._friction = friction;

            //Default values
            _jumpVelocity = 8;
            _maxVelocity = 8;
            _moveSpeed = 32;
            _isMoving = false;

            _deltaX = 0;
            _deltaY = 0;
            _velocityX = 0;
            _velocityY = 0;
        }

        public Vector2 calculateMovement(int dir) {
            //Apply gravity
            if (!_collisionState.below)
                _velocityY += _gravity * Time.deltaTime;

            //Apply initial force
            switch (dir) {
                case 0:
                    if(_collisionState.below)
                        _velocityY = -_jumpVelocity;
                    break;
                case 1:
                    _velocityX += _moveSpeed * Time.deltaTime;
                    if (_collisionState.below)
                        _velocityY = -_jumpVelocity;
                    break;
                case 2:
                    _velocityX += _moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    _velocityX += _moveSpeed * Time.deltaTime;
                    break;
                case 4:
                    break;
                case 5:
                    _velocityX -= _moveSpeed * Time.deltaTime;
                    break;
                case 6:
                    _velocityX -= _moveSpeed * Time.deltaTime;
                    break;
                case 7:
                    _velocityX -= _moveSpeed * Time.deltaTime;
                    if (_collisionState.below)
                        _velocityY = -_jumpVelocity;
                    break;
                case -1:
                    break;
            }

            /*Calculate friction
             * V = V0 - at
             */
            if (_velocityX > 0) _velocityX = Mathf.clamp(_velocityX - ((_friction * _gravity) * Time.deltaTime), 0, _maxVelocity);
            else _velocityX = Mathf.clamp(_velocityX + ((_friction * _gravity) * Time.deltaTime), -_maxVelocity, 0);

            if (Mathf.approximately(0, _velocityX)) _velocityX = 0;

            //Check statements
            if (_collisionState.left || _collisionState.right) _velocityX = 0;
            if (_collisionState.below && _velocityY >= 0) _velocityY = 0;
            _isMoving = (_velocityX != 0) ? true : false;

            return new Vector2(_velocityX, _velocityY);
        }
    }
}
