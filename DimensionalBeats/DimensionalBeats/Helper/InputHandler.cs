﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Helpers{
    class InputHandler {
        KeyboardState keyboardState;
        KeyboardState previousState;

        public InputHandler() {

        }

        public int getMovement() {
            keyboardState = Keyboard.GetState();
            if (isLeft(previousState) ^ isLeft(keyboardState)) {
                if (isUp(previousState) || isUp(keyboardState))
                    return 7;
                else if (isDown(previousState) || isDown(keyboardState))
                    return 5;
            } else if (isRight(previousState) ^ isRight(keyboardState)) {
                if (isUp(previousState) || isUp(keyboardState))
                    return 1;
                else if (isDown(previousState) || isDown(keyboardState))
                    return 3;
            }
            if (isLeft(keyboardState)) return 6;
            if (isRight(keyboardState)) return 2;
            if (isUp(keyboardState)) return 0;
            if (isDown(keyboardState)) return 4;


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
    }
}