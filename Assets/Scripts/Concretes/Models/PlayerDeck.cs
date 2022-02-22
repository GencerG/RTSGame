using RTSGame.Abstracts.Models;
using RTSGame.Enums;
using System.Collections.Generic;

namespace RTSGame.Concretes.Models
{
    /// <summary>
    /// Stores Player Deck for each hero player selected for their deck.
    /// </summary>
    public class PlayerDeck : IUnitCollection
    {
        #region Fields

        private List<UnitModel> _playerCurrentDeck;

        #endregion

        #region Constructor

        public PlayerDeck()
        {
            _playerCurrentDeck = new List<UnitModel>();
        }

        #endregion

        #region Interface

        public void Add(UnitModel unit)
        {
            if (_playerCurrentDeck.Contains(unit))
                return;

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

        #endregion
    }
}