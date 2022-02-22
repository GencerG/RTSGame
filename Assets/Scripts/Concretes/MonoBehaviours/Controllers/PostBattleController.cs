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
        [SerializeField] private GameObject _parent;
        [SerializeField] private Button _returnButton;
        [SerializeField] private UnitProgress _progressPrefab;
        [SerializeField] private Transform _unitProgressLayout;

        private List<ProgressModel> _progressModels;

        #endregion

        #region Abstract

        public override void Initialize()
        {
            _progressModels = new List<ProgressModel>();

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
            var battleDeck = GameManager.Instance.PlayerDeck.GetAll();
            PrepareUnitsForNextBattle(battleDeck, result);
            InitializeUI(result);
            GameManager.Instance.IncreasePlayCount();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Grant rewards, resets relevant data.
        /// </summary>
        /// <param name="battleDeck"></param>
        /// <param name="result"></param>
        private void PrepareUnitsForNextBattle(List<UnitModel> battleDeck, BattleResult result)
        {
            for (int i = 0; i < battleDeck.Count; ++i)
            {
                var progressModel = new ProgressModel();
                progressModel.Name = battleDeck[i].Name;
                progressModel.UnitColor = battleDeck[i].UnitColor;

                // granting rewards for each alive unit.
                if (result == BattleResult.Victory)
                {
                    if (!battleDeck[i].IsDead)
                    {
                        battleDeck[i].Experience++;
                        progressModel.GainedExperience++;

                        if (battleDeck[i].Experience % Constants.GAME_CONFIGS.EXPERIENCE_TO_LEVEL == 0)
                        {
                            battleDeck[i].Experience = 1;

                            battleDeck[i].Level++;
                            progressModel.GainedLevel++;

                            battleDeck[i].AttackPower += battleDeck[i].AttackPower / Constants.GAME_CONFIGS.LEVEL_UP_MODIFIER;
                            progressModel.GainedAttackPower = battleDeck[i].AttackPower / Constants.GAME_CONFIGS.LEVEL_UP_MODIFIER;

                            battleDeck[i].MaximumHealth += battleDeck[i].MaximumHealth / Constants.GAME_CONFIGS.LEVEL_UP_MODIFIER;
                            progressModel.GainedHealth = battleDeck[i].MaximumHealth / Constants.GAME_CONFIGS.LEVEL_UP_MODIFIER;

                        }
                    }
                }

                // resetting health
                battleDeck[i].IsDead = false;
                battleDeck[i].Health = battleDeck[i].MaximumHealth;
                _progressModels.Add(progressModel);
            }
        }

        /// <summary>
        /// Activates result text, return button and spawns progress cards.
        /// </summary>
        /// <param name="result"></param>
        private void InitializeUI(BattleResult result)
        {
            // activating UI.
            _parent.SetActive(true);
            _victoryText.SetActive(result == BattleResult.Victory);
            _defeatText.SetActive(result == BattleResult.Defeat);

            for (int i = 0; i < _progressModels.Count; ++i)
            {
                var instance = Instantiate(_progressPrefab, _unitProgressLayout);
                instance.Initialize(_progressModels[i]);
            }
        }

        #endregion
    }
}
