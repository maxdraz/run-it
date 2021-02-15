using UnityEngine;

namespace RunIt.Settings
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
