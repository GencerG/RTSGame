using RTSGame.Abstracts.Models;
using RTSGame.Concretes.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RTSGame.Concretes.MonoBehaviours
{
    /// <summary>
    /// Handles the hero cards that are spawned in main menu scene.
    /// </summary>
    public class UnitCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Fields

        [SerializeField] private Text _unitNameText;
        [SerializeField] private Image _unitImage;
        [SerializeField] private GameObject _highlighter;


        public UnitModel UnitModel { get; private set; }

        private float _tapDuration = 0.0f;
        private bool _isHolding = false;
        private bool _toggle = false;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes model and update visuals by given model.
        /// </summary>
        /// <param name="model"></param>
        public void Initalize(UnitModel model)
        {
            UnitModel = model;
            _unitNameText.text = UnitModel.Name;
            _unitImage.color = UnitModel.UnitColor;
        }

        #endregion

        #region Interface

        public void OnPointerDown(PointerEventData eventData)
        {
            _isHolding = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isHolding = false;

            if (_tapDuration <= Constants.GAME_CONFIGS.HOLD_DURATION)
            {
                _toggle = !_toggle;
                // MessageBroker.Default.Publish(new EventUnitCardTapped { UnitModel = UnitModel, IsSelected = _toggle, Highlighter = _highlighter });
                EventBus.EventUnitCardTapped?.Invoke(UnitModel, _highlighter, _toggle);
            }

            _tapDuration = 0.0f;
            //MessageBroker.Default.Publish(new EventUnitCardReleased());
            EventBus.EventUnitCardReleased?.Invoke();
        }

        #endregion

        #region Mono Behaviour

        private void Update()
        {
            if (_isHolding)
            {
                _tapDuration += Time.deltaTime;
                if (_tapDuration >= Constants.GAME_CONFIGS.HOLD_DURATION)
                {
                    _isHolding = false;
                    // MessageBroker.Default.Publish(new EventUnitCardTappedAndHold { Position = transform.position,UnitModel = UnitModel });
                    EventBus.EventUnitCardTappedAndHold(UnitModel, transform.position);
                }
            }
        }

        #endregion
    }
}
