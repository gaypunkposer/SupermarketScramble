using GameRules;
using Movement;
using Movement.States;
using StateMachine;
using UnityEngine;

namespace Interactions
{
    public class Interact : MoveAbility
    {
        public float interactDistance;

        private HoldItem _itemHolder;
        private IInteractable _lookedAt;

        public Interact()
        {
            Priority = int.MaxValue;
        }

        private void Start()
        {
            _itemHolder = GetComponent<HoldItem>();
        }

        public override bool ShouldRunThisState(SMInput input)
        {
            IInteractable i = null;
            if (Physics.Raycast(PlayerStatus.Instance.cameraTransform.position,
                PlayerStatus.Instance.cameraTransform.forward, out var hit, interactDistance, 1 << 10 | 1) && hit.collider.gameObject.layer == 10)
            {
                if (hit.rigidbody != null)
                    i = hit.rigidbody.gameObject.GetComponent<IInteractable>();
                else
                    i = hit.collider.gameObject.GetComponent<IInteractable>();
            }

            if (i != _lookedAt)
            {
                _lookedAt?.EndLookAt();
                i?.StartLookAt();

                if (i != null)
                    PlayerStatus.Instance.UI.Tooltip.ShowTooltip(i.GetTooltip());
                else
                    PlayerStatus.Instance.UI.Tooltip.HideTooltip();

                _lookedAt = i;

                if (i as Item)
                {
                    _itemHolder.SetLookedAtItem(i as Item);
                }
                else
                {
                    _itemHolder.SetLookedAtItem(null);
                }
            }

            if (((MovementInput)input).interact)
            {
                _lookedAt?.Interact();
            }

            return false;
        }
    }
}