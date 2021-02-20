using System;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Settings
{
    public class CursorSettings : MonoBehaviour
    {
        public static CursorSettings Instance;
        [SerializeField] private CursorLockMode defaultMode;

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
            
            SetCursorMode(defaultMode);
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void SetCursorMode(CursorLockMode mode)
        {
            Cursor.lockState = mode;
        }
        
    }
}
