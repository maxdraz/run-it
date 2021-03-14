using RunIt.Detection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Movement
{
    public class Jumper : ParkourBehaviour
    {
        [SerializeField] private SphereCaster groundDetector;
        [SerializeField] private float jumpSpeed;
        protected override void Start()
        {
            base.Start();
            action.started += OnJump;
        }

        private void OnDisable()
        {
            action.started -= OnJump;
        }


        private void OnJump(InputAction.CallbackContext ctx)
        {
            if(!groundDetector.Detected) return;
            
            var rbVelocity = rb.velocity;
            var newVel = new Vector3(rbVelocity.x, jumpSpeed, rbVelocity.z);
            rb.velocity = newVel;
        }

        public void Jump(Vector3 jumpVelocity, bool needsGrounded)
        {
            if (needsGrounded)
            {
                if (!groundDetector.Detected) return;
            }
          
            rb.velocity = jumpVelocity;

        }
    }
}