using UnityEngine;

namespace RunIt.Detection
{
    public abstract class Detector : MonoBehaviour
    {
        [SerializeField] protected bool drawGizmos;
        public bool detected;
        public delegate void DetectedHandler(Collider other);
        public event DetectedHandler Enter;
        public delegate void DetectionHandler(Collider other);
        public event DetectionHandler Stay;
        public delegate void ExitHandler(Collider other);
        public event ExitHandler Exit;

        protected void InvokeEnter(Collider other)
        {
            Enter?.Invoke(other);
        }
        
        protected void InvokeStay(Collider other)
        {
            Stay?.Invoke(other);
        }
        
        protected void InvokeExit(Collider other)
        {
            Exit?.Invoke(other);
        }
    }
}