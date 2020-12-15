using StateMachine;
using UnityEngine;

namespace Movement
{
    public class MovementInputProducer : MonoBehaviour, ISMInputProducer
    {
        public SMInput GetInput(State availableState)
        {
            return new MovementInput(availableState);
        }

        public SMInput GetInput(SMInput availableState)
        {
          return new MovementInput(availableState);  
        }
    }
}
