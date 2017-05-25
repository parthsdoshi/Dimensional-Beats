using Microsoft.Xna.Framework.Input;
using System;
using Nez;
using Microsoft.Xna.Framework;

namespace DimensionalBeats.Helper{
    class InputHandler {

        public InputHandler() {

        }

        public int getMovement() {
            KeyboardState keyboardState = Input.currentKeyboardState;
            KeyboardState previousState = Input.previousKeyboardState;
            Keys[] keysPressed = keyboardState.GetPressedKeys();

            if ((isLeft(previousState) && isUp(keyboardState)) ||
                (isLeft(keyboardState) && isUp(previousState))) return 7;
            if ((isLeft(previousState) && isDown(keyboardState)) &&
                (isLeft(keyboardState) && isDown(previousState))) return 5;
            if ((isRight(previousState) && isUp(keyboardState)) ||
                (isRight(keyboardState) && isUp(previousState))) return 1;
            if ((isRight(previousState) && isDown(keyboardState)) &&
                (isRight(keyboardState) && isDown(previousState))) return 3;

            if (isLeft(keyboardState)) return 6;
            if (isRight(keyboardState)) return 2;
            if (isUp(keyboardState)) return 0;
            if (isDown(keyboardState)) return 4;


            return -1;
        }

        public int getEvent() {
            //Jump event
            if (Input.isKeyPressed(Keys.W) || Input.isKeyPressed(Keys.Space)) return 0;
            if (Input.isKeyPressed(Keys.D1) || Input.isKeyPressed(Keys.Q)) {
                Debug.log("Event registered: Ability 1");
                return 1;
            }
            if (Input.isKeyPressed(Keys.D2) || Input.isKeyPressed(Keys.E)) {
                Debug.log("Event registered: Ability 2");
                return 2;
            }
            if (Input.leftMouseButtonPressed) {
                Debug.log("Event registered: Fire ability");
                return 10;
            }
            return -1;
        }

        //Get direction of mouse from player
        public float getMouseDirectionInRad(Vector2 pos) {
            float x = Input.scaledMousePosition.X - pos.X;
            float y = Input.scaledMousePosition.Y - pos.Y;

            return Mathf.atan2(y, x);
        }

        public Boolean isLeft(KeyboardState k) {
            if (k.IsKeyDown(Keys.A))
                return true;
            return false;
        }

        public Boolean isRight(KeyboardState k) {
            if (k.IsKeyDown(Keys.D))
                return true;
            return false;
        }

        public Boolean isUp(KeyboardState k) {
            if (k.IsKeyDown(Keys.W))
                return true;
            return false;
        }

        public Boolean isDown(KeyboardState k) {
            if (k.IsKeyDown(Keys.S))
                return true;
            return false;
        }

        public Boolean justPressed(Keys k) {
            return Input.isKeyPressed(k);
        }
    }
}
