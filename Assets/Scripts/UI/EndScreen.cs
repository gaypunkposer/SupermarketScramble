using GameRules;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EndScreen : MonoBehaviour
    {
        public GameObject background;
        public TMP_Text text;

        public void Hide()
        {
            background.SetActive(false);
        }

        public void Show(int rent, float sum)
        {
            text.text = $"Stolen item total: {sum}\n" +
                        $"Rent payment: {rent}\n" +
                        $"Current balance: {PlayerStatus.Instance.Balance}";
            background.SetActive(true);
        }
    }
}
