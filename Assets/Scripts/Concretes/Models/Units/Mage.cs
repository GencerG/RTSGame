using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Mage : UnitModel
    {
        public override string Name { get; set; } = "Jaina";

        public override float Health { get; set; } = 140f;

        public override float MaximumHealth { get; set; } = 140f;

        public override float AttackPower { get; set; } = 135f;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(0.25f, 0.78f, 0.92f);
    }
}
