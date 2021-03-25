using System;
using FMOD;
using RunIt.Detection;
using UnityEngine;

namespace RunIt.Movement
{
    public class Vault : ParkourBehaviour
    {
        [SerializeField] private RaycastDetector lowWallDetector;
        [SerializeField] private RaycastDetector highWallDetector;
        [SerializeField] private Detector groundDetector;
        private bool vaultStarted;
        private Vector3 endPos;
        [SerializeField] private float upOffset;
        [SerializeField] private float forwardOffset;
        [SerializeField] private float baseVaultSpeed = 3f;
        private Vector3 prevVelocity;
        [SerializeField] private float heightThreshold;
        [SerializeField] private float lowWallFallThreshold;
        [SerializeField] private float highWallFallThreshold;
        [SerializeField] private float minDistToEndPoint = 0.01f;

        private void OnEnable()
        {
            lowWallDetector.RayStay += OnWallStay2;
            highWallDetector.RayStay += OnHighWallStay;
        }

        private void OnDisable()
        {
            lowWallDetector.RayStay -= OnWallStay2;
            highWallDetector.RayStay -= OnHighWallStay;
        }

        private void Update()
        {
            if (vaultStarted)
            {
                rb.isKinematic = true;
                var toTarget = endPos - transform.position;

                var vaultSpeed = Mathf.Max(baseVaultSpeed, prevVelocity.magnitude);
                
                transform.Translate(toTarget.normalized * (baseVaultSpeed * Time.deltaTime), Space.World);

                if (toTarget.magnitude <= minDistToEndPoint)
                {
                    rb.isKinematic = false;
                    vaultStarted = false;
                   //rb.velocity = transform.forward * prevVelocity.magnitude;
                }
            }
        }

        void OnWallStay(Ray ray,RaycastHit hit)
        {
            if (vaultStarted || action.ReadValue<float>() <=0) return;

            SetVaultProperties(hit);
            vaultStarted = true;
        }
        void OnWallStay2(Ray ray,RaycastHit hit)
        {
            if (vaultStarted || groundDetector.detected || rb.velocity.y < -lowWallFallThreshold) return; // dflt 0.6f
            var isWallVaultable = IsWallValutable(hit);
            if(!isWallVaultable) return;
            
            SetVaultProperties(hit);
            vaultStarted = true;
        }
        
        void OnHighWallStay(Ray ray,RaycastHit hit)
        {
            if (vaultStarted || groundDetector.detected || rb.velocity.y < -highWallFallThreshold) return; // dflt -2f
            var isWallVaultable = IsWallValutable(hit);
            if(!isWallVaultable) return;
            
            SetVaultProperties(hit);
            vaultStarted = true;
        }

        private bool IsWallValutable(RaycastHit hit)
        {
            var colliderY = hit.collider.bounds.max.y;
            var hitPosY = hit.point.y;

            var distToTop = colliderY - hitPosY;

            if (distToTop > heightThreshold)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetVaultProperties(RaycastHit hit)
        {
            //set endPos
            var hitPos = hit.point;
            var maxY = hit.collider.bounds.max.y;

            prevVelocity = rb.velocity;
            
            endPos = new Vector3(hitPos.x, maxY + upOffset, hitPos.z) + (transform.forward * forwardOffset);

        }
        

        private void OnDrawGizmos()
        {
            if (endPos == Vector3.zero) return;
            Gizmos.DrawSphere(endPos, 0.5f);
        }
    }
}