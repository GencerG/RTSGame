using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Shaman : UnitModel
    {
        public override string Name { get; set; } = "Thrall";

        public override float Health { get; set; } = 150f;

        public override float MaximumHealth { get; set; } = 150f;

        public override float AttackPower { get; set; } = 75f;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(0.0f, 0.44f, 0.87f);
    }
}
