using GameRules;
using UnityEngine;
using UnityEngine.Serialization;

namespace Movement
{
    public class CollisionHandler : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }
        
        public CapsuleCollider capCollider;
        public CharacterController controller;
        
        [HideInInspector] public float height;
        [HideInInspector] public float radius;
        [HideInInspector] public Vector3 center;

        private Vector3 _moveFixed;
        private Vector3 _moveUpdate;
        
        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            capCollider.height = height;
            controller.height = height;

            capCollider.radius =
                radius + 0.25f; //Make the CapsuleCollider radius larger so the physics collider has priority over the CharacterController.
            controller.radius = radius;

            capCollider.center = center;
            controller.center = center;
        }
    }
}