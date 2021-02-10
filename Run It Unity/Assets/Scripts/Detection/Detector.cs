using UnityEngine;

namespace RunIt.Detection
{
    public abstract class Detector : MonoBehaviour
    {
        public bool detected;
        public delegate void DetectionHandler();
        public event DetectionHandler Detected;

        protected void InvokeDetected()
        {
            Detected?.Invoke();
        }
    }
}