﻿using Microsoft.Xna.Framework.Input;
using System;
using Nez;

namespace DimensionalBeats.Helper{
    class InputHandler {

        public InputHandler() {

        }

        public int getMovement() {
            KeyboardState keyboardState = Input.currentKeyboardState;
            KeyboardState previousState = Input.previousKeyboardState;
            Keys[] keyPressed = keyboardState.GetPressedKeys();

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

            return -1;
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
