using RTSGame.Abstracts.Models;
using RTSGame.Concretes.Models;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Concretes.MonoBehaviours
{
    /// <summary>
    /// Handles the hero progress cards that are spawned in post battle scene.
    /// </summary>
    public class UnitProgress : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Text _nameText;
        [SerializeField] private Text _experienceText;
        [SerializeField] private Text _healthText;
        [SerializeField] private Text _attackPowerText;
        [SerializeField] private Text _levelText;
        [SerializeField] private Image _background;

        #endregion

        #region Public Methods

        public void Initialize(ProgressModel model)
        {
            _nameText.text = model.Name;
            _experienceText.text = $"Experience +{model.GainedExperience}";
            _healthText.text = $"Maximum Health +{model.GainedHealth}";
            _attackPowerText.text = $"Attack Power +{model.GainedAttackPower}";
            _levelText.text = $"Level +{model.GainedLevel}";
            _background.color = model.UnitColor;
        }

        #endregion
    }
}
