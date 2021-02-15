using System;
using RunIt.Detection;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Player
{
    public class Health : MonoBehaviour, ITextDisplayable
    {
        [SerializeField] private int health;
        public int HealthValue => health;

        [SerializeField] private int maxHealth = 10;
        [SerializeField] private Detector damageDetector; 
        public delegate void DeathHandler();
        public event DeathHandler PlayerDied;

        private void Awake()
        {
            health = maxHealth;
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


        public string GetDisplayText()
        {
            return "Health: " + health.ToString();
        }

        public event ITextDisplayable.ValueChangedHandler ValueChanged;
    }
}