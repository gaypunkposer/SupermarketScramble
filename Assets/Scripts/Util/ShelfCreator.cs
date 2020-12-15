using NaughtyAttributes;
using UnityEngine;

namespace Util
{
    [ExecuteInEditMode]
    public class ShelfCreator : MonoBehaviour
    {
        public GameObject shelfEnd;
        public GameObject shelfMid;
        public float midShelfDistance;
        public float endShelfDistance;
        public Vector3 direction;
        
        [ValidateInput("GreaterThanZero", "Must be greater than or equal to zero")]
        public int length;

        [Button]
        private void Generate()
        {
            transform.DestroyAllChildren();
            
            Transform st = Instantiate(shelfEnd, transform).transform;
            st.localPosition = Vector3.zero;
            
            for (int i = 0; i < length; i++)
            {
                Transform ch = Instantiate(shelfMid, transform).transform;
                ch.localPosition = direction * midShelfDistance * (i+1);
            }
            
            Transform end = Instantiate(shelfEnd, transform).transform;
            end.localPosition = direction * endShelfDistance * (length + 1);
            end.localEulerAngles = Vector3.forward * 180 + Vector3.left * 90;
        }

        private bool GreaterThanZero(int val)
        {
            return val >= 0;
        }
    }
}
