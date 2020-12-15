using StateMachine;

namespace Movement.States
{
    public class Sprint : MoveAbility
    {
        public Sprint()
        {
            Priority = 1;
        }

        public override bool ShouldRunThisState(SMInput input)
        {
            return ((MovementInput)input).sprint;
        }

    }
}