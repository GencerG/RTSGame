using RTSGame.Abstracts.Models;

namespace RTSGame.Events
{
    public struct EventUnitCardTapped
    {
        public UnitModel UnitModel;
        public bool IsSelected;
    }
}