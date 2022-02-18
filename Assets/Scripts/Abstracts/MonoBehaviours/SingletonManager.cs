using UnityEngine;

namespace RTSGame.Abstracts.MonoBehaviours
{
    public abstract class SingletonManager<ManagerType> : MonoBehaviour where ManagerType : SingletonManager<ManagerType>
    {
        #region Singleton

        public static ManagerType Instance { get; private set; }

        #endregion

        #region Fields

        private bool _isInitialized = false;

        #endregion

        #region Abstract Methods

        public abstract void OnInitialized();
        public abstract void OnClearInstance();

        #endregion

        #region Mono Behaviour

        protected virtual void Awake()
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                OnInitialized();
            }
        }

        protected virtual void OnDestroy()
        {
            if (_isInitialized)
            {
                OnClearInstance();
            }
        }

        #endregion
    }
}