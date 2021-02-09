using System;
using RunIt.Console;
using RunIt.Detection;
using RunIt.Movement;
using RunIt.Rotation;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

namespace RunIt.Testing
{
    public class GroundMoveTest3 : ParkourBehaviour
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float maxAcceleration;
        [SerializeField] private float friction;
        private Vector3 inputDir;
        private Vector3 acceleration;
        private Vector3 velocity;
   
        private void Update()
        {
            ConsoleCommands.ClearConsole();

            inputDir = GetLocalInputDirection();
            
            var prevVel = velocity;
            velocity = MoveGround(inputDir, prevVel);
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }

        private Vector3 GetLocalInputDirection()
        {
            var inputRaw = action.ReadValue<Vector2>();
            var dir = new Vector3(inputRaw.x, 0,
                inputRaw.y).normalized;
            
            return transform.TransformDirection(dir);
        }

        private Vector3 CalculateAcceleration(Vector3 currentVelocity,Vector3 accelDir,float accel, float maxVel)
        {
            var projVel = Vector3.Dot(currentVelocity, accelDir);
            var accelVel = accel;
            if (projVel + accel > maxVel)
            {
                accelVel = maxSpeed - projVel;
            }
            
            return currentVelocity + accelDir * accelVel;
        }
        
        private Vector3 Accelerate(Vector3 accelDir, Vector3 prevVelocity, float accelerate, float max_velocity)
        {
            float projVel = Vector3.Dot(prevVelocity, accelDir); // Vector projection of Current velocity onto accelDir.
            float accelVel = accelerate * Time.deltaTime; // Accelerated velocity in direction of movment

            // If necessary, truncate the accelerated velocity so the vector projection does not exceed max_velocity
            if(projVel + accelVel > max_velocity)
                accelVel = max_velocity - projVel;

            return prevVelocity + accelDir * accelVel;
        }

        private Vector3 MoveGround(Vector3 accelDir, Vector3 prevVelocity)
        {
            // Apply Friction
            float speed = prevVelocity.magnitude;
            if (speed != 0) // To avoid divide by zero errors
            {
                float drop = speed * friction * Time.deltaTime;
                prevVelocity *= Mathf.Max(speed - drop, 0) / speed; // Scale the velocity based on friction.
            }

            // ground_accelerate and max_velocity_ground are server-defined movement variables
            return Accelerate(accelDir, prevVelocity, maxAcceleration, maxSpeed);
        }

    }
}