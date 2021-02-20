using System;
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
            Application.Quit();
        }
    }
}