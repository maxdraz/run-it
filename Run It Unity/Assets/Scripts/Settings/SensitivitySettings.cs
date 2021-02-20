using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

namespace RunIt.Settings
{
   
    public class SensitivitySettings : MonoBehaviour
    {
        public static SensitivitySettings Instance;
        public delegate void ValueChangeHandler(float value);
        
        public event ValueChangeHandler ValueChanged;
        [SerializeField] private Slider sensitivitySlider;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void OnEnable()
        {
            sensitivitySlider.onValueChanged.AddListener (delegate {OnValueChanged ();});
            InvokeValueChanged(sensitivitySlider.value);
        }

        private void OnDestroy()
        {
            sensitivitySlider.onValueChanged.RemoveListener(delegate {OnValueChanged ();});
        }

        private void OnValueChanged()
        {
            InvokeValueChanged(sensitivitySlider.value);
        }

        private void InvokeValueChanged(float value)
        {
            var roundedValue = Mathf.Round(value * 10) / 10;
            ValueChanged?.Invoke(roundedValue);
        }
    }
}