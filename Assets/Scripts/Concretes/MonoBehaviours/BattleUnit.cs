using RTSGame.Abstracts.Models;
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
    }
}