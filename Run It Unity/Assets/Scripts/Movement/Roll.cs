using System;
using System.Collections;
using RunIt.Audio;
using RunIt.Detection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Movement
{
    public class Roll : ParkourBehaviour
    {
        [SerializeField] private GameObject cameraHolder;
        private Animator cameraAnimator;
        [SerializeField] private Detector groundDetector;
        [SerializeField] private Detector fallDetector;
        [SerializeField] private float inputWindow;
        private Timer inputWindowTimer;
        [SerializeField] private FMODEventPlayer rollSound;
        private bool canRoll;
        

        protected override void Awake()
        {
            base.Awake();
            cameraAnimator = cameraHolder.GetComponent<Animator>();
            inputWindowTimer = new Timer(inputWindow);
        }

        private void OnEnable()
        {
            StartCoroutine(SubscribeToInputCoroutine());
            fallDetector.Enter += OnGroundEnter;
            groundDetector.Exit += OnGroundExit;
        }

        private void OnDisable()
        {
            action.started -= OnActionStart;
            fallDetector.Enter -= OnGroundEnter;
            groundDetector.Exit -= OnGroundExit;
        }


        private void Update()
        {
            if (inputWindowTimer.isRunning)
            {
                inputWindowTimer.Update();
            }
        }

        protected override void OnActionStart(InputAction.CallbackContext ctx) // Roll
        {
            ExecuteRoll();
        }

        protected override IEnumerator SubscribeToInputCoroutine()
        {
            yield return new WaitForEndOfFrame();
            action.started += OnActionStart;
        }

        private void OnGroundEnter(Collider other)
        {
            inputWindowTimer.Start();
        }

        private void ExecuteRoll()
        {
            if(!fallDetector.detected || !inputWindowTimer.isRunning || !canRoll) return;

            var vel = rb.velocity;
            var xZVelocity = new Vector3(vel.x,0,vel.z);
            rb.velocity = transform.forward * xZVelocity.magnitude;
            
            cameraAnimator.Play("Base Layer.CameraRollAnim", -1,0);
            
            //play sound
            rollSound.Play();

            canRoll = false;
        }

        private void OnGroundExit(Collider other)
        {
            canRoll = true;
        }
    }
}