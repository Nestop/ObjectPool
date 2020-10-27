using UnityEngine;
using Utils;

namespace Game.Managers
{
    public class GameManager : MBSingleton<GameManager>
    {
        [SerializeField] private Camera mainCamera;
        public Camera MainCamera => mainCamera;

        protected override void OnSingletonAwake()
        {
            DontDestroyOnLoad(gameObject);

            if (mainCamera != null) return;
            
            var cam = new GameObject("MainCamera");
            mainCamera = cam.AddComponent<Camera>();
        }
    }
}