using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class GameManager : SingletonManager<GameManager>
    {
        #region Fields

        [SerializeField] private Controller[] _mainMenuSceneControllers;
        [SerializeField] private Controller[] _battleSceneControllers;
        [SerializeField] private Controller[] _staticControllers;

        public IUnitCollection PlayerCollection { get; private set; }
        public IUnitCollection PlayerDeck { get; private set; }

        public bool IsInputActive { get; private set; } = true;
        public int PlayCount { get; private set; } = 0;

        #endregion

        #region Abstract Methods

        public override void OnInitialized()
        {
            DontDestroyOnLoad(this);

            PlayerCollection = new PlayerCollection();
            PlayerDeck = new PlayerDeck();

            SceneManager.sceneLoaded += OnSceneLoaded;

            foreach (var controller in _staticControllers)
            {
                var controllerInstance = Instantiate(controller);
                controllerInstance.Initialize();
            }
        }

        public override void OnClearInstance()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

        }

        #endregion

        #region Helper Methods

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            switch (arg0.buildIndex)
            {
                case Constants.SCENE_INDEXES.MAIN_MENU_SCENE:
                    foreach (var controller in _mainMenuSceneControllers)
                    {
                        var controllerInstance = Instantiate(controller);
                        controllerInstance.Initialize();
                    }
                    break;

                case Constants.SCENE_INDEXES.BATTLE_SCENE:
                    foreach (var controller in _battleSceneControllers)
                    {
                        var controllerInstance = Instantiate(controller);
                        controllerInstance.Initialize();
                    }
                    break;
            }
        }

        #endregion

        #region Public Methods

        public void SetInputActive(bool value)
        {
            IsInputActive = value;
        }

        public void IncreasePlayCount()
        {
            PlayCount++;
        }

        #endregion
    }
}