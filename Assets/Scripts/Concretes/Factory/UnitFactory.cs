using RTSGame.Abstracts.Models;
using RTSGame.Concretes.Models;
using RTSGame.Enums;

namespace RTSGame.Concretes.Factory
{
    public static class UnitFactory
    {
        public static UnitModel CreateUnit(UnitType type, Team team)
        {
            switch (type)
            {
                case UnitType.DemonHunter:
                    return new DemonHunter()
                        .SetId((int)type)
                        .SetTeam(team);

                case UnitType.Paladin:
                    return new Paladin()
                        .SetId((int)type)
                        .SetTeam(team);

                case UnitType.Warrior:
                    return new Warrior()
                        .SetId((int)type)
                        .SetTeam(team);

                case UnitType.Druid:
                    return new Warrior()
                        .SetId((int)type)
                        .SetTeam(team);

                case UnitType.Hunter:
                    return new Warrior()
                        .SetId((int)type)
                        .SetTeam(team);

                case UnitType.Mage:
                    return new Warrior()
                        .SetId((int)type)
                        .SetTeam(team);

                case UnitType.Priest:
                    return new Warrior()
                        .SetId((int)type)
                        .SetTeam(team);

                case UnitType.Rogue:
                    return new Warrior()
                        .SetId((int)type)
                        .SetTeam(team);

                case UnitType.Shaman:
                    return new Warrior()
                        .SetId((int)type)
                        .SetTeam(team);

                case UnitType.Warlock:
                    return new Warrior()
                        .SetId((int)type)
                        .SetTeam(team);

                default:
                    return null;
            }
        }
    }
}
