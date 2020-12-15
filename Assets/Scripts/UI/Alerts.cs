using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Alerts : MonoBehaviour
    {
        public GameObject alertPrefab;
        public float alertLifeTime;
        public float alertFadeTime;

        public void AddAlert(string message)
        {
            TMP_Text text = Instantiate(alertPrefab, transform).GetComponent<TMP_Text>();
            text.text = message;
            StartCoroutine(FadeAlert(text));
        }

        private IEnumerator FadeAlert(TMP_Text text)
        {
            yield return new WaitForSeconds(alertLifeTime);
            float life = alertFadeTime;
            while (life >= 0)
            {
                Color c = text.color;
                c.a = life / alertFadeTime;
                text.color = c;
                life -= Time.deltaTime;
                yield return null;
            }
            Destroy(text.gameObject);
        }
    }
}
