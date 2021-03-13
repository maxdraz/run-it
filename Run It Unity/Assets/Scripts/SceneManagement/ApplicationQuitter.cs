using System;
using RunIt.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.SceneManagement
{
    public class ApplicationQuitter : MonoBehaviour
    {
        [SerializeField] private string quitActionName;
        private InputAction quitAction;

        private void OnEnable()
        {
            quitAction = InputManager.Instance.GetAction(quitActionName);

            quitAction.canceled += OnQuit;
        }

        private void OnDisable()
        {
            quitAction.canceled -= OnQuit;
        }

        private void OnQuit(InputAction.CallbackContext ctx)
        {
            Application.Quit();
        }
    }
}