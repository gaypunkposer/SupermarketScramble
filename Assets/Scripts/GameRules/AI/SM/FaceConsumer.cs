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
        private static readonly int FaceParam = Shader.PropertyToID("_MainTex");

        public int GetPriority()
        {
            return -1;
        }

        public void ReceiveOutput(SMOutput output)
        {
            if (output.input.state == output.input.previous.state) return;
            
            Vector2 target = Vector2.zero;
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

        private IEnumerator TransitionFace(Vector2 target)
        {
            float i = 0;
            Vector2 orig = faceRenderer.material.GetTextureOffset(FaceParam);
            while (i <= 1)
            {
                faceRenderer.material.SetTextureOffset(FaceParam, Vector2.Lerp(orig, target, i));
                i += Time.deltaTime;
                yield return null;
            }
        }
    }
}
