using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Factory;
using RTSGame.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Concretes.MonoBehaviours
{
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

            if (collection.Count == 0)
            {
                _playerCollection.Add(UnitFactory.CreateUnit(UnitType.DemonHunter, Team.Blue));
                _playerCollection.Add(UnitFactory.CreateUnit(UnitType.Paladin, Team.Blue));
                _playerCollection.Add(UnitFactory.CreateUnit(UnitType.Warrior, Team.Blue));
            }

            for (int i = 0; i < collection.Count; ++i)
            {
                if (_lockedUnitList.Contains((UnitType)collection[i].Id))
                {
                    _lockedUnitList.Remove((UnitType)collection[i].Id);
                }
            }

            var playCount = GameManager.Instance.PlayCount;
            if (playCount % 5 == 0 && playCount != 0)
            {
                var randomIndex = Random.Range(0, _lockedUnitList.Count);
                var randomUnlock = _lockedUnitList[randomIndex];
                _lockedUnitList.RemoveAt(randomIndex);
                _playerCollection.Add(UnitFactory.CreateUnit(randomUnlock, Team.Blue));
            }

            InitializeUI();
        }

        public override void Clear()
        {
        }

        #endregion

        #region Helper Methods

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
