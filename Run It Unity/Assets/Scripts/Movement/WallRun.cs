using System;
using System.Collections;
using RunIt.Audio;
using RunIt.Detection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Movement
{
    public class WallRun : ParkourBehaviour
    {
        [SerializeField] private Detector wallDetector;
        [SerializeField] private Detector groundDetector;
        [SerializeField] private float scale;
        [SerializeField] private bool canWallrun;
        [SerializeField] private float fallThreshold;
        [SerializeField] private FMODEventPlayer wallrunSound;
       

        private void OnEnable()
        {
            StartCoroutine(SubscribeToInputCoroutine());
            groundDetector.Enter += OnGrounded;
        }

        private void OnDisable()
        {
            action.started -= ExecuteWallRun;
            groundDetector.Enter -= OnGrounded;
        }

        private void ExecuteWallRun(InputAction.CallbackContext ctx)
        {
            if (!wallDetector.detected || groundDetector.detected || !canWallrun) return;

            var velocity = rb.velocity;
            
            if (velocity.y <= -fallThreshold) return;   // if we are falling too much, dont wall run
            velocity.y = 0;

            var direction =Quaternion.AngleAxis(20f, Vector3.right) * Vector3.up;
            direction = transform.TransformDirection(direction);

            rb.velocity = direction * velocity.magnitude * scale;

            canWallrun = false;
            
            //play sound
            wallrunSound.Play();
            
        }

        protected override IEnumerator SubscribeToInputCoroutine()
        {
            yield return new WaitForEndOfFrame();
            action.started += ExecuteWallRun;
        }

        private void OnGrounded(Collider other)
        {
            canWallrun = true;
        }
    }
}