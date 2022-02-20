using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Events;
using RTSGame.Enums;
using UniRx;

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

            InitializeDeck();
        }

        public override void Clear()
        {
        }

        private void InitializeDeck()
        {
            var playerDeck = _playerDeck.GetAll();

            if (playerDeck.Count > 0)
            {
                playerDeck.Clear();
            }
        }

        private void OnUnitSelected(EventUnitCardTapped obj)
        {
            if (obj.IsSelected)
            {
                var deckCount = _playerDeck.GetAll().Count;
                if (deckCount < 3)
                {
                    _playerDeck.Add(obj.UnitModel);
                }
            }
            else
            {
                _playerDeck.Remove((UnitType)obj.UnitModel.Id);
            }
        }
    }
}
