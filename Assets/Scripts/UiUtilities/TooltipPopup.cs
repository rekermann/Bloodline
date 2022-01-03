using System;
using System.Text;
using Equipment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UiUtilities
{
    public class TooltipPopup : MonoBehaviour
    {
        [SerializeField] private GameObject popupCanvasObject;
        [SerializeField] private RectTransform popupObject;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float padding;

        private Canvas popupCanvas;

        private void Awake()
        {
            popupCanvas = popupCanvasObject.GetComponent<Canvas>();
        }

        private void Update()
        {
            FollowCursor();
        }

        private void FollowCursor()
        {
            if(!popupCanvasObject.activeSelf) {return;}

            Vector3 newPos = Input.mousePosition + offset;
            newPos.z = 0f;
            float rightEdgeToScreenEdgeDistance =
                Screen.width - (newPos.x + popupObject.rect.width * popupCanvas.scaleFactor / 2) - padding;
            if (rightEdgeToScreenEdgeDistance < 0)
            {
                newPos.x += rightEdgeToScreenEdgeDistance;
            }

            float leftEdgeToScreenEdgeDistance =
                0 - (newPos.x - popupObject.rect.width * popupCanvas.scaleFactor / 2) + padding;
            if (leftEdgeToScreenEdgeDistance > 0)
            {
                newPos.x += rightEdgeToScreenEdgeDistance;
            }

            float topEdgeToScreenEdgeDistance =
                Screen.height - (newPos.y + popupObject.rect.height * popupCanvas.scaleFactor) - padding;
            if (topEdgeToScreenEdgeDistance < 0)
            {
                newPos.y += topEdgeToScreenEdgeDistance;
            }

            popupObject.transform.position = newPos;
        }

        public void DisplayInfo(EquipmentData item)
        {
            if (item == null) return;
            StringBuilder builder = new StringBuilder();
            builder.Append(item.GetTooltipInfoText());
            infoText.text = builder.ToString();
            gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
        }

        public void HideInfo()
        {
            popupCanvasObject.SetActive(false);
        }
    }
}
