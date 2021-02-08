using RunIt.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Rotation
{
    public class InputRotator : MonoBehaviour
    {
        private float angle;
        [SerializeField] private bool invert;
        [SerializeField] private string actionName = "Look X";
        private InputAction action;
        [SerializeField] private float speed = 360f;
        [SerializeField] private float sensitivity = 0.1f;
        [SerializeField] private Vector3 axis = Vector3.up;
        [SerializeField] private float maxAngle;
        // Start is called before the first frame update
        private void OnValidate()
        {
            if (invert)
            {
                if (Mathf.Sign(speed) > 0)
                {
                    speed *= -1;
                }
            }
            else if(!invert)
            {
                speed *= Mathf.Sign(speed);
            }
        }

        private void Start()
        {
            action = InputManager.Instance.GetAction(actionName);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (action == null) return;
            
           var input = action.ReadValue<float>();
           angle += input * speed* sensitivity * Time.deltaTime;

           if (maxAngle > 0)
            {
                angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

            }

            Quaternion rot = Quaternion.AngleAxis(angle, axis);
            transform.localRotation = rot;
        }

        
    }
}
