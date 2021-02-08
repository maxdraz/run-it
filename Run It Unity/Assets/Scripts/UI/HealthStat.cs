using System;
using RunIt.Player;
using UnityEngine;

namespace RunIt.UI
{
    [RequireComponent(typeof(Health))]
    public class HealthStat : Stat
    {
        [SerializeField] private Health health;
        public override event StatChangeHandler StatChanged;
        private void Awake()
        {
            health = GetComponent<Health>();
        }
        
        private void OnEnable()
        {
            health.HealthChaged += OnHealthChanged;
        }

        private void OnDisable()
        {
            health.HealthChaged -= OnHealthChanged;
        }

        private void OnHealthChanged(int h)
        {
            StatChanged?.Invoke(h);
        }

        public override object GetValue()
        {
            return health.HealthValue;
        }
    }
}