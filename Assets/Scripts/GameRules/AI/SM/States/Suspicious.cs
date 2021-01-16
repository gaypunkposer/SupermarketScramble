using StateMachine;
using UnityEngine;

namespace GameRules.AI.SM.States
{
    public class Suspicious : State
    {
        public float investigateMin = 5;
        public float investigateDistance = 1;
        public float growRate = 1;
        public float decayRate = 1;
        public float minFollowSuspicion = 0.5f;
        
        private Vector3 _investigationSite;
        private Vector3 _lastPlayerLoc;
        private Transform _objPos;

        public Suspicious()
        {
            Priority = 10;
        }

        public override bool ShouldRunThisState(SMInput input)
        {
            AIInput inpt = (AIInput) input;
            
            if (inpt.canSeePlayer)
                _lastPlayerLoc = PlayerStatus.Instance.Transform.position;
            
            //If we're at the investigation site, or there's no investigation site, decay the sus level
            if (Vector3.Distance(transform.position, _investigationSite) <= investigateDistance ||
                _investigationSite == Vector3.zero)
                inpt.suspicion = inpt.suspicion > 0 ? inpt.suspicion - decayRate * Time.deltaTime : 0;

            //If the player is in sight and being sus, or there's been a noise, or we're still investigating something, return true
            return inpt.canSeePlayer && PlayerStatus.Instance.Suspicion > 0 ||
                   inpt.latestObjectOfInterest != null || inpt.suspicion > 0;
        }

        public override SMOutput Run(SMInput input)
        {
            AIInput inpt = (AIInput) input;
            
            //Go investigate a sound if the player isn't in sight or not doing anything suspicious
            if (inpt.latestObjectOfInterest != _objPos && inpt.latestObjectOfInterest != null)//&& (!inpt.CanSeePlayer || PlayerState.Instance.Suspicion <= 0))
            {
                _investigationSite = inpt.latestObjectOfInterest.position;
                inpt.suspicion = inpt.suspicion >= investigateMin ? inpt.suspicion + investigateMin : investigateMin;
                _objPos = inpt.latestObjectOfInterest;
            }
            else if (_objPos != null)
            {
                _investigationSite = _objPos.position;
            }
            //If player is doing shenanigans in front of the helper, investigate
            else if (inpt.canSeePlayer && PlayerStatus.Instance.Suspicion >= minFollowSuspicion)
            {
                _investigationSite = _lastPlayerLoc;
                inpt.suspicion += PlayerStatus.Instance.Suspicion * growRate * Time.deltaTime;
                //PlayerState.Instance.UI.Alerts.AddAlert("A Helper noticed you do that!");
            }

            if (inpt.suspicion <= 0)
            {
                _investigationSite = Vector3.zero;
                inpt.latestObjectOfInterest = null;
                _objPos = null;
            }
            
            //If the sus level is high enough, go investigate the scene of the "crime"
            return inpt.suspicion >= investigateMin ? new AIOutput(input, _investigationSite) : new AIOutput(input, transform.position);
        }
    }
}