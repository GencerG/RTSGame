using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Events
{
    public struct EventUnitCardTappedAndHold
    {
        public UnitModel UnitModel;
        public Vector3 Position;
    }
}