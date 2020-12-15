using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interactions
{
    public class PlayerInventory : MonoBehaviour
    {
        public float StoredItemWeight { get; private set; }

        public float maxStorage = 50;

        private List<Item> _items;

        private void Start()
        {
            _items = new List<Item>();
        }

        public bool StoreItem(Item i)
        {
            if (StoredItemWeight + i.stats.weight > maxStorage) return false;

            ForceStoreItem(i);
            return true;
        }

        public void ForceStoreItem(Item i)
        {
            StoredItemWeight += i.stats.weight;
            _items.Add(i);
            i.gameObject.SetActive(false);
        }

        public Item RetrieveItem(int num)
        {
            if (num >= _items.Count || num < 0)
                throw new ArgumentException("Num must be within bounds of list");
            

            Item i = _items[num];
            StoredItemWeight -= i.stats.weight;
            _items.RemoveAt(num);
            i.gameObject.SetActive(true);
            return i;
        }

        public void RemoveItem(Item i)
        {
            RetrieveItem(_items.IndexOf(i));
        }
        
        public List<Item> GetItems()
        {
            List<Item> list = new List<Item>(_items);
            return list;
        }

        public void ClearInventory()
        {
            _items.Clear();
        }
    }
}
