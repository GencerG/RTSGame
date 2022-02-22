using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Factory;
using RTSGame.Enums;
using RTSGame.Events;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using RTSGame.Abstracts.Models;
using UnityEngine.SceneManagement;
using RTSGame.Concretes.Models;

namespace RTSGame.Concretes.MonoBehaviours
{
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
            _unitsOnBattleground = new Dictionary<Team, List<BattleUnit>>();
            GameManager.Instance.SetInputActive(true);

            SpawnPlayerDeck();
            SpawnEnemy();

            MessageBroker.Default.Receive<EventUnitCardTapped>()
                .Subscribe(OnUnitCardTapped)
                .AddTo(gameObject);

            MessageBroker.Default.Receive<EventUnitDied>()
                .Subscribe(OnUnitDied)
                .AddTo(gameObject);
        }


        public override void Clear()
        {
        }

        #endregion

        #region Helper Methods

        private void OnUnitCardTapped(EventUnitCardTapped obj)
        {
            StartAttackSequence(obj.UnitModel);
        }

        private void StartAttackSequence(UnitModel model)
        {
            GameManager.Instance.SetInputActive(false);
            var targetTeam = model.UnitTeam == Team.Blue ? Team.Red : Team.Blue;
            var attacker = GetAttacker(model);
            var enemy = GetRandomEnemy(targetTeam);
            enemy.TakeDamage(model.AttackPower);
            AnimateAttacker(attacker, enemy);
        }

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

        private BattleUnit GetRandomEnemy(Team enemyTeam)
        {
            var enemyList = _unitsOnBattleground[enemyTeam];

            var randomIndex = UnityEngine.Random.Range(0, enemyList.Count);

            return enemyList[randomIndex];
        }

        private void AnimateAttacker(BattleUnit attacker, BattleUnit enemy)
        {
            var duration = Constants.GAME_CONFIGS.ATTACK_ANIMATION_DURATION;
            var sequence = DOTween.Sequence();
            var initialPosition = attacker.transform.position;
            sequence.Append(attacker.transform.DOMove(enemy.transform.position, duration).OnComplete(() => enemy.UpdateHealthBar()));
            sequence.Append(attacker.transform.DOMove(initialPosition, duration).OnComplete(OnAttackSequeunceComplete));
        }

        private void OnUnitDied(EventUnitDied obj)
        {
            obj.BattleUnit.Model.IsDead = true;
            var unitTeam = obj.BattleUnit.Model.UnitTeam;
            _unitsOnBattleground[unitTeam].Remove(obj.BattleUnit);
        }

        private void OnAttackSequeunceComplete()
        {
            if (IsGameOver())
            {
                MessageBroker.Default.Publish(new EventBattleOver { BattleResult = _battleResult });
                return;
            }
            _currentTurn++;
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
