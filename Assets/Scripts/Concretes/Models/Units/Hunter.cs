using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Hunter : UnitModel
    {
        public override string Name { get; set; } = "Rexxar";

        public override float Health { get; set; } = 150f;

        public override float MaximumHealth { get; set; } = 150f;

        public override float AttackPower { get; set; } = 100f;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(0.67f, 0.83f, 0.45f);
    }
}