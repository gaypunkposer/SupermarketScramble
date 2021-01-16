using GameRules;
using UnityEngine;

namespace Movement
{
    public class BendToLookDown : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            var rotation = PlayerStatus.Instance.cameraTransform.localEulerAngles.x;
            rotation = rotation > 180 ? rotation - 360 : rotation; //Make rotation relative to -180, 180
            rotation *= 0.85f;
            _animator.SetBoneLocalRotation(HumanBodyBones.Chest, Quaternion.Euler(new Vector3(rotation, 0, 0)));
        }
    }
}