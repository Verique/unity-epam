using System;
using UnityEngine;

namespace Services
{
    public class InputManager : MonoBehaviour, IService
    {
        private Camera mainCam;
        private GameManager gameManager;
        
        public event Action<Vector2> WasdInput;
        public event Action<Vector3> MouseMoved;
        public event Action LmbHold; 
        public event Action ReloadKeyUp;
        public event Action PauseKeyUp;

        private void Start()
        {
            mainCam = ServiceLocator.Instance.Get<CameraManager>().Cam;
            gameManager = ServiceLocator.Instance.Get<GameManager>();
        }

        private static Vector2 Wasd => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        private void Update()
        {
            if (!gameManager.GamePaused)
            {
                if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out var hit))
                    MouseMoved?.Invoke(hit.point);
                WasdInput?.Invoke(Wasd);
                if (Input.GetKeyUp(KeyCode.R)) ReloadKeyUp?.Invoke();
                if (Input.GetMouseButton(0)) LmbHold?.Invoke();
            }

            if (Input.GetKeyUp(KeyCode.Escape)) PauseKeyUp?.Invoke();
            
        }
    }
}
