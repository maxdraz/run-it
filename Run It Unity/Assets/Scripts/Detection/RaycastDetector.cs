using System;
using UnityEditor.PackageManager;
using UnityEngine;

namespace RunIt.Detection
{
    public class RaycastDetector : Detector
    {
        [SerializeField] private float rayLength;
        [SerializeField] private LayerMask toDetect;
        private Ray ray;
        private RaycastHit hitInfo;
        [SerializeField] private bool firstHit;

        private void Start()
        {
            var trans = transform;
            ray = new Ray(trans.position, trans.forward);
        }

        private void Update()
        {
            ray.origin = transform.position;
            ray.direction = transform.forward;

            var hit = CastRayOnce();

            if (hit == true)
            {
                if (firstHit) return;
                print("hit once");
                firstHit = true;
            }
            else
            {
                firstHit = false;
            }
            
            CastRayContinuously();
            
        }

        private bool CastRayOnce()
        {
            return Physics.Raycast(ray, out hitInfo, rayLength, toDetect);
        }
        
        private void CastRayContinuously()
        {
            if (Physics.Raycast(ray, out hitInfo, rayLength, toDetect))
            {
                
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * rayLength);
            Gizmos.DrawSphere(hitInfo.point, 0.5f);
        }
    }
}