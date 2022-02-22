using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Events;
using RTSGame.Enums;
using UniRx;
using RTSGame.Concretes.Models;
using UnityEngine;

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

            /*
            MessageBroker.Default.Receive<EventUnitCardTapped>()
                .Subscribe(OnUnitSelected)
                .AddTo(gameObject);
            */

            EventBus.EventUnitCardTapped += OnUnitSelected;

            InitializeDeck();
        }

        private void OnDestroy()
        {
            EventBus.EventUnitCardTapped -= OnUnitSelected;
        }

        private void InitializeDeck()
        {
            var playerDeck = _playerDeck.GetAll();

            if (playerDeck.Count > 0)
            {
                playerDeck.Clear();
            }
        }

        private void OnUnitSelected(UnitModel model, GameObject highlighter, bool isSelected)
        {
            if (isSelected)
            {
                var deckCount = _playerDeck.GetAll().Count;
                if (deckCount < Constants.GAME_CONFIGS.DECK_SIZE)
                {
                    highlighter.SetActive(true);
                    _playerDeck.Add(model);
                }
            }
            else
            {
                highlighter.SetActive(false);
                _playerDeck.Remove((UnitType)model.Id);
            }
        }
    }
}
