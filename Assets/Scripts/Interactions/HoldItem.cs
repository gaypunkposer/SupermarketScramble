using GameRules;
using Movement;
using Movement.States;
using StateMachine;
using UnityEngine;
using Util;

namespace Interactions
{
    public class HoldItem : MoveAbility
    {
        public Item Holding => _heldItem;
        public float throwForce;
        public Transform grabAttachPoint;
        
        private Item _heldItem;
        private Item _itemLookedAt;
        private Transform _cam;

        private static readonly int Store = Animator.StringToHash("Store");
        private static readonly int Pickup = Animator.StringToHash("Pickup");
        private static readonly int Throw = Animator.StringToHash("Throw");
        private static readonly int Retrieve = Animator.StringToHash("Retrieve");
        private static readonly int CancelHold = Animator.StringToHash("Cancel Hold");

        //Run after the Interact script
        public HoldItem()
        {
            Priority = int.MaxValue - 1;
        }
        
        private void Start()
        {
            //Set up animation callbacks
            AnimEventHandler.RegisterHandler("Grab", GrabItem);
            AnimEventHandler.RegisterHandler("Throw", ThrowItem);
            _cam = PlayerStatus.Instance.cameraTransform;
        }
        
        private void GrabItem()
        {
            if (!_itemLookedAt)
            {
                PlayerStatus.Instance.Viewmodel.SetTrigger(CancelHold);
                return;
            }

            if (_heldItem != null) return;
            
            _itemLookedAt.gameObject.SetActive(true);
            _itemLookedAt.Rigidbody.velocity = Vector3.zero;
            _itemLookedAt.Rigidbody.isKinematic = true;
            _itemLookedAt.SetParent(grabAttachPoint);
            _heldItem = _itemLookedAt;
            _itemLookedAt = null;
        }

        private void ThrowItem()
        {
            if (_heldItem == null) return;
            
            _heldItem.ClearParent();
            _heldItem.Rigidbody.isKinematic = false;
            _heldItem.Rigidbody.AddForce(_cam.forward * throwForce);
            
            _heldItem = null;
        }

        public void DropItem()
        {
            if (_heldItem == null) return;
            
            _heldItem.ClearParent();
            _heldItem.Rigidbody.isKinematic = false;
            _heldItem = null;
        }
        
        private void StoreItem()
        {
            if (_heldItem == null) return;
            if (PlayerStatus.Instance.Inventory.StoreItem(_heldItem))
            {
                _heldItem = null;
                PlayerStatus.Instance.Viewmodel.SetTrigger(Store);
            }
            else
                PlayerStatus.Instance.UI.Alerts.AddAlert("Your inventory is full!");
        }
        
        public void RetrieveItem(Item i)
        {
            _itemLookedAt = i;
            PlayerStatus.Instance.Viewmodel.SetTrigger(Retrieve);
            GrabItem();
        }

        public void SetLookedAtItem(Item i)
        {
            if (_heldItem != null) return;   
            _itemLookedAt = i;
        }

        public void InitiateGrabItem()
        {
            if (_heldItem != null) return;
            AnimatorStateInfo info = PlayerStatus.Instance.Viewmodel.GetCurrentAnimatorStateInfo(1);
            if (!info.IsName("Default")) return;
            
            PlayerStatus.Instance.Viewmodel.SetTrigger(Pickup);
        }
        
        public override bool ShouldRunThisState(SMInput input)
        {
            MovementInput inpt = (MovementInput) input;
            if (inpt.interact && _heldItem != null)
                PlayerStatus.Instance.Viewmodel.SetTrigger(Throw);
            else if (inpt.store && _heldItem != null)
            {
                StoreItem(); // Remove when there's an animation for store        
            }

            return false;
        }
    }
}
