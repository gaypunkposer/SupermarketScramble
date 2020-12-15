using GameRules;
using UnityEngine;

namespace Interactions
{
    public class EndDayInteractable : MonoBehaviour, IInteractable
    {
        
        public string GetTooltip()
        {
            return "Leave for the day";
        }

        public void StartLookAt()
        {
            //do something?
        }

        public void EndLookAt()
        {
            //do something?
        }

        public void Interact()
        {
            PlayerState.Instance.Leave();
        }
    }
}
