using RTSGame.Concretes.Models;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTSGame.Concretes.MonoBehaviours
{
    /// <summary>
    /// Handles game initialization. you can start all the services you want to use in here.
    /// </summary>
    public class GameInitializer : MonoBehaviour
    {
        private IEnumerator Start()
        {
            // By doing this, we get rid of black scene when application starts.
            yield return null;

            // Do initialization stuff here.
            Debug.Log("Starting the game");
            SceneManager.LoadScene(Constants.SCENE_INDEXES.MAIN_MENU_SCENE);
        }
    }
}