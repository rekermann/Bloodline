using System;
using System.Collections.Generic;
using CardMetaData;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PlayerHand
{
    public class HandUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [NonSerialized] private GridLayoutGroup _gridLayoutGroup;
        private const int NotHovered = 0;
        public GameObject cardSlot;
        private List<GameObject> cardsRefInHand = new List<GameObject>();

        public GameObject zoomedCard;

        public void Start()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }

        public void Update()
        {
            if (zoomedCard.activeSelf)
            {
                zoomedCard.transform.position = Input.mousePosition + new Vector3(0, 300);
            }
        }

        public void AddCard(BaseCardObject card)
        {
            var cardObj = Instantiate(this.cardSlot, Vector3.zero, Quaternion.identity, gameObject.transform);
            cardObj.GetComponent<CardPopulate>().baseCardObject = card;
            cardsRefInHand.Add(cardObj);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _gridLayoutGroup.padding.top = -200;
            UpdateLayout();

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _gridLayoutGroup.padding.top = NotHovered;
            UpdateLayout();
        }


        public void UpdateLayout()
        {
            _gridLayoutGroup.CalculateLayoutInputHorizontal();
            _gridLayoutGroup.CalculateLayoutInputVertical();
            _gridLayoutGroup.SetLayoutHorizontal();
            _gridLayoutGroup.SetLayoutVertical();
        }

        public void SetZoomedCard(BaseCardObject card)
        {
            zoomedCard.SetActive(true);
            zoomedCard.GetComponent<CardPopulate>().SetupZoomedCard(card);
        }

        public void UnsetZoomedCard()
        {
            zoomedCard.SetActive(false);
        }

        public void RemoveCard(GameObject obj)
        {
            cardsRefInHand.Remove(obj);
            Destroy(obj);
        }

        public List<GameObject> GetCardsInHand()
        {
            return cardsRefInHand;
        }
    }
}
