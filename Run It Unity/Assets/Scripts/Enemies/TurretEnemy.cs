using System;
using RunIt.Detection;
using UnityEngine;

namespace RunIt.Enemies
{
    public class TurretEnemy : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Quaternion originalRotation;
        [Range(0, 1f)] [SerializeField] private float sampleSpeed = 0.1f;
        [SerializeField] private Detector targetDetector;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private float shotCooldown;
        private float timer;
        private void Start()
        {
            originalRotation = transform.rotation;
            timer = shotCooldown;
        }

        private void OnEnable()
        {
            targetDetector.Enter += OnTargetAcquired;
            targetDetector.Exit += OnTargetLost;
        }

        private void OnDisable()
        {
            targetDetector.Enter -= OnTargetAcquired;
            targetDetector.Exit -= OnTargetLost;
        }

        private void Update()
        {
            var angleToOriginal = Quaternion.Angle(transform.rotation,originalRotation);
            print(angleToOriginal);

            if (angleToOriginal >= 0.2f && !target)
            {
                RotateToTarget(originalRotation);
            }
            
            if (!target) return;

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Shoot();
                timer = shotCooldown;
            }
            
            print("rotating to target");
            RotateToTarget(target);
        }

        private void RotateToTarget(Transform to)
        {
            var toTarget = (to.position - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(toTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);
        }
        
        private void RotateToTarget(Quaternion to)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, to, 0.1f);
        }

        private void OnTargetLost(Collider other)
        {
            target = null;
        }
        
        private void OnTargetAcquired(Collider other)
        {
            print("target acquired");
            target = other.transform;
        }

        private void Shoot()
        {
            var bullet = GameObject.Instantiate(bulletPrefab);
            bullet.transform.position = bulletSpawnPoint.transform.position;
            bullet.transform.rotation = transform.rotation;
        }
    }
}