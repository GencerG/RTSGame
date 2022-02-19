using RTSGame.Abstracts.MonoBehaviours;
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

            MessageBroker.Default.Receive<EventUnitCardTappedAndHold>()
                .Subscribe(OnUnitCardTappedAndHold)
                .AddTo(gameObject);

            MessageBroker.Default.Receive<EventUnitCardReleased>()
                .Subscribe(OnUnitCardReleased)
                .AddTo(gameObject);
        }

        public override void Clear()
        {
        }

        private void OnUnitCardReleased(EventUnitCardReleased obj)
        {
            _infoPopup.gameObject.SetActive(false);
        }

        private void OnUnitCardTappedAndHold(EventUnitCardTappedAndHold obj)
        {
            _infoPopup.transform.position = obj.Position;
            _infoPopup.SetPopup(obj.UnitModel);
        }
    }
}