using System;
using FMODUnity;
using RunIt.Audio;
using RunIt.Detection;
using RunIt.Enemies;
using RunIt.UI;
using UnityEngine;

namespace RunIt.Player
{
    [System.Serializable]
    public class DamageInfo
    {
        public int damageAmout;
        public enum DamageType
        {
            Fall,
            Enemy,
            Hazard
        }

        public DamageType damageType;
    }
    
    public class Health : MonoBehaviour, ITextDisplayable, IPlayerAttribute
    {
        [SerializeField] private int health;
        public int HealthValue => health;

        [SerializeField] private int maxHealth = 10;
        public int MaxHealth => maxHealth;
        public delegate void DeathHandler(DamageInfo damageInfo);
        public event DeathHandler PlayerDied;
        [SerializeField] private FMODEventPlayer deathSound;
        [SerializeField] private FMODEventPlayer damagedSound;

        private void Awake()
        {
            SetHealth(health);
        }

        private void Start()
        {
            DisplayInitialHealth();
        }


        public void SubtractHealth(DamageInfo damageInfo)
        {
            health -= damageInfo.damageAmout;
            health = Mathf.Max(0, health);
            ValueChanged?.Invoke();
            

            if (health <= 0)
            {
                
                PlayerDied?.Invoke(damageInfo);
                health = maxHealth;
                
                //death sound
                deathSound.Play();
            }
            
            //damage sounds
            if (damageInfo.damageType == DamageInfo.DamageType.Enemy)
            {
                damagedSound.Play();
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

        private void SetHealth(int value)
        {
            health = value;
            ValueChanged?.Invoke();
        }

        public string GetDisplayText()
        {
            return "Health: " + health.ToString();
        }

        private void OnDamaged(Collider other)
        {
            var damageable = other.GetComponentInParent<Damageable>();

            SubtractHealth(damageable.DamageInfo);
        }

        public event ITextDisplayable.ValueChangedHandler ValueChanged;
    }
}