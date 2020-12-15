using UnityEngine;

namespace GameRules
{
    public class GameState : MonoBehaviour
    {
        public static GameState Instance { get; private set; }

        public int Rent { get; private set; } = 25; //Eventually, have it so that rent increases with each access
        public ItemManager Items { get; private set; }
        public bool Running { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
                DestroyImmediate(gameObject);
            else
                Instance = this;

            DontDestroyOnLoad(gameObject);

            Items = GetComponentInChildren<ItemManager>();
        }

        public void Pause()
        {
            Running = false;
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Running = true;
            Time.timeScale = 1;
        }

        public void ResetLevel()
        {
            Items.RespawnAllItems();
            //reset the ai helpers
        }
    }
}
