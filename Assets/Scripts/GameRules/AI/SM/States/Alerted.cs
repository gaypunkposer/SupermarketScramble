﻿using StateMachine;
using UnityEngine;

namespace GameRules.AI.SM.States
{
    [RequireComponent(typeof(Suspicious))]
    public class Alerted : State
    {
        public float alertMin = 10;
        public float decayRate = .5f;

        private bool _alerted;
        
        public Alerted()
        {
            Priority = 100;
        }

        public override bool ShouldRunThisState(SMInput input)
        {
            if (((AIInput) input).suspicion <= alertMin)
                _alerted = false;
            return ((AIInput)input).suspicion > alertMin;
        }

        public override SMOutput Run(SMInput input)
        {
            AIInput inpt = (AIInput) input;
            if (inpt.canSeePlayer && PlayerStatus.Instance.Suspicion > 0)
            {
                if (!_alerted)
                {
                    PlayerStatus.Instance.UI.Alerts.AddAlert("Security is going to remove you from the store. Run!");
                    _alerted = true; //Only alert once, until things have died down
                }
                
            }

            if (_alerted && Vector3.Distance(transform.position, PlayerStatus.Instance.Transform.position) <= 2.5f)
            {
                PlayerStatus.Instance.Fail();
                inpt.suspicion = 0;
            }

            return new AIOutput(input, PlayerStatus.Instance.Transform.position);
        }
    }
}
