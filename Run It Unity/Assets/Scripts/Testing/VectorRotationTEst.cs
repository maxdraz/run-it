using System;
using UnityEngine;

namespace RunIt.Testing
{
    public class VectorRotationTEst : MonoBehaviour
    {
        private RaycastHit hitInfo;
        [SerializeField] private float length;
        [SerializeField] private LayerMask mask;
        private Ray ray;
        private Vector3 cross;
        [SerializeField] private float angle;
        private void Update()
        {
            var t = transform;
            ray.origin = t.position;
            ray.direction = t.forward;

            if (Physics.Raycast(ray, out hitInfo, length, mask))
            {
            }

            cross = Vector3.Cross(Vector3.up, hitInfo.normal);
            
            var dot = Vector3.Dot(cross, ray.direction);
   
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(ray.origin,ray.origin + ray.direction * length);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(hitInfo.point, hitInfo.point +  hitInfo.normal);

            var runDir = CalculateWallRunDir(ray.direction, hitInfo.normal, Vector3.up, angle);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(hitInfo.point, hitInfo.point + runDir);
        }

        private Vector3 CalculateWallRunDir(Vector3 incomingDirection, Vector3 normal, Vector3 up, float elevationAngle)
        {
            var right = Vector3.Cross(up, normal);
            var dot = Vector3.Dot(incomingDirection, right);

            if (dot > 0)
            {
                return RotateVector(right, elevationAngle, normal);
            }
            else if (dot < 0)
            {
                return RotateVector(-right, -elevationAngle, normal);
            }
            else
            {
                return Vector3.zero;
            }
        }

        private Vector3 RotateVector(Vector3 vector, float angle, Vector3 axis)
        {
            var rot = Quaternion.AngleAxis(angle, axis);
            vector = rot * vector;

            return vector;
        }
    }
}