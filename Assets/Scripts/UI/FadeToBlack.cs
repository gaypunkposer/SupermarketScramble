using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FadeToBlack : MonoBehaviour
    {
        public delegate void FadeCallback();

        public RawImage fadeObject;
        public float fadeTime;

        public void StartFade(FadeCallback callback)
        {
            StartCoroutine(Fade(callback));
        }

        public void SetAlpha(float a)
        {
            Color c = Color.black;
            c.a = a;
            fadeObject.color = c;
        }
        
        private IEnumerator Fade(FadeCallback callback)
        {
            float timer = 0;
            while (timer <= fadeTime)
            {
                Color c = fadeObject.color;
                c.a = timer / fadeTime;
                fadeObject.color = c;
                
                timer += Time.deltaTime;
                yield return null;
            }

            callback();
        }
    }
}
