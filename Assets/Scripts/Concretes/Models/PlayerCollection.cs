using RTSGame.Abstracts.Models;
using RTSGame.Enums;
using System.Collections.Generic;

namespace RTSGame.Concretes.Models
{
    public class PlayerCollection : IUnitCollection
    {
        private List<UnitModel> _collection;

        public PlayerCollection()
        {
            _collection = new List<UnitModel>();
        }

        public void Add(UnitModel unit)
        {
            if (!_collection.Contains(unit))
            {
                _collection.Add(unit);
            }
        }

        public UnitModel Get(UnitType unitType)
        {
            for (int i = 0; i < _collection.Count; ++i)
            {
                if ((int)unitType == _collection[i].Id)
                {
                    return _collection[i];
                }
            }

            return null;
        }

        public List<UnitModel> GetAll()
        {
            return _collection;
        }
    }
}
