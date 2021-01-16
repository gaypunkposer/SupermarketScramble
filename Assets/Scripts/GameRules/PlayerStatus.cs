using System.Linq;
using Cinemachine;
using Interactions;
using Movement;
using Movement.States;
using StateMachine;
using UI;
using UnityEngine;

namespace GameRules
{
    public class PlayerStatus : MonoBehaviour
    {
        public static PlayerStatus Instance { get; private set; }
        
        public SMRunner MovementSM { get; private set; }
        public CollisionHandler CollisionHandler { get; private set; }
        public Animator Viewmodel { get; private set; }
        public PlayerInventory Inventory { get; private set; }
        public UIManager UI { get; private set; }
        public HoldItem Hand { get; private set; }

        public bool FreezeMouse { get; set; }
        
        public Transform Transform => CollisionHandler.controller.transform;
        [HideInInspector] public Transform cameraTransform;
        public float Suspicion => PlayerSuspicion();
        public float Balance { get; private set; } = 0f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                DestroyImmediate(gameObject);
            else
                Instance = this;

            DontDestroyOnLoad(gameObject);

            MovementSM = GetComponentInChildren<SMRunner>();

            CollisionHandler = GetComponentInChildren<CollisionHandler>();
            Viewmodel = GetComponentInChildren<Animator>();
            Inventory = GetComponentInChildren<PlayerInventory>();
            UI = GetComponentInChildren<UIManager>();
            Hand = GetComponentInChildren<HoldItem>();

            cameraTransform = GetComponentInChildren<CinemachineVirtualCamera>().transform;
        }
        
        //Hardcoding this for now
        private float PlayerSuspicion()
        {
            float level = 0;

            //If the player is moving weirdly, that's suspicious
            switch (((MovementInput)MovementSM.LastInput).state)
            {
                case Crouch _:
                    level += 0.25f;
                    break;
                case CrouchRun _:
                    level += 0.5f;
                    break;
                case Sprint _:
                    level += 0.75f;
                    break;
            }

            //If the player is performing some bad action, that's suspicious
            AnimatorStateInfo info = Viewmodel.GetCurrentAnimatorStateInfo(1);
            if (info.IsName("Store") || info.IsName("Throw"))
            {
                level += 5f;
            }

            //If the player is in some sort of non-pausing menu, that's suspicious
            if (FreezeMouse)
            {
                level += 0.25f;
            }
            
            return level;
        }

        public void Fail()
        {
            Inventory.ClearInventory();
            UI.Alerts.AddAlert("You have been caught. They are taking all the items you stole.");
            Leave();
        }

        public void Leave()
        {
            MovementSM.enabled = false;
            FreezeMouse = true;
            
            HoldItem hold = MovementSM.stateContainer.GetComponent<HoldItem>();
            if (hold.Holding != null)
            {
                Item item = hold.Holding;
                hold.DropItem();
                Inventory.ForceStoreItem(item);
            }
            
            UI.Fade.StartFade(CompleteDay);
        }
        
        private void CompleteDay()
        {
            MovementSM.transform.localPosition = Vector3.up;
            MovementSM.transform.localRotation = Quaternion.identity;
            int rent = GameStatus.Instance.Rent;
            float sum = Inventory.GetItems().Sum(i =>
                i.stats.value * GameStatus.Instance.Items.db.rarities[i.Rarity].valueMultiplier);
            Balance += sum - rent;
            UI.EndScreen.Show(rent, sum);
            Inventory.ClearInventory();
            GameStatus.Instance.ResetLevel();
        }

        public void StartNextDay()
        {
            UI.EndScreen.Hide();
            UI.Fade.SetAlpha(0);
            MovementSM.enabled = true;
            FreezeMouse = false;
        }
    }
}