using System;
using CardMetaData;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerHand
{
    public class DiscardUi : MonoBehaviour
    {
        [NonSerialized] private GridLayoutGroup _gridLayoutGroup;
        public GameObject cardSlot;

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

        public GameObject AddCard(BaseCardObject card)
        {
            var cardObj = Instantiate(this.cardSlot, Vector3.zero, Quaternion.identity, gameObject.transform);
            cardObj.GetComponent<CardPopulate>().baseCardObject = card;
            cardObj.GetComponent<PlayableCard>().locked = true;
            return cardObj;
        }
    
        public void SetZoomedCard(BaseCardObject card)
        {
            zoomedCard.SetActive(true);
            zoomedCard.GetComponent<CardPopulate>().SetupCard(card);
        }

        public void UnsetZoomedCard()
        {
            zoomedCard.SetActive(false);
        }
    }
}
