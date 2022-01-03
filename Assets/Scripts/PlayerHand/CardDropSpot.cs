using System.Collections.Generic;
using CardMetaData;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PlayerHand
{
    public class CardDropSpot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public int slots;
        public GameObject cardSlot;
        private List<GameObject> _cards = new List<GameObject>();
        public List<CardData.CardType> acceptedCardTypes = new List<CardData.CardType>();
        public bool lockCard;
        private GridLayoutGroup _gridLayoutGroup;
        public delegate void CardChange(BaseCardObject card);
        public event CardChange OnCardChange = (BaseCardObject card) => {};

        public void Start()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }
    
        public void UpdateLayout()
        {
            _gridLayoutGroup.CalculateLayoutInputHorizontal();
            _gridLayoutGroup.CalculateLayoutInputVertical();
            _gridLayoutGroup.SetLayoutHorizontal();
            _gridLayoutGroup.SetLayoutVertical();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CardManager.Instance.cardDropSpot = this;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CardManager.Instance.cardDropSpot = null;
        }

        public bool CheckIfFull()
        {
            return _cards.Count < slots;
        }

        public bool CheckIfCanPlay(BaseCardObject card)
        {
            return acceptedCardTypes.Contains(card.GetCardData().cardType);
        }

        public bool AddCard(BaseCardObject card)
        {
            if (_cards.Count >= slots) return false;
            if (!acceptedCardTypes.Contains(card.GetCardData().cardType)) return false;
            var cardObj = Instantiate(this.cardSlot, Vector3.zero, Quaternion.identity, gameObject.transform);
            _cards.Add(cardObj);
            cardObj.GetComponent<CardPopulate>().baseCardObject = card;
            cardObj.GetComponent<PlayableCard>().locked = lockCard;
            OnCardChange(card);
            return true;
        }

        public BaseCardObject SwapCard(BaseCardObject card)
        {
            if (!acceptedCardTypes.Contains(card.GetCardData().cardType)) return card;
            var cardObj = Instantiate(this.cardSlot, Vector3.zero, Quaternion.identity, gameObject.transform);
            _cards.Add(cardObj);
            cardObj.GetComponent<CardPopulate>().baseCardObject = card;
            cardObj.GetComponent<PlayableCard>().locked = lockCard;
            BaseCardObject returnCard = _cards[0].GetComponent<CardPopulate>().baseCardObject;
            GameObject deleteCard = _cards[0];
            _cards.RemoveAt(0);
            Destroy(deleteCard);
            OnCardChange(card);
            return returnCard;
        }


        public void RemoveCard(GameObject cardObj)
        {
            _cards.Remove(cardObj);
        }

        public void ClearSlots()
        {
            foreach (var card in _cards)
            {
                Destroy(card);
            }
            _cards.Clear();
        }
    }
}
