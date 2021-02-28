using System;
using RunIt.Player;
using UnityEngine;

namespace RunIt.Enemies
{
    public class Lava : Damageable
    {
        private bool dealtDamage;
        private void OnTriggerEnter(Collider other)
        {
            if (dealtDamage) return;
            
            var health = other.gameObject.GetComponent<Health>();
            if (!health) return;
            
            health.SubtractHealth(damage);
          
            dealtDamage = true;
        }

    }
}