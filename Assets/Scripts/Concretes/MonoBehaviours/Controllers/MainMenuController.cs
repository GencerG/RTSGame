using RTSGame.Abstracts.MonoBehaviours;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class MainMenuController : Controller
    {
        [SerializeField] private Button _battleButton;

        public override void Initialize()
        {
            _battleButton.onClick.AddListener(OnBattleButtonClicked);
        }

        public override void Clear()
        {
            _battleButton.onClick.RemoveAllListeners();
        }

        private void OnBattleButtonClicked()
        {
            var deckCount = GameManager.Instance.PlayerDeck.GetAll().Count;

            if (deckCount < 3)
            {
                return;
            }

            SceneManager.LoadScene(2);
        }
    }
}