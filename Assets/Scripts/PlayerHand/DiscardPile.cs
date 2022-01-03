using System.Collections.Generic;
using CardMetaData;
using UnityEngine;

namespace PlayerHand
{
    public class DiscardPile : MonoBehaviour
    {
        private List<BaseCardObject> _discardList = new List<BaseCardObject>();
        private bool _toggle;
        public DiscardUi DiscardUi;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HideDiscard();
            }
        }

        public void AddCardToDiscard(BaseCardObject card)
        {
            if(card != null) _discardList.Add(card);
        }
    
        public void RemoveCardFromDiscard(BaseCardObject card)
        {
            _discardList.Remove(card);
        }

        public void ShowDiscardPile()
        {
            _toggle = true;
            DiscardUi.gameObject.SetActive(_toggle);
        }

        public void ToggleDiscardUi()
        {
            _toggle = !_toggle;
            DiscardUi.gameObject.SetActive(_toggle);
        
        }

        public void HideDiscard()
        {
            _toggle = false;
            DiscardUi.gameObject.SetActive(_toggle);
        }
    }
}
