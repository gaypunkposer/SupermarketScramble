using UnityEngine;
using UnityEngine.Serialization;

namespace Util
{
    public class LooseChild : MonoBehaviour
    {
        public Transform parentObject;
        public bool copyPosition;
        public bool localPosition;
        public bool copyRotation;
        public bool localRotation;
        public float transformFollowMotivation;
        public float rotationFollowMotivation;
        public Vector3 positionOffset;
        public Vector3 rotationOffset; //These offsets don't work properly, will have to investigate
        
        private void LateUpdate()
        {
            if (!parentObject) return;
            if (copyPosition)
            {
                if (localPosition)
                {
                    var position = transform.localPosition;
                    position += (parentObject.localPosition - position) * transformFollowMotivation;
                    position += positionOffset;
                    transform.localPosition = position;
                }
                else
                {
                    var position = transform.position;
                    position += (parentObject.position - position) * transformFollowMotivation;
                    position += positionOffset;
                    transform.position = position;
                }
            }

            if (copyRotation)
            {
                if (localRotation)
                {
                    var rot = transform.localEulerAngles;
                    rot += (parentObject.localEulerAngles - rot) * rotationFollowMotivation;
                    rot += rotationOffset;
                    transform.localEulerAngles = rot; 
                }

                else
                {
                    var rot = transform.eulerAngles;
                    rot += (parentObject.eulerAngles - rot) * rotationFollowMotivation;
                    rot += rotationOffset;
                    transform.eulerAngles = rot;   
                }
            }
        }
    }
}