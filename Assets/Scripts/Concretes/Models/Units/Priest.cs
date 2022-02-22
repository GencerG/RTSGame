using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public class Priest : UnitModel
    {
        public override string Name { get; set; } = "Anduin";

        public override float Health { get; set; } = 200f;

        public override float MaximumHealth { get; set; } = 200f;

        public override float AttackPower { get; set; } = 40f;

        public override int Experience { get; set; } = 1;

        public override int Level { get; set; } = 1;

        public override Color UnitColor { get; set; } = new Color(1.0f, 1.0f, 1.0f);
    }
}
