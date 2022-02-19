using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using UnityEngine;
using UniRx;
using RTSGame.Events;
using System;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class DeckController : Controller
    {
        #region Model

        private IUnitCollection _playerDeck;

        #endregion

        public override void Initialize()
        {
            if (_playerDeck == null)
            {
                _playerDeck = GameManager.Instance.PlayerDeck;
            }

            MessageBroker.Default.Receive<EventUnitCardTapped>()
                .Subscribe(OnUnitSelected)
                .AddTo(gameObject);
        }

        public override void Clear()
        {
        }

        private void OnUnitSelected(EventUnitCardTapped obj)
        {
            _playerDeck.Add(obj.UnitModel);
        }
    }
}