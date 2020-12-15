using UnityEngine;

namespace Util
{
    public static class UtilityExtensions
    {
        public static void DestroyAllChildren(this Transform t)
        {
            GameObject[] go = new GameObject[t.childCount];
            for (var i = 0; i < t.childCount; i++)
            {
                go[i] = t.GetChild(i).gameObject; 
            }

            foreach (var g in go)
            {
                Object.DestroyImmediate(g);
            }
        }
    }
}
