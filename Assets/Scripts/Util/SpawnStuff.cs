using NaughtyAttributes;
using UnityEngine;

namespace Util
{
    [ExecuteInEditMode]
    public class SpawnStuff : MonoBehaviour
    {
        public GameObject toSpawn;
        public int amount;
        public Vector3 spawnAxis;
        
        [Button]
        public void SpawnItems()
        {
            transform.DestroyAllChildren();
            for (int i = -amount/2; i <= amount/2; i++)
            {
                GameObject go = Instantiate(toSpawn, transform);
                go.transform.localPosition = spawnAxis * i;
                go.SetActive(true);
            }
        }
    }
}
