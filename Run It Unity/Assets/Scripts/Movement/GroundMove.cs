using RunIt.Detection;
using RunIt.Input;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class GroundMove : ParkourBehaviour
    {
        [SerializeField] private float acceleration;
        [SerializeField] private float maxSpeed;
        [SerializeField] private TriggerDetector groundTriggerDetector;

        void FixedUpdate()
        {
            if (!groundTriggerDetector.detected) return; //if input not set or not grounded, do nothing
            
            ExecuteMovement();
        }

        private Vector3 GetInputDirection()
        {
            if (action == null) InputManager.Instance.GetAction(inputName);

            var input = action.ReadValue<Vector2>();
            var dir = transform.TransformDirection(new Vector3(
                input.x, 0, input.y)).normalized;

            return dir;
        }

        private void ExecuteMovement()
        {
            var inputDir = GetInputDirection();

            var velocity = rb.velocity;
            velocity += inputDir * acceleration;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            rb.velocity = velocity;
        }
    }
}
