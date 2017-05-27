using Microsoft.Xna.Framework.Input;
using System;
using Nez;
using Microsoft.Xna.Framework;

namespace DimensionalBeats.Helper{
    static class InputHandler {

        static public int getMovement() {
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

        static public int getEvent() {
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
            if (Input.isKeyPressed(Keys.D3) || Input.isKeyPressed(Keys.R)) {
                Debug.log("Event registered: Ability 3");
                return 3;
            }
            if (Input.leftMouseButtonPressed) {
                Debug.log("Event registered: Fire ability");
                return 10;
            }
            return -1;
        }

        //Get direction of mouse from player
        static public float getMouseDirectionInRad(Vector2 pos) {
            float x = Input.scaledMousePosition.X - pos.X;
            float y = Input.scaledMousePosition.Y - pos.Y;

            return Mathf.atan2(y, x);
        }

        static public Boolean isLeft(KeyboardState k) {
            if (k.IsKeyDown(Keys.A))
                return true;
            return false;
        }

        static public Boolean isRight(KeyboardState k) {
            if (k.IsKeyDown(Keys.D))
                return true;
            return false;
        }

        static public Boolean isUp(KeyboardState k) {
            if (k.IsKeyDown(Keys.W))
                return true;
            return false;
        }

        static public Boolean isDown(KeyboardState k) {
            if (k.IsKeyDown(Keys.S))
                return true;
            return false;
        }

        static public Boolean justPressed(Keys k) {
            return Input.isKeyPressed(k);
        }
    }
}
