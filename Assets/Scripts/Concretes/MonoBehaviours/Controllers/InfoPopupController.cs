using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Models;
using UnityEngine;

namespace RTSGame.Concretes.MonoBehaviours
{
    /// <summary>
    /// Handles unit info popups when unit is tapped and hold.
    /// </summary>
    public class InfoPopupController : Controller
    {
        #region Fields

        [SerializeField] private InfoPopup _infoPopup;
        [SerializeField] private Vector3 _offset;
        private Vector2 _infoPopupSize;

        public override void Initialize()
        {
            DontDestroyOnLoad(this);

            _infoPopupSize = _infoPopup.GetComponent<RectTransform>().sizeDelta;

            /*  MessageBroker.Default.Receive<EventUnitCardTappedAndHold>()
                  .Subscribe(OnUnitCardTappedAndHold)
                  .AddTo(gameObject);

              MessageBroker.Default.Receive<EventUnitCardReleased>()
                  .Subscribe(OnUnitCardReleased)
                  .AddTo(gameObject);
            */

            EventBus.EventUnitCardTappedAndHold += OnUnitCardTappedAndHold;
            EventBus.EventUnitCardReleased += OnUnitCardReleased;
        }

        #endregion

        #region Mono Behaviour

        private void OnDestroy()
        {
            EventBus.EventUnitCardTappedAndHold -= OnUnitCardTappedAndHold;
            EventBus.EventUnitCardReleased -= OnUnitCardReleased;
        }

        #endregion

        #region Call Backs

        /// <summary>
        /// Closes popup when player stops touching
        /// </summary>
        private void OnUnitCardReleased()
        {
            _infoPopup.gameObject.SetActive(false);
        }

        /// <summary>
        /// Activates and initializes info popup when player tapped and hold on an unit.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="position"></param>
        private void OnUnitCardTappedAndHold(UnitModel model, Vector3 position)
        {
            var localOffset = _offset;

            // if the card is near the right side of the screen, mirror the position
            if (Screen.width - position.x < _infoPopupSize.x)
            { 
                localOffset = localOffset * -1;
                localOffset.x += -_infoPopupSize.x;
            }

            _infoPopup.transform.position = position + localOffset;
            _infoPopup.SetPopup(model);
        }

        #endregion
    }
}