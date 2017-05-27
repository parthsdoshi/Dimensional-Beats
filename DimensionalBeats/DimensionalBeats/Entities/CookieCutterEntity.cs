using System;
using Nez;
using DimensionalBeats.Helper;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using DimensionalBeats.Controllers;

namespace DimensionalBeats.Entities {
    class CookieCutterEntity : Entity{

        public bool isMovingLeft { get; set; }
        public bool isMovingRight { get; set; }

        protected bool _currentState;

        //Testing purposes
        public CookieCutterEntity(Vector2 position) : base() {
            addComponent<Sprite>(new PrototypeSprite(16, 16)).setColor(Color.Red);
            this.position = position;

            isMovingLeft = false;
            isMovingRight = false;
        }

        public CookieCutterEntity(String name, Vector2 position, ref Sprite sprite, Controller controller) : base(name) {
            this.position = position;
            sprite.setEnabled(false);
            addComponent<Sprite>(sprite);
            addComponent<Controller>(controller);

            isMovingLeft = false;
            isMovingRight = false;

            Debug.log("Player created");
        }

        public override void onAddedToScene() {
            base.onAddedToScene();
            Debug.log("Player added to scene");
        }

        //Change sprite of the entity
        public void loadSprites(Sprite sprite) {
            //Eventually load all sprites from a sprite sheet into a sprite-state manager

            /*
            if (getComponent<Sprite>() != null) removeComponent<Sprite>();

            addComponent<Sprite>(sprite);
            */
        }

        public override void update() {
            base.update();
            //Debug.log("X: " + position.X + " Y: " + position.Y);
        }
    }
}
