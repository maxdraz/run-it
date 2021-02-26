using System;
using System.Collections.Generic;
using FMODUnity;
using RunIt.Detection;
using UnityEngine;

namespace RunIt.Movement
{
    public class HorizontalWallRunV2 : ParkourBehaviour
    {
        
        [SerializeField] private float speedMultiplier = 1f;
        [SerializeField] private float wallRunTime;
        [SerializeField] private float timer;
        [SerializeField] private bool wallDetected;
        private Vector3 wallRunDirection;
        [SerializeField] private float wallRunAngle = 50f;
        [SerializeField] private float jumpOffAngle = 50f;
        [SerializeField] private List<RaycastDetector> wallDetectors;
        [SerializeField] private Detector groundDetector;
        [SerializeField] private bool hasTouchedGround;
        [SerializeField] private float jumpOffSpeed = 10f;
        private float speed;
        

        private void OnEnable()
        {
            foreach (var wallDetector in wallDetectors)
            {
                wallDetector.RayEnter += OnWallEnter;
                wallDetector.RayExit += OnWallExit;
            }

            groundDetector.Stay += OnGroundStay;
        }

        private void OnDisable()
        {
            foreach (var wallDetector in wallDetectors)
            {
                wallDetector.RayEnter -= OnWallEnter;
                wallDetector.RayExit -= OnWallExit;
            }

            groundDetector.Stay -= OnGroundStay;
        }

        private void Update()
        {
            if (!groundDetector.detected && wallDetected && timer >0)
            {
                if (action.ReadValue<float>() > 0)  //jump off
                {
                    ExecuteJumpOff();
                    timer = 0;
                    return;
                }
                
                timer -= Time.deltaTime;
                ExecuteWallRun();
            }
        }

        private void OnWallEnter(Ray ray, RaycastHit hitInfo)
        {
            speed = rb.velocity.magnitude;
            wallRunDirection = CalculateWallRunDir(transform.forward, hitInfo.normal, Vector3.up, wallRunAngle);
            wallDetected = true;

        }

        private void OnWallExit(Ray ray, RaycastHit hitInfo)
        {
            wallDetected = false;
            timer = 0;
        }

        private void OnGroundStay(Collider other)
        {
            timer = wallRunTime;
        }
        
        

        private void ExecuteWallRun()
        {
            var newVel = wallRunDirection *speed;
            rb.velocity = newVel;
        }

        private void ExecuteJumpOff()
        {
            var trans = transform;
            var dir = RotateVector(trans.forward, -jumpOffAngle, trans.right).normalized;

            rb.velocity = dir * jumpOffSpeed;
            
            print("jumped outwards");
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