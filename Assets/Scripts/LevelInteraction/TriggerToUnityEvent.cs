using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace LevelInteraction
{
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider>
    {
    }
    
    [RequireComponent(typeof(Collider))]
    public class TriggerToUnityEvent : MonoBehaviour
    {
        public bool shouldInvokeEvents = true;
        public TriggerEvent enter;
        public bool invokeOncePerCollider;
        public TriggerEvent stay;
        public TriggerEvent leave;

        private List<Collider> _inTrigger = new List<Collider>();
        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (shouldInvokeEvents) enter.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            if (_inTrigger.Contains(other) && invokeOncePerCollider) return;
            if (!_inTrigger.Contains(other)) _inTrigger.Add(other);
            if (shouldInvokeEvents) stay.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_inTrigger.Contains(other)) _inTrigger.Remove(other);
            if (shouldInvokeEvents) leave.Invoke(other);
        }
    }
}
