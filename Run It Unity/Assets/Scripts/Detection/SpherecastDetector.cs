using System;
using UnityEngine;

namespace RunIt.Detection
{
    public class SpherecastDetector : Detector
    {
        [SerializeField] private float radius = 1f;
        [SerializeField] private float length = 2f;
        private RaycastHit hitInfo;
        
        private void Update()
        {
            if (Physics.SphereCast(transform.position, radius, transform.forward, out hitInfo, length))
            {
                
            }
            
            
        }

        private void OnDrawGizmos()
        {
            if(!drawGizmos) return;
            Gizmos.color = Color.yellow;
            var center = transform.TransformPoint(Vector3.forward * length);
            Gizmos.DrawWireSphere(center, radius);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(hitInfo.point, hitInfo.point +  hitInfo.normal);
        }
    }
}