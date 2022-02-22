using RTSGame.Abstracts.MonoBehaviours;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RTSGame.Abstracts.Models;
using System.Collections.Generic;
using RTSGame.Enums;
using RTSGame.Concretes.Models;

namespace RTSGame.Concretes.MonoBehaviours
{
    /// <summary>
    /// Manipulates data after battle is over. Grant rewards to units and prepares them for next battle.
    /// </summary>
    public class PostBattleController : Controller
    {
        #region Fields

        [SerializeField] private GameObject _victoryText;
        [SerializeField] private GameObject _defeatText;
        [SerializeField] private Button _returnButton;

        #endregion

        #region Abstract

        public override void Initialize()
        {
            /* MessageBroker.Default.Receive<EventBattleOver>()
                 .Subscribe(OnBattleOver)
                 .AddTo(gameObject);
            */
            EventBus.EventBattleOver += OnBattleOver;
            _returnButton.onClick.AddListener(OnReturnButtonClicked);
        }

        #endregion

        #region Mono Behaviour

        private void OnDestroy()
        {
            EventBus.EventBattleOver -= OnBattleOver;
        }

        #endregion

        #region Call Backs

        /// <summary>
        /// This function is called when retuns button is clicked. Loads Main Menu Scene.
        /// </summary>
        private void OnReturnButtonClicked()
        {
            SceneManager.LoadScene(Constants.SCENE_INDEXES.MAIN_MENU_SCENE);
        }

        /// <summary>
        /// This function is called when battle is over. Handles UI and granting rewards.
        /// </summary>
        /// <param name="result"></param>
        private void OnBattleOver(BattleResult result)
        {
            // activating UI.
            _victoryText.SetActive(result == BattleResult.Victory);
            _defeatText.SetActive(result == BattleResult.Defeat);

            var battleDeck = GameManager.Instance.PlayerDeck.GetAll();

            PrepareUnitsForNextBattle(battleDeck, result);

            GameManager.Instance.IncreasePlayCount();
            _returnButton.gameObject.SetActive(true);
        }

        #endregion

        #region Helper Methods

        private void PrepareUnitsForNextBattle(List<UnitModel> battleDeck, BattleResult result)
        {
            for (int i = 0; i < battleDeck.Count; ++i)
            {
                // granting rewards for each alive unit.
                if (result == BattleResult.Victory)
                {
                    if (!battleDeck[i].IsDead)
                    {
                        battleDeck[i].Experience++;
                        if (battleDeck[i].Experience % Constants.GAME_CONFIGS.EXPERIENCE_TO_LEVEL == 0)
                        {
                            battleDeck[i].Experience = 1;
                            battleDeck[i].Level++;
                            battleDeck[i].AttackPower += battleDeck[i].AttackPower / Constants.GAME_CONFIGS.LEVEL_UP_MODIFIER;
                            battleDeck[i].MaximumHealth += battleDeck[i].MaximumHealth / Constants.GAME_CONFIGS.LEVEL_UP_MODIFIER;
                        }
                    }
                }

                // resetting health
                battleDeck[i].IsDead = false;
                battleDeck[i].Health = battleDeck[i].MaximumHealth;
            }
        }

        #endregion
    }
}
