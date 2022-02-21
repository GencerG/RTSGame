using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Mage : UnitModel
    {
        public override string Name { get; set; } = "Jaina";

        public override int Health { get; set; } = 10000;

        public override int MaximumHealth { get; set; } = 10000;

        public override int AttackPower { get; set; } = 1000;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(0.25f, 0.78f, 0.92f);
    }
}
