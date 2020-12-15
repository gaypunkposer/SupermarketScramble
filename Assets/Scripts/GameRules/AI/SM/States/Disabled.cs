using StateMachine;
using UnityEngine;

namespace GameRules.AI.SM.States
{
    public class Disabled : State
    {
        public float disableTime;

        private float _timer;
        public Disabled()
        {
            Priority = 1000;
        }
        public override bool ShouldRunThisState(SMInput input)
        {
            return ((AIInput) input).disabled || _timer > 0;
        }

        public override SMOutput Run(SMInput input)
        {
            if (((AIInput) input).disabled)
                _timer = disableTime;
            else
                _timer -= Time.deltaTime;
                    
            return new AIOutput(input, transform.position);
        }
    }
}
