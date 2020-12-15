using System;
using System.Collections.Generic;
using System.Linq;
using GameRules;
using Interactions;
using Movement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UI
{
    public class InventoryGUI : MonoBehaviour
    {
        public bool Showing => _active;
        public GameObject buttonPrefab;
        public GameObject rowPrefab;
        public Transform content;
        public GameObject background;
        public TMP_Text statText;
        public int rowMax;
        
        private bool _active;
        private Transform _currentRow;

        public void Toggle(bool newState)
        {
            if (newState)
                ToggleOn();
            else
                ToggleOff();
        }
        
        private void Start()
        {
            ToggleOff();
        }

        private void ToggleOff()
        {
            PlayerState.Instance.FreezeMouse = false;
            background.SetActive(false);
            _active = false;
        }
        
        private void ToggleOn()
        {
            PlayerState.Instance.FreezeMouse = true;
            content.DestroyAllChildren();
            
            List<Item> items = PlayerState.Instance.Inventory.GetItems();

            _currentRow = Instantiate(rowPrefab, content).transform;
            
            int count = 0;
            foreach (Item i in items)
            {
                if (count >= rowMax)
                {
                    _currentRow = Instantiate(rowPrefab, content).transform;
                    count = 0;
                }
                GameObject go = Instantiate(buttonPrefab, _currentRow);
                go.name = i.id;
                TMP_Text t = go.GetComponentInChildren<TMP_Text>();
                t.text = $"{i.stats.friendlyName}\n" + /*${i.stats.value * GameState.Instance.Items.db.rarities[i.Rarity].valueMultiplier}\n" + */
                         $"{i.stats.weight}kg";
                go.GetComponent<Button>().onClick.AddListener(() => SelectItem(i));
                count++;
            }

            statText.text =
                $"Total Weight: {PlayerState.Instance.Inventory.StoredItemWeight}/{PlayerState.Instance.Inventory.maxStorage}\n";/*Total Value: {items.Sum(i => i.stats.value)}";*/
            
            background.SetActive(true);
            _active = true;
        }

        private void SelectItem(Item val)
        {
            PlayerState.Instance.Inventory.RemoveItem(val);
            PlayerState.Instance.Hand.RetrieveItem(val);
            
            ToggleOff();
        }
    }
}
