using RTSGame.Abstracts.Models;
using RTSGame.Enums;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class DemonHunter : UnitModel
    {
        public override string Name { get; set; } = "Illidan";

        public override int Health { get; set; } = 10000;

        public override int MaximumHealth { get; set; } = 10000;

        public override int AttackPower { get; set; } = 1000;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(163.0f / 255.0f, 48.0f / 255.0f, 201.0f / 255.0f);
    }
}
