using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Warrior : UnitModel
    {
        public override int Id { get; set; }

        public override string Name { get; set; } = "Varian";

        public override int Health { get; set; } = 1000;

        public override int AttackPower { get; set; } = 1000;

        public override int Experience { get; set; } = 0;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(198.0f / 255.0f, 155.0f / 255.0f, 109.0f / 255.0f);
    }
}
