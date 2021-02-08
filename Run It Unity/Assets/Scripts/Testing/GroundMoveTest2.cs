using System;
using RunIt.Movement;
using UnityEngine;

namespace RunIt.Testing
{
    public class GroundMoveTest2 : ParkourBehaviour
    {
        [SerializeField] private float maxSpeed = 5f;
        private Vector3 velocity;
        private void FixedUpdate()
        {
            var input = GetLocalInputDirection();
            var current = velocity;
            var desired = input * maxSpeed;
            var delta = desired - current;
            var acceleration = delta / Time.deltaTime;
            velocity += acceleration;
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