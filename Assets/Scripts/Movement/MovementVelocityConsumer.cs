﻿using GameRules;
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
            ((MovementInput) output.input).lateralVelocity = new Vector3(_velocity.x, 0, _velocity.z);
            ((MovementOutput) output).velocity = _velocity;
            
            PlayerStatus.Instance.CollisionHandler.controller.Move(_velocity * Time.deltaTime);
            PlayerStatus.Instance.CollisionHandler.Rigidbody.velocity = _velocity;
        }
    }
}
