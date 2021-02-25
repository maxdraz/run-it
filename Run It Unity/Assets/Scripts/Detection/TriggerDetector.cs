using System;
using UnityEngine;

namespace RunIt.Detection
{
    public class TriggerDetector : Detector
    {
        [SerializeField] private Collider collider;
        //[SerializeField] private string toDetect;
        private Vector3 collisionPoint;
        [SerializeField] private LayerMask toDetect;
        
     
        private void Awake()
        {
            if (collider == null)
            {
                collider = GetComponent<Collider>();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
           
            
            if (((1 << other.gameObject.layer) & toDetect) != 0)
            {
                detected = true;
                
                InvokeEnter(other);
            }
            
        }

        private void OnTriggerStay(Collider other)
        {
            if(((1<<other.gameObject.layer) & toDetect) != 0)
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
            if(((1<<other.gameObject.layer) & toDetect) != 0)
            {
                InvokeExit(other);
            } 

        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos) return;
            Gizmos.DrawSphere(collisionPoint, 0.5f);
        }
    }
}