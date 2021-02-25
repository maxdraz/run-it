using System;
using RunIt.Detection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Movement
{
    public class HorizontalWallRun : ParkourBehaviour
    {
        [SerializeField] private Detector groundDetector;
        [SerializeField] private RaycastDetector[] wallDetectors;
        [SerializeField] private float wallRunStrength;
        [SerializeField] private float wallRunAngle = 50f;
        private Vector3 wallRunDirection;
        [SerializeField] private bool hasWallRan;
        [SerializeField] private bool isGrounded;
        [SerializeField] private bool wallDetected;
        [SerializeField] private bool canWallRun;

        [SerializeField] private float wallRunTime = 1.5f;
        [SerializeField] private float timer = 0;
        private bool inputLetGo;
        
      
        private void OnEnable()
        {
            foreach (var detector in wallDetectors)
            {
                detector.RayEnter += OnWallEnter;
                detector.RayStay += OnWallStay;
                detector.RayExit += OnWallExit;
            }

            groundDetector.Enter += OnGrounded;
            //input
           // action.canceled += 
        }

        private void OnDisable()
        {
            foreach (var detector in wallDetectors)
            {
                detector.RayEnter -= OnWallEnter;
                detector.RayStay -= OnWallStay;
                detector.RayExit -= OnWallExit;
            }
            groundDetector.Enter -= OnGrounded;
        }

        private void Update()
        {
            wallDetected = CheckIfWallDetected();
            isGrounded = groundDetector.detected;
            timer -= Time.deltaTime;
            timer = Mathf.Max(0, timer);
            if (wallDetected && !isGrounded && !hasWallRan)
            {
                if (action.ReadValue<float>() > 0)
                {
                    timer = wallRunTime;
                }
                canWallRun = true;
                timer = wallRunTime;
            }
            else
            {
                canWallRun = false;
            }
            
        }

        private void FixedUpdate()
        {
            if(!canWallRun) return;

            var force = wallRunDirection * wallRunStrength;
            rb.AddForce(force, ForceMode.Acceleration);
            print("wallrunnign!!!!!");
        }
            
            
            //if (!canWallRun && groundDetector.detected && hasWallRan) return;

            //var newVel = wallRunDirection * rb.velocity.magnitude;
            //rb.AddForce(newVel,ForceMode.VelocityChange);

            // hasWallRan = true;
        

        private bool CheckIfWallDetected()
        {
            foreach (var detector in wallDetectors)
            {
                if (detector.detected) return true;
            }

            return false;
        }
        
        private void OnWallEnter(Ray ray, RaycastHit hitInfo)
        {
            wallRunDirection = CalculateWallRunDir(transform.forward, hitInfo.normal, Vector3.up, wallRunAngle);
          

        }
        
        private void OnWallStay(Ray ray, RaycastHit hitInfo)
        {
            wallRunDirection = CalculateWallRunDir(transform.forward, hitInfo.normal, Vector3.up, wallRunAngle);
        }

        private void OnWallExit(Ray ray, RaycastHit hitInfo)
        {
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

        private void OnGrounded(Collider other)
        {
            hasWallRan = false;
        }

        private void OnGroundExit(Collider other)
        {
            
        }

        private Vector3 RotateVector(Vector3 vector, float angle, Vector3 axis)
        {
            var rot = Quaternion.AngleAxis(angle, axis);
            vector = rot * vector;

            return vector;
        }

        private void OnActionCanceled(InputAction.CallbackContext ctx)
        {
            inputLetGo = true;
        }
    }
}