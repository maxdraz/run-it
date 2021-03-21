using System;
using RunIt.Player;
using UnityEngine;

namespace RunIt.Enemies
{
    public class Bullet : Damageable
    {
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private float force = 1f;
        private float timer;
        private Rigidbody rb;
        private bool dealtDamage;

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

        private void OnCollisionEnter(Collision other)
        {
            if (dealtDamage) return;
            
            var health = other.gameObject.GetComponent<Health>();
            if (!health) return;
            
            health.SubtractHealth(damageInfo);
            Destroy(this.gameObject);
            dealtDamage = true;
        }
    }
}