using System;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Player
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int health;
        public int HealthValue => health;

        [SerializeField] private int maxHealth = 10;

        public delegate void DeathHandler();
        public event DeathHandler PlayerDied;

        public delegate void HealthChangeHandler(int health);
        public event HealthChangeHandler HealthChaged;

        private void Awake()
        {
            health = maxHealth;
        }

        private void Update()
        {
            
        }

        public void SubtractHealth(int amt)
        {
            health -= amt;
            HealthChaged?.Invoke(health);
            if (health <= 0)
            {
                PlayerDied?.Invoke();
                health = 0;
            }
        }

        public object GetValue()
        {
            return health;
        }

      
    }
}