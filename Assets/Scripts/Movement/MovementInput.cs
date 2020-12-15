using StateMachine;
using UnityEngine;

namespace Movement
{
    /// <summary>
    ///     A container class for the player's current state.
    /// </summary>
    public class MovementInput : SMInput
    {
        public Vector3 lateralVelocity; //The player's velocity along the xz plane
        
        public readonly Vector3 directionalInput; //The input from the player - WASD or Left Stick
        
        public readonly bool sprint;
        public readonly bool crouch; 
        public readonly bool interact;
        public readonly bool store;
        public readonly bool inventoryToggle;

        public MovementInput(State start) : base(start)
        {
        }

        public MovementInput(SMInput prev) : base(prev)
        {
            directionalInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            crouch = Input.GetButtonDown("Crouch");
            sprint = Input.GetButton("Sprint");
            interact = Input.GetButtonDown("Interact");
            store = Input.GetButtonDown("Store");
            inventoryToggle = Input.GetButtonDown("View Inventory");
            lateralVelocity = ((MovementInput) prev).lateralVelocity;
        }
    }
}