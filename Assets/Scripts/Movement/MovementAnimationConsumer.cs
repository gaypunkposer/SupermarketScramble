using GameRules;
using StateMachine;
using UnityEngine;

namespace Movement
{
    public class MovementAnimationConsumer : MonoBehaviour, ISMOutputConsumer
    {
        private static readonly int AnimSpeed = Animator.StringToHash("Movement Speed");
        private static readonly int AnimState = Animator.StringToHash("Movement State");
        
        public int GetPriority()
        {
            return 1;
        }

        public void ReceiveOutput(SMOutput output)
        {
            //Feed data into the player's viewmodel animation controller
            PlayerStatus.Instance.Viewmodel.SetInteger(AnimState, output.input.state.Priority);
            PlayerStatus.Instance.Viewmodel.SetFloat(AnimSpeed,
                ((MovementInput) output.input).lateralVelocity.magnitude);
            PlayerStatus.Instance.Viewmodel.transform.localPosition = Vector3.down;
            PlayerStatus.Instance.Viewmodel.transform.localEulerAngles = Vector3.zero;
        }
    }
}
