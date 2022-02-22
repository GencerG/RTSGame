using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RTSGame.Concretes.MonoBehaviours
{
    /// <summary>
    /// Handles main menu UI.
    /// </summary>
    public class MainMenuController : Controller
    {
        #region Fields

        [SerializeField] private Button _battleButton;
        [SerializeField] private Text _headerText;

        #endregion

        #region Abstract

        public override void Initialize()
        {
            // initializing battle button
            _battleButton.onClick.AddListener(OnBattleButtonClicked);
            var deckSize = Constants.GAME_CONFIGS.DECK_SIZE;
            _headerText.text = $"SELECT {deckSize} HEROES TO BATTLE";
        }

        #endregion

        #region Mono Behaviour

        private void OnDestroy()
        {
            _battleButton.onClick.RemoveAllListeners();
        }

        #endregion

        #region Call Backs

        /// <summary>
        /// This function is called when battle button is clicked. Loads Battle Scene.
        /// </summary>
        private void OnBattleButtonClicked()
        {
            var deckCount = GameManager.Instance.PlayerDeck.GetAll().Count;

            // player must select 3 cards.
            if (deckCount < Constants.GAME_CONFIGS.DECK_SIZE)
            {
                return;
            }

            SceneManager.LoadScene(Constants.SCENE_INDEXES.BATTLE_SCENE);
        }

        #endregion
    }
}