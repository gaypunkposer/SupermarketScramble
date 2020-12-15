using System;
using GameRules;
using UnityEngine;

namespace Interactions
{
    [CreateAssetMenu(fileName = "ItemDB", menuName = "ScriptableObjects/ItemDatabase", order = 1)]
    public class ItemDatabase : ScriptableObject
    {
        public GameObject[] items;
        public ItemStats[] stats;
        [Tooltip("0 is lowest, n is highest")] public ItemRarity[] rarities;
    }

    [Serializable]
    public struct ItemRarity
    {
        public Color color;
        public float valueMultiplier;
    }
}
