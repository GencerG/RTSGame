using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Warlock : UnitModel
    {
        public override string Name { get; set; } = "Guldan";

        public override float Health { get; set; } = 170f;

        public override float MaximumHealth { get; set; } = 170f;

        public override float AttackPower { get; set; } = 100f;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(0.53f, 0.53f, 0.93f);
    }
}
