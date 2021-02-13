using System;
using RunIt.Detection;
using UnityEngine;

namespace RunIt.Player
{
    public class DamageHandler : MonoBehaviour
    {
        private Rigidbody rb;
        private Health health;
        [SerializeField] private float fallDamageSpeed = 10;
        [SerializeField] private int fallDamage;
        [SerializeField] private Detector fallDetector;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            fallDetector.Enter += OnFall;
           
        }

        private void Update()
        {
          
        }

        private void OnDisable()
        {
            fallDetector.Enter -= OnFall;
        }

        private void OnFall(Collider other)
        {
            if (rb == null) return;
            
            if (Mathf.Abs(rb.velocity.y) >= fallDamageSpeed)
            {
                health.SubtractHealth(fallDamage);
                
            }
            
        }
    }
}