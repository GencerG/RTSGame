using UnityEngine;

namespace RTSGame.Abstracts.MonoBehaviours
{
    public abstract class Controller : MonoBehaviour
    {
        public abstract void Initialize();

        public abstract void Clear();
    }
}
