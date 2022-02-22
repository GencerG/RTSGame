using RTSGame.Abstracts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Concretes.MonoBehaviours
{
    /// <summary>
    /// Handles info popup UI element.
    /// </summary>
    public class InfoPopup : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _nameText;
        [SerializeField] private Text _levelText;
        [SerializeField] private Text _attackPowerText;
        [SerializeField] private Text _experienceText;
        [SerializeField] private Image _backgroundImage;

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates infor popup visual by given model data.
        /// </summary>
        /// <param name="model"></param>
        public void SetPopup(UnitModel model)
        {
            _nameText.text = $"Name: {model.Name}";
            _levelText.text = $"Level: {model.Level}";
            _attackPowerText.text = $"Attack Power: {model.AttackPower}";
            _experienceText.text = $"Experience: {model.Experience}";
            _backgroundImage.color = model.UnitColor;

            gameObject.SetActive(true);
        }

        #endregion
    }
}