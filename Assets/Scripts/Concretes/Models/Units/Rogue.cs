using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Rogue : UnitModel
    {
        public override string Name { get; set; } = "Valeera";

        public override float Health { get; set; } = 150f;

        public override float MaximumHealth { get; set; } = 150f;

        public override float AttackPower { get; set; } = 120f;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(1.0f, 0.96f, 0.41f);
    }
}
