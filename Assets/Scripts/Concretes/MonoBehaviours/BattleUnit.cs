using RTSGame.Abstracts.Models;
using RTSGame.Enums;
using RTSGame.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class BattleUnit : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SpriteRenderer _unitSpriteRenderer;
        [SerializeField] private TMP_Text _unitNameText;
        [SerializeField] private Image _hpBar;

        private float _tapDuration = 0;
        private bool _isHolding = false; 

        #endregion

        #region Model

        public UnitModel Model { get; private set; }

        #endregion

        #region Public Methods

        public void Initialize(UnitModel model)
        {
            Model = model;

            _unitSpriteRenderer.color = model.UnitColor;
            _unitNameText.text = model.Name;

            UpdateHealthBar();
        }

        public void TakeDamage(int value)
        {
            Model.Health -= value;

            if (Model.Health <= 0)
            {
                MessageBroker.Default.Publish(new EventUnitDied { BattleUnit = this });
            }
        }

        public void UpdateHealthBar()
        {
            _hpBar.fillAmount = ((float)Model.Health / (float)Model.MaximumHealth);
        }

        #endregion

        #region Mono Behaviour

        private void OnMouseDown()
        {
            if (Model.IsDead)
                return;

            if (!GameManager.Instance.IsInputActive)
                return;

            if (Model.UnitTeam != Team.Blue)
                return;

            _isHolding = true;
        }

        private void OnMouseUp()
        {
            if (Model.IsDead)
                return;

            if (!GameManager.Instance.IsInputActive)
                return;

            if (Model.UnitTeam != Team.Blue)
                return;

            if (_tapDuration <= 2.0f)
            {
                MessageBroker.Default.Publish(new EventUnitCardTapped { UnitModel = Model, IsSelected = true });
            }

            _tapDuration = 0.0f;
            _isHolding = false;
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
                    var position = Camera.main.WorldToScreenPoint(transform.position);
                    MessageBroker.Default.Publish(new EventUnitCardTappedAndHold
                    {
                        Position = position,
                        UnitModel = Model
                    });
                }
            }
        }

        #endregion
    }
}