using System;
using RunIt.Detection;
using RunIt.Movement;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

namespace RunIt.Testing
{
    public class GroundMoveTest3 : ParkourBehaviour
    {
        private Vector3 acceleration;
        private float maxSpeed;
        private float maxAcceleration;

        private Vector3 velocity;
        private void Update()
        {
            var inputDir = GetLocalInputDirection();

            acceleration = CalculateAcceleration(inputDir, maxSpeed, maxAcceleration);

            velocity += acceleration * Time.deltaTime;
            velocity.y = rb.velocity.y;
            
            rb.velocity = velocity;
        }

        private Vector3 CalculateAcceleration(Vector3 direction, float desiredSpeed, float accel )
        {
            

            var currentSpeed = Vector3.Dot(velocity, direction);
            var addSpeed = desiredSpeed - currentSpeed;

            if (addSpeed <= 0)  // going where we want at the speed we want
            {
                return direction * accel;
            }
            
            return Vector3.zero;
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