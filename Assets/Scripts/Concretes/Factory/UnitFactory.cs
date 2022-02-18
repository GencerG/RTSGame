using RTSGame.Abstracts.Models;
using RTSGame.Concretes.Models;
using RTSGame.Enums;

namespace RTSGame.Concretes.Factory
{
    public static class UnitFactory
    {
        public static UnitModel CreateUnit(UnitType type)
        {
            switch (type)
            {
                case UnitType.DemonHunter:
                    return new DemonHunter()
                        .SetId((int)type);

                case UnitType.Paladin:
                    return new Paladin()
                        .SetId((int)type);

                case UnitType.Warrior:
                    return new Warrior()
                        .SetId((int)type);

                default:
                    return null;
            }
        }
    }
}
