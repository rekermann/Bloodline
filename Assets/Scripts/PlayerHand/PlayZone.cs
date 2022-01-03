using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerHand
{
    public class PlayZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            CardManager.Instance.SetInPlayZone();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CardManager.Instance.SetOutsidePlayZone();
        }
    }
}
