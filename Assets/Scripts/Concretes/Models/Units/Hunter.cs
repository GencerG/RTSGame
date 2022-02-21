using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Hunter : UnitModel
    {
        public override string Name { get; set; } = "Rexxar";

        public override int Health { get; set; } = 10000;

        public override int MaximumHealth { get; set; } = 10000;

        public override int AttackPower { get; set; } = 1000;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(0.67f, 0.83f, 0.45f);
    }
}