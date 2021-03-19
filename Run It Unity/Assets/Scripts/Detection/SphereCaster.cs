using UnityEngine;

namespace RunIt.Detection
{
    public class SphereCaster : MonoBehaviour
    {
        [SerializeField] private bool drawGizmos;
        [SerializeField] private bool detected;
        public bool Detected => detected;
        [SerializeField] private Color color;
        [SerializeField] private float radius;
        [SerializeField] private float maxlength;
        [SerializeField] private LayerMask toDetect;
        private RaycastHit hitInfo;
        private Ray ray;
        private bool firstHit;

        public delegate void EnterHandler(RaycastHit hitInfo);
        public event EnterHandler Enter;
        public delegate void StayHandler(RaycastHit hitInfo);
        public event StayHandler Stay;
        public delegate void ExitHandler(RaycastHit hitInfo);
        public event ExitHandler Exit;

        private void Update()
        {
            Cast();
        }

        private void Cast()
        {
            var myTrans = transform;
            ray.origin = myTrans.position;
            ray.direction = myTrans.forward;
            
            if (Physics.SphereCast(ray, radius, out hitInfo, maxlength,toDetect))
            {
                detected = true;
                if (!firstHit)
                {
                    //invoke enter
                    Enter?.Invoke(hitInfo);
                }
                Stay?.Invoke(hitInfo);
            }
            else
            {
                detected = false;
                Exit?.Invoke(hitInfo);
            }
        }

        private void OnDrawGizmos()
        {
            if(!drawGizmos) return;
            Gizmos.color = color;
            
            if (!Application.isPlaying)
            {
                var myTrans = transform;
                ray = new Ray(){origin = myTrans.position, direction = myTrans.forward};
                Gizmos.DrawSphere(ray.origin + (ray.direction * maxlength),radius);
            }
           
            Gizmos.DrawSphere(hitInfo.point,radius);
        }
    }
}