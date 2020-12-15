using StateMachine;
using UnityEngine;

namespace GameRules.AI.SM.States
{
    public class Neutral : State
    {
        public Vector3[] patrolPoints;
        public float pointDistance;
        
        private int _currentPoint;
        public Neutral()
        {
            Priority = 0;
        }

        public override bool ShouldRunThisState(SMInput input)
        {
            return true;
        }

        public override SMOutput Run(SMInput input)
        {
            if (Vector3.Distance(patrolPoints[_currentPoint], transform.position) <= pointDistance)
            {
                _currentPoint++;
            }
            
            if (_currentPoint >= patrolPoints.Length)
                _currentPoint = 0;
            return new AIOutput(input, patrolPoints[_currentPoint]);
        }
    }
}
