using System;
using RunIt.Console;
using RunIt.Detection;
using RunIt.Movement;
using RunIt.Rotation;
using UnityEngine;

namespace RunIt.Testing
{
    public class GroundMoveTest4 : ParkourBehaviour
    {
        [SerializeField] private float maxSidewaysSpeed;
        [SerializeField] private float maxForwardSpeed;
        [SerializeField] private float sidewaysAcceleration;
        [SerializeField] private float forwardAcceleration;
        [SerializeField] private float turnDelta;
        [SerializeField] private float strength;
        private float maxAcceleration;
        private float maxSpeed;
        [SerializeField] private float turnDeltaThreshold;
        [SerializeField] private float groundFriction = 10f;
        [SerializeField] private float turnFriction = 10f;
        [SerializeField] private Vector3 velocity;
        [SerializeField] private Vector3 acceleration;
        [SerializeField] private InputRotator rotator;
        [SerializeField] private Detector groundDetector;

        private void OnEnable()
        {
            groundDetector.Detected += OnGrounded;
        }
        private void OnDisable()
        {
            groundDetector.Detected -= OnGrounded;
        }

        private void Update()
        {
          //  ConsoleCommands.ClearConsole();
          if(!groundDetector.detected) return;
            var inputDir = GetLocalInputDirection();
            turnDelta = rotator.AngleDelta;
            
            var dotInputForward = Vector3.Dot(inputDir, transform.forward);
            if (dotInputForward <= 0.7f)    // if moving to sides or backwards
            {
                maxSpeed = maxSidewaysSpeed;
                maxAcceleration = sidewaysAcceleration;
            }
            else
            {
                maxSpeed = maxForwardSpeed;
                maxAcceleration = forwardAcceleration;
            }
            
            if (inputDir == Vector3.zero)   // apply friction when no input
            {
                acceleration = -velocity * groundFriction;
                if (velocity.magnitude <= 0.01f)
                {
                    velocity = Vector3.zero;
                }
            }
            else // otherwise accelerate normally
            {
                acceleration = inputDir * maxAcceleration;
            } 
            
            if (turnDelta >= turnDeltaThreshold) //if turning rapidly
            {
                // maxSpeed = slowSpeed;   //slow down the character
                acceleration = -velocity * turnFriction;
                print("clamping");
            }

            if (dotInputForward >= 0.7f)
            {
                var desired = inputDir * maxSpeed;
                var current = velocity;
                var targetVel = desired - current;
                acceleration = velocity + targetVel * strength;
            }
                
                
            velocity += acceleration * Time.deltaTime;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
            
//            print("turn delta: " + turnDelta);
        }
        
        private Vector3 GetLocalInputDirection()
        {
            velocity = rb.velocity;
            
            var inputRaw = action.ReadValue<Vector2>();
            var dir = new Vector3(inputRaw.x, 0,
                inputRaw.y).normalized;
            return transform.TransformDirection(dir);
        }

        void OnGrounded()
        {
            velocity = transform.forward * maxSpeed;
        }
    }
}