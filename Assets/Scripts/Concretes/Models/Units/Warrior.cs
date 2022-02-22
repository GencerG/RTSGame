using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Warrior : UnitModel
    {
        public override string Name { get; set; } = "Varian";

        public override float Health { get; set; } = 300f;

        public override float MaximumHealth { get; set; } = 300f;

        public override float AttackPower { get; set; } = 50f;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(0.76f, 0.61f, 0.43f);
    }
}
