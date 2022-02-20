using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class GameInitializer : MonoBehaviour
    {
        private IEnumerator Start()
        {
            // By doing this, we get rid of black scene when application starts;
            yield return null;

            // Do initialization stuff here.
            Debug.Log("Starting the game");
            SceneManager.LoadScene(1);
        }
    }
}