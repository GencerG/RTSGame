using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Sargeras : UnitModel
    {
        public override string Name { get; set; } = "Sargeras";

        public override float Health { get; set; } = 775f;

        public override float MaximumHealth { get; set; } = 775f;

        public override float AttackPower { get; set; } = 65f;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(0.86f, 0.41f, 0.18f);
    }
}
