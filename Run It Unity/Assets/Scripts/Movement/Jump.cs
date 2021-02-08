using RunIt.Detection;
using UnityEngine;

namespace RunIt.Movement
{
    public class Jump : ParkourBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Detector groundDetector;

        // Update is called once per frame
        void FixedUpdate()
        {
            if(!groundDetector.detected) return;

            if (action.ReadValue<float>() <= 0) return;

            var jumpForce = new Vector3(0, speed, 0);
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }
    }
}
