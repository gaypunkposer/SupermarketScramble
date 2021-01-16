using GameRules;
using Movement.States;
using StateMachine;
using UnityEngine;

namespace Movement
{
    public class MovementColliderConsumer : MonoBehaviour, ISMOutputConsumer
    {
        public int GetPriority()
        {
            return 0;
        }

        //Make adjustments to the colliders as necessary
        public void ReceiveOutput(SMOutput output)
        {
            float height;
            float radius;
            Vector3 center;
            switch (output.input.state)
            {
                case CrouchRun _:
                case Crouch _:
                    height = 1f;
                    radius = 0.45f;
                    center = Vector3.down;
                    break;
                default:
                    height = 2f;
                    radius = 0.5f;
                    center = Vector3.zero;    
                    break;
            }

            PlayerStatus.Instance.CollisionHandler.height = height;
            PlayerStatus.Instance.CollisionHandler.radius = radius;
            PlayerStatus.Instance.CollisionHandler.center = center;
        }
    }
}
