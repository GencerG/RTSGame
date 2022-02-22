using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Paladin : UnitModel
    {
        public override string Name { get; set; } = "Tirion";

        public override float Health { get; set; } = 350f;

        public override float MaximumHealth { get; set; } = 350f;

        public override float AttackPower { get; set; } = 50f;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(0.96f, 0.55f, 0.73f);
    }
}