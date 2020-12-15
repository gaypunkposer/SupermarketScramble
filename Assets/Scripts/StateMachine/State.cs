using UnityEngine;

namespace StateMachine
{
    public abstract class State : MonoBehaviour
    {
        public int Priority { get; protected set; }

        protected State()
        {
            Priority = int.MinValue;
        }
        //Should the machine run this state? Will run on every state, in descending order, until a state satisfies this request.
        //Can act as an 'update' for any states that need to run, but don't need to prevent other states from running
        public abstract bool ShouldRunThisState(SMInput input);
        //Run the state that satisfies ShouldRunThisState
        public abstract SMOutput Run(SMInput input);
    }
}
