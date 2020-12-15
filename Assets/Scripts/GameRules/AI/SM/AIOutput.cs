using StateMachine;
using UnityEngine;

namespace GameRules.AI.SM
{
    public class AIOutput : SMOutput
    {
        public Vector3 moveToLocation;

        private AIOutput(SMInput input) : base(input)
        {
        }

        public AIOutput(SMInput input, Vector3 loc) : this(input)
        {
            moveToLocation = loc;
        }
    }
}
