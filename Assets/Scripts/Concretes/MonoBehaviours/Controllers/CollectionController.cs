using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Factory;
using RTSGame.Concretes.Models;
using RTSGame.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private Text _progressText;

        #endregion

        #region Abstract

        public override void Initialize()
        {
            if (_playerCollection == null)
            {
                _playerCollection = GameManager.Instance.PlayerCollection;
            }

            // if player has no card at start, creating new units from factory.
            if (_playerCollection.GetAll().Count == 0)
            {
                // if there is no save file, adding starter heroes to collection
                if (!LocalStorage.Exists(Constants.GAME_CONFIGS.SAVE_FILE_NAME))
                {
                    _playerCollection.Add(UnitFactory.CreateUnit(UnitType.DemonHunter, Team.Blue));
                    _playerCollection.Add(UnitFactory.CreateUnit(UnitType.Paladin, Team.Blue));
                    _playerCollection.Add(UnitFactory.CreateUnit(UnitType.Warrior, Team.Blue));
                }
                else
                {
                    // loading data and updating collection
                    var loadedData = LocalStorage.Load<List<UnitModel>>(Constants.GAME_CONFIGS.SAVE_FILE_NAME);
                    foreach (var model in loadedData)
                    {
                        _playerCollection.Add(model);
                    }
                }
            }

            var collection = _playerCollection.GetAll();

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

                    LocalStorage.Save<List<UnitModel>>(Constants.GAME_CONFIGS.SAVE_FILE_NAME, _playerCollection.GetAll());
                }
            }

            InitializeUI(playCount);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Spawns UI cards for each unit player have
        /// </summary>
        private void InitializeUI(int progress)
        {
            var collection = _playerCollection.GetAll();

            foreach (var unit in collection)
            {
                var card = Instantiate(_cardPrefab, _gridLayout);
                card.Initalize(unit);
            }

            _progressText.text = $"New Hero Progress: {progress % 5} / {Constants.GAME_CONFIGS.PLAY_COUNT_REWARD}";
        }

        #endregion
    }
}
