using System;
using RunIt.Input;
using RunIt.Settings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Rotation
{
    public class InputRotator : MonoBehaviour
    {
        [SerializeField] private bool playerControlled;
        private float angle;
        [SerializeField] private bool invert;
        [SerializeField] private string actionName = "Look X";
        private InputAction action;
        [SerializeField] private float speed = 360f;
        [SerializeField] private float sensitivity = 0.1f;
        [SerializeField] private Vector3 axis = Vector3.up;
        [SerializeField] private float maxAngle;
        private float angleDelta;
        public float AngleDelta => Mathf.Abs(angleDelta);

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
            angle = Quaternion.Angle(Quaternion.identity, transform.localRotation);
        }

        private void OnEnable()
        {
            if (!playerControlled) return;
            SensitivitySettings.Instance.ValueChanged += OnSensitivityChanged;
        }

        private void OnDisable()
        {
            if (!playerControlled) return;
            SensitivitySettings.Instance.ValueChanged -= OnSensitivityChanged;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (action == null) return;
            var prevAngle = angle;
            var input = action.ReadValue<float>();
           angle += input * speed* sensitivity * Time.deltaTime;

           if (maxAngle > 0)
            {
                angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

            }

            Quaternion rot = Quaternion.AngleAxis(angle, axis);
            transform.localRotation = rot;

            angleDelta = angle - prevAngle;
        }

        private void OnSensitivityChanged(float value)
        {
            print("recieved message");
            sensitivity = value;
        }
    }
}
