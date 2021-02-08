using System;
using UnityEngine;

namespace RunIt.UI
{
    public class SpeedStat : Stat
    {
        private Rigidbody rb;
        [SerializeField] private int speed;
        public override event StatChangeHandler StatChanged;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            speed = (int) rb.velocity.magnitude;
            
            StatChanged?.Invoke(speed);
        }

        public override object GetValue()
        {
            return speed;
        }
    }
}