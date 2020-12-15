using UnityEngine;

namespace UI
{
    [ExecuteInEditMode]
    public class UIManager : MonoBehaviour
    {
        public Alerts Alerts { get; private set; }
        public InventoryGUI Inventory { get; private set; }
        public Tooltip Tooltip { get; private set; }
        public FadeToBlack Fade { get; private set; }
        public EndScreen EndScreen { get; private set; }

        private void Awake()
        {
            Alerts = GetComponentInChildren<Alerts>();
            Inventory = GetComponentInChildren<InventoryGUI>();
            Tooltip = GetComponentInChildren<Tooltip>();
            Fade = GetComponentInChildren<FadeToBlack>();
            EndScreen = GetComponentInChildren<EndScreen>();
        }
    }
}
