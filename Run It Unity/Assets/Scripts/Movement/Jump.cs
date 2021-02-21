using System;
using RunIt.Audio;
using RunIt.Detection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Movement
{
    public class Jump : ParkourBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Detector groundDetector;
        [SerializeField] private FMODEventPlayer jumpSound;
        [SerializeField] private bool inputLetGo = true;
        [SerializeField] private bool canJump = true;

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
            print("jumped");
            var jumpForce = new Vector3(0, speed, 0);
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }

        private void OnInputStarted(InputAction.CallbackContext ctx)
        {
            if (groundDetector.detected)
            {
                jumpSound.Play();
            }

           // inputLetGo = false;
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
    }
}
