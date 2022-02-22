using RTSGame.Abstracts.Models;
using UnityEngine;

namespace RTSGame.Events
{
    public struct EventUnitCardTapped
    {
        public UnitModel UnitModel;
        public GameObject Highlighter;
        public bool IsSelected;
    }
}