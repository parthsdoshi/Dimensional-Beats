using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionalBeats.Controllers.Enemy_Controller {
    class EnemyMeleeController : EnemyController, IUpdatable {

        //Physics values
        public float velocity { get; set; }
        public float maxVelocity { get; set; }
        private float _jumpHeight { get; set; }

        //Mob values
        private float _health { get; }
        private float _damage { get; }

        public EnemyMeleeController() : base() {

        }

        public void update() {
            throw new NotImplementedException();
        }
    }
}
