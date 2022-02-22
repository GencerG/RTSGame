using RTSGame.Abstracts.MonoBehaviours;
using UnityEngine;
using UniRx;
using RTSGame.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RTSGame.Abstracts.Models;
using System.Collections.Generic;
using RTSGame.Enums;
using RTSGame.Concretes.Models;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class PostBattleController : Controller
    {
        [SerializeField] private GameObject _victoryText;
        [SerializeField] private GameObject _defeatText;
        [SerializeField] private Button _returnButton;

        public override void Initialize()
        {
            /* MessageBroker.Default.Receive<EventBattleOver>()
                 .Subscribe(OnBattleOver)
                 .AddTo(gameObject);
            */
            EventBus.EventBattleOver += OnBattleOver;
            _returnButton.onClick.AddListener(OnReturnButtonClicked);
        }

        private void OnDestroy()
        {
            EventBus.EventBattleOver -= OnBattleOver;
        }

        private void OnReturnButtonClicked()
        {
            SceneManager.LoadScene(Constants.SCENE_INDEXES.MAIN_MENU_SCENE);
        }

        private void OnBattleOver(BattleResult result)
        {
            _victoryText.SetActive(result == BattleResult.Victory);
            _defeatText.SetActive(result == BattleResult.Defeat);

            var battleDeck = GameManager.Instance.PlayerDeck.GetAll();

            PrepareUnitsForNextBattle(battleDeck, result);

            GameManager.Instance.IncreasePlayCount();
            _returnButton.gameObject.SetActive(true);
        }

        private void PrepareUnitsForNextBattle(List<UnitModel> battleDeck, BattleResult result)
        {
            for (int i = 0; i < battleDeck.Count; ++i)
            {
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

                battleDeck[i].IsDead = false;
                battleDeck[i].Health = battleDeck[i].MaximumHealth;
            }
        }
    }
}
