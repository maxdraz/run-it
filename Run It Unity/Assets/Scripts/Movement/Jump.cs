using System;
using RunIt.Audio;
using RunIt.Detection;
using RunIt.Testing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Movement
{
    public class Jump : ParkourBehaviour
    {
        [SerializeField] private float jumpForceMagnitude;
        [SerializeField] private Detector groundDetector;
        [SerializeField] private FMODEventPlayer jumpSound;
        private bool inputLetGo = true;
        private bool canJump = true;
        private GroundMoveTest4 groundMove;
        

        protected override void Start()
        {
            base.Start();
            action.started += OnInputStarted;
            action.canceled += OnInputCanceled;

            groundMove = GetComponent<GroundMoveTest4>();
        }

        private void OnDisable()
        {
            action.started -= OnInputStarted;
            action.canceled -= OnInputCanceled;
        }

        private void Update()
        {
            //canJump = CheckIfCanJump();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            canJump = CheckIfCanJump();
            if(!canJump) return;
            
            ExecuteJump();
            inputLetGo = false;
        }

        private void ExecuteJump()
        {
            var jumpForce = new Vector3(0, jumpForceMagnitude, 0);
            //rb.AddForce(transform.forward + jumpForce, ForceMode.Impulse);
            var mag = rb.velocity.magnitude;
            var jumpVel = ((groundMove.InputDir)*mag) + new Vector3(0, jumpForceMagnitude, 0);
            rb.velocity = jumpVel;
        }

        private void OnInputStarted(InputAction.CallbackContext ctx)
        {
            if (groundDetector.detected)
            {
                jumpSound.Play();
            }
        }
        private void OnInputCanceled(InputAction.CallbackContext ctx)
        {
            inputLetGo = true;
        }

       
        private bool CheckIfCanJump()
        {
            var input = action.ReadValue<float>();
            return groundDetector.detected && input > 0 && inputLetGo;
        }
        
        public void ExecuteJump(Vector3 jumpVelocity, bool needsGrounded)
        {
            if (needsGrounded)
            {
                if (!groundDetector.detected) return;
            }
          
            rb.velocity = jumpVelocity;

        }
    }
}
