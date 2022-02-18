using RTSGame.Abstracts.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class UnitCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Text _unitNameText;
        [SerializeField] private Image _unitImage;

        private UnitModel _unitModel;

        private float _tapDuration = 0;
        private bool _isHolding = false;

        public void Initalize(UnitModel model)
        {
            _unitModel = model;
            _unitNameText.text = _unitModel.Name;
            _unitImage.color = _unitModel.UnitColor;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isHolding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isHolding = false;
            _tapDuration = 0.0f;
        }

        private void Update()
        {
            if (_isHolding)
            {
                _tapDuration += Time.deltaTime;
                if (_tapDuration >= 3.0f)
                {
                    _isHolding = false;
                }
            }
        }
    }
}