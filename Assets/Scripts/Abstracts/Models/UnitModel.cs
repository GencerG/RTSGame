using UnityEngine;

namespace RTSGame.Abstracts.Models
{
    public abstract class UnitModel
    {
        public abstract int Id { get; set; }

        public abstract string Name { get; set; }

        public abstract int Health { get; set; }

        public abstract int AttackPower { get; set; }

        public abstract int Experience { get; set; }

        public abstract int Level { get; set; }

        public abstract Color UnitColor { get; set; }

        public bool IsDead { get; set; } = false;

        public UnitModel SetId(int id)
        {
            Id = id;
            return this;
        }
    }
}
