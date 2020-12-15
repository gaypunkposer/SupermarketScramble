using System.Collections;
using GameRules.AI.SM.States;
using StateMachine;
using UnityEngine;

namespace GameRules.AI.SM
{
    public class FaceConsumer : MonoBehaviour, ISMOutputConsumer
    {
        public MeshRenderer faceRenderer;

        private Coroutine _transitionFace;
        private static readonly int FaceParam = Shader.PropertyToID("Vector2_6FF8B282");

        public int GetPriority()
        {
            return -1;
        }

        public void ReceiveOutput(SMOutput output)
        {
            if (output.input.state == output.input.previous.state) return;
            
            Vector4 target = Vector4.zero;
            switch (output.input.state)
            {
                case Neutral _:
                    //zero
                    break;
                case Suspicious _:
                    target.x = 0.5f;
                    break;
                case Alerted _:
                    target.y = -0.5f;
                    break;
                case Disabled _:
                    target.x = 0.5f;
                    target.y = -0.5f;
                    break;
            }

            if (_transitionFace != null) StopCoroutine(_transitionFace);
            _transitionFace = StartCoroutine(TransitionFace(target));
        }

        private IEnumerator TransitionFace(Vector4 target)
        {
            float i = 0;
            Vector4 orig = faceRenderer.material.GetVector(FaceParam);
            while (i < 1)
            {
                faceRenderer.material.SetVector(FaceParam, Vector4.Lerp(orig, target, i));
                i += Time.deltaTime;
                yield return null;
            }
        }
    }
}
