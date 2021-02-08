using System;
using RunIt.Input;
using RunIt.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Testing
{
    public class FrictionTest : MonoBehaviour
    {
        [SerializeField] private Vector3 gravity;
        [SerializeField] private float frictionCoefficient;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float sideSpeed;
 
        [SerializeField] private Vector3 velocity;
        private InputAction action;
        private Rigidbody rb;

        private void Start()
        {
            action = InputManager.Instance.GetAction("Movement");
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var inputRaw = action.ReadValue<Vector2>();
            var inputDir = new Vector3(inputRaw.x, 0,
                inputRaw.y).normalized;

            var moveForce = inputDir;
            moveForce.x *= sideSpeed;
            moveForce.z *= moveSpeed;

            var frictionForce = Vector3.zero;
            if (inputDir == Vector3.zero)
            {
                frictionForce = -velocity * frictionCoefficient;
                print("friciton");
                velocity += frictionForce * Time.deltaTime;

                if (velocity.magnitude < 0.01f)
                {
                    print("complete stop");
                    velocity = Vector3.zero;
                }
            }
            else
            {
                var desired = moveForce;
                var current = velocity;
                var toAdd = desired - current;
                velocity += toAdd * Time.deltaTime; 
                print("moving");
            }

            
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            velocity = transform.TransformDirection(velocity);
           // rb.velocity = new Vector3(velocity.x, rb.velocity.y,velocity.z);
           transform.position += velocity * Time.deltaTime;
        }
    }
}