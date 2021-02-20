using System;
using RunIt.Input;
using RunIt.Settings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.UI.Menus
{
    public class MenuWindowManager : MonoBehaviour
    {
        [SerializeField] private bool pauseGame;
        private bool gamePaused;
        [SerializeField] private GameObject entryWindow;
        private InputAction pauseAction;
        [SerializeField] private string pauseActionName = "Pause";

        private void Awake()
        {
            if (!entryWindow)
            {
                entryWindow = transform.GetChild(0).gameObject;
            }
        }

        private void Start()
        {
            pauseAction = InputManager.Instance.GetAction(pauseActionName);
            pauseAction.started += OnPauseAction;
        }

        private void OnDisable()
        {
            pauseAction.started -= OnPauseAction;
        }

        void OnPauseAction(InputAction.CallbackContext ctx)
        {
            if (!pauseGame) return;
            if (!gamePaused)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }

        void SetChildrenActive(bool value)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.gameObject.SetActive(value);
            }
        }

        public void PauseGame()
        {
            CursorSettings.Instance.SetCursorMode(CursorLockMode.None);
            Time.timeScale = 0;
            gamePaused = true;
            entryWindow.SetActive(true);
        }

        public void UnPauseGame()
        {
            CursorSettings.Instance.SetCursorMode(CursorLockMode.Locked);
            Time.timeScale = 1f;
            gamePaused = false;
            SetChildrenActive(false);
        }
    }
}