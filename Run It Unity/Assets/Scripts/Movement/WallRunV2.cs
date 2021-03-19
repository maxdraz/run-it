using System;
using RunIt.Detection;
using UnityEngine;

namespace RunIt.Movement
{
    public class WallRunV2 : ParkourBehaviour
    {
        [SerializeField] private SphereCaster groundDetector;
        [SerializeField] private SphereCaster wallDetector;
        [SerializeField] private bool canWallRun;
        [SerializeField] private bool completeSetup;
        private float timer;
        [SerializeField] private float deceleration;
        private Vector3 incomingVelocity;
        
        private void OnEnable()
        {
            groundDetector.Enter += OnGrounded;
        }

        private void OnDisable()
        {
            groundDetector.Enter -= OnGrounded;
        }

        private void Update()
        {
            if(action == null) return;
            // if holding jump
            if (!wallDetector.Detected || !groundDetector.Detected) return;
            if(action.ReadValue<float>() <= 0) return;
            SetupWallRun();
            //speed based on speed, min speed

            if (!canWallRun) return;
            
            ExecuteWallRun();
        }

        

        private void SetupWallRun()
        {
            if (completeSetup) return;
            
            canWallRun = true;
            incomingVelocity = rb.velocity;

            completeSetup = true;

        }
        
        private void ExecuteWallRun()
        {
            print("executing wall run");
            var newVel = Vector2.up * incomingVelocity.magnitude;
            newVel += -newVel.normalized * (deceleration * Time.deltaTime);
            if (newVel.magnitude <= 0.1f)
            {
                canWallRun = false;
            }
            rb.velocity = newVel;
        }

        void OnGrounded(RaycastHit hit)
        {
            canWallRun = false;
            completeSetup = false;
        }
    }
}