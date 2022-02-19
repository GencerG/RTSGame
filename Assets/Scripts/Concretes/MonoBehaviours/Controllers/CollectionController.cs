using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Factory;
using RTSGame.Enums;
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
                _playerCollection.Add(UnitFactory.CreateUnit(UnitType.DemonHunter));
                _playerCollection.Add(UnitFactory.CreateUnit(UnitType.Paladin));
                _playerCollection.Add(UnitFactory.CreateUnit(UnitType.Warrior));
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