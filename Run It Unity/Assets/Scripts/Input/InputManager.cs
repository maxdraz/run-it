using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputActionAsset asset;
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
