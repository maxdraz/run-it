using System;
using FMODUnity;
using RunIt.Detection;
using RunIt.Enemies;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Player
{
    public class Health : MonoBehaviour, ITextDisplayable, IPlayerAttribute
    {
        [SerializeField] private int health;
        public int HealthValue => health;

        [SerializeField] private int maxHealth = 10;
        public int MaxHealth => maxHealth;
        public delegate void DeathHandler();
        public event DeathHandler PlayerDied;

        private void Awake()
        {
            ResetHealth();
        }

        private void Start()
        {
            DisplayInitialHealth();
        }


        public void SubtractHealth(int amt)
        {
            health -= amt;
            health = Mathf.Max(0, health);
            ValueChanged?.Invoke();
            

            if (health <= 0)
            {
                PlayerDied?.Invoke();
                health = maxHealth;
                print("health on death is: " + health);
            }
        }

        public object GetValue()
        {
            return health;
        }

        private void DisplayInitialHealth()
        {
            ValueChanged?.Invoke();
        }

        public void ResetHealth()
        {
            health = maxHealth;
            ValueChanged?.Invoke();
           
        }

        public string GetDisplayText()
        {
            return "Health: " + health.ToString();
        }

        private void OnDamaged(Collider other)
        {
            var damageable = other.GetComponentInParent<Damageable>();

            SubtractHealth(damageable.Damage);
        }

        public event ITextDisplayable.ValueChangedHandler ValueChanged;
    }
}