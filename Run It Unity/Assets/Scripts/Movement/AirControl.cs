using RunIt.Detection;
using UnityEngine;

namespace RunIt.Movement
{
    public class AirControl : ParkourBehaviour
    {
        [SerializeField] private float acceleration = 0.2f;
        [SerializeField] private float maxSpeed;
        [SerializeField] private Detector groundDetector;
        
        void FixedUpdate()
        {
            if (groundDetector.detected) return; //if input not set and grounded, do nothing
            
            var inputDir = GetInputDirection();
            rb.velocity += inputDir * acceleration;
        }

        private Vector3 GetInputDirection()
        {
            if(action == null) return Vector3.zero;

            var input = action.ReadValue<Vector2>();
            var dir = transform.TransformDirection(new Vector3(
                input.x, 0, input.y)).normalized;

            return dir;
        }
    }
}