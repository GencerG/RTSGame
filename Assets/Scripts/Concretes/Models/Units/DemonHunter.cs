using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class DemonHunter : UnitModel
    {
        public override string Name { get; set; } = "Illidan";

        public override float Health { get; set; } = 9999f;

        public override float MaximumHealth { get; set; } = 9999f;

        public override float AttackPower { get; set; } = 9999f;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(0.64f, 0.19f, 0.79f);
    }
}
