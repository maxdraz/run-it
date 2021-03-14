using System;
using RunIt.Audio;
using RunIt.Detection;
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
        

        protected override void Start()
        {
            base.Start();
            action.started += OnInputStarted;
            action.canceled += OnInputCanceled;
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
            rb.AddForce(transform.forward + jumpForce, ForceMode.Impulse);
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
