using StateMachine;
using UnityEngine;

namespace Movement
{
    public class MovementOutput : SMOutput
    {
        //Just feed the outputted velocity and current input into the consumers
        public Vector3 velocity;

        public MovementOutput(SMInput input) : base(input)
        {
        }

        public MovementOutput(SMInput input, Vector3 vel) : this(input)
        {
            velocity = vel;
        }
    }
}
