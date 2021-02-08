using System;
using System.Collections;
using RunIt.Detection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Movement
{
    public class WallRun : ParkourBehaviour
    {
        [SerializeField] private Detector wallDetector;
        [SerializeField] private Detector groundDetector;
        private void FixedUpdate()
        {
            
        }

        private void OnEnable()
        {
            StartCoroutine(SubscribeToInputCoroutine());
        }

        private void OnDisable()
        {
            action.started -= ExecuteWallRun;
        }

        private void ExecuteWallRun(InputAction.CallbackContext ctx)
        {
            if (!wallDetector.detected|| groundDetector.detected) return;

            var velocity = rb.velocity;

            var direction =Quaternion.AngleAxis(20f, Vector3.right) * Vector3.up;
            direction = transform.TransformDirection(direction);

            rb.velocity = direction * velocity.magnitude;
        }

        protected override IEnumerator SubscribeToInputCoroutine()
        {
            yield return new WaitForEndOfFrame();
            action.started += ExecuteWallRun;
        }
    }
}