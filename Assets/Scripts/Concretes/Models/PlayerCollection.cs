using RTSGame.Abstracts.Models;
using RTSGame.Enums;
using System.Collections.Generic;

namespace RTSGame.Concretes.Models
{
    /// <summary>
    /// Stores Unit Models for each hero player have.
    /// </summary>
    public class PlayerCollection : IUnitCollection
    {
        #region Fields

        private List<UnitModel> _collection;

        #endregion

        #region Constructor

        public PlayerCollection()
        {
            _collection = new List<UnitModel>();
        }

        #endregion

        #region Interface

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

        public void Remove(UnitType unitType)
        {
            var unit = Get(unitType);
            if (unit != null)
            {
                _collection.Remove(unit);
            }
        }

        public List<UnitModel> GetAll()
        {
            return _collection;
        }

        #endregion
    }
}
