using StateMachine;
using UnityEngine;

namespace Movement.States
{
    [RequireComponent(typeof(Crouch))]
    public class CrouchRun : MoveAbility
    {
        private Crouch _crouch;

        public CrouchRun()
        {
            Priority = 3;
        }

        private void Start()
        {
            _crouch = GetComponent<Crouch>();
        }

        public override bool ShouldRunThisState(SMInput input)
        {
            return _crouch.Crouching && ((MovementInput)input).sprint;
        }
    }
}