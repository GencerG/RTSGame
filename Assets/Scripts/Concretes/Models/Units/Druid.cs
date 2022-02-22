using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Druid : UnitModel
    {
        public override string Name { get; set; } = "Malfurion";

        public override float Health { get; set; } = 190f;

        public override float MaximumHealth { get; set; } = 190f;

        public override float AttackPower { get; set; } = 95f;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(1.0f, 0.49f, 0.04f);
    }
}
