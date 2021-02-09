using System;
using RunIt.Console;
using RunIt.Input;
using RunIt.Movement;
using RunIt.Rotation;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Testing
{
    public class GroundMoveTest2 : ParkourBehaviour
    {
        [SerializeField] private float forwardAcceleration;
        [SerializeField] private float sideAcceleration;
        private float maxSpeed;
        [SerializeField] private float slowDownScale;
        [SerializeField] private float forwardMaxSpeed;
        [SerializeField] private float sideMaxSpeed;
        [SerializeField] private float fricitonCoefficient = 10f;
        [SerializeField] private Vector3 velocity;
        [SerializeField] private Vector3 acceleration;
        private InputAction scaleAction;
        [SerializeField] private InputRotator rotator;
        protected override void Start()
        {
            base.Start();
            scaleAction = InputManager.Instance.GetAction("Scale");
        }

        private void Update()
        {
            ConsoleCommands.ClearConsole();
            var inputDir = GetLocalInputDirection();

            print(rotator.AngleDelta);
            
            //dot
            var dot = Vector3.Dot(inputDir, transform.forward);

            if (dot <= 0.7f)
            {
                //apply different accelerations
                //apply different maxSpeeds
                inputDir *= sideAcceleration;
                maxSpeed = sideMaxSpeed;
            }
            else
            {
                //apply normal acceleration
                //aplly normal maxSPeed
                inputDir *= forwardAcceleration;
                maxSpeed = forwardMaxSpeed;
            }
            
            var dotVelocity = Vector3.Dot(transform.forward, velocity.normalized);
            
            print("dot velocity: " + dotVelocity);
            
            
            acceleration = inputDir;
            
            
            if (inputDir == Vector3.zero)   
            {
                acceleration = -velocity * fricitonCoefficient;
                if (velocity.magnitude <= 0.01f)
                {
                    velocity = Vector3.zero;
                }
            }
            if (velocity.magnitude >= 1 && dotVelocity <= 0.8f)
            {
                acceleration = -velocity * slowDownScale;
            }
            
            velocity += acceleration * Time.deltaTime;
            
           
            
            print("velocity magnitude: " + velocity.magnitude);
            print("angle delta: " + rotator.AngleDelta);
            
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }
        
        private Vector3 GetLocalInputDirection()
        {
            velocity = rb.velocity;
            
            var inputRaw = action.ReadValue<Vector2>();
            var dir = new Vector3(inputRaw.x, 0,
                inputRaw.y).normalized;
            return transform.TransformDirection(dir);
        }

        private void OnCollisionStay(Collision other)
        {
            //code for sliding along wall when running into it
        }
    }
}