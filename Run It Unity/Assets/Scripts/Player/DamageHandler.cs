using System;
using RunIt.Detection;
using RunIt.Movement;
using UnityEngine;

namespace RunIt.Player
{
    public class DamageHandler : MonoBehaviour
    {
        private Rigidbody rb;
        private Health health;
        [SerializeField] private DamageInfo damageInfo;
        [SerializeField] private float fallDamageSpeed = 10;
        [SerializeField] private Detector fallDetector;
        private Roll roll;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            health = GetComponent<Health>();
            roll = GetComponent<Roll>();
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
            if (rb == null || roll.IsRolling) return;
            
            if (Mathf.Abs(rb.velocity.y) >= fallDamageSpeed)
            {
                health.SubtractHealth(damageInfo);
                
            }
            
        }
    }
}