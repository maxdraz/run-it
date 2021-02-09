using System;
using UnityEngine;

namespace RunIt.Testing
{
    public class DotTest : MonoBehaviour
    {
        private Vector3 desired;
        private Vector3 vel;
        private Ray ray;
        [SerializeField] private float rayLength;

        private void Update()
        {
            ray.origin = transform.position;
            ray.direction = transform.forward * rayLength;
            print(ray.direction.magnitude);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(ray.origin, ray.direction * rayLength);
        }
    }
}