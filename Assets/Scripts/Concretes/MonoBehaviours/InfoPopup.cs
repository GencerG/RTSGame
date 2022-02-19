using RTSGame.Abstracts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class InfoPopup : MonoBehaviour
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _levelText;
        [SerializeField] private Text _attackPowerText;
        [SerializeField] private Text _experienceText;
        [SerializeField] private Image _backgroundImage;

        public void SetPopup(UnitModel model)
        {
            _nameText.text = $"Name: {model.Name}";
            _levelText.text = $"Level: {model.Level}";
            _attackPowerText.text = $"Attack Power: {model.AttackPower}";
            _experienceText.text = $"Experience: {model.Experience}";
            _backgroundImage.color = model.UnitColor;

            gameObject.SetActive(true);
        }
    }
}