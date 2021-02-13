using System;
using RunIt.Detection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Movement
{
    public class WallSlide : ParkourBehaviour
    {
        [SerializeField] private Detector wallSlideDetector;
        [SerializeField] private Detector groundDetector;
        [SerializeField] private float wallFriction;
        [SerializeField] private float jumpOffForce;
        [SerializeField] private bool canJumpOff;
        private GameObject currentWall;

        private void OnEnable()
        {
            groundDetector.Enter += OnGroundEnter;
            wallSlideDetector.Enter += OnWallEnter;
        }

        private void OnDisable()
        {
            groundDetector.Enter -= OnGroundEnter;
            wallSlideDetector.Enter -= OnWallEnter;
        }

        private void FixedUpdate()
        {
            if (!wallSlideDetector.detected || rb.velocity.y >= -2f) return;

            var velocity = rb.velocity;
            velocity.y += wallFriction;
            rb.velocity = velocity;
            
            if (action.ReadValue<float>() > 0 && canJumpOff)
            {
                JumpFromWall();
                canJumpOff = false;
            }
            
        }

        private void JumpFromWall()
        {
            var jumpDirection = Quaternion.AngleAxis(-40f, Vector3.right) * Vector3.forward;
            jumpDirection = transform.TransformDirection(jumpDirection);
            rb.AddForce(jumpDirection * jumpOffForce, ForceMode.Force);
        }

        private void OnGroundEnter(Collider other)
        {
            canJumpOff = true;
            currentWall = null;
        }

        private void OnWallEnter(Collider other)
        {
            if (other.gameObject != currentWall)
            {
                canJumpOff = true;
                currentWall = other.gameObject;
            }
        }
        
        
    }
}