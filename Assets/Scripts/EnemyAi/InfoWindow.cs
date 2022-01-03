using System.Collections;
using System.Collections.Generic;
using CardMetaData;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace EnemyAi
{
    public class InfoWindow : MonoBehaviour, IAltHoverable
    {
        private bool _hovered;
        public delegate void AltInfoWindow();
        public event AltInfoWindow OnAltHover = () => {};
        private InfoWindowData _infoWindowData;


        public void Update()
        {
            if (_hovered)
            {
                UiManager.Instance.EnableInformationWindow(_infoWindowData);
                _hovered = false;
            }
        }
        public void AltHovered()
        {
            OnAltHover();
            if(_hovered) return;
            _hovered = true;
        }

        public void AltUnHovered()
        {
            _hovered = false;
            UiManager.Instance.DisableInformationWindow();
        }
    
        public void SetInfoWindowData(string name, int health, int maxHealth, BaseCardObject card)
        {
            _infoWindowData = new InfoWindowData(name, health, maxHealth, card);
        }
    
    }

    public class InfoWindowData
    {
        public string name;
        public int health;
        public int maxHealth;
        public BaseCardObject card;
    
        public InfoWindowData(string name, int health, int maxHealth, BaseCardObject card)
        {
            this.name = name;
            this.health = health;
            this.maxHealth = maxHealth;
            this.card = card;
        }
    }
}