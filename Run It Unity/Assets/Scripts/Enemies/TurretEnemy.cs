using System;
using RunIt.Detection;
using UnityEngine;

namespace RunIt.Enemies
{
    public class TurretEnemy : MonoBehaviour
    {
        private Transform target;
        private Quaternion originalRotation;
        [SerializeField] private Transform turretHead;
        [Range(0, 1f)] [SerializeField] private float sampleSpeed = 0.1f;
        [SerializeField] private Detector targetDetector;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private float shotCooldown;
        private float timer;
        private void Start()
        {
            if (turretHead == null)
            {
                turretHead = transform;
            }
            
            originalRotation = turretHead.rotation;
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
            var angleToOriginal = Quaternion.Angle(turretHead.rotation,originalRotation);

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

            RotateToTarget(target);
        }

        private void RotateToTarget(Transform to)
        {
            var toTarget = (to.position - turretHead.position).normalized;
            var lookRotation = Quaternion.LookRotation(toTarget);
            turretHead.rotation = Quaternion.Slerp(turretHead.rotation, lookRotation, 0.1f);
        }
        
        private void RotateToTarget(Quaternion to)
        {
            turretHead.rotation = Quaternion.Slerp(turretHead.rotation, to, 0.1f);
        }

        private void OnTargetLost(Collider other)
        {
            target = null;
        }
        
        private void OnTargetAcquired(Collider other)
        {
            target = other.transform;
        }

        private void Shoot()
        {
            var bullet = GameObject.Instantiate(bulletPrefab);
            bullet.transform.position = bulletSpawnPoint.transform.position;
            bullet.transform.rotation = turretHead.rotation;
        }
    }
}