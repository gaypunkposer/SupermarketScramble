using GameRules;
using StateMachine;
using UnityEngine;

namespace Movement.States
{
    public class Crouch : MoveAbility
    {
        public bool Crouching { get; private set; }

        public Crouch()
        {
            Priority = 2;
        }

        public override bool ShouldRunThisState(SMInput input)
        {
            if (CrouchTouchingRoof()) return true;

            if (((MovementInput) input).crouch) Crouching = !Crouching;

            return Crouching;
        }

        private bool CrouchTouchingRoof()
        {
            return Physics.SphereCast(PlayerState.Instance.Transform.position, .3f, Vector3.up,
                out var info, .95f, 1, QueryTriggerInteraction.Ignore);
        }
    }
}