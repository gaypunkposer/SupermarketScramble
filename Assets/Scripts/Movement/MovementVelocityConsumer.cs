using GameRules;
using StateMachine;
using UnityEngine;

namespace Movement
{
    public class MovementVelocityConsumer : MonoBehaviour, ISMOutputConsumer
    {
        private Vector3 _velocity;

        public int GetPriority()
        {
            return 2;
        }

        public void ReceiveOutput(SMOutput output)
        {
            //Take the velocity given to us by the state machine, and modify it for the other consumers/next run
            _velocity += ((MovementOutput) output).velocity;
            ((MovementInput) output.input).lateralVelocity = _velocity;
            ((MovementOutput) output).velocity = _velocity;
            
            PlayerState.Instance.CollisionHandler.controller.Move(_velocity * Time.deltaTime);
            PlayerState.Instance.CollisionHandler.Rigidbody.velocity = _velocity;
        }
    }
}
