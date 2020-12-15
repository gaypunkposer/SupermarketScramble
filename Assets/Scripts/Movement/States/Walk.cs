using StateMachine;

namespace Movement.States
{
    public class Walk : MoveAbility
    {
        public Walk()
        {
            Priority = 0;
        }

        public override bool ShouldRunThisState(SMInput input)
        {
            return true;
        }
    }
}