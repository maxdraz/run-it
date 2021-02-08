using System;
using UnityEngine;

namespace RunIt.Detection
{
    public class Detector : MonoBehaviour
    {
        [SerializeField] private Collider collider;
        [SerializeField] private string toDetect;
        public bool detected;

        public delegate void DetectionHandler();
        public event DetectionHandler Detected;

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
                Detected?.Invoke();
                
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == toDetect)
            {
                detected = true;
            }
            else
            {
                detected = false;
            }

        }
        private void OnTriggerExit(Collider other)
        {
            detected = false;
        }
        
        
    }
}