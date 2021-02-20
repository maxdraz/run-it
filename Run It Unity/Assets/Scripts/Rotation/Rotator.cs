using System;
using UnityEngine;

namespace RunIt.Rotation
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private bool shouldRotate = true;
        [SerializeField] private float anglesPerSec;
        [SerializeField] private Vector3 axis;
        private float angle;
        [SerializeField] private bool invert;

        private void Start()
        {
            angle = Quaternion.Angle(Quaternion.identity, transform.rotation);
        }

        private void LateUpdate()
        {
            if(!shouldRotate) return;
            
            angle += invert ? -anglesPerSec * Time.deltaTime : anglesPerSec * Time.deltaTime;
            angle %= 360f;
            var rot = Quaternion.AngleAxis(angle, axis);
            transform.localRotation = rot;
        }
    }
}