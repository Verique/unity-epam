using System;
using UnityEngine;

namespace Services
{
    public class InputManager : MonoBehaviour, IService
    {
        private Camera mainCam;
        
        public event Action<Vector2> WasdInput;
        public event Action<Vector3> MouseMoved;
        public event Action LMBHold; 
        public event Action ReloadKeyUp;

        private void Start()
        {
            mainCam = ServiceLocator.Instance.Get<CameraManager>().Cam;
        }

        private static Vector2 Wasd => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        private void Update()
        {
            if (Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out var hit)) MouseMoved?.Invoke(hit.point);
            WasdInput?.Invoke(Wasd);
            if (Input.GetKeyUp(KeyCode.R)) ReloadKeyUp?.Invoke();
            if (Input.GetMouseButton(0)) LMBHold?.Invoke();
            
        }
    }
}
