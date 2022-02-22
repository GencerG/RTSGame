using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Factory;
using RTSGame.Concretes.Models;
using RTSGame.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Concretes.MonoBehaviours
{
    /// <summary>
    /// Handles player collection. Adds or removes units. Initializes UI.
    /// </summary>
    public class CollectionController : Controller
    {
        #region Model

        private IUnitCollection _playerCollection;

        #endregion

        #region Fields

        [SerializeField] private Transform _gridLayout;
        [SerializeField] private UnitCard _cardPrefab;
        [SerializeField] private List<UnitType> _lockedUnitList;

        #endregion

        #region Abstract

        public override void Initialize()
        {
            if (_playerCollection == null)
            {
                _playerCollection = GameManager.Instance.PlayerCollection;
            }

            var collection = _playerCollection.GetAll();

            // if player has no card at start, creating new units from factory.
            if (collection.Count == 0)
            {
                _playerCollection.Add(UnitFactory.CreateUnit(UnitType.DemonHunter, Team.Blue));
                _playerCollection.Add(UnitFactory.CreateUnit(UnitType.Paladin, Team.Blue));
                _playerCollection.Add(UnitFactory.CreateUnit(UnitType.Warrior, Team.Blue));
            }

            // removing units from locked list for each unit player have
            for (int i = 0; i < collection.Count; ++i)
            {
                if (_lockedUnitList.Contains((UnitType)collection[i].Id))
                {
                    _lockedUnitList.Remove((UnitType)collection[i].Id);
                }
            }

            // if player played 5 games, unlocking random unit.
            var playCount = GameManager.Instance.PlayCount;
            if (playCount % Constants.GAME_CONFIGS.PLAY_COUNT_REWARD == 0 && playCount != 0)
            {
                if (_lockedUnitList.Count > 0)
                {
                    var randomIndex = Random.Range(0, _lockedUnitList.Count);
                    var randomUnlock = _lockedUnitList[randomIndex];

                    // removing unlocked unit from list.
                    _lockedUnitList.RemoveAt(randomIndex);

                    _playerCollection.Add(UnitFactory.CreateUnit(randomUnlock, Team.Blue));
                }
            }

            InitializeUI();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Spawns UI cards for each unit player have
        /// </summary>
        private void InitializeUI()
        {
            var collection = _playerCollection.GetAll();

            foreach (var unit in collection)
            {
                var card = Instantiate(_cardPrefab, _gridLayout);
                card.Initalize(unit);
            }
        }

        #endregion
    }
}
