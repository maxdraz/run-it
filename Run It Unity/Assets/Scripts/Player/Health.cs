using System;
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

        [SerializeField] private Detector damageDetector; 
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

        private void OnEnable()
        {
            damageDetector.Enter += OnDamaged;
        }

        private void OnDisable()
        {
            damageDetector.Enter -= OnDamaged;
        }


        public void SubtractHealth(int amt)
        {
            health -= amt;
            health = Mathf.Max(0, health);
            ValueChanged?.Invoke();
            if (health <= 0) 
            {
                PlayerDied?.Invoke();
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
            print("health reset" + health);
            health = maxHealth;
            ValueChanged?.Invoke();
            print(health);
            print(maxHealth);
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