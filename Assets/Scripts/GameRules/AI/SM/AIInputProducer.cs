using StateMachine;
using UnityEngine;

namespace GameRules.AI.SM
{
    public class AIInputProducer : MonoBehaviour, ISMInputProducer
    {
        public Transform headPos;
        public float maxDetectDistance;
        
        private bool _disableTrigger;
        private Transform _interestObject;
        
        public SMInput GetInput(State start)
        {
            return new AIInput(start);
        }

        public SMInput GetInput(SMInput prevInput)
        {
            var res = new AIInput(prevInput, _disableTrigger, FindPlayer(), _interestObject, ((AIInput)prevInput).suspicion, null);
            if (_disableTrigger) _disableTrigger = false;
            return res;
        }

        private bool FindPlayer()
        {
            var playerPos = PlayerState.Instance.Transform.position;

            if (Vector3.Distance(playerPos, headPos.position) > maxDetectDistance)
                return false;
            
            if (Physics.Raycast(headPos.position, playerPos - headPos.position, out var hit, maxDetectDistance,
                1 << 9 | 1, QueryTriggerInteraction.Ignore))
                return hit.collider.gameObject.layer == 9; //Return that the thing we hit was the player
            
            return false;
        }

        public void TriggerDisabled()
        {
            _disableTrigger = true;
        }

        public void AlertSound(Transform t)
        {
            _interestObject = t;
        }
    }
}
