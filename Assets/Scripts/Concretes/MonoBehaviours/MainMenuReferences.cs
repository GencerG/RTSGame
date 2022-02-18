using UnityEngine;
using UnityEngine.UI;

namespace RTSGame.Concretes.MonoBehaviours
{
    public class MainMenuReferences : MonoBehaviour
    {
        [SerializeField] private Transform _gridLayout;
        public Transform GridLayout { get { return _gridLayout; } }

        [SerializeField] private Button _battleButton;
    }
}