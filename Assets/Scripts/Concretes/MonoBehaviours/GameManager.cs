using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Models;
using RTSGame.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class GameManager : SingletonManager<GameManager>
    {
        #region Fields

        private Dictionary<Team, List<UnitController>> _availableUnitsMap;
        public PlayerCollection PlayerCollection { get; private set; }

        [SerializeField] private UnitCard _cardPrefab;
        [SerializeField] private MainMenuReferences _mainMenuPrefab;
        private MainMenuReferences _mainMenuReferencesInstance;

        #endregion

        #region Abstract Methods

        public override void OnInitialized()
        {
            DontDestroyOnLoad(this);

            _availableUnitsMap = new Dictionary<Team, List<UnitController>>();
            _availableUnitsMap[Team.Blue] = new List<UnitController>();
            _availableUnitsMap[Team.Red] = new List<UnitController>();

            PlayerCollection = new PlayerCollection();
            PlayerCollection.AddUnitToCollection(UnitType.DemonHunter);
            PlayerCollection.AddUnitToCollection(UnitType.Warrior);
            PlayerCollection.AddUnitToCollection(UnitType.Paladin);

            _mainMenuReferencesInstance = Instantiate(_mainMenuPrefab);
            foreach (var item in PlayerCollection._playerUnitCollection)
            {
                var card = Instantiate(_cardPrefab, _mainMenuReferencesInstance.GridLayout);
                card.Initalize(item);
            }
        }

        public override void OnClearInstance()
        {
            _availableUnitsMap.Clear();
        }

        #endregion

        #region Public Methods

        public void AddToUnitMap(UnitController character)
        {
            var team = character.UnitTeam;

            if (!_availableUnitsMap[team].Contains(character))
            {
                _availableUnitsMap[team].Add(character);
            }
        }
        
        public void RemoveFromUnitMap(UnitController character)
        {
            var team = character.UnitTeam;

            if (_availableUnitsMap[team].Contains(character))
            {
                _availableUnitsMap[team].Remove(character);
            }
        }

        #endregion

        #region Helper Methods

        #endregion
    }
}