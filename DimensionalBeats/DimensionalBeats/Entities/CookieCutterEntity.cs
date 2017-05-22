using System;
using Nez;
using DimensionalBeats.Helper;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using DimensionalBeats.Controllers;

namespace DimensionalBeats.Entities {
    class CookieCutterEntity : Entity{

        private InputHandler inputHandler;

        public CookieCutterEntity(Vector2 position) : base() {
            addComponent<Sprite>(new PrototypeSprite(32, 32)).setColor(Color.Red);
            this.position = position;

            inputHandler = new InputHandler();
        }

        public CookieCutterEntity(String name, Vector2 position, Sprite sprite, Controller controller) : base(name) {
            this.name = name;
            this.position = position;
            addComponent<Sprite>(sprite);
            addComponent<Controller>(controller);

            inputHandler = new InputHandler();
        }

        public override void update() {
            base.update();
        }

    }
}
