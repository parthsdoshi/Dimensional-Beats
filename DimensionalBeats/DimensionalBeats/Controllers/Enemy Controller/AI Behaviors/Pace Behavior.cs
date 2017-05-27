
using System;
using Nez.AI.BehaviorTrees;
using DimensionalBeats.Entities;

namespace DimensionalBeats.Controllers.Enemy_Controller.AI_Behaviors {
    class Pace_Behavior : Behavior<CookieCutterEntity> {

        //Initialize everything needed before executing behavior
        public override void onStart() {
            base.onStart();
        }

        //Do stuff when behavior ends
        public override void onEnd() {
            base.onEnd();
        }

        public override TaskStatus update(CookieCutterEntity context) {
            throw new NotImplementedException();
        }
    }
}
