using RTSGame.Abstracts.Models;
using RTSGame.Enums;
using System.Collections.Generic;

namespace RTSGame.Concretes.Models
{
    public class PlayerDeck : IUnitCollection
    {
        private List<UnitModel> _playerCurrentDeck;

        public PlayerDeck()
        {
            _playerCurrentDeck = new List<UnitModel>();
        }

        public void Add(UnitModel unit)
        {
            if (_playerCurrentDeck.Contains(unit))
                return;

            if (_playerCurrentDeck.Count == 3)
            {
                _playerCurrentDeck.RemoveAt(0);
                _playerCurrentDeck.Add(unit);
                return;
            }

            _playerCurrentDeck.Add(unit);
        }

        public UnitModel Get(UnitType unitType)
        {
            for (int i = 0; i < _playerCurrentDeck.Count; ++i)
            {
                if ((int)unitType == _playerCurrentDeck[i].Id)
                {
                    return _playerCurrentDeck[i];
                }
            }

            return null;
        }

        public void Remove(UnitType unitType)
        {
            var unit = Get(unitType);
            if (unit != null)
            {
                _playerCurrentDeck.Remove(unit);
            }
        }

        public List<UnitModel> GetAll()
        {
            return _playerCurrentDeck;
        }
    }
}