using GameRules;
using StateMachine;
using UnityEngine;

namespace Movement.States
{
    public abstract class MoveAbility : State
    {
        public float speed;
        
        public override SMOutput Run(SMInput input)
        {
            MovementInput inpt = (MovementInput) input;

            Vector3 vel = PlayerStatus.Instance.Transform.TransformDirection(inpt.directionalInput.normalized) * speed;
            vel -= inpt.lateralVelocity;
            vel.y = -1;

            return new MovementOutput(input, vel);
        }
    }
}