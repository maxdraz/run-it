using System;
using UnityEngine;

namespace RunIt.Detection
{
    public class RaycastDetector : Detector
    {
        [SerializeField] private Color gizmoColor;
        [SerializeField] private float rayLength;
        [SerializeField] private bool firstHit;
        [SerializeField] private LayerMask toDetect;
        private Ray ray;
        private RaycastHit hitInfo;
        


        private void Reset()
        {
            gizmoColor = Color.red;
            rayLength = 1;
        }

        //raycast specific events
        public delegate void RayEnterHandler(Ray ray, RaycastHit hit);
        public delegate void RayStayHandler(Ray ray,RaycastHit hit);
        public delegate void RayExitHandler(Ray ray,RaycastHit hit);

        public event RayEnterHandler RayEnter;
        public event RayStayHandler RayStay;
        public event RayExitHandler RayExit;

        private void Start()
        {
            var trans = transform;
            ray = new Ray(trans.position, trans.forward);
        }

        private void Update()
        {
            var t = transform;
            ray.origin = t.position;
            ray.direction = t.forward;

            //var hit = CastRayOnce();


            if (Physics.Raycast(ray, out hitInfo, rayLength, toDetect))
            {
                if (firstHit == false) // first hit
                {
                    RayEnter?.Invoke(ray, hitInfo);
                    detected = true;
                    firstHit = true;
                }
                
                //conmntinuous event
                RayStay?.Invoke(ray, hitInfo);
            }
            else
            {
                if (firstHit)
                {
                    RayExit?.Invoke(ray, hitInfo);
                }

                detected = false;
                firstHit = false;
            }
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
            if (!drawGizmos) return;
            Gizmos.color = gizmoColor;
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * rayLength);
            Gizmos.DrawSphere(hitInfo.point, 0.2f);
        }
    }
}