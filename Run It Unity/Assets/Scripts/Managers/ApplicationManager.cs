using System;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace RunIt.Managers
{
    public class ApplicationManager : MonoBehaviour
    {
        public static ApplicationManager Instance;

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


        public void QuitGame()
        {
            if (Application.isEditor)
            {
                EditorApplication.isPlaying = false;
            }
            else
            {
                Application.Quit(); 
            }
            
        }
    }
}