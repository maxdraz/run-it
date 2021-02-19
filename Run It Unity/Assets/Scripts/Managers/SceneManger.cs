using System;
using RunIt.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Managers
{
    public class SceneManger : MonoBehaviour
    {
        public static SceneManger Instance;
        private InputAction action;
        private int currentSceneIndex;

        [SerializeField] private string restartActionName;

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
        }

        void Start()
        {
            currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            
            action = InputManager.Instance.GetAction(restartActionName);
            action.started += OnRestart;
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            action.started -= OnRestart;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void LoadNextScene()
        {
           if(!CanLoadNextScene()) return;
            currentSceneIndex++;
            LoadScene(currentSceneIndex);
        }

        private void OnRestart(InputAction.CallbackContext ctxt)
        {
            LoadScene(currentSceneIndex);
        }

        private void LoadScene(int index)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
        }

        public bool CanLoadNextScene()
        {
            if (currentSceneIndex <= UnityEngine.SceneManagement.SceneManager.sceneCount - 1)
            {
                return true;
            }
            else return false;
        }
    }
}