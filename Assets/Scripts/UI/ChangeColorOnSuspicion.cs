using GameRules;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ChangeColorOnSuspicion : MonoBehaviour
    {
        public float suspicionCap;
        public Color leastSuspicion;
        public Color maxSuspicion;
        
        private Image _img;

        private void Start()
        {
            _img = GetComponent<Image>();
        }
        
        private void Update()
        {
            Color c = Color.Lerp(leastSuspicion, maxSuspicion, PlayerStatus.Instance.Suspicion / suspicionCap);
            _img.color = c;
        }
    }
}
