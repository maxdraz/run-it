using System;
using UnityEngine;

namespace RunIt.Detection
{
    public class TriggerDetector : Detector
    {
        [SerializeField] private Collider collider;
        [SerializeField] private string toDetect;
        private Vector3 collisionPoint;

        

        private void Awake()
        {
            if (collider == null)
            {
                collider = GetComponent<Collider>();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == toDetect)
            {
                detected = true;
                InvokeEnter(other);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == toDetect)
            {
                detected = true;
                collisionPoint = other.ClosestPoint(transform.position);
                
            }
            else
            {
                detected = false;
            }

        }
        private void OnTriggerExit(Collider other)
        {
            detected = false;
            
            if (other.tag == toDetect)
            {
                
                InvokeExit(other);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(collisionPoint, 0.5f);
        }
    }
}