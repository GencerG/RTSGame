using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Factory;
using RTSGame.Enums;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using RTSGame.Abstracts.Models;
using RTSGame.Concretes.Models;

namespace RTSGame.Concretes.MonoBehaviours
{
    /// <summary>
    /// Handles combats and turns.
    /// </summary>
    public class BattleController : Controller
    {
        #region Fields

        [SerializeField] private Transform[] _unitSpawnPoints;
        [SerializeField] private Transform _enemySpawnPoint;
        [SerializeField] private BattleUnit _battleUnitPrefab;

        private Dictionary<Team, List<BattleUnit>> _unitsOnBattleground;

        private int _currentTurn = 0;

        private BattleResult _battleResult;

        #endregion

        #region Abstract

        public override void Initialize()
        {
            // storing every unit on battleground in dictionary in order to manage combat.
            _unitsOnBattleground = new Dictionary<Team, List<BattleUnit>>();

            GameManager.Instance.SetInputActive(true);

            SpawnPlayerDeck();
            SpawnEnemy();

            /*  MessageBroker.Default.Receive<EventUnitCardTapped>()
                  .Subscribe(OnUnitCardTapped)
                  .AddTo(gameObject);

              MessageBroker.Default.Receive<EventUnitDied>()
                  .Subscribe(OnUnitDied)
                  .AddTo(gameObject);
            */

            EventBus.EventUnitCardTapped += OnUnitCardTapped;
            EventBus.EventUnitDied += OnUnitDied;
        }

        private void OnDestroy()
        {
            EventBus.EventUnitCardTapped -= OnUnitCardTapped;
            EventBus.EventUnitDied -= OnUnitDied;
        }

        #endregion

        #region Call Backs

        /// <summary>
        /// This function is called when a hero is selected by player
        /// </summary>
        /// <param name="model"></param>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        private void OnUnitCardTapped(UnitModel model, GameObject obj, bool value)
        {
            StartAttackSequence(model);
        }

        /// <summary>
        /// This function is called when a unit dies. Removes unit from dictionary.
        /// </summary>
        /// <param name="unit"></param>
        private void OnUnitDied(BattleUnit unit)
        {
            unit.Model.IsDead = true;
            var unitTeam = unit.Model.UnitTeam;
            _unitsOnBattleground[unitTeam].Remove(unit);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Starts attack sequence for player.
        /// </summary>
        /// <param name="model"></param>
        private void StartAttackSequence(UnitModel model)
        {
            GameManager.Instance.SetInputActive(false);

            // getting target team in order to make it work for both player and enemy
            var targetTeam = model.UnitTeam == Team.Blue ? Team.Red : Team.Blue;

            // finding selected attacker
            var attacker = GetAttacker(model);

            // finding random enemy
            var enemy = GetRandomEnemy(targetTeam);
            enemy.TakeDamage(model.AttackPower);

            // starting animation after data manipulation
            AnimateAttacker(attacker, enemy);
        }

        /// <summary>
        /// Finds unit from dictionary with same id given model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private BattleUnit GetAttacker(UnitModel model)
        {
            var battleUnitList = _unitsOnBattleground[model.UnitTeam];
            for (int i = 0; i < battleUnitList.Count; ++i)
            {
                if (model.Id == battleUnitList[i].Model.Id)
                    return battleUnitList[i];
            }
            return null;
        }

        /// <summary>
        /// Finds random enemy from dictionary.
        /// </summary>
        /// <param name="enemyTeam"></param>
        /// <returns></returns>
        private BattleUnit GetRandomEnemy(Team enemyTeam)
        {
            var enemyList = _unitsOnBattleground[enemyTeam];

            var randomIndex = UnityEngine.Random.Range(0, enemyList.Count);

            return enemyList[randomIndex];
        }

        /// <summary>
        /// Moves attacker to the enemy.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="enemy"></param>
        private void AnimateAttacker(BattleUnit attacker, BattleUnit enemy)
        {
            var duration = Constants.GAME_CONFIGS.ATTACK_ANIMATION_DURATION;
            var sequence = DOTween.Sequence();
            var initialPosition = attacker.transform.position;
            sequence.Append(attacker.transform.DOMove(enemy.transform.position, duration).OnComplete(() => enemy.UpdateHealthBar()));
            sequence.Append(attacker.transform.DOMove(initialPosition, duration).OnComplete(OnAttackSequeunceComplete));
        }

        /// <summary>
        /// This function is called when attack animations are over.
        /// </summary>
        private void OnAttackSequeunceComplete()
        {
            // Checking for survivors.
            if (IsGameOver())
            {
                //MessageBroker.Default.Publish(new EventBattleOver { BattleResult = _battleResult });
                EventBus.EventBattleOver?.Invoke(_battleResult);
                return;
            }

            _currentTurn++;
            // in order to manage turns, checking turn counter is even or odd, even => players turn, odds => enemy turn
            if (_currentTurn % 2 == 0)
            {
                GameManager.Instance.SetInputActive(true);
            }

            else
            {
                var enemy = GetRandomEnemy(Team.Red);
                GameManager.Instance.SetInputActive(false);
                StartAttackSequence(enemy.Model);
            }
        }

        /// <summary>
        /// Checks all alive units on both red and blue team.
        /// </summary>
        /// <returns></returns>
        private bool IsGameOver()
        {
            if (_unitsOnBattleground[Team.Red].Count == 0)
            {
                _battleResult = BattleResult.Victory;
            }
            if (_unitsOnBattleground[Team.Blue].Count == 0)
            {
                _battleResult = BattleResult.Defeat;
            }

            return (_unitsOnBattleground[Team.Red].Count == 0 || _unitsOnBattleground[Team.Blue].Count == 0);
        }

        /// <summary>
        /// Creates enemy from factory and instansiates it.
        /// </summary>
        private void SpawnEnemy()
        {
            var enemyList = new List<BattleUnit>();
            var enemy = UnitFactory.CreateUnit(UnitType.Sargeras, Team.Red);

            var enemyInstance = Instantiate(_battleUnitPrefab, _enemySpawnPoint.position, Quaternion.identity);
            enemyInstance.transform.localScale = new Vector3(2f, 2f, 2f);
            enemyInstance.Initialize(enemy);

            enemyList.Add(enemyInstance);
            _unitsOnBattleground.Add(Team.Red, enemyList);
        }

        /// <summary>
        /// Gets player deck and spawns each unit in player decks
        /// </summary>
        private void SpawnPlayerDeck()
        {
            var playerDeck = GameManager.Instance.PlayerDeck.GetAll();
            var playerUnitList = new List<BattleUnit>();

            for (int i = 0; i < playerDeck.Count; ++i)
            {
                var instance = Instantiate(_battleUnitPrefab, _unitSpawnPoints[i].position, Quaternion.identity);
                instance.Initialize(playerDeck[i]);
                playerUnitList.Add(instance);
            }

            _unitsOnBattleground.Add(Team.Blue, playerUnitList);
        }

        #endregion
    }
}
