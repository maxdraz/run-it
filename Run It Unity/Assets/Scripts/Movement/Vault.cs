using System;
using FMOD;
using RunIt.Detection;
using UnityEngine;

namespace RunIt.Movement
{
    public class Vault : ParkourBehaviour
    {
        [SerializeField] private RaycastDetector vaultDetector;
        private bool vaultStarted;
        private Vector3 endPos;
        [SerializeField] private float upOffset;
        [SerializeField] private float forwardOffset;
        [SerializeField] private float vaultSpeed = 3f;
        private Vector3 prevVelocity;

        private void OnEnable()
        {
            vaultDetector.RayStay += OnWallStay;
        }

        private void OnDisable()
        {
            vaultDetector.RayStay -= OnWallStay;
        }

        private void Update()
        {
            if (vaultStarted)
            {
                rb.isKinematic = true;
                var toTarget = endPos - transform.position;
                
                transform.Translate(toTarget.normalized * (vaultSpeed * Time.deltaTime), Space.World);

                if (toTarget.magnitude <= 0.01f)
                {
                    rb.isKinematic = false;
                    vaultStarted = false;
                    //rb.velocity = prevVelocity;
                }
            }
        }

        void OnWallStay(Ray ray,RaycastHit hit)
        {
            if (vaultStarted || action.ReadValue<float>() <=0) return;

            SetVaultProperties(hit);
            vaultStarted = true;

            //endPos = hit.point + new Vector3(0, 2, 0);
        }

        private void SetVaultProperties(RaycastHit hit)
        {
            print("called");
            //set endPos
            var hitPos = hit.point;
            var maxY = hit.collider.bounds.max.y;
            var colliderCenter = hit.collider.bounds.center;

            var newY = hitPos.y + 2;

            prevVelocity = rb.velocity;
            
            endPos = new Vector3(hitPos.x, maxY + forwardOffset, hitPos.z) + transform.forward * forwardOffset;

        }

        private void OnDrawGizmos()
        {
            if (endPos == Vector3.zero) return;
            Vector3 vec = new Vector3(0, 0, 1);
            Gizmos.DrawSphere(endPos + transform.forward, 0.5f);
        }
    }
}