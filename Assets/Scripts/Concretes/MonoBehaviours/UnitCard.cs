using RTSGame.Abstracts.Models;
using RTSGame.Events;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class UnitCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Text _unitNameText;
        [SerializeField] private Image _unitImage;
        [SerializeField] private GameObject _highlighter;

        public UnitModel UnitModel { get; private set; }

        private float _tapDuration = 0.0f;
        private bool _isHolding = false;
        private bool _toggle = false;

        public void Initalize(UnitModel model)
        {
            UnitModel = model;
            _unitNameText.text = UnitModel.Name;
            _unitImage.color = UnitModel.UnitColor;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isHolding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isHolding = false;

            if (_tapDuration <= 2.0f)
            {
                _toggle = !_toggle;
                MessageBroker.Default.Publish(new EventUnitCardTapped { UnitModel = UnitModel, IsSelected = _toggle, Highlighter = _highlighter });
            }

            _tapDuration = 0.0f;
            MessageBroker.Default.Publish(new EventUnitCardReleased());
        }

        private void Update()
        {
            if (_isHolding)
            {
                _tapDuration += Time.deltaTime;
                if (_tapDuration >= 2.0f)
                {
                    _isHolding = false;
                    MessageBroker.Default.Publish(new EventUnitCardTappedAndHold
                    { 
                        Position = transform.position,
                        UnitModel = UnitModel 
                    });
                }
            }
        }
    }
}
