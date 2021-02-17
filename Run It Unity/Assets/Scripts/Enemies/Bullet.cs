using System;
using UnityEngine;

namespace RunIt.Enemies
{
    public class Bullet : Damageable
    {
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private float force = 1f;
        private float timer;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rb.AddForce(transform.forward * force, ForceMode.VelocityChange);
            timer = lifeTime;
        }

        private void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}