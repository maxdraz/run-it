using System;
using RunIt.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Testing
{
    public class VelocityTest : MonoBehaviour
    {
        [SerializeField] private string inputName;
        private InputAction action;
        private Rigidbody rb;
        [SerializeField] private float angle;
        [SerializeField] private float speed;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            action = InputManager.Instance.GetAction(inputName);

            action.started += OnJump;
        }

        private void OnDisable()
        {
            action.started -= OnJump;
        }

        void OnJump(InputAction.CallbackContext ctx)
        {
            print("jumped");
           var newVel = RotateVector(transform.forward, angle, transform.right);

            rb.velocity = newVel * speed;
        }
        
        private Vector3 RotateVector(Vector3 vector, float angle, Vector3 axis)
        {
            var rot = Quaternion.AngleAxis(angle, axis);
            vector = rot * vector;

            return vector;
        }
    }
}