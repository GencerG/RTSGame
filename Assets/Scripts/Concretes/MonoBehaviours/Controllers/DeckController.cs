using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Events;
using RTSGame.Enums;
using UniRx;
using RTSGame.Concretes.Models;
using UnityEngine;

namespace RTSGame.Concretes.MonoBehaviours
{
    /// <summary>
    /// Handles player deck. Adds or removes units. Highlights selected unit cards.
    /// </summary>
    public class DeckController : Controller
    {
        #region Model

        private IUnitCollection _playerDeck;

        #endregion

        #region Abstract

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

        #endregion

        #region Mono Behaviour

        private void OnDestroy()
        {
            EventBus.EventUnitCardTapped -= OnUnitSelected;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Initializes player deck.
        /// </summary>
        private void InitializeDeck()
        {
            var playerDeck = _playerDeck.GetAll();

            //if a battle has just ended, clearing deck to re-select heroes.
            if (playerDeck.Count > 0)
            {
                playerDeck.Clear();
            }
        }

        #endregion

        #region Call Backs

        /// <summary>
        /// This function is called when player selects an unit. Adds model to list and highlights selected unit card.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="highlighter"></param>
        /// <param name="isSelected"></param>
        private void OnUnitSelected(UnitModel model, GameObject highlighter, bool isSelected)
        {
            if (isSelected)
            {
                // can not select more than 3 cards.
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

        #endregion
    }
}
