using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RunIt.Detection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RunIt.Movement
{
    public class Crouch : ParkourBehaviour
    {
        private List<CapsuleCollider> colliders;
        [SerializeField] private Transform camera;
        [SerializeField] private Transform crouchedCameraTransform;
        private Vector3 originalCameraPos;
        [SerializeField] private Detector groundDetector;

        protected override void Awake()
        {
            base.Awake();
            colliders = GetComponents<CapsuleCollider>().ToList();
            originalCameraPos = camera.transform.localPosition;
        }

        private void OnEnable()
        {
            StartCoroutine(SubscribeToInputCoroutine());
        }
        
        private void OnDisable()
        {
            action.started -= OnActionStart;
            action.canceled -= OnActionCanceled;
        }

        protected override void OnActionStart(InputAction.CallbackContext ctx)
        {
            if (!groundDetector.detected) return;
            
            var newPos = crouchedCameraTransform.position;
           camera.transform.position = newPos;

            colliders[0].enabled = false;
            colliders[1].enabled = true;
        }

        protected override void OnActionCanceled(InputAction.CallbackContext ctx)
        {
            if (!groundDetector.detected) return;
            
            camera.transform.localPosition = originalCameraPos;
            
            colliders[0].enabled = true;
            colliders[1].enabled = false;
        }

        protected override IEnumerator SubscribeToInputCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            action.started += OnActionStart;
            action.canceled += OnActionCanceled;
        }
    }
}