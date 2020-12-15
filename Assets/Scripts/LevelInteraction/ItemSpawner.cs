using GameRules;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace LevelInteraction
{
    public class ItemSpawner : MonoBehaviour
    {
        public string overrideID;
        public int minRarity = -1;
        public int maxRarity = -1;

        private void Start()
        {
            SpawnNewItem();
        }

        public void SpawnNewItem()
        {
            transform.DestroyAllChildren();

            int num = Random.Range(minRarity, maxRarity);
            GameObject go = GameState.Instance.Items.GetItemWithRarity(overrideID, num);

            go.transform.parent = transform;
            go.transform.localRotation = Quaternion.identity;

            Physics.Raycast(transform.position, Vector3.down, out var hit, Mathf.Infinity, 1);
            go.transform.position = hit.point;
        }
    }
}