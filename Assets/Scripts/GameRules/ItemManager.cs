using System;
using System.Collections.Generic;
using System.Linq;
using Interactions;
using LevelInteraction;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameRules
{
    public class ItemManager : MonoBehaviour
    {
        public ItemDatabase db;
        public ItemStats[] overrides;

        private Dictionary<string, ItemStats> _statsDict;
        private Dictionary<string, GameObject> _itemsDict;

        private void Awake()
        {
            LoadStats();
            _itemsDict = new Dictionary<string, GameObject>();
            
            foreach (var g in db.items)
            {
                Item i = g.GetComponent<Item>();
                _itemsDict.Add(i.id, g);
            }
        }

        public void LoadStats()
        {
            _statsDict = new Dictionary<string, ItemStats>();
            LoadStatsIntoDict(db.stats);
            LoadStatsIntoDict(overrides);
        }

        public ItemStats GetItemStats(string itemName)
        {
            return _statsDict[itemName];
        }
        
        //Returns an item of id with rarity. Will give a random item or rarity if id is empty or rarity is not in range
        public GameObject GetItemWithRarity(string id, int rarity)
        {
            //Not a fan of this line of code, but it'll do for now
            GameObject go = id.Length > 0 ? Instantiate(_itemsDict[id]) : Instantiate(_itemsDict[_itemsDict.Keys.ToArray()[Random.Range(0, _itemsDict.Count)]]);
            go.GetComponent<Item>().Rarity = rarity >= 0 ? rarity : Random.Range(0, db.rarities.Length);
            return go;
        }

        private void LoadStatsIntoDict(IEnumerable<ItemStats> arr)
        {
            foreach (var t in arr)
            {
                ItemStats ist = t;
                if (ist.id.Length == 0)
                {
                    Debug.LogError("Item provided with no id.");
                    continue;
                }

                if (ist.friendlyName.Length == 0)
                {
                    Debug.LogWarning($"Item provided with id '{ist.id}', but no friendly name.");
                    ist.friendlyName = ist.id;
                }

                if (_statsDict.ContainsKey(ist.id))
                    _statsDict[ist.id] = ist;
                else
                    _statsDict.Add(ist.id, ist);
            }
        }

        public void RespawnAllItems()
        {
            ItemSpawner[] spawners = FindObjectsOfType<ItemSpawner>();
            foreach (ItemSpawner i in spawners)
            {
                i.SpawnNewItem();
            }
            
        }
    }

    [Serializable]
    public struct ItemStats
    {
        public string friendlyName;
        public string id;
        public float value;
        public float weight;
    }
}