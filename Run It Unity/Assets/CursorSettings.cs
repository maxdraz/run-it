using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunIt
{
    public class CursorSettings : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
