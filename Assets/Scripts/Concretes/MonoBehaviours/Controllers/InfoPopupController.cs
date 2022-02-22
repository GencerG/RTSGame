using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Models;
using RTSGame.Events;
using UniRx;
using UnityEngine;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class InfoPopupController : Controller
    {
        [SerializeField] private InfoPopup _infoPopup;

        public override void Initialize()
        {
            DontDestroyOnLoad(this);

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

        private void OnDestroy()
        {
            EventBus.EventUnitCardTappedAndHold -= OnUnitCardTappedAndHold;
            EventBus.EventUnitCardReleased -= OnUnitCardReleased;
        }

        private void OnUnitCardReleased()
        {
            _infoPopup.gameObject.SetActive(false);
        }

        private void OnUnitCardTappedAndHold(UnitModel model, Vector3 position)
        {
            _infoPopup.transform.position = position;
            _infoPopup.SetPopup(model);
        }
    }
}