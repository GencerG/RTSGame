using RTSGame.Abstracts.Models;
using RTSGame.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using RTSGame.Concretes.Models;

namespace RTSGame.Concretes.MonoBehaviours
{
    /// <summary>
    /// Handles units that are spawned on battleground.
    /// </summary>
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

        /// <summary>
        /// Initializes visuals by given model.
        /// </summary>
        /// <param name="model"></param>
        public void Initialize(UnitModel model)
        {
            Model = model;

            _unitSpriteRenderer.color = model.UnitColor;
            _unitNameText.text = model.Name;

            UpdateHealthBar();
        }

        /// <summary>
        /// Decreases health. If health goes below zero, fires event.
        /// </summary>
        /// <param name="value"></param>
        public void TakeDamage(float value)
        {
            Model.Health -= value;

            if (Model.Health <= 0)
            {
                //MessageBroker.Default.Publish(new EventUnitDied { BattleUnit = this });
                EventBus.EventUnitDied?.Invoke(this);
            }
        }

        /// <summary>
        /// Updates helath bar visual.
        /// </summary>
        public void UpdateHealthBar()
        {
            _hpBar.fillAmount = (Model.Health / Model.MaximumHealth);
        }

        #endregion
    }
}