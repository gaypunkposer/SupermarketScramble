using GameRules;
using UnityEngine;

namespace Movement
{
    public class MouseLook : MonoBehaviour
    {
        public Transform parent;

        private void Update()
        {
            if (!PlayerStatus.Instance.FreezeMouse)
            {
                var mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

                var yangle = transform.localEulerAngles;
                yangle.x -= mouseInput.y;
                yangle.x = yangle.x > 180 ? yangle.x - 360 : yangle.x;
                yangle.x = Mathf.Clamp(yangle.x, -89, 89); //Clamp between -89 and 89 to avoid gimble lock


                var xangle = parent.eulerAngles;
                xangle.y += mouseInput.x;
                xangle.x = 0f;

                transform.localRotation = Quaternion.Euler(yangle);
                parent.rotation = Quaternion.Euler(xangle);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}