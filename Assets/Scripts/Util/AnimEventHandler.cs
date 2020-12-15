using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class AnimEventHandler : MonoBehaviour
    {
        public delegate void AnimDelegate();

        private static readonly Dictionary<string, AnimDelegate> _dict = new Dictionary<string, AnimDelegate>();

        public static void RegisterHandler(string name, AnimDelegate del)
        {
            if (_dict.ContainsKey(name))
                Debug.LogError($"Animation event handler has already been registered for '{name}'.");
            else
                _dict.Add(name, del);
        }

        public static void DeRegisterHandler(string name)
        {
            if (!_dict.ContainsKey(name))
                Debug.LogError($"Animation event '{name}' has no handler.");
            else
                _dict.Remove(name);
        }

        public static void UpdateHandler(string name, AnimDelegate newDel)
        {
            if (!_dict.ContainsKey(name))
                Debug.LogError($"Animation event '{name}' has not yet been registered.");
            else
                _dict[name] = newDel;
        }

        //Requires that the function name be 'AnimEvent' when creating animation events, and the string be the registered name for the callback
        public void AnimEvent(string target)
        {
            if (_dict.ContainsKey(target))
                _dict[target]();
            else
                Debug.LogError($"Animation event '{target}' was called but no handler was registered.");
        }
    }
}