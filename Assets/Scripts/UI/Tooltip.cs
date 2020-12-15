using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Tooltip : MonoBehaviour
    {
        public TMP_Text text;

        private void Awake()
        {
            HideTooltip();
        }

        public void ShowTooltip(string mess)
        {
            text.text = mess;
            text.gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            text.gameObject.SetActive(false);
        }
    }
}
