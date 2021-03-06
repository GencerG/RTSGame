using Newtonsoft.Json;
using RTSGame.Enums;
using UnityEngine;

namespace RTSGame.Abstracts.Models
{
    public abstract class UnitModel
    {
        public int Id { get; set; }

        public Team UnitTeam { get; set; }

        public abstract string Name { get; set; } 

        public abstract float Health { get; set; }

        public abstract float MaximumHealth { get; set; }

        public abstract float AttackPower { get; set; }

        public abstract int Experience { get; set; }

        public abstract int Level { get; set; }

        [JsonIgnore]
        public abstract Color UnitColor { get; set; }

        public bool IsDead { get; set; } = false;

        public UnitModel SetId(int id)
        {
            Id = id;
            return this;
        }

        public UnitModel SetTeam(Team team)
        {
            UnitTeam = team;
            return this;
        }
    }
}
