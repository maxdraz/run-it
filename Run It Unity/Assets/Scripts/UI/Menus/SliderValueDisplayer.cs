using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RunIt.UI.Menus
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SliderValueDisplayer : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        private TextMeshProUGUI tmp;

        private void Awake()
        {
            tmp = GetComponent<TextMeshProUGUI>();
        }

        public void OnEnable()
        {
            slider.onValueChanged.AddListener (delegate {OnValueChanged ();});
            UpdateValueDisplay();
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(delegate {OnValueChanged ();});
        }

        // Invoked when the value of the slider changes.
        private void OnValueChanged()
        {
            UpdateValueDisplay();
        }

        private void UpdateValueDisplay()
        {
            var value = slider.value;
            var roundedValue = Mathf.Round(value * 10) / 10;

            tmp.text = roundedValue.ToString();
        }
    }
}