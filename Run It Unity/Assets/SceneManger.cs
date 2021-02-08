using System;
using System.Collections;
using System.Collections.Generic;
using RunIt.Input;
using RunIt.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt
{
    public class SceneManger : MonoBehaviour
    {
        private InputAction action;
        private int currentScene;

        [SerializeField] private string restartActionName;
        // Start is called before the first frame update
        void Start()
        {
            currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            
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

        private void OnRestart(InputAction.CallbackContext ctxt)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
        }
    }
}
