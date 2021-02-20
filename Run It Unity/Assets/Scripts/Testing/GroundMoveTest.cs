using System;
using RunIt.Console;
using RunIt.Movement;
using UnityEngine;

namespace RunIt.Testing
{
    public class GroundMoveTest : ParkourBehaviour
    {
        [SerializeField] private float minSpeed = 1f;
        [SerializeField] private float maxSpeed = 20f;
        [SerializeField] private float sideAcceleration = 1f;
        [SerializeField] private float forwardAcceleration = 5f;
        [SerializeField] private float fricitonCoefficient = 1f;
        [SerializeField] private float scale = 2f;
        [SerializeField] private float turnStrength = 20f;
        [SerializeField] private Vector3 velocity;
        private Vector3 acceleration;
        private Vector3 inputDir;
        [SerializeField] private float sidewaysMaxSpeed;
        [SerializeField] private float forwardMaxSpeed;
        private Vector3 targetVelocity;

        private void Update()
        {
            inputDir = GetLocalInputDirection();

            //decelerate
            if (inputDir == Vector3.zero)
            {
                acceleration = FrictionForce();

                if (velocity.magnitude <= 0.01)
                {
                    velocity = Vector3.zero;
                }
            }

            //velocity += acceleration * Time.deltaTime;
            var dot = Vector3.Dot(transform.forward, velocity.normalized);
            print("dot: " + dot);
            if (dot <= 0.8f)
            {
                var current = velocity;
                var desired = inputDir * forwardAcceleration;
                targetVelocity = desired - current;
                acceleration = targetVelocity * scale;
            }
            else
            {
                acceleration = inputDir * forwardAcceleration;
            }

            velocity += acceleration * Time.deltaTime;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            transform.position += velocity * Time.deltaTime;
            
            print("speed: " + velocity.magnitude);

        }

        private Vector3 FrictionForce()
        {
            return -velocity * fricitonCoefficient;
            
        }


        private void ExecuteMovement1()
        {
            //set deceleration / acceleration
            if (inputDir == Vector3.zero)   
            {
                acceleration = -velocity * fricitonCoefficient;
                if (velocity.magnitude <= 0.01f)
                {
                    velocity = Vector3.zero;
                }
            }
            else
            { 
                var dot = Vector3.Dot(transform.forward, velocity.normalized);
                var desired = inputDir * maxSpeed;
                var current = velocity;
                var accel = desired - current;
                acceleration = accel * maxSpeed;
               
                //execute acceleration
                velocity += acceleration * Time.deltaTime;
               
                if (dot >= 0.8)
                {
                    //clamp velocity
                    velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
                    print("dot boost");
                }
                else
                {
                    //clamp velocity
                    velocity = Vector3.ClampMagnitude(velocity, minSpeed);
                }

            }
            //execute displacement
            transform.position += velocity * Time.deltaTime;

            print(velocity.magnitude);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + velocity);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + acceleration);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position + velocity, transform.InverseTransformDirection(inputDir * forwardAcceleration));
        }

        private Vector3 GetLocalInputDirection()
        {
            var inputRaw = action.ReadValue<Vector2>();
            var dir = new Vector3(inputRaw.x, 0,
                inputRaw.y).normalized;
            
            return transform.TransformDirection(dir);
        }
    }
}