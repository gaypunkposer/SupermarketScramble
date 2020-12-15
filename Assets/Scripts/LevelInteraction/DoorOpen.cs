using UnityEngine;

namespace LevelInteraction
{
    [RequireComponent(typeof(Collider))]
    public class DoorOpen : MonoBehaviour
    {
        public Transform leftDoor;
        public Transform rightDoor;
        public float openSpeed;
        public float maxOpenState;
        public Vector3 openDir = Vector3.right;

        private readonly Vector3[] _origPos = new Vector3[2];
        private readonly Vector3[] _targetPos = new Vector3[2];
        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
            _origPos[0] = leftDoor.position;
            _origPos[1] = rightDoor.position;
            _targetPos[0] = _origPos[0];
            _targetPos[1] = _origPos[1];
        }
        
        public void OpenDoor(Collider other)
        {
            _targetPos[0] = _origPos[0] - openDir * maxOpenState;
            _targetPos[1] = _origPos[1] + openDir * maxOpenState;
        }

        public void CloseDoor(Collider other)
        {
            _targetPos[0] = _origPos[0];
            _targetPos[1] = _origPos[1];
        }

        private void Update()
        {
            //Exponential growth / decay for closing / opening
            leftDoor.position = Vector3.Lerp(leftDoor.position, _targetPos[0], openSpeed * Time.deltaTime);
            rightDoor.position = Vector3.Lerp(rightDoor.position, _targetPos[1], openSpeed * Time.deltaTime);
        }
    }
}
