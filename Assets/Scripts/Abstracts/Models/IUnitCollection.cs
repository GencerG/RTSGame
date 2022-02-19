using RTSGame.Enums;
using System.Collections.Generic;

namespace RTSGame.Abstracts.Models
{
    public interface IUnitCollection
    {
        void Add(UnitModel unit);

        UnitModel Get(UnitType unitType);

        void Remove(UnitType unitType);

        List<UnitModel> GetAll();
    }
}
