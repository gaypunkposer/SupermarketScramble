using GameRules;
using Movement;
using Movement.States;
using StateMachine;

namespace UI
{
    //I try to consolidate everything that requires player input into the player's state machine, so this becomes a MoveAbility
    public class ToggleInventoryScreen : MoveAbility
    {
        public ToggleInventoryScreen()
        {
            Priority = int.MaxValue;
        }
    
        public override bool ShouldRunThisState(SMInput input)
        {
            if (!((MovementInput)input).inventoryToggle) return false;
        
            PlayerState.Instance.UI.Inventory.Toggle(!PlayerState.Instance.UI.Inventory.Showing);
            return false;
        }
    }
}
