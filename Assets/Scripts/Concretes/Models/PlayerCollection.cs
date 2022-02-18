using RTSGame.Abstracts.Models;
using RTSGame.Concretes.Factory;
using RTSGame.Enums;
using System.Collections.Generic;

namespace RTSGame.Concretes.Models
{
    public class PlayerCollection
    {
        public List<UnitModel> _playerUnitCollection;

        public PlayerCollection()
        {
            _playerUnitCollection = new List<UnitModel>();
        }

        public UnitModel GetUnitFromCollection(UnitType unit)
        {
            for (int i = 0; i < _playerUnitCollection.Count; ++i)
            {
                if ((int)unit == _playerUnitCollection[i].Id)
                {
                    return _playerUnitCollection[i];
                }
            }

            return null;
        }

        public void AddUnitToCollection(UnitType unit)
        {
            var newUnit = UnitFactory.CreateUnit(unit);

            if (!_playerUnitCollection.Contains(newUnit))
            {
                _playerUnitCollection.Add(newUnit);
            }
        }

        public void RemoveUnitFromCollection(UnitType unit)
        {
            for (int i = 0; i < _playerUnitCollection.Count; ++i)
            {
                if ((int)unit == _playerUnitCollection[i].Id)
                {
                    _playerUnitCollection.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
