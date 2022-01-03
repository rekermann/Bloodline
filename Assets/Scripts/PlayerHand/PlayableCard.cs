using System.Collections.Generic;
using CardMetaData;
using EnemyAi;
using Tiles;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace PlayerHand
{
    public class PlayableCard : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler, IAltHoverable
    {
        private CardData _card;
        public bool locked;
        private bool enemiesInRange;
        private int copyCount = 1;
        private BaseCardObject _baseCardObject;
        private PlayerController _playerController;
        private PlayerMovementController _playerMovementController;
    
        private enum CardPhases
        {
            None,
            StartEffects,
            Targeting,
            Main,
            EndEffects,
            Done
        }

        private CardPhases _phase = CardPhases.None;

        private bool onKill;
        private int baseCombatValue;
        private int tmpCombatValue;
        private int tmpRangeValue;
        private int aoeRange;
        private MapTile target;
    

        public void Start()
        {
            _card = GetComponent<CardPopulate>().GetCardData();
            _baseCardObject = GetComponent<CardPopulate>().baseCardObject;
            _playerController = UiManager.Instance.playerRef.GetComponent<PlayerController>();
            _playerMovementController = UiManager.Instance.playerRef.GetComponent<PlayerMovementController>();
            SetCombatValue();
            GetComponent<CardPopulate>().SetupCard(baseCombatValue);
        }

        public void SetCombatValue()
        {
            baseCombatValue = _playerController.GetCardValues(_baseCardObject);
        }

        private void ContinuePhase()
        {
            _phase++;
            _playerController.playerMovementController.OnMove -= ContinuePhase;
            switch (_phase)
            {

                case CardPhases.StartEffects:
                    BeforeMainEffects();
                    break;
                case CardPhases.Targeting:
                    ResolveTargeting();
                    break;
                case CardPhases.Main:
                    ResolveMain();
                    break;
                case CardPhases.EndEffects:
                    
                    AfterMainEffects();
                    break;
                case CardPhases.Done:
                    DonePlayCard();
                    break;
                default:
                    DonePlayCard();
                    break;
            }
        }
    
    
        public void ChooseTarget(GameObject obj)
        {
            MapTile tile = obj.GetComponent<MapTile>();
            if(tile == null) return;
            if(!tile.highlighted) return;
            UiManager.Instance.mouseController.OnClickedObject -= ChooseTarget;
            target = tile;
            TileManager.Instance.UnhighlightTiles();
        
            ContinuePhase();

        }

        private void OnKillEffect()
        {
            if (_baseCardObject.onKillEffect.Length  < 1) return;
            EffectResolver(_baseCardObject.onKillEffect);
        }

        private void GainEffectResolver(int effectValue, string valueUsedAs)
        {
            if (valueUsedAs.ToLower() == "gold")
            {
                
            }

            if (valueUsedAs.ToLower() == "damage")
            {
                tmpCombatValue += effectValue;
            }

            if (valueUsedAs.ToLower() == "damagemulti")
            {
                tmpCombatValue = (tmpCombatValue  + _card.cardCombatValue) * (effectValue - 1);
            }

            if (valueUsedAs.ToLower() == "aoerange")
            {
                aoeRange += effectValue;
            }

            if (valueUsedAs.ToLower() == "range")
            {
                tmpRangeValue += effectValue;
            }
            if (valueUsedAs.ToLower() == "boost")
            {
                _playerController.GainBoost(effectValue);
            }
        
        }
    
        private void DrawEffectResolver(int effectValue, string valueUsedAs)
        {
            if (valueUsedAs.ToLower() == "cards")
            {
                _playerController.DrawCard(effectValue);
            }
        }

        private void DealEffectResolver(int effectValue, string valueUsedAs)
        {
            if (target.unitOnTile.GetComponent<Enemy>().TakeDamage(effectValue))
            {
                OnKillEffect();
            }
        }

        private void TakeEffectResolver(int effectValue, string valueUsedAs)
        {
            if (valueUsedAs.ToLower() == "damage")
            {
                _playerController.TakeDamage(effectValue);
            }
        }

        private void HealEffectResolver(int effectValue, string valueUsedAs)
        {
            if (valueUsedAs.ToLower() == "health")
            {
                _playerController.HealDamage(effectValue);
            }
        }

        private void CopyEffectResolver(int effectValue, string valueUsedAs)
        {
            if (valueUsedAs.ToLower() == "this")
            {
                copyCount += effectValue;
            }
        }
    

        public void PlayCard()
        {
        
            UiManager.Instance.DisableAction();
            ContinuePhase();
        }
    
        private void BeforeMainEffects()
        {
            CheckForTriggerCard();
            if (_baseCardObject.boostEffect.Length > 0)
            {
                BoostCard();
            }
        
            ContinuePhase();
        }

        private void CheckForTriggerCard()
        {
            foreach(var card in CardManager.Instance.handUi.GetCardsInHand())
            {
                if (card.GetComponent<CardPopulate>().GetCardData().cardType == CardData.CardType.Trigger)
                {
                    if (ResourceTriggerParser(card))
                    {
                        break;
                    }
                }
            }
        }

        private bool ResourceTriggerParser(GameObject card)
        {
            if (card.GetComponent<CardPopulate>().baseCardObject.beforeMainEffect.ToLower().Contains("next"))
            {
                string[] tmp = card.GetComponent<CardPopulate>().baseCardObject.beforeMainEffect.Split(' ');
                if (!tmp[1].ToLower().Contains(_card.cardType.ToString().ToLower()))
                {
                    return false;
                }
            
                foreach (var effect in  card.GetComponent<CardPopulate>().baseCardObject.boostEffect)
                {
                    EffectResolver(effect);
                }
            
                CardManager.Instance.SendToDiscard(_baseCardObject);
                CardManager.Instance.handUi.RemoveCard(card);
                return true;
            }

            return false;
        }

        private void BoostCard()
        {
            if (!_playerController.UseBoost(_playerController.boostPool)) return;
            foreach (var boostEffect in _baseCardObject.boostEffect)
            {
                string[] boostArr = boostEffect.Split(';');
                string[] touple = boostArr[0].Split(' ');

                if (!touple[1].Contains("x"))
                {
                    if (int.Parse(touple[1]) > _playerController.boostPool)
                    {
                        continue;
                    }
                }
                EffectResolver(boostEffect, _playerController.boostPool);
            }
        
        }

        private void EffectResolver(string effectText, int xValue = 0)
        {
            string[] boostEffects = effectText.Split(';');
            foreach (var effect in boostEffects)
            {
                if (effect.Length == 0)
                {
                    continue;
                }
                int value;
                string[] touple = effect.Split(' ');
            
                if (touple[1].Contains("x"))
                {
                    value = StringToMathParser.StringToMath(touple[1], xValue);
                }
                else
                {
                    value = int.Parse(touple[1]);
                }
            
                if (touple[0].ToLower() == "gain")
                {
                    GainEffectResolver(value, touple[2]);
                }
                if (touple[0].ToLower() == "draw")
                {
                    DrawEffectResolver(value, touple[2]);
                }

                if (touple[0].ToLower() == "heal")
                {
                    HealEffectResolver(value, touple[2]);
                }
                if (touple[0].ToLower() == "copy")
                {
                    CopyEffectResolver(value, touple[2]);
                }
            }
        
        }

        private void ResolveMain()
        {
            tmpCombatValue += _baseCardObject.GetCardData().cardCombatValue;
            tmpCombatValue *= copyCount;
        
            if (_card.cardType == CardData.CardType.Agility)
            {
                _playerController.playerMovementController.OnMove += ContinuePhase;
                _playerController.playerMovementController.HighlightMove(tmpCombatValue);
            }
            if (_card.cardType == CardData.CardType.Versatile)
            {
                _playerController.GainArmor(tmpCombatValue);
                if (target != null)
                {
                    ResolveCombat();
                }
                ContinuePhase();
            }
            if (_card.cardType == CardData.CardType.Defense)
            {
                _playerController.GainArmor(tmpCombatValue);
                ContinuePhase();
            }

            if (_card.cardType == CardData.CardType.Attack || _card.cardType == CardData.CardType.Tactic)
            {
                ResolveCombat();
                ContinuePhase();
            }
            
            if(_card.cardType == CardData.CardType.Resource) ContinuePhase();
        
        
        
        }

        public void ResolveCombat()
        {
            
           
            if (_baseCardObject.attackType == BaseCardObject.AttackTypeEnum.Aoe)
            {
                
                aoeRange += _baseCardObject.aoeRange;
                List<MapTile> enemiesInRange = Pathfinding.GetEnemiesInRange(aoeRange, target);
                foreach (var tile in enemiesInRange)
                {
                    if (tile.unitOnTile.GetComponent<Enemy>().TakeDamage(tmpCombatValue))
                    {
                        onKill = true;
                    }
                }
            }
            else if(_baseCardObject.attackType == BaseCardObject.AttackTypeEnum.Single)
            {
                if (target.unitOnTile.GetComponent<Enemy>().TakeDamage(tmpCombatValue))
                {
                    onKill = true;
                }
            }
            else if (_baseCardObject.attackType == BaseCardObject.AttackTypeEnum.SlashAoe)
            {
                List<MapTile> enemiesInRange = Pathfinding.GetSlashTargets(target, 0);
                foreach (var tile in enemiesInRange)
                {
                    if(tile.unitOnTile == null) continue;
                    if (tile.unitOnTile.GetComponent<Enemy>().TakeDamage(tmpCombatValue))
                    {
                        onKill = true;
                    }
                }
            
            }
            
        }

        private void ResolveTargeting()
        {
            if (!enemiesInRange && _card.cardType == CardData.CardType.Versatile)
            {
                target = null;
                ContinuePhase();
            }
            else if (_baseCardObject.origin == BaseCardObject.OriginEnum.Self)
            {
                target = _playerMovementController.tileStandingOn;
                ContinuePhase();
            }
            else if (_baseCardObject.origin == BaseCardObject.OriginEnum.EnemyTarget)
            {
                tmpRangeValue += _baseCardObject.range;
                List<MapTile> enemiesInRange = Pathfinding.GetEnemiesInRange(tmpRangeValue, _playerMovementController.tileStandingOn);
                foreach (var tile in enemiesInRange) { TileManager.Instance.HighlightTile(tile); }
                UiManager.Instance.mouseController.OnClickedObject += ChooseTarget;
            }

        }
    
        private void AfterMainEffects()
        {
            UiManager.Instance.playerRef.GetComponent<Hand>().RemoveCard(_baseCardObject);
            if (_baseCardObject.afterMainEffect.Length > 0)
            {
                EffectResolver(_baseCardObject.afterMainEffect);
            }

            if (onKill)
            {
                OnKillEffect();
            }
            ContinuePhase();
        }

        private void DonePlayCard()
        {
            if (_card.cardType == CardData.CardType.Resource)
            {
                UiManager.Instance.equipmentManager.RemoveResourceItem(_baseCardObject);
            }
            UiManager.Instance.EnableAction();
            _playerController.UseActionPoint(_baseCardObject.cardCost);
            CardManager.Instance.SendToDiscard(_baseCardObject);
            CardManager.Instance.handUi.RemoveCard(gameObject);
        }
    
        public void OnDrag(PointerEventData eventData)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            if(locked) return;
            var mousePosition = Input.mousePosition;
            mousePosition.z = GameManager.Instance.mainCamera.transform.position.z * -1;
            transform.position = mousePosition;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if(Input.GetMouseButton(1))
            {
                UseAsBoost();
            }
            // Can't remove  lol?? 
        }
    
        public void OnPointerUp(PointerEventData eventData)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if(locked) return;
            if (CardManager.Instance.inPlayZone)
            {
                if (UiManager.Instance.CheckIfCanTakeAction() && _card.cardType != CardData.CardType.Trigger && 
                    _playerController.CanPlayCard(_baseCardObject.cardCost))
                {
                    if (_playerController.CanPlayCard(CheckRange(), _baseCardObject.cardCost))
                    {
                        enemiesInRange = true;
                    }

                    if (_card.cardType != CardData.CardType.Attack || enemiesInRange)
                    {
                        PlayCard();
                        gameObject.SetActive(false);
                    }

                }
            }
            if (CardManager.Instance.cardDropSpot != null)
            {
                CardDropSpotLogic();
            }
            CardManager.Instance.UpdateHandUi();
            if (transform.parent.GetComponent<CardDropSpot>() != null)
                transform.parent.GetComponent<CardDropSpot>().UpdateLayout();
        }

        public int CheckRange()
        {
            int tmpRange = _baseCardObject.range;
        
            foreach(var boostEffect in _baseCardObject.boostEffect)
            {
                if (boostEffect.ToLower().Contains("range"))
                {
                    EffectResolver(boostEffect, _playerController.boostPool);
                }
                
            }
            tmpRange += tmpRangeValue;
            tmpRangeValue = 0;
            tmpCombatValue = baseCombatValue;
            return tmpRange;

        }

        public void CardDropSpotLogic()
        {
            if (transform.parent.GetComponent<CardDropSpot>() != null)
            {
                transform.parent.GetComponent<CardDropSpot>().RemoveCard(gameObject);
            }
            BaseCardObject thisCard = _baseCardObject;
            if (CardManager.Instance.cardDropSpot.CheckIfCanPlay(thisCard))
            {
                if (!CardManager.Instance.cardDropSpot.CheckIfFull())
                {
                    BaseCardObject card = CardManager.Instance.cardDropSpot.SwapCard(thisCard);
                    UiManager.Instance.playerRef.GetComponent<Hand>().RemoveCard(thisCard);
                    UiManager.Instance.playerRef.GetComponent<Hand>().AddCard(card);
                
                    GetComponent<CardPopulate>().SetupCard(card);
                }
                else
                {
                    if (CardManager.Instance.cardDropSpot.AddCard(thisCard))
                    {
                        CardManager.Instance.UpdateHandUi();
                        if(transform.parent.GetComponent<HandUi>() != null)
                            UiManager.Instance.playerRef.GetComponent<Hand>().RemoveCard(_baseCardObject);
                        CardManager.Instance.handUi.RemoveCard(gameObject);
                    }
                
                }
            
            }

        }

        private void UseAsBoost()
        {
            _playerController.GainBoost(_baseCardObject.cardBoostValue);
            UiManager.Instance.playerRef.GetComponent<Hand>().RemoveCard(_baseCardObject);
            CardManager.Instance.SendToDiscard(_baseCardObject);
            CardManager.Instance.handUi.RemoveCard(gameObject);
        }

        public void AltHovered()
        {
            CardManager.Instance.handUi.SetZoomedCard(_baseCardObject);
        }

        public void AltUnHovered()
        {
            CardManager.Instance.handUi.UnsetZoomedCard();
        
        }

    
    }
}
