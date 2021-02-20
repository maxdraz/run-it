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
        private bool canJump;

        protected override void Start()
        {
            base.Start();
            action.started += OnJump;
        }

        private void OnDisable()
        {
            action.started -= OnJump;
        }

        private void Update()
        {
            //canJump = groundDetector.detected;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(!groundDetector.detected || action.ReadValue<float>() <= 0) return;
            
            var jumpForce = new Vector3(0, speed, 0);
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }

        private void OnJump(InputAction.CallbackContext ctx)
        {
           // if(!groundDetector.detected) return;
             //   jumpSound.Play();
            //    print("playing oonce");
           // }
        }
    }
}
