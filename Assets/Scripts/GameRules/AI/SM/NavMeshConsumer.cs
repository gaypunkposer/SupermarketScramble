using StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace GameRules.AI.SM
{
    public class NavMeshConsumer : MonoBehaviour, ISMOutputConsumer
    {
        public NavMeshAgent agent;
        public AudioSource source;
        
        private Vector3 _previousTargetPosition;
        public int GetPriority()
        {
            return -1;
        }

        public void ReceiveOutput(SMOutput output)
        {
            Vector3 loc = ((AIOutput) output).moveToLocation;
            
            if (loc == _previousTargetPosition) return;
            
            agent.SetDestination(loc);
            _previousTargetPosition = loc;

            if (agent.velocity.magnitude > 0.01f && !source.isPlaying)
            {
                source.Play();
            }
        }
    }
}
