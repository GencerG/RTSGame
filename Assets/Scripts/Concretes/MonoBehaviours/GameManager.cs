using RTSGame.Abstracts.Models;
using RTSGame.Abstracts.MonoBehaviours;
using RTSGame.Concretes.Models;
using UnityEngine;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class GameManager : SingletonManager<GameManager>
    {
        #region Fields

        [SerializeField] private Controller[] _controllers;

        public IUnitCollection PlayerCollection { get; private set; }
        public IUnitCollection PlayerDeck { get; private set; }

        #endregion

        #region Abstract Methods

        public override void OnInitialized()
        {
            DontDestroyOnLoad(this);

            PlayerCollection = new PlayerCollection();
            PlayerDeck = new PlayerDeck();

            foreach (var controller in _controllers)
            {
                var controllerInstance = Instantiate(controller);
                controllerInstance.Initialize();
            }
        }


        public override void OnClearInstance()
        {

        }

        #endregion

        #region Helper Methods

        #endregion
    }
}