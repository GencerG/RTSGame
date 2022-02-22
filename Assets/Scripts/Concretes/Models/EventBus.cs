using RTSGame.Abstracts.Models;
using RTSGame.Concretes.MonoBehaviours;
using RTSGame.Enums;
using System;
using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public static class EventBus
    {
        public static Action<BattleUnit> EventUnitDied;
        public static Action EventUnitCardReleased;
        public static Action<UnitModel, GameObject, bool> EventUnitCardTapped;
        public static Action<UnitModel, Vector3> EventUnitCardTappedAndHold;
        public static Action<BattleResult> EventBattleOver;
    }
}