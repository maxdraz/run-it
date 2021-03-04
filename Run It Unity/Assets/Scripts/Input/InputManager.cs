using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Input
{
    public class InputManager : MonoBehaviour
    {
        private Controls controls;
        private InputActionAsset asset;
        public static InputManager Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            controls = new Controls();
            asset = controls.asset;
            asset.Enable();
        }
        private void OnEnable()
        {
            if (!asset.enabled)
            {
                asset.Enable();
            }
        }

        private void OnDisable()
        {
            asset.Disable();
        }
        
        public InputAction GetAction(string actionName)
        {
            return asset.FindAction(actionName);
        }
    }
}
